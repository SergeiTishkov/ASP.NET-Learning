using System;
using System.Diagnostics;

namespace Intersector.Demo
{
    public class MessageSender : IMessageSender
    {
        public void Send(int userId, string message)
        {
            // log userId, message
            // send message
            Debug.WriteLine("Message was sent");
        }
    }
}
