
using Clinic.Core.DTOs;
using Clinic.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Clinic.Infrastructure.Repositories.Services
{
    public class EmailService :IEmailService
    {

        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(EmailDTO emailDTO)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Clinic Support", configuration["EmailSetting:From"]));
            message.To.Add(new MailboxAddress(emailDTO.To, emailDTO.To));
            message.Subject = emailDTO.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDTO.Content,
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(
                        configuration["EmailSetting:smtp"],
                        int.Parse(configuration["EmailSetting:Port"]), true);

                    await smtp.AuthenticateAsync(configuration["EmailSetting:UserName"]
                        , configuration["EmailSetting:Password"]);

                    await smtp.SendAsync(message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Email send failed: " + ex.Message);
                }
                finally
                {
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
            }

        }
    }
}
