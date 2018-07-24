using System;

namespace AutoMailSenderApp.Abstractions
{
    public interface IFileHandler : IDisposable
    {
        string MailToSendFP { get; }

        string InvalidMailFP { get; }

        void Enable();

        void Disable();
    }
}
