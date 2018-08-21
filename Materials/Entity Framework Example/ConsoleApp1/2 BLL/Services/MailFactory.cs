using System.Net.Mail;
using WasteProducts.Logic.Common.Services;

namespace WasteProducts.Logic.Services
{
    public class MailFactory : IMailFactory
    {
        public MailMessage Create(string from, string to, string subject, string body)
        {
            return new MailMessage(from, to, subject, body);
        }
    }
}
