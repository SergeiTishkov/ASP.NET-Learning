using System.Dynamic;
using Module1.Lib;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Module1.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            config.AddTarget(new ColoredConsoleTarget("console"));
            config.AddRuleForAllLevels("console");

            LogManager.Configuration = config;
            var logger = LogManager.GetCurrentClassLogger();


            var scanner = new NetworkScaner(logger);
            var result = scanner.Scan("127.0.0.1", "8.8.8.8", "example.com");
            System.Console.WriteLine(result);
            System.Console.ReadLine();
        }
    }
}
