using System;
using System.IO;
using System.Net.Mail;
using AutoMailSenderApp.Abstractions;

namespace AutoMailSenderApp.Infrastructure
{
    public class FileSender : IFileSender
    {
        private readonly ISmtpHandler _mailSender;

        private readonly ISendingFileFactory _sendingFileCreator;

        public FileSender(string ourMail, string targetMail, ISmtpHandler mailSender, ISendingFileFactory creator)
        {
            this.OurMail = ourMail;
            this.TargetMail = targetMail;
            this._mailSender = mailSender;
            this._sendingFileCreator = creator;
        }

        public string OurMail { get; }

        public string TargetMail { get; }

        public void Dispose() => this._mailSender.Dispose();

        /// <summary>
        /// Send email with attachment.
        /// </summary>
        /// <param name="fullPath">Full path of attaching file.</param>
        public void Send(string fullPath)
        {
            using (ISendingFile file = this._sendingFileCreator.Create(this.OurMail, this.TargetMail, fullPath))
            {
                this._mailSender.Send(file);
            }
        }
    }
}