using System.IdentityModel.Tokens.Jwt;
using AmazonApiServer.Data;
using AmazonApiServer.DTOs.Auth;
using AmazonApiServer.Helpers;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using AmazonApiServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AmazonApiServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ApplicationContext _context;
		private readonly IToken _tokenService;
		private readonly IEmail _emailService;
		private readonly TokenValidationParameters _validationParameters;

		public AuthController(ApplicationContext context, IToken token, IEmail emailService, TokenValidationParameters validationParameters)
		{
			_context = context;
			_tokenService = token;
			_emailService = emailService;
			_validationParameters = validationParameters;
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

			var jwt = _tokenService.CreateJwtToken(user);
			var refreshToken = await _tokenService.CreateRefreshTokenAsync(user);

			return Ok(new AuthResponseDto
			{
				AccessToken = jwt,
				RefreshToken = refreshToken,
				User = new
				{
					id = user.Id,
					first_name = user.FirstName,
					last_name = user.LastName,
					email = user.Email,
					role = user.Role?.Name,
					profile_photo = user.ProfilePhoto
				}
			});
		}

		// Шаг 1: Запрос регистрации — email + password
		[HttpPost("register-start")]
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

			await _emailService.SendConfirmationCodeAsync(dto.Email, code, "Registration Confirmation", "Here is your confirmation code for registration:");
			return Ok("Verification code sent to email.");
		}

		// Шаг 2: Проверка кода
		[HttpPost("verify")]
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
		[HttpPost("register-complete")]
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
				ProfilePhoto = "",
				RegistrationDate = DateTime.UtcNow,
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
			var oldToken = await _context.RefreshTokens
				.Include(t => t.User)
				.ThenInclude(u => u.Role)
				.FirstOrDefaultAsync(t => t.Token == dto.RefreshToken && !t.IsRevoked);

			var handler = new JwtSecurityTokenHandler();
			var principal = handler.ValidateToken(oldToken?.Token, _validationParameters, out var validatedToken);

			if (!principal.HasClaim(c => c.Type == "tokenType" && c.Value == "refreshToken"))
				return Unauthorized(new { error = "Invalid or expired refresh token" });

			if (oldToken == null || oldToken.ExpiresAt < DateTime.UtcNow)
				return Unauthorized(new { error = "Invalid or expired refresh token" });
			
			oldToken.IsRevoked = true;

			var newRefreshToken = await _tokenService.CreateRefreshTokenAsync(oldToken.User);
			var jwt = _tokenService.CreateJwtToken(oldToken.User);

			await _context.SaveChangesAsync();

			return Ok(new AuthResponseDto
			{
				AccessToken = jwt,
				RefreshToken = newRefreshToken,
				User = new
				{
					id = oldToken.User.Id,
					first_name = oldToken.User.FirstName,
					last_name = oldToken.User.LastName,
					email = oldToken.User.Email,
					role = oldToken.User.Role?.Name
				}
			});
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
			Response.Cookies.Delete("access_token");
			Response.Cookies.Delete("refresh_token");

			return Ok(new { message = "Logged out successfully" });
		}

		//[HttpPost("verify")]
		//public async Task<bool> VerifyConfirmationCodeAsync(string email, string code)
		//{
		//	var entry = await _context.EmailVerificationCodes.FirstOrDefaultAsync(e => e.Email == email);

		//	// Очистка, если истёк срок
		//	if (entry == null || entry.IsExpired)
		//	{
		//		if (entry != null)
		//		{
		//			_context.EmailVerificationCodes.Remove(entry);
		//			await _context.SaveChangesAsync();
		//		}
		//		return false;
		//	}

		//	if (entry.Code != code)
		//		return false;

		//	return true;
		//}

		[HttpPost("reset-start")]
		public async Task<IActionResult> StartPasswordReset([FromBody] PasswordResetStartDto dto)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
			if (user == null)
				return NotFound("User with this email not found");

			var existing = await _context.EmailVerificationCodes.FirstOrDefaultAsync(e => e.Email == dto.Email);
			if (existing != null)
			{
				_context.EmailVerificationCodes.Remove(existing);
				await _context.SaveChangesAsync();
			}

			var code = CodeGenerator.Generate6DigitCode();

			_context.EmailVerificationCodes.Add(new EmailVerificationCode
			{
				Email = dto.Email,
				Code = code
			});
			await _context.SaveChangesAsync();

			await _emailService.SendConfirmationCodeAsync(dto.Email, code, "Password Reset", "Here is your password reset code:");

			return Ok("Password reset code sent to your email.");
		}

		[HttpPost("reset-complete")]
		public async Task<IActionResult> CompletePasswordReset([FromBody] PasswordResetCompleteDto dto)
		{
			var entry = await _context.EmailVerificationCodes.FirstOrDefaultAsync(e => e.Email == dto.Email);
			if (entry == null || entry.IsExpired || entry.Code != dto.Code)
				return BadRequest("Invalid or expired code");

			if (!PasswordValidator.IsValid(dto.NewPassword))
				return BadRequest("Password must be at least 8 characters long and contain upper, lower letters and digits.");

			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
			if (user == null)
				return NotFound("User not found");

			user.PasswordHash = PasswordHasher.HashPassword(dto.NewPassword);

			_context.EmailVerificationCodes.Remove(entry);
			await _context.SaveChangesAsync();

			return Ok("Password reset successfully");
		}

		[HttpPost("resend-code")]
		public async Task<IActionResult> ResendCode([FromBody] ResendCodeDto dto)
		{
			// dto.Purpose: "register" или "reset"
			if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Purpose))
				return BadRequest("Email and purpose are required.");

			dto.Purpose = dto.Purpose.ToLower();

			if (dto.Purpose != "register" && dto.Purpose != "reset")
				return BadRequest("Invalid purpose. Allowed values: register, reset");

			// Проверка существования пользователя
			var userExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);

			if (dto.Purpose == "register" && userExists)
				return BadRequest("User with this email already exists.");
			if (dto.Purpose == "reset" && !userExists)
				return NotFound("User with this email not found.");

			// Ищем запись с кодом
			var entry = await _context.EmailVerificationCodes.FirstOrDefaultAsync(e => e.Email == dto.Email);

			if (entry != null)
			{
				_context.EmailVerificationCodes.Remove(entry);
				await _context.SaveChangesAsync();
			}

			// Генерация нового кода
			var code = CodeGenerator.Generate6DigitCode();

			var newEntry = new EmailVerificationCode
			{
				Email = dto.Email,
				Code = code
			};

			_context.EmailVerificationCodes.Add(newEntry);
			await _context.SaveChangesAsync();

			// Локализация и тексты
			string title, subtitle;
			if (dto.Purpose == "register")
			{
				title = "Registration Confirmation";
				subtitle = "Here is your confirmation code for registration:";
			}
			else // reset
			{
				title = "Password Reset";
				subtitle = "Here is your password reset code:";
			}

			await _emailService.SendConfirmationCodeAsync(dto.Email, code, title, subtitle);

			return Ok("Verification code resent to email.");
		}

		[HttpPost("change-password")]
		[Authorize]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
		{
			// 1. Получаем ID пользователя из токена
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
			if (string.IsNullOrEmpty(userIdClaim))
				return Unauthorized("Invalid token");

			var userId = Guid.Parse(userIdClaim);

			// 2. Находим пользователя в БД
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
			if (user == null)
				return NotFound("User not found");

			// 3. Проверяем текущий пароль
			if (!PasswordHasher.VerifyPassword(dto.CurrentPassword, user.PasswordHash))
				return BadRequest("Current password is incorrect");

			// 4. Проверяем сложность нового пароля
			if (!PasswordValidator.IsValid(dto.NewPassword))
				return BadRequest("Password must be at least 8 characters long and contain upper, lower letters and digits.");

			// 5. Меняем пароль
			user.PasswordHash = PasswordHasher.HashPassword(dto.NewPassword);

			await _context.SaveChangesAsync();

			// 6. (Опционально) Отзываем все refresh-токены
			var tokens = _context.RefreshTokens.Where(t => t.UserId == user.Id && !t.IsRevoked);
			foreach (var t in tokens)
				t.IsRevoked = true;

			await _context.SaveChangesAsync();

			return Ok("Password changed successfully. Please log in again.");
		}
	}
}
