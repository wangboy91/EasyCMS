using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wboy.Infrastructure.Core.Configuration
{
    public interface ISettingInitialization
    {
        /// <summary>
        /// 初始化Setting配置
        /// <remarks>
        /// 将所有ISetting类型的属性持久化到存储设备
        /// </remarks>
        /// </summary>
        bool Initialization();
    }
}
