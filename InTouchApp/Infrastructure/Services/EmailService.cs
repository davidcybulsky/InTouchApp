using InTouchApi.Application.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;

namespace InTouchApi.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string sendToEmail, string subject, string body)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_configuration["EmailSender:Email"]));
            email.To.Add(MailboxAddress.Parse(sendToEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = body };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(_configuration["EmailSender:Connection"], 587,
                                    MailKit.Security.SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_configuration["EmailSender:Email"],
                                         _configuration["EmailSender:Password"]);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            Log.Logger.Information($"Email to {sendToEmail} was sent, the subject of the emial was {subject}");
        }
    }
}
