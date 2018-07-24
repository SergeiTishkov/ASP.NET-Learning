using System.IO;
using AutoMailSenderApp.Abstractions;

namespace AutoMailSenderApp.Infrastructure
{
    public class FileWatcher : IFileWatcher
    {
        private FileSystemWatcher _watcher;

        public FileWatcher(string path, string filter, NotifyFilters notifyFilter, IFileManipulator manipulator)
        {
            this.Path = path;
            this.Filter = filter;
            this.NotifyFilter = notifyFilter;

            manipulator.CreateFolder(this.Path);

            this._watcher = new FileSystemWatcher(this.Path, this.Filter)
            {
                NotifyFilter = this.NotifyFilter
            };
        }

        public string Path { get; }

        public string Filter { get; }

        public NotifyFilters NotifyFilter { get; }

        public bool EnableRaisingEvents
        {
            get => this._watcher.EnableRaisingEvents;
            set => this._watcher.EnableRaisingEvents = value;
        }

        public void Dispose()
        {
            this._watcher.Dispose();
        }

        /// <summary>
        /// Fires when new file arrived in the watched folder.
        /// </summary>
        public event FileSystemEventHandler Created
        {
            add => this._watcher.Created += value;
            remove => this._watcher.Created -= value;
        }
    }
}
