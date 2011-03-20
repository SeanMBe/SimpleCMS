using System;
using System.Diagnostics;

namespace SimpleCMS.Infrastructure.Logging
{
    public static class LogService
    {
        private static ILogger defaultLogger;

        // Exposed only for testing...
        public static void SetLogger(ILogger logger)
        {
            defaultLogger = logger;
        }

        public static ILogger GetLogger(Type sourceType)
        {
            return defaultLogger ?? new NullLogger();
        }

        public static ILogger GetCurrentClassLogger()
        {
            const int callingMethodFrame = 1;
            var stackFrame = new StackFrame(callingMethodFrame, false);
            return GetLogger(stackFrame.GetMethod().DeclaringType);
        }
    }
}
