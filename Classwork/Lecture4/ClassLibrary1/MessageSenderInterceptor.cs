using Castle.DynamicProxy;
using System;

namespace Intersector.Demo
{
    public class MessageSenderInterceptor : IInterceptor
    {
        private readonly ILogger _logger;

        public MessageSenderInterceptor(ILogger logger)
        {
            _logger = logger;
        }
        public void Intercept(IInvocation invocation)
        {
            var userId = invocation.GetArgumentValue(0);
            var message = invocation.GetArgumentValue(1);

            _logger.Log($"User {userId} with Message {message} {DateTime.UtcNow}");

            invocation.Proceed();
        }
    }
}
