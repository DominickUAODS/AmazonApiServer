using AmazonApiServer.Data;
using AmazonApiServer.DTOs.Auth;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using AmazonApiServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmazonApiServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ApplicationContext _context;
		private readonly IToken _token;
		private readonly IEmail _emailService;

		public AuthController(ApplicationContext context, IToken token, IEmail emailService)
		{
			_context = context;
			_token = token;
			_emailService = emailService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var user = await _context.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.Email == dto.Email);

			if (user == null || !PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash!))
				return Unauthorized(new { error = "Invalid credentials" });

			var token = _token.CreateJwtToken(user);
			return Ok(new
			{
				token,
				user = new
				{
					user.Id,
					user.FirstName,
					user.LastName,
					user.Email,
					user.Role?.Name
				}
			});
		}

		// Шаг 1: Запрос регистрации — email + password
		[HttpPost("register/start")]
		public async Task<IActionResult> RegisterStart([FromBody] RegistrationRequestDto dto)
		{
			var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
			if (exists)
				return BadRequest("User already registered");

			var code = CodeGenerator.Generate6DigitCode();
			var hashedPassword = PasswordHasher.HashPassword(dto.Password);

			var existingEntry = await _context.EmailVerificationCodes.FirstOrDefaultAsync(x => x.Email == dto.Email);

			if (existingEntry != null)
			{
				_context.EmailVerificationCodes.Remove(existingEntry);
				await _context.SaveChangesAsync();
			}

			var entry = new EmailVerificationCode
			{
				Email = dto.Email,
				Code = code,
				HashedPassword = hashedPassword
			};

			_context.EmailVerificationCodes.Add(entry);
			await _context.SaveChangesAsync();

			await _emailService.SendConfirmationCodeAsync(dto.Email, code);
			return Ok("Verification code sent to email.");
		}

		// Шаг 2: Проверка кода
		[HttpPost("register/verify")]
		public async Task<IActionResult> VerifyCode([FromBody] EmailCodeVerificationDto dto)
		{
			var entry = await _context.EmailVerificationCodes.FirstOrDefaultAsync(e => e.Email == dto.Email);

			if (entry == null || entry.IsExpired || entry.Code != dto.Code)
			{
				return BadRequest("Invalid or expired code.");
			}

			return Ok("Code verified successfully.");
		}

		// Шаг 3: Завершение регистрации (first + last name)
		[HttpPost("register/complete")]
		public async Task<IActionResult> CompleteRegistration([FromBody] CompleteRegistrationDto dto)
		{
			var entry = await _context.EmailVerificationCodes.FirstOrDefaultAsync(e => e.Email == dto.Email);

			if (entry == null)
				return BadRequest("Email not verified");

			// получаем роль "Customer"
			var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer");
			if (role == null)
				return StatusCode(500, "Role 'Customer' not found");

			var user = new User
			{
				Id = Guid.NewGuid(),
				Email = entry.Email,
				PasswordHash = entry.HashedPassword,
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				IsActive = true,
				ProfilePhoto = "/images/default.png",
				//RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
				RoleId = role.Id
			};

			_context.Users.Add(user);
			_context.EmailVerificationCodes.Remove(entry);
			await _context.SaveChangesAsync();

			return Ok("User created successfully");
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> RefreshToken([FromBody] RefreshDto dto)
		{
			var token = await _context.RefreshTokens
				.Include(t => t.User)
				.ThenInclude(u => u.Role)
				.FirstOrDefaultAsync(t => t.Token == dto.RefreshToken && !t.IsRevoked);

			if (token == null || token.ExpiresAt < DateTime.UtcNow)
				return Unauthorized(new { error = "Invalid or expired refresh token" });

			token.IsRevoked = true;

			var newToken = new RefreshToken
			{
				Id = Guid.NewGuid(),
				UserId = token.UserId,
				ExpiresAt = DateTime.UtcNow.AddDays(7),
				Token = Guid.NewGuid().ToString()
			};

			_context.Add(newToken);
			await _context.SaveChangesAsync();

			var jwt = _token.CreateJwtToken(token.User);
			return Ok(new { token = jwt, refreshToken = newToken.Token });
		}

		[HttpPost("logout")]
		[Authorize]
		public async Task<IActionResult> Logout([FromBody] LogoutDto dto)
		{
			var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == dto.RefreshToken && !t.IsRevoked);

			if (token == null)
				return NotFound(new { error = "Token not found or already revoked" });

			token.IsRevoked = true;
			await _context.SaveChangesAsync();

			// Удалим токены из cookie
			Response.Cookies.Delete("jwt_token");
			Response.Cookies.Delete("refresh_token");

			return Ok(new { message = "Logged out successfully" });
		}

		public async Task<bool> VerifyConfirmationCodeAsync(string email, string code)
		{
			var entry = await _context.EmailVerificationCodes.FirstOrDefaultAsync(e => e.Email == email);

			// Очистка, если истёк срок
			if (entry == null || entry.IsExpired)
			{
				if (entry != null)
				{
					_context.EmailVerificationCodes.Remove(entry);
					await _context.SaveChangesAsync();
				}
				return false;
			}

			if (entry.Code != code)
				return false;

			return true;
		}
	}
}
