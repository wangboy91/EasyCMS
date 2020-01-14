using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Wboy.Infrastructure.Core.Dependency
{
    /// <summary>
    /// Provides information about types in the current web application. 
    /// Optionally this class can look at all assemblies in the bin folder.
    /// </summary>
    public class WebAppTypeFinder : AppDomainTypeFinder
    {
        #region Fields

        private bool _ensureBinFolderAssembliesLoaded = true;
        private bool _binFolderAssembliesLoaded = false;

        #endregion

        #region Methods

        /// <summary>
        /// Gets a physical disk path of \Bin directory
        /// </summary>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
           // web 工程的目录结构可能不太一样
          return AppDomain.CurrentDomain.BaseDirectory;
        }


        public override IList<Assembly> GetAssemblies()
        {
            if (this._ensureBinFolderAssembliesLoaded && !_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                string binPath = GetBinDirectory();
                //*将bin目录下的dll load到Appdomain中*/
                LoadMatchingAssemblies(binPath);
            }

            return base.GetAssemblies();
        }
        #endregion
    }
}
