using System;
using System.Net.Mail;

namespace AutoMailSenderApp.Abstractions
{
    public interface ISendingFile : IDisposable
    {
        MailMessage Message { get; }

        void AddAttachment(string fullPath);
    }
}
