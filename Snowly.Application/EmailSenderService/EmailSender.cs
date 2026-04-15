using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Org.BouncyCastle.Security;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.EmailSenderService
{
    public class EmailSender
    {
        private readonly string _senderName;
        private readonly string _senderEmail;
        private readonly string _apiKey;

        public EmailSender()
        {
            _senderName = Environment.GetEnvironmentVariable("EMAIL_SENDER_NAME") ?? "Snowly";
            _senderEmail = Environment.GetEnvironmentVariable("EMAIL_SENDER") ?? throw new Exception("EMAIL_SENDER not set");
            _apiKey = Environment.GetEnvironmentVariable("EMAIL_PASSWORD") ?? throw new Exception("EMAIL_PASSWORD not set");
        }
        public async Task<bool> SendMail(string email, string code)
        {
            string htmlBody;

            if (email == "xxx@gmail.com")
                htmlBody = await LoadSnowEmailTemplateAsync(code, true);
            else
                htmlBody = await LoadSnowEmailTemplateAsync(code, false);
            try
            {
                var client = new SendGridClient(_apiKey);
                var from = new EmailAddress(_senderEmail, _senderName);
                var subject = "Snowly Kayıt Doğrulama İşlemi";
                var to = new EmailAddress(email);
                var plainTextContent = "Snowly Kayıt Doğrulama İşlemi";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlBody);

                var response = await client.SendEmailAsync(msg);

                if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                {
                    Console.WriteLine("Email sent successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to send email.");
                    string body = await response.Body.ReadAsStringAsync();
                    Console.WriteLine($"Response body: {body}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EMAIL SEND ERROR -> " + ex.ToString());
                return false;
            }
        }

        public async Task<string> LoadSnowEmailTemplateAsync(string code, bool isPrivate)
        {
            string templatePath;
            if(isPrivate) templatePath = Path.Combine(AppContext.BaseDirectory, "HTMLTemplates", "Snow.html");
            else templatePath = Path.Combine(AppContext.BaseDirectory, "HTMLTemplates", "NormalHtmlTemplate.html");

            if (!File.Exists(templatePath))
                return "Mail İçeriği Bulunamadı";

            string html = await File.ReadAllTextAsync(templatePath);

            html = html.Replace("{CODE}", code);

            return html;
        }
    }
}
