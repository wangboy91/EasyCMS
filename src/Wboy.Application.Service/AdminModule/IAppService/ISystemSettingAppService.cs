using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Infrastructure.Core;

namespace Wboy.Application.Service.AdminModule.IAppService
{
    public interface ISystemSettingAppService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        Task<PagedList<SystemSettingDto>> SearchAsync(BaseFilter filters);
        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<SystemSettingDto> FindAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        SystemSettingDto FindByKey(string key);
        #endregion

        #region 操作数据
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, string value, string description);
        /// <summary>
        /// 修改兑换
        /// </summary>
        /// <param name="isOpen"></param>
        /// <returns></returns>
       Task<bool> UpdateExchange(bool isOpen);
        #endregion
    }
}
