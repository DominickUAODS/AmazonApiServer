namespace AmazonApiServer.Interfaces
{
	public interface IEmail
	{
		Task SendConfirmationCodeAsync(string email, string code, string title, string subtitle);
	}
}
