using System;
using System.Net;
using System.Net.Mail;
using AutoMailSenderApp.Abstractions;

namespace AutoMailSenderApp.Infrastructure
{
    public class SmtpHandler : ISmtpHandler
    {
        private SmtpClient _smtpClient;

        public SmtpHandler(string host, int port, SmtpDeliveryMethod method, ICredentialsByHost credentials, bool enableSsl)
        {
            this.Host = host;
            this.Port = port;
            this.DeliveryMethod = method;
            this.Credentials = credentials;
            this.EnableSsl = enableSsl;

            this._smtpClient = new SmtpClient
            {
                Host = this.Host,
                Port = this.Port,
                DeliveryMethod = this.DeliveryMethod,
                Credentials = this.Credentials,
                EnableSsl = this.EnableSsl,
            };
        }

        public string Host { get; }

        public int Port { get; }

        public SmtpDeliveryMethod DeliveryMethod { get; }

        public ICredentialsByHost Credentials { get; }

        public bool EnableSsl { get; }

        public void Dispose()
        {
            this._smtpClient.Dispose();
        }

        /// <summary>
        /// Sends ISendingFile to email, specified at the time of its creation.
        /// </summary>
        /// <param name="sendingFile">Instance of ISendingFile to mail with its attachment.</param>
        public void Send(ISendingFile sendingFile)
        {
            this._smtpClient.Send(sendingFile.Message);
        }
    }
}
