using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wboy.Domain
{
    /// <summary>
    /// Base contract for support 'dialect specific queries'.
    /// </summary>
    public interface ISql
    {
        /// <summary>
        /// Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">
        /// Command to execute
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        ///</param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>The number of affected records</returns>
        int ExecuteCommand(string sqlCommand, params object[] parameters);

        int ExecuteCommand(string sqlCommand);

    }
}
