using System;
using System.Diagnostics;
using SimpleCMS.Core.Logging;

namespace SimpleCMS.Core.Services
{
    public static class LogService
    {
        public static ILogger GetCurrentClassLogger()
        {
            const int callingMethodFrame = 1;
            var stackFrame = new StackFrame(callingMethodFrame, false);
            var sourceType = stackFrame.GetMethod().DeclaringType;

            return GetLoggerForEnvironment(sourceType);
        }

        private static ILogger GetLoggerForEnvironment(Type sourceType)
        {
            return new ConsoleLogger(sourceType);
        }
    }
}