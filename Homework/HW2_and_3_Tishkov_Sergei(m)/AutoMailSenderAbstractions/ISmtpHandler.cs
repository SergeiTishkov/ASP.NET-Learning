using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace AutoMailSenderApp.Abstractions
{
    public interface ISmtpHandler : IDisposable
    {
        int Port { get; }

        SmtpDeliveryMethod DeliveryMethod { get; }

        string Host { get; }

        ICredentialsByHost Credentials { get; }

        bool EnableSsl { get; }

        void Send(ISendingFile sendingFile);
    }
}
