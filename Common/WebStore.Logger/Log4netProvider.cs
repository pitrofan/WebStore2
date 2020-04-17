using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Xml;

namespace WebStore.Logger
{
    public sealed class Log4netProvider : ILoggerProvider
    {
        private readonly string configurationFile;

        readonly ConcurrentDictionary<string, Log4netLogger> loggers = new ConcurrentDictionary<string, Log4netLogger>();

        public Log4netProvider(string ConfigurationFile) => configurationFile = ConfigurationFile;



        ILogger ILoggerProvider.CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, category => 
            {
                var xml = new XmlDocument();
                xml.Load(configurationFile);
                return new Log4netLogger(category, xml["log4net"]);
            });
        }

        void IDisposable.Dispose() => loggers.Clear();
    }


}
