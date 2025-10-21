﻿// Security/PasswordHasher.cs
using System.Security.Cryptography;
using System.Text;

namespace AmazonApiServer.Services
{
	public static class PasswordHasher
	{
		private const int SaltSize = 16; // 128 bit
		private const int KeySize = 32;  // 256 bit
		private const int Iterations = 100_000;
		private const char Delimiter = ';';

		public static string HashPassword(string password)
		{
			using var rng = RandomNumberGenerator.Create();
			byte[] salt = new byte[SaltSize];
			rng.GetBytes(salt);

			var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
			return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(key));
		}

		public static bool VerifyPassword(string password, string hash)
		{
			var parts = hash.Split(Delimiter);
			if (parts.Length != 2)
				return false;

			var salt = Convert.FromBase64String(parts[0]);
			var key = Convert.FromBase64String(parts[1]);

			var keyToCheck = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);

			return CryptographicOperations.FixedTimeEquals(keyToCheck, key);
		}
	}
}
