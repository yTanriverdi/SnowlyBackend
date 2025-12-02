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

            Console.WriteLine($"EMAIL_SENDER_NAME: {_senderName}");
            Console.WriteLine($"EMAIL_SENDER: {_senderEmail}");
            Console.WriteLine($"EMAIL_PASSWORD: {(_apiKey != null ? new string('*', _apiKey.Length) : "NULL")}");
        }
        public async Task<bool> SendMail(string email, string code)
        {
            string htmlBody;

            if (email == "aydoganozlem99@gmail.com" || email == "ozlemaydogan99@gmail.com")
                htmlBody = await LoadSnowEmailTemplateAsync(code, true);
            else
                htmlBody = await LoadSnowEmailTemplateAsync(code, false);

            //try
            //{
            //    var message = new MimeMessage();
            //    message.From.Add(new MailboxAddress(_senderName, _senderEmail));
            //    message.To.Add(MailboxAddress.Parse(email));
            //    message.Subject = "Snowly Kayıt Doğrulama İşlemi";
            //    message.Body = new TextPart("html")
            //    {
            //        Text = htmlBody
            //    };

            //    using (var client = new SmtpClient())
            //    {
            //        Console.WriteLine("Connecting to SMTP...");
            //        await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            //        Console.WriteLine("Connected. Authenticating...");

            //        await client.AuthenticateAsync("apikey", _password);
            //        Console.WriteLine("Authenticated. Sending message...");

            //        await client.SendAsync(message);
            //        Console.WriteLine("Message sent. Disconnecting...");

            //        await client.DisconnectAsync(true);
            //        Console.WriteLine("Disconnected.");
            //    }

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //    return false;
            //}
            try
            {
                var client = new SendGridClient(_apiKey);
                var from = new EmailAddress(_senderEmail, _senderName);
                var subject = "Snowly Kayıt Doğrulama İşlemi";
                var to = new EmailAddress(email);
                var plainTextContent = "Snowly Kayıt Doğrulama İşlemi";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlBody);

                Console.WriteLine("Sending email via SendGrid API...");
                var response = await client.SendEmailAsync(msg);

                Console.WriteLine($"SendGrid Response Status: {response.StatusCode}");
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
