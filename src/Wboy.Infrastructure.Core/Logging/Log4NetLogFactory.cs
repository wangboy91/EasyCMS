using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net;
using log4net.Config;

namespace Wboy.Infrastructure.Core.Logging
{
    /// <summary>
    /// A log4 Source base, log factory
    /// </summary>
    public class Log4NetLogFactory : ILoggerFactory
    {
        private readonly log4net.Repository.ILoggerRepository _loggerRepository;
        public Log4NetLogFactory()
        {
            _loggerRepository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(_loggerRepository, new FileInfo("log4net.config"));
        }

        /// <summary>
        /// Create the log4 source log
        /// </summary>
        /// <returns>New ILog based on Trace Source infrastructure</returns>
        public ILogger Create(string name)
        {

            var log = LogManager.GetLogger(_loggerRepository.Name, name);
            return new Log4NetLog(log);
        }
    }
}
