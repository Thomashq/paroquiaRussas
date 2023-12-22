using paroquiaRussas.Models;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Interfaces;
using System.Net;
using System.Net.Mail;

namespace paroquiaRussas.Services
{
    public class MailServices : IEmail
    {
        private readonly IConfiguration _configuration;

        public MailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Send(Mail mail)
        {
            try
            {
                MailApiConfig mailApi = new MailApiConfig()
                {
                    Host = _configuration.GetValue<string>("MailApiConfig:Host"),
                    Name = _configuration.GetValue<string>("MailApiConfig:Name"),
                    UserName = _configuration.GetValue<string>("MailApiConfig:UserName"),
                    Password = _configuration.GetValue<string>("MailApiConfig:Password"),
                    Port = _configuration.GetValue<int>("MailApiConfig:Port"),
                    MailFrom = _configuration.GetValue<string>("MailApiConfig:UserName"),
                    MailTo = _configuration.GetValue<string>("MailApiConfig:MailTo")
                };

                MailMessage mailMessage= new MailMessage()
                {
                    From = new MailAddress(mailApi.UserName, mailApi.Name)
                };

                string mailBody = $"{mail.MailContent}\n\nInformações de Contato:\n{mail.MailContact}";

                mailMessage.To.Add(mailApi.MailTo);
                mailMessage.Subject = mail.MailSubject;
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(mailApi.Host, mailApi.Port))
                {
                    smtp.Credentials = new NetworkCredential(mailApi.UserName, mailApi.Password);
                    smtp.EnableSsl = true;

                    smtp.Send(mailMessage);
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
