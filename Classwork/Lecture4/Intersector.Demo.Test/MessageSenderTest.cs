using System;
using Castle.DynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Intersector.Demo.Test
{
    [TestClass]
    public class MessageSenderTest
    {
        [TestMethod]
        public void SendMessageWithoutInterseptor()
        {
            var logger = new FileLogger();

            var messageSender = new MessageSender();

            messageSender.Send(123, "Hello, user");
        }

        [TestMethod]
        public void SendMessageWithInterseptor()
        {
            var logger = new FileLogger();
            var builder = new ProxyGenerator();
            var messageSender = new MessageSender();

            var proxy = builder.CreateInterfaceProxyWithTarget<IMessageSender>(messageSender,
                new MessageSenderInterceptor(logger));

            proxy.Send(123, "Hello, user");

            messageSender.Send(123, "Hello, user");
        }

        [TestMethod]
        public void SendMessageWithInjection()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ILogger>().To<FileLogger>();
            kernel.Bind<IMessageSender>().ToMethod((ctx) =>
            {
                var builder = new ProxyGenerator();
                var messageSender = new MessageSender();

                return builder.CreateInterfaceProxyWithTarget<IMessageSender>(messageSender,
                new MessageSenderInterceptor(ctx.Kernel.Get<ILogger>()));
            });

            var proxy = kernel.Get<IMessageSender>();

            proxy.Send(123, "Hello, 123 user");
        }
    }
}
