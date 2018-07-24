using System;
using System.IO;

namespace AutoMailSenderApp.Abstractions
{
    public interface IFileWatcher : IDisposable
    {
        string Path { get; }

        string Filter { get; }

        NotifyFilters NotifyFilter { get; }

        bool EnableRaisingEvents { get; set; }

        event FileSystemEventHandler Created;
    }
}
