using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace WebStore.Logger
{
    public sealed class Log4netLogger : ILogger
    {
        readonly ILog log;

        public Log4netLogger(string categoryName, XmlElement configuration)
        {
            var loggerRepository = LogManager.CreateRepository(
                    Assembly.GetEntryAssembly(),
                    typeof(log4net.Repository.Hierarchy.Hierarchy));
            log = LogManager.GetLogger(loggerRepository.Name, categoryName);
            log4net.Config.XmlConfigurator.Configure(loggerRepository, configuration);
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    return log.IsDebugEnabled;

                case LogLevel.Information:
                    return log.IsInfoEnabled;

                case LogLevel.Warning:
                    return log.IsWarnEnabled;

                case LogLevel.Error:
                    return log.IsErrorEnabled;

                case LogLevel.Critical:
                    return log.IsFatalEnabled;

                case LogLevel.None:
                    return false;

                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
            return false;
        }

        public void Log<TState>(
            LogLevel level, 
            EventId eventId, 
            TState state, 
            Exception error, 
            Func<TState, Exception, string> formatter)
        {
            if (formatter is null)
                throw new ArgumentNullException(nameof(formatter));

            if (!IsEnabled(level)) return;

            var logMessage = formatter(state, error);

            if (string.IsNullOrEmpty(logMessage) && error is null) return;

            switch (level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    log.Debug(logMessage);
                    break;

                case LogLevel.Information:
                    log.Info(logMessage);
                    break;

                case LogLevel.Warning:
                    log.Warn(logMessage);
                    break;

                case LogLevel.Error:
                    log.Error(logMessage ?? error.ToString());
                    break;

                case LogLevel.Critical:
                    log.Fatal(logMessage ?? error.ToString());
                    break;

                case LogLevel.None:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }


}
