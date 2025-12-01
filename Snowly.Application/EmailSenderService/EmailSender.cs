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
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
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
                var fromName = _configuration["EmailSettings:SenderName"];
                var fromAddress = _configuration["EmailSettings:SenderEmail"];
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:Port"]);
                var password = _configuration["EmailSettings:AppPassword"];

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName, fromAddress));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = "Snowly Kayıt Doğrulama Kodu";
                message.Body = new TextPart("html")
                {
                    Text = htmlBody
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(fromAddress, password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception)
            {
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
