using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Application.Service.AdminModule.IAppService;
using Wboy.Domain.PlatformModule.IRepository;
using Wboy.Infrastructure.Core;

namespace Wboy.Application.Service.AdminModule.AppService
{
    public class SystemSettingAppService : ISystemSettingAppService
    {
        #region 基础属性
        private readonly ISystemSettingRepository _systemSettingRepository;

        public SystemSettingAppService(ISystemSettingRepository systemSettingRepository)
        {
            _systemSettingRepository = systemSettingRepository;
        }
        #endregion
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<SystemSettingDto>> SearchAsync(BaseFilter filters)
        {
            var page = await _systemSettingRepository.GetPageList(filters.page - 1, filters.rows, filters.keywords);
            return new PagedList<SystemSettingDto>()
            {
                Rows = page.Rows.Select(x => new SystemSettingDto(x)).ToList(),
                PageIndex = page.PageIndex,
                PageSize = page.PageSize,
                Records = page.Records
            };
        }

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<SystemSettingDto> FindAsync(Guid id)
        {
            var entity = await _systemSettingRepository.FindAsync(id);
            var dto = new SystemSettingDto(entity);
            return dto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SystemSettingDto FindByKey(string key)
        {
            var entity = _systemSettingRepository.Get(key);
            return new SystemSettingDto(entity);
        }
        #endregion

        #region 操作数据
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid id,string value, string description)
        {
            var entity = _systemSettingRepository.Get(id);
            if (entity == null)
                throw new NeedToShowFrontException("配置不存在");
            entity.Description = description;
            entity.Value = value;
            return await _systemSettingRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 修改兑换
        /// </summary>
        /// <param name="isOpen"></param>
        /// <returns></returns>
        public async Task<bool> UpdateExchange(bool isOpen)
        {
            var entity = _systemSettingRepository.Get("SettingBoxPush.IsOpenExchange");
            if(entity == null)
                throw new NeedToShowFrontException("配置不存在");
            entity.Value = isOpen ? "true" : "false";
            return await _systemSettingRepository.UnitOfWork.CommitAsync() > 0;
        }
        #endregion
    }
}
