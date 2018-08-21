using System;
using System.Net;
using System.Net.Mail;

namespace WasteProducts.Logic.Common.Services
{
    public interface IMailService : IDisposable
    {
        IMailFactory MailFactory { get; }

        string OurEmail { get; set; }

        void Send(string to, string subject, string body);

        bool IsValidEmail(string email);
    }
}
