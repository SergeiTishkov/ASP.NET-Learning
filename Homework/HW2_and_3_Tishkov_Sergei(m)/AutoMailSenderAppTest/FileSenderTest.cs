using AutoMailSenderApp.Infrastructure;
using System.IO;
using Moq;
using AutoMailSenderApp.Abstractions;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoMailSenderAppTest
{
    [TestClass]
    public class FileSenderTest
    {
        [TestMethod]
        public void TestOfCorrectFileSending()
        {
            var smtpHandlerMoq = new Mock<ISmtpHandler>();

            var sendingFileMoq = new Mock<ISendingFile>();

            var sendingFileFactoryMoq = new Mock<ISendingFileFactory>();
            sendingFileFactoryMoq.Setup(m => m.Create("pseudoemail@mail.ru", "pseudoemail2@mail.ru", "pseudoCorrectAddress")).Returns(sendingFileMoq.Object);

            smtpHandlerMoq.Setup(f => f.Send(sendingFileMoq.Object)).Verifiable();

            FileSender fileSender = new FileSender("pseudoemail@mail.ru", "pseudoemail2@mail.ru", smtpHandlerMoq.Object, sendingFileFactoryMoq.Object);

            fileSender.Send("pseudoCorrectAddress");

            smtpHandlerMoq.Verify(h => h.Send(sendingFileMoq.Object), Times.Once);
        }

        [TestMethod]
        public void TestOfNotCorrectFileSending()
        {
            var smtpHandlerMoq = new Mock<ISmtpHandler>();

            var sendingFileFactoryMoq = new Mock<ISendingFileFactory>();
            sendingFileFactoryMoq.Setup(m => m.Create("pseudoemail@mail.ru", "pseudoemail2@mail.ru", "pseudoIncorrectAddress")).Throws<FileNotFoundException>();

            FileSender fileSender = new FileSender("pseudoemail@mail.ru", "pseudoemail2@mail.ru", smtpHandlerMoq.Object, sendingFileFactoryMoq.Object);

            NUnit.Framework.Assert.Throws<FileNotFoundException>(() => fileSender.Send("pseudoIncorrectAddress"));
        }
    }
}

//     
