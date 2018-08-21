using System.Net.Mail;

namespace WasteProducts.Logic.Common.Services
{
    /// <summary>
    /// Provides support for creating MailMessage objects.
    /// </summary>
    public interface IMailFactory
    {
        /// <summary>
        /// Creates new MailMessage object.
        /// </summary>
        /// <param name="from"> A System.String that contains the address of the sender of the e-mail message.</param>
        /// <param name="to">A System.String that contains the address of the recipient of the e-mail message.</param>
        /// <param name="subject">A System.String that contains the subject text.</param>
        /// <param name="body">A System.String that contains the message body.</param>
        /// <param name="attachmentPath">A System.String that contains the path to the attachment file.</param>
        /// <returns>A MailMessage object.</returns>
        MailMessage Create(string from, string to, string subject, string body);
    }
}
