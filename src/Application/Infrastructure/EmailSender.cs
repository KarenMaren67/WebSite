using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Application.Infrastructure
{
	public class EmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
	{
		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var message = new MimeMessage
			{
				Subject = subject,
				Body = new TextPart("plain")
				{
					Text = htmlMessage
				}
			};

			message.From.Add(new MailboxAddress("cerama-shop@yandex.ru"));
			message.To.Add(new MailboxAddress(email));


			using (var client = new SmtpClient())
			{
				// For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
				client.ServerCertificateValidationCallback = (s, c, h, e) => true;

				client.Connect("smtp.yandex.ru", 465, true);

				// Note: only needed if the SMTP server requires authentication
				client.Authenticate("cerama-shop@yandex.ru", "YaCeramaDavidKaren1995");

				await client.SendAsync(message);
				client.Disconnect(true);
			}
		}
	}
}
