using System;

namespace AutoMailSenderApp.Abstractions
{
    public interface IFileSender : IDisposable
    {
        string OurMail { get; }

        string TargetMail { get; }

        void Send(string fullPath);
    }
}
