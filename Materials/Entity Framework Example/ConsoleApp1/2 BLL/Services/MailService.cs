using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using WasteProducts.Logic.Common.Services;

namespace WasteProducts.Logic.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpClient _smtpClient;

        public MailService(SmtpClient smtpClient, string ourEmail, IMailFactory mailFactory)
        {
            _smtpClient = smtpClient;
            OurEmail = ourEmail;
            MailFactory = mailFactory;
        }

        public IMailFactory MailFactory { get; }

        public string OurEmail { get; set; }

        public void Dispose()
        {
            _smtpClient.Dispose();
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public void Send(string to, string subject, string body)
        {
            using (MailMessage message = MailFactory.Create(OurEmail, to, subject, body))
            {
                _smtpClient.Send(message);
            }
        }
    }
}
