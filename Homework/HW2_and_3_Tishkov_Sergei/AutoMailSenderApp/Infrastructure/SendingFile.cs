using System.Net.Mail;
using AutoMailSenderApp.Abstractions;

namespace AutoMailSenderApp.Infrastructure
{
    public class SendingFile : ISendingFile
    {
        public SendingFile(string from, string to, string attach)
        {
            this.Message = new MailMessage(from, to);
            this.AddAttachment(attach);
        }

        public MailMessage Message { get; }

        public void AddAttachment(string fullPath)
        {
            this.Message.Attachments.Add(new Attachment(fullPath));
        }

        public void Dispose()
        {
            this.Message.Dispose();
        }
    }
}
