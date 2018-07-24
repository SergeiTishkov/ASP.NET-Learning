using AutoMailSenderApp.Abstractions;

namespace AutoMailSenderApp.Infrastructure
{
    public class SendingFileFactory : ISendingFileFactory
    {
        /// <summary>
        /// Creates a new instance of ISendingFile with specified parameters.
        /// </summary>
        /// <param name="from">Email address using for mailing ISendingFile.</param>
        /// <param name="to">Target email address of ISendingFile.</param>
        /// <param name="attach">Full path to the specified attachment mailing with ISendingFile.</param>
        /// <returns>A new instance of ISendingFile.</returns>
        public ISendingFile Create(string from, string to, string attach)
        {
            return new SendingFile(from, to, attach);
        }
    }
}
