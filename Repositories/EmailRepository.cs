using System.Net;
using System.Net.Mail;
using AmazonApiServer.Interfaces;

public class EmailRepository : IEmail
{
	private readonly IConfiguration _config;
	private readonly ILogger<EmailRepository> _logger;

	public EmailRepository(IConfiguration config, ILogger<EmailRepository> logger)
	{
		_config = config;
		_logger = logger;
	}

	public async Task SendConfirmationCodeAsync(string email, string code)
	{
		// конфигурация из appsettings.json
		var smtpHost = _config["Email:SmtpHost"];
		var smtpPort = int.Parse(_config["Email:SmtpPort"]!);
		var smtpUser = _config["Email:SmtpUser"];
		var smtpPass = _config["Email:SmtpPass"];
		var fromEmail = _config["Email:From"]!;

		using var client = new SmtpClient(smtpHost, smtpPort)
		{
			EnableSsl = true,
			Credentials = new NetworkCredential(smtpUser, smtpPass)
		};

		MailMessage mail = new MailMessage(fromEmail, email)
		{
			Subject = "Confirmation Code",
			Body = $"Your confirmation code: {code}"
		};

		await client.SendMailAsync(mail);
		_logger.LogInformation("Confirmation email sent to {email}", email);
	}
}
