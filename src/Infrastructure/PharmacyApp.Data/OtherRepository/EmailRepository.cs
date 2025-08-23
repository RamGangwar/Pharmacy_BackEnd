using Microsoft.Extensions.Options;
using PharmacyApp.Application.OtherRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using PharmacyApp.Domain.Helper;
using Microsoft.Extensions.Logging;

namespace PharmacyApp.Data.OtherRepository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly EmailNotificationOptions _emailoption;
        private IHostingEnvironment _env;
        private readonly ILogger<EmailRepository> _logger;
        public EmailRepository(IOptionsMonitor<EmailNotificationOptions> option,
            IHostingEnvironment env, ILogger<EmailRepository>  logger)
        {
            _emailoption = option.CurrentValue;
            _env = env;
            _logger = logger;
        }

        public string ReadEmailTemplate(string filename)
        {
            string body = string.Empty;
            var pathToFile = _env.WebRootPath
                           + Path.DirectorySeparatorChar.ToString()
                           + "EmailTemplate"
                           + Path.DirectorySeparatorChar.ToString()
                           + filename;

            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                body = SourceReader.ReadToEnd();
            }
            return body;
        }

        public Task<bool> SendEmail(string mailto, string body, string subject, string mailcc = "",
            byte[] fileattachment = null, string FileName = "")
        {
            try
            {                
                using (MailMessage _mailmsg = new MailMessage())
                {
                    _mailmsg.IsBodyHtml = true;
                    _mailmsg.From = new MailAddress(_emailoption.Fromemail);
                    _mailmsg.To.Add(new MailAddress(mailto));

                    if (!string.IsNullOrEmpty(mailcc))
                    {
                        foreach (string cc in mailcc.Split(','))
                        {
                            if (!string.IsNullOrEmpty(cc))
                            {
                                _mailmsg.CC.Add(new MailAddress(cc));
                            }
                        }
                    }

                    _mailmsg.Subject = subject;
                    _mailmsg.Body = body;
                    if (fileattachment != null)
                    {
                        Attachment att = new Attachment(new MemoryStream(fileattachment), FileName);
                        _mailmsg.Attachments.Add(att);
                    }

                    SmtpClient _smtp = new SmtpClient();
                    _smtp.Host = _emailoption.Host;
                    _smtp.Port = _emailoption.Port;
                    _smtp.EnableSsl = _emailoption.SSL;
                    NetworkCredential _network = new NetworkCredential(
                        _emailoption.UserName,
                        _emailoption.Password);
                    _smtp.Credentials = _network;
                    _smtp.Send(_mailmsg);
                }                
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Task.FromResult(false);
            }
        }
    }
}
