using System;

namespace SimpleCMS.Core.Logging
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger(Type sourceType)
        {
        }

        public void Debug(object message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Debug(object message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(object message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(object message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}