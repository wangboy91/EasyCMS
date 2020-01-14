using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Infrastructure.Core.Logging
{
    /// <summary>
    /// 日志创建工厂类
    /// </summary>
    public static class LoggerFactory
    {
        private static ILoggerFactory _loggerFactory;


        /// <summary>
        /// 设置一个Logger的创建工厂实例
        /// </summary>
        /// <param name="loggerFactory">工厂实例</param>
        public static void SetCurrent(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            //_logger = null;
        }

       // private static ILogger _logger;
        /// <summary>
        /// 创建一个新的Logger
        /// </summary>
        /// <returns>创建后的 Logger</returns>
        public static ILogger CreateLoger(string name= "Log4Net")
        {
            //if (_loggerFactory != null && _logger == null)
            //{
            //    _logger = _loggerFactory.Create("Log4Net");
            //}
            //return _logger;

            return _loggerFactory?.Create(name);
        }
    }
}
