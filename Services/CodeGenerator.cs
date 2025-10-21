namespace AmazonApiServer.Services
{
	public class CodeGenerator
	{
		public static string Generate6DigitCode()
		{
			var random = new Random();
			return random.Next(1, 1000000).ToString("D6");
		}
	}
}
