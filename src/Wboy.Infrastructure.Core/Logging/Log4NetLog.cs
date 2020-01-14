using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Wboy.Infrastructure.Core.Logging
{
    public class Log4NetLog : ILogger
    {
        private readonly log4net.ILog _log = null;
        public Log4NetLog(log4net.ILog log)
        {
            _log = log;
        }

        public void Debug(string message, params object[] args)
        {
            var msg = string.Format(CultureInfo.InvariantCulture, message, args);

            if (_log.IsDebugEnabled)
            {
                _log.Debug(msg);
            }
        }

        public void Debug(string message, Exception exception, params object[] args)
        {
            var msg = string.Format(CultureInfo.InvariantCulture, message, args);

            if (_log.IsDebugEnabled)
            {
                _log.Debug(msg, exception);
            }
        }

        public void Debug(object item)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(item.ToString());
            }
        }

        public void Fatal(string message, params object[] args)
        {
            var msg = string.Format(CultureInfo.InvariantCulture, message, args);
            if (_log.IsFatalEnabled)
            {
                _log.Fatal(msg);
            }
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            var msg = string.Format(CultureInfo.InvariantCulture, message, args);
            if (_log.IsFatalEnabled)
            {
                _log.Fatal(msg, exception);
            }
        }

        public void Info(string message, params object[] args)
        {
            var msg = !args.Any() ? message : string.Format(CultureInfo.InvariantCulture, message, args);
            if (_log.IsInfoEnabled)
            {
                _log.Info(msg);
            }
        }

        public void Warning(string message, params object[] args)
        {
            var msg = string.Format(CultureInfo.InvariantCulture, message, args);
            if (_log.IsWarnEnabled)
            {
                _log.Warn(msg);
            }
        }

        public void Error(string message, params object[] args)
        {
            var msg = string.Format(CultureInfo.InvariantCulture, message, args);
            if (_log.IsErrorEnabled)
            {
                _log.Error(msg);
            }
        }

        public void Error(string message, Exception exception, params object[] args)
        {
            var msg = string.Format(CultureInfo.InvariantCulture, message, args);
            if (_log.IsErrorEnabled)
            {
                _log.Error(msg, exception);
            }
        }
    }
}
