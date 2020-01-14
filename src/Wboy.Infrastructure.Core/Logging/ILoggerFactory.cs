using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Infrastructure.Core.Logging
{
    public interface ILoggerFactory
    {
        /// <summary>
        /// Create a new ILog
        /// </summary>
        /// <returns>The ILog created</returns>
        ILogger Create(string name);
    }
}
