using MimeKit;
using MailKit.Security;
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

	public async Task SendConfirmationCodeAsync(string email, string code, string title, string subtitle)
	{
		// конфигурация из appsettings.json
		var smtpHost = _config["Email:SmtpHost"];
		var smtpPort = int.Parse(_config["Email:SmtpPort"]!);
		var smtpUser = _config["Email:SmtpUser"];
		var smtpPass = _config["Email:SmtpPass"];
		var fromEmail = _config["Email:From"]!;
		var fromName = _config["Email:FromName"] ?? "Support";

		// Загружаем HTML-шаблон
		var templatePath = Path.Combine("Templates", "CodeEmail.html");
		if (!File.Exists(templatePath))
		{
			_logger.LogError("Email template not found: {path}", templatePath);
			throw new FileNotFoundException("Email template not found.", templatePath);
		}

		var html = await File.ReadAllTextAsync(templatePath);

		html = html
			.Replace("{{TITLE}}", title)
			.Replace("{{SUBTITLE}}", subtitle)
			.Replace("{{CODE}}", code);

		// Создаём письмо
		var message = new MimeMessage();
		message.From.Add(new MailboxAddress(fromName, fromEmail));
		message.To.Add(MailboxAddress.Parse(email));
		message.Subject = title;

		var builder = new BodyBuilder
		{
			HtmlBody = html,
			TextBody = $"Your confirmation code is: {code}"
		};
		message.Body = builder.ToMessageBody();

		// Отправка через SMTP
		using var client = new MailKit.Net.Smtp.SmtpClient();
		await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
		await client.AuthenticateAsync(smtpUser, smtpPass);
		await client.SendAsync(message);
		await client.DisconnectAsync(true);

		_logger.LogInformation("Confirmation email sent to {email}", email);
	}
}
