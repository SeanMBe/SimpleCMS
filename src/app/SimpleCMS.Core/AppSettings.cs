using System.Configuration;

namespace SimpleCMS.Core
{
    public static class AppSettings {
        public static class Environment
        {
            static readonly string environment = GetKey("Environment");

            public static bool Release {
                get { return !Test && environment == "Release"; }
            }

            public static bool Debug {
                get { return !Test && environment == "Debug"; }
            }

            public static bool Test {
                get {
                    return string.IsNullOrEmpty(environment);
                }
            }
        }

        static string GetKey(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }
    }
}