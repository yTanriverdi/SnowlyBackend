using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.EmailSenderService
{
    public class EmailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _senderName;
        private readonly string _senderEmail;
        private readonly string _password;

        public EmailSender()
        {
            _smtpServer = Environment.GetEnvironmentVariable("EMAIL_SMTP") ?? throw new Exception("EMAIL_SMTP not set");
            _smtpPort = int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT") ?? "587");
            _senderName = Environment.GetEnvironmentVariable("EMAIL_SENDER_NAME") ?? "Snowly";
            _senderEmail = Environment.GetEnvironmentVariable("EMAIL_SENDER") ?? throw new Exception("EMAIL_SENDER not set");
            _password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD") ?? throw new Exception("EMAIL_PASSWORD not set");
        }
        public async Task<bool> SendMail(string email, string code)
        {
            string htmlBody;

            if (email == "aydoganozlem99@gmail.com" || email == "ozlemaydogan99@gmail.com")
                htmlBody = await LoadSnowEmailTemplateAsync(code, true);
            else
                htmlBody = await LoadSnowEmailTemplateAsync(code, false);

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_senderName, _senderEmail));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = "Snowly Kayıt Doğrulama İşlemi";
                message.Body = new TextPart("html")
                {
                    Text = htmlBody
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("apikey", _password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
