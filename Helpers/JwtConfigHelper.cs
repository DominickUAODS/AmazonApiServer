﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AmazonApiServer.Helpers
{
	public class JwtConfigHelper
	{
		public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
		{
			return new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = configuration["Jwt:Issuer"],
				ValidAudience = configuration["Jwt:Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
				ClockSkew = TimeSpan.Zero
			};
		}
	}
}
