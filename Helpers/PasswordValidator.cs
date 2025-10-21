using System.Text.RegularExpressions;

namespace AmazonApiServer.Helpers
{
	public class PasswordValidator
	{
		public static bool IsValid(string password)
		{
			if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
				return false;

			bool hasUpper = Regex.IsMatch(password, "[A-Z]");
			bool hasLower = Regex.IsMatch(password, "[a-z]");
			bool hasDigit = Regex.IsMatch(password, "[0-9]");

			return hasUpper && hasLower && hasDigit;
		}
	}
}
