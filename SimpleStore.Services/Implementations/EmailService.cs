using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleStore.Entities;
using SimpleStore.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace SimpleStore.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly SmtpConfiguration _options;

        public EmailService(
            ILogger<EmailService> logger,
            IOptions<AppConfig> options
            )
        {
            _logger = logger;
            _options = options.Value.SmtpConfiguration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MailMessage(
                    new MailAddress(_options.UserName, _options.From),
                    new MailAddress(email))
                {
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                using var smtpClient = new SmtpClient(_options.Server, _options.Port)
                {
                    Credentials = new NetworkCredential(_options.UserName, _options.Password),
                    EnableSsl = _options.EnableSsl
                };

                await smtpClient.SendMailAsync(emailMessage);
            }
            catch (SmtpException ex)
            {
                _logger.LogWarning(ex, $"No se puede enviar el correo {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error al enviar el correo a {email} {ex.Message}");
            }
        }
    }
}
