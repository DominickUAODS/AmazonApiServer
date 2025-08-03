namespace AmazonApiServer.Interfaces
{
	public interface IEmail
	{
		Task SendConfirmationCodeAsync(string email, string code);
	}
}
