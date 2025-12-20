using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using NuGet.Configuration;
using PhysioWeb.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using PhysioWeb.Data;

namespace PhysioWeb.Repository
{
    public class EmailServiceRepository : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly DbHelper _dbHelper;

        public EmailServiceRepository(IOptions<EmailSettings> settings, DbHelper dbHelper)
        {
            _settings = settings.Value;
            _dbHelper = dbHelper;

        }
        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody, List<IFormFile> attachments = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                _settings.SenderName, _settings.SenderEmail));

            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };

            // Attach images or files
            if (attachments != null)
            {
                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        using var stream = file.OpenReadStream();
                        bodyBuilder.Attachments.Add(
                            file.FileName, stream);
                    }
                }
            }

            message.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _settings.SmtpServer,
                _settings.Port,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _settings.Username,
                _settings.Password);

            try
            {
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);

                SaveEmailLogAsync(toEmail, subject, htmlBody, "Sent", "Email Sent Successfully");
                // SUCCESS
            }
            catch (Exception ex)
            {
                SaveEmailLogAsync(toEmail, subject, htmlBody, "Failed", "Email Not Sent Successfully");

                // FAILURE
                // Log ex.Message
            }
        }

        private async Task SaveEmailLogAsync(string toEmail, string subject, string body, string status, string errorMessage = null)
        {
            try
            {
                string[] parametersName = { "@ToEmail", "@Subject", "@Body", "@Status", "@ErrorMessage" };

                object[] Values = { toEmail, subject, body, status, errorMessage };

                string Sp = "FMR_SaveEmailLog";

                // No result expected, but call for execution
                await _dbHelper.ExecuteNonQueryAsync(
                    Sp, parametersName, Values);
            }
            catch
            {
                // Do NOT throw here to avoid breaking main flow
            }
        }

    }
}
