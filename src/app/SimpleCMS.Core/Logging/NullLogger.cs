using System;

namespace SimpleCMS.Core.Logging
{
    public class NullLogger : ILogger
    {
        public NullLogger(Type sourceType)
        {
            
        }

        public void Debug(object message)
        {
        }

        public void Debug(object message, Exception exception)
        {
        }

        public void Info(object message)
        {
        }

        public void Info(object message, Exception exception)
        {
        }

        public void Warn(object message)
        {
        }

        public void Warn(object message, Exception exception)
        {
        }

        public void Error(object message)
        {
        }

        public void Error(object message, Exception exception)
        {
        }

        public void Fatal(object message)
        {
        }

        public void Fatal(object message, Exception exception)
        {
        }
    }
}