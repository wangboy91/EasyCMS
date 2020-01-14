using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Domain.AdminModule.IRepository;

namespace Wboy.MySql.EfCore.AdminModule.Repository
{
    public class AdminConfigRepository : Repository<AdminConfigEntity>, IAdminConfigRepository
    {
        public AdminConfigRepository()
            : base(new EfUnitOfWork())
        { }

        #region 验证数据
        /// <summary>
        /// 验证是否初始完成
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ExistDataInitedAsync()
        {
            return await _dbSet.AnyAsync(x => x.IsDataInited);
        }
        /// <summary>
        ///  验证是否初始完成
        /// </summary>
        /// <returns></returns>
        public bool ExistDataInited()
        {
            return _dbSet.Any(x => x.IsDataInited);
        }
        #endregion
    }
}
