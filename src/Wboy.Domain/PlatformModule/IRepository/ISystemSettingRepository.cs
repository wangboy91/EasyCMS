using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain.PlatformModule.Entities;
using Wboy.Infrastructure.Core;

namespace Wboy.Domain.PlatformModule.IRepository
{
    public interface ISystemSettingRepository : IRepository<SystemSetting>
    {
        SystemSetting Get(string key);
        /// <summary>
        /// 异步获取配置分页列表
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页值</param>
        /// <param name="keyword">关键字</param>
        /// <param name="excludeType">排除类型</param>
        /// <returns></returns>
        Task<PagedList<SystemSetting>> GetPageList(int pageIndex, int pageSize, string keyword);
    }
}
