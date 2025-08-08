using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AmazonApiServer.Data;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using Microsoft.IdentityModel.Tokens;

public class TokenRepository : IToken
{
	private readonly ApplicationContext _context;
	private readonly IConfiguration _configuration;

	public TokenRepository(ApplicationContext context, IConfiguration configuration)
	{
		_context = context;
		_configuration = configuration;
	}

	public string CreateJwtToken(User user)
	{
		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role?.Name ?? "Customer"),
			new Claim("tokenType", "accessToken")
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddHours(2),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public async Task<string> CreateRefreshTokenAsync(User user)
	{
		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role?.Name ?? "Customer"),
			new Claim("tokenType", "refreshToken")
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddDays(7),
			signingCredentials: creds
		);

		var refreshToken = new RefreshToken
		{
			Id = Guid.NewGuid(),
			UserId = user.Id,
			Token = new JwtSecurityTokenHandler().WriteToken(token),
			ExpiresAt = token.ValidTo,
			IsRevoked = false
		};

		_context.Add(refreshToken);
		await _context.SaveChangesAsync();

		return refreshToken.Token;
	}
}
