using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using System;

namespace ECO3_Testing.ComponentHelpers
{
    public class LoggingHelper
    {
        private static ILog log;
        private static ConsoleAppender _consoleApender;
        private static FileAppender _fileAppender;
        private static RollingFileAppender _rollingFileAppender;
        private static string layout = "%date{dd-MMM-yyyy-HH:mm:ss} [%class] [%level] [%method] - %message%newline";
        public static string Layout
        {
            set { layout = value; }
        }
        private static PatternLayout GetPatternLayout()
        {
            var patternLayout = new PatternLayout()
            {
                ConversionPattern = layout
            };
            patternLayout.ActivateOptions();
            return patternLayout;
        }
        private static ConsoleAppender GetConsoleAppender()
        {
            var consoleAppender = new log4net.Appender.ConsoleAppender()
            {
                Name = "ConsoleAppender",
                Layout = GetPatternLayout(),
                Threshold = Level.All
            };
            consoleAppender.ActivateOptions();
            return consoleAppender;
        }
        private static FileAppender GetFileAppender()
        {
            var fileAppender = new FileAppender()
            {
                Name = "FileAppender",
                Layout = GetPatternLayout(),
                Threshold = Level.All,
                AppendToFile = true,
                File = "FileLogger.log"
            };
            fileAppender.ActivateOptions();
            return fileAppender;
        }
        private static RollingFileAppender GetRollingAppender()
        {
            var rollingFileAppender = new RollingFileAppender()
            {
                Name = "Rolling FileAppender",
                Layout = GetPatternLayout(),
                Threshold = Level.All,
                AppendToFile = true,
                File = "RollingFileLogger.log",
                MaximumFileSize = "1MB",
                MaxSizeRollBackups = 15
            };
            rollingFileAppender.ActivateOptions();
            return rollingFileAppender;
        }
        public static ILog GetLogger(Type type)
        {
            if (_consoleApender == null)
            {
                _consoleApender = GetConsoleAppender();
            }

            //if (_fileAppender == null)
            //{
            //    _fileAppender = GetFileAppender();
            //}

            //if (_rollingFileAppender == null)
            //{
            //    _rollingFileAppender = GetRollingAppender();
            //}

            if (log != null)
                return log;

            //BasicConfigurator.Configure(_consoleApender, _fileAppender, _rollingFileAppender);
            BasicConfigurator.Configure(_consoleApender);
            log = LogManager.GetLogger(type);
            return log;
        }
    }
}
