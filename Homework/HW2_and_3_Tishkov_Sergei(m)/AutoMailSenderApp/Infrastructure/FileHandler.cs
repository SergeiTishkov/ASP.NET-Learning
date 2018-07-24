using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using AutoMailSenderApp.Abstractions;
using AutoMailSenderApp.Infrastructure;
using log4net;

namespace AutoMailSenderApp.Infrastructure
{
    public class FileHandler : IFileHandler
    {
        private IFileWatcher _fileWatcher;

        private IFileManipulator _manipulator;

        private IFileSender _fileSender;

        private ILog _logger;

        public FileHandler(string mailToSendFP, string invalidMailFP, IFileWatcher watcher, IFileManipulator manipulator, IFileSender sender, ILog logger)
        {
            this.MailToSendFP = mailToSendFP;
            this.InvalidMailFP = invalidMailFP;

            this._fileWatcher = watcher;
            this._manipulator = manipulator;
            this._fileSender = sender;
            this._logger = logger;

            this._manipulator.CreateFolder(this.MailToSendFP);
            this._manipulator.CreateFolder(this.InvalidMailFP);
        }

        public string MailToSendFP { get; }

        public string InvalidMailFP { get; }

        public void Dispose()
        {
            this._fileWatcher.Created -= this.HandleNewFile;
            this._fileSender.Dispose();
            this._fileWatcher.Dispose();
        }

        /// <summary>
        /// Enable this instance of FileWatcher to begin searching for appearance of new files and mailing them.
        /// </summary>
        public void Enable()
        {
            this._fileWatcher.Created += this.HandleNewFile;
            this._logger.Info("Application is ready to work"); ////////////////////
            this._fileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Disable this instance of FileWatcher from searching and mailing of new files.
        /// </summary>
        public void Disable()
        {
            this._fileWatcher.Created -= this.HandleNewFile;
            this._logger.Info("Application is stopped its work"); ////////////////////
            this._fileWatcher.EnableRaisingEvents = false;
        }

        private void HandleNewFile(object sender, FileSystemEventArgs args)
        {
            string fullPath = args.FullPath;

            try
            {
                this._fileSender.Send(fullPath);
                this._logger.Info($"Mail with attachment ({fullPath}) has been sent"); ////////////
                this._manipulator.DeleteFile(fullPath);
                this._logger.Info($"Sent out attachment ({fullPath}) has been deleted"); ///////////////////////////////////////
            }
            catch (IOException ioE)
            {
                WorkWihException(ioE);
            }
            catch (SmtpException smtpE)
            {
                WorkWihException(smtpE);
            }

            void WorkWihException(Exception ex)
            {
                this._manipulator.MoveInvalidAttachment(fullPath, this.InvalidMailFP);
                this._logger.Error(ex); /////////////////////////
                this._logger.Warn($"Invalid attachment ({fullPath}) has been moved to \"{this.InvalidMailFP}\""); ///////////////////////////////////////
            }
        }
    }
}
