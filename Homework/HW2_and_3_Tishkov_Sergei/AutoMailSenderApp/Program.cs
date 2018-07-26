using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using AutoMailSenderApp.Abstractions;
using AutoMailSenderApp.Infrastructure;
using log4net;
using SamopalIndustries;

namespace AutoMailSenderApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IFileHandler handler = InstantiateDefaultFileHandlerDI();
            handler.Enable();

            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
            }
        }

        private static IFileHandler InstantiateDefaultFileHandlerDI()
        {
            var di = new SamopalDI();

            di.BindDefault<AppSettingsReader>();

            di.BindDefault<ISmtpHandler>((args) =>
            {
                AppSettingsReader appSettingsReader = di.GetDefault<AppSettingsReader>();

                string mail = (string)appSettingsReader.GetValue("OurMailAddress", typeof(string));
                string host = (string)appSettingsReader.GetValue("Host", typeof(string));
                int port = (int)appSettingsReader.GetValue("Port", typeof(int));
                SmtpDeliveryMethod method = (SmtpDeliveryMethod)appSettingsReader.GetValue("SMTPDeliveryMethod", typeof(int));
                string ourMailPassword = (string)appSettingsReader.GetValue("OurMailPassword", typeof(string));
                bool enableSsl = (bool)appSettingsReader.GetValue("EnableSSL", typeof(bool));

                return new SmtpHandler(host, port, method, new NetworkCredential(mail, ourMailPassword), enableSsl);
            });

            di.BindDefault<ISendingFileFactory, SendingFileFactory>();

            di.BindDefault<IFileSender>((args) =>
            {
                AppSettingsReader appSettingsReader = di.GetDefault<AppSettingsReader>();

                string ourMail = (string)appSettingsReader.GetValue("OurMailAddress", typeof(string));
                string targetMail = (string)appSettingsReader.GetValue("TargetMailAddress", typeof(string));

                return new FileSender(ourMail, targetMail, di.GetDefault<ISmtpHandler>(), di.GetDefault<ISendingFileFactory>());
            });

            di.BindDefault<IFileManipulator, FileManipulator>();

            di.BindDefault<IFileWatcher>((args) =>
            {
                AppSettingsReader appSettingsReader = di.GetDefault<AppSettingsReader>();

                string mailToSendFolderPath = (string)appSettingsReader.GetValue("MailToSendFP", typeof(string));
                NotifyFilters notifyFilter = (NotifyFilters)appSettingsReader.GetValue("NotifyFilter", typeof(int));
                string filter = (string)appSettingsReader.GetValue("Filter", typeof(string));

                return new FileWatcher(mailToSendFolderPath, filter, notifyFilter, di.GetDefault<IFileManipulator>());
            });

            di.BindDefault<ILog>((args) => LogManager.GetLogger(typeof(FileHandler)));

            di.BindDefault<IFileHandler>((args) =>
            {
                AppSettingsReader appSettingsReader = di.GetDefault<AppSettingsReader>();

                string mailToSendFolderPath = (string)appSettingsReader.GetValue("MailToSendFP", typeof(string));
                string invalidMailFolderPath = (string)appSettingsReader.GetValue("InvalidMailFP", typeof(string));

            return new FileHandler(
                mailToSendFolderPath,
                invalidMailFolderPath,
                di.GetDefault<IFileWatcher>(),
                di.GetDefault<IFileManipulator>(),
                di.GetDefault<IFileSender>(),
                di.GetDefault<ILog>());
            });

            return di.GetDefault<IFileHandler>();
        }

        private static FileHandler InstantiateDefaultFileHandler()
        {
            AppSettingsReader appSettingsReader = new AppSettingsReader();

            string ourMail = (string)appSettingsReader.GetValue("OurMailAddress", typeof(string));
            string targetMail = (string)appSettingsReader.GetValue("TargetMailAddress", typeof(string));

            ISmtpHandler smtpHandler = GetSmtpHandler(appSettingsReader);
            ISendingFileFactory fileFactory = new SendingFileFactory();

            FileSender sender = new FileSender(ourMail, targetMail, smtpHandler, fileFactory);

            IFileManipulator manipulator = new FileManipulator();

            string mailToSendFolderPath = (string)appSettingsReader.GetValue("MailToSendFP", typeof(string));
            string invalidMailFolderPath = (string)appSettingsReader.GetValue("InvalidMailFP", typeof(string));

            // NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            NotifyFilters notifyFilter = (NotifyFilters)appSettingsReader.GetValue("NotifyFilter", typeof(int));
            string filter = (string)appSettingsReader.GetValue("Filter", typeof(string));
            IFileWatcher watcher = new FileWatcher(mailToSendFolderPath, filter, notifyFilter, manipulator);

            ILog logger = LogManager.GetLogger(typeof(FileHandler));

            FileHandler result = new FileHandler(mailToSendFolderPath, invalidMailFolderPath, watcher, manipulator, sender, logger);
            return result;

            SmtpHandler GetSmtpHandler(AppSettingsReader settingsReader)
            {
                string host = (string)settingsReader.GetValue("Host", typeof(string));
                int port = (int)settingsReader.GetValue("Port", typeof(int));
                SmtpDeliveryMethod method = (SmtpDeliveryMethod)settingsReader.GetValue("SMTPDeliveryMethod", typeof(int));
                string ourMailPassword = (string)settingsReader.GetValue("OurMailPassword", typeof(string));
                bool enableSsl = (bool)settingsReader.GetValue("EnableSSL", typeof(bool));

                return new SmtpHandler(host, port, method, new NetworkCredential(ourMail, ourMailPassword), enableSsl);
            }
        }
    }
}
