using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wboy.Domain.PlatformModule.Entities;
using Wboy.Domain.PlatformModule.IRepository;
using Wboy.Infrastructure.Core;

namespace Wboy.MySql.EfCore.PlatformModule.Repository
{
    public class SystemSettingRepository : Repository<SystemSetting>, ISystemSettingRepository
    {
        public SystemSettingRepository(EfUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public SystemSetting Get(string key)
        {
            return _dbSet.FirstOrDefault(i => i.Key == key);
        }

        /// <summary>
        /// 异步获取配置分页列表
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页值</param>
        /// <param name="keyword">关键字</param>
        /// <param name="excludeType">排除类型</param>
        /// <returns></returns>
        public async Task<PagedList<SystemSetting>> GetPageList(int pageIndex, int pageSize, string keyword)
        {
            var query = _dbSet.Where(x => true);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Key.Contains(keyword));
            }
            return await query.OrderBy(x => x.Key)
                               .PagingAsync(pageIndex, pageSize);
        }
    }
}
