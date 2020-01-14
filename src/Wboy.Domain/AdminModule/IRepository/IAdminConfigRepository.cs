using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.Domain.AdminModule.IRepository
{
    public interface IAdminConfigRepository : IRepository<AdminConfigEntity>
    {
        #region 验证数据
        /// <summary>
        /// 验证是否初始完成
        /// </summary>
        /// <returns></returns>
        Task<bool> ExistDataInitedAsync();
        /// <summary>
        ///  验证是否初始完成
        /// </summary>
        /// <returns></returns>
        bool ExistDataInited();
        #endregion
    }
}
