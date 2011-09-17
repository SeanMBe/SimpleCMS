using System;

namespace SimpleCMS.Core.Logging
{
    public interface ILogger
    {
        void Debug(object message);
        void Debug(object message, Exception exception);
        void Error(object message);
        void Error(object message, Exception exception);
    }
}