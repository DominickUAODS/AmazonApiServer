using AmazonApiServer.Interfaces;
using MailKit.Security;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailRepository : IEmail
{
	private readonly IConfiguration _config;
	private readonly ILogger<EmailRepository> _logger;

	public EmailRepository(IConfiguration config, ILogger<EmailRepository> logger)
	{
		_config = config;
		_logger = logger;
	}

	//public async Task SendConfirmationCodeAsync(string email, string code, string title, string subtitle)
	//{
	//	// конфигурация из appsettings.json
	//	var smtpHost = _config["Email:SmtpHost"];
	//	var smtpPort = int.Parse(_config["Email:SmtpPort"]!);
	//	var smtpUser = _config["Email:SmtpUser"];
	//	var smtpPass = _config["Email:SmtpPass"];
	//	var fromEmail = _config["Email:From"]!;
	//	var fromName = _config["Email:FromName"] ?? "Support";

	//	//// Загружаем HTML-шаблон
	//	//var templatePath = Path.Combine("Templates", "CodeEmail.html");
	//	//if (!File.Exists(templatePath))
	//	//{
	//	//	_logger.LogError("Email template not found: {path}", templatePath);
	//	//	throw new FileNotFoundException("Email template not found.", templatePath);
	//	//}

	//	//var html = await File.ReadAllTextAsync(templatePath);

	//	//html = html

	//	// HTML-шаблон письма (встроенный)
	//	var htmlTemplate = @"
	//		<!DOCTYPE html>
	//		<html>
	//		<head>
	//			<meta charset='UTF-8'>
	//			<title>{{TITLE}}</title>
	//		</head>
	//		<body style='font-family: Arial, sans-serif; background-color: #f7f7f7; padding: 20px;'>
	//			<div style='max-width: 600px; margin: 0 auto; background: #fff; border-radius: 8px; padding: 30px; box-shadow: 0 2px 6px rgba(0,0,0,0.1);'>
	//				<h2 style='color: #333; margin-bottom: 10px;'>{{TITLE}}</h2>
	//				<p style='color: #555; font-size: 16px;'>{{SUBTITLE}}</p>
	//				<h1 style='color: #3498db; text-align: center; font-size: 36px; letter-spacing: 4px;'>{{CODE}}</h1>
	//				<p style='color: #888; font-size: 14px; text-align: center;'>This code is valid for 10 minutes.</p>
	//			</div>
	//		</body>
	//		</html>";

	//	// Подставляем значения
	//	var html = htmlTemplate
	//		.Replace("{{TITLE}}", title)
	//		.Replace("{{SUBTITLE}}", subtitle)
	//		.Replace("{{CODE}}", code);

	//	// Создаём письмо
	//	var message = new MimeMessage();
	//	message.From.Add(new MailboxAddress(fromName, fromEmail));
	//	message.To.Add(MailboxAddress.Parse(email));
	//	message.Subject = title;

	//	var builder = new BodyBuilder
	//	{
	//		HtmlBody = html,
	//		TextBody = $"Your confirmation code is: {code}"
	//	};
	//	message.Body = builder.ToMessageBody();

	//	// Отправка через SMTP
	//	using var client = new MailKit.Net.Smtp.SmtpClient();
	//	await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
	//	await client.AuthenticateAsync(smtpUser, smtpPass);
	//	await client.SendAsync(message);
	//	await client.DisconnectAsync(true);

	//	_logger.LogInformation("Confirmation email sent to {email}", email);
	//}

	public async Task SendConfirmationCodeAsync(string email, string code, string title, string subtitle)
	{
		var apiKey = _config["SendGrid:ApiKey"];
		_logger.LogInformation("SendGrid API key length: {len}", apiKey?.Length ?? 0);
		var client = new SendGridClient(apiKey);

		var from = new EmailAddress("vladimir.demch@gmail.com", "Support");
		var subject = title;
		var to = new EmailAddress(email);
		var plainText = $"Your confirmation code is: {code}";
		//var html = $"<p>{subtitle}</p><h1>{code}</h1><p>This code is valid for 10 minutes.</p>";

		var htmlTemplate = @"
			<!DOCTYPE html>
			<html>
			<head>
				<meta charset='UTF-8'>
				<title>{{TITLE}}</title>
			</head>
			<body style='font-family: Arial, sans-serif; background-color: #f7f7f7; padding: 20px;'>
				<div style='max-width: 600px; margin: 0 auto; background: #fff; border-radius: 8px; padding: 30px; box-shadow: 0 2px 6px rgba(0,0,0,0.1);'>
					<h2 style='color: #333; margin-bottom: 10px;'>{{TITLE}}</h2>
					<p style='color: #555; font-size: 16px;'>{{SUBTITLE}}</p>
					<h1 style='color: #3498db; text-align: center; font-size: 36px; letter-spacing: 4px;'>{{CODE}}</h1>
					<p style='color: #888; font-size: 14px; text-align: center;'>This code is valid for 10 minutes.</p>
				</div>
			</body>
			</html>";

		// Подставляем значения
		var html = htmlTemplate
			.Replace("{{TITLE}}", title)
			.Replace("{{SUBTITLE}}", subtitle)
			.Replace("{{CODE}}", code);

		var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, html);
		var response = await client.SendEmailAsync(msg);

		var body = await response.Body.ReadAsStringAsync();
		_logger.LogError("SendGrid response body: {body}", body);

		_logger.LogInformation("SendGrid response: {status}", response.StatusCode);
	}
}
