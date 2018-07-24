using System;
using System.IO;
using AutoMailSenderApp.Abstractions;
using AutoMailSenderApp.Infrastructure;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;

namespace AutoMailSenderTest
{
    [TestClass]
    public class FileHandlerTest
    {
        [TestMethod]
        public void TestOfFiringACreatedEvent()
        {
            var sendingFileMoq = new Mock<ISendingFile>();

            var fileSenderMoq = new Mock<IFileSender>();
            fileSenderMoq.Setup(s => s.Send("a\\b")).Verifiable();

            var folderWatcherMoq = new Mock<IFileWatcher>();

            var manipulatorMoq = new Mock<IFileManipulator>();

            var loggerMoq = new Mock<ILog>();

            FileHandler fileHandler =
                new FileHandler("doesn't matter", "doesn't matter again", folderWatcherMoq.Object, manipulatorMoq.Object, fileSenderMoq.Object, loggerMoq.Object);

            fileHandler.Enable();

            folderWatcherMoq.Raise((m => m.Created += null), new FileSystemEventArgs(WatcherChangeTypes.All, "a", "b"));

            fileSenderMoq.Verify(s => s.Send("a\\b"), Times.Once);
        }
    }
}
