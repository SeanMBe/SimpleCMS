using System;
using System.Configuration;
using System.Diagnostics;

namespace SimpleCMS.Infrastructure.Logging
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
            var environment = ConfigurationManager.AppSettings["Environment"];

            if (string.IsNullOrEmpty(environment))
                return new NullLogger(sourceType);

            switch (environment)
            {
                case "Release":
                    return new NullLogger(sourceType); // real logger
                default:
                    return new NullLogger(sourceType);
            }
        }
    }
}
