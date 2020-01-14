using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wboy.Infrastructure.Core.Configuration
{
    public interface ISettingProvider<TSettings> where TSettings : ISetting, new()
    {
        /// <summary>
        /// ISettings
        /// </summary>
        TSettings Settings { get; }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="setting">ISettings</param>
        void SaveSettings(TSettings setting);

        /// <summary>
        /// 删除ISettings
        /// </summary>
        void DeleteSettings();
    }
}
