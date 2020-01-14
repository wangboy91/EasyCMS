using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Application.Service.AdminModule.IAppService;
using Wboy.Domain.AdminModule.IRepository;
using Wboy.Infrastructure.Core;

namespace Wboy.Application.Service.AdminModule.AppService
{
    public class LogAppService : ILogAppService
    {
        #region 基础属性
        private readonly ILoginLogRepository _loginLogRepository;
        private readonly IPageViewRepository _pageViewRepository;
        public LogAppService(ILoginLogRepository loginLogRepository,
            IPageViewRepository pageViewRepository)
        {
            _pageViewRepository = pageViewRepository;
            _loginLogRepository = loginLogRepository;
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取登录日志
        /// </summary>
        /// <param name="filters">过滤器</param>
        /// <returns></returns>
        public async Task<PagedList<LoginLogDto>> SearchLoginLogsAsync(LogFilters filters)
        {
            var page = await _loginLogRepository.GetPageList(filters.page-1, filters.rows, filters.keywords);
            return new PagedList<LoginLogDto>()
            {
                Rows = page.Rows.Select(x => new LoginLogDto(x)).ToList(),
                PageIndex = page.PageIndex,
                PageSize = page.PageSize,
                Records = page.Records
            };
        }

        /// <summary>
        /// 获取访问日志
        /// </summary>
        /// <param name="filters">过滤器</param>
        /// <returns></returns>
        public async Task<PagedList<VisitDto>> SearchVisitLogsAsync(LogFilters filters)
        {
            var page = await _pageViewRepository.GetPageList(filters.page-1, filters.rows, filters.keywords);
            return new PagedList<VisitDto>()
            {
                Rows = page.Rows.Select(x => new VisitDto(x)).ToList(),
                PageIndex = page.PageIndex,
                PageSize = page.PageSize,
                Records = page.Records
            };
        }
        #endregion
    }
}
