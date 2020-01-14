using System.Threading.Tasks;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Infrastructure.Core;


namespace Wboy.Application.Service.AdminModule.IAppService
{
    /// <summary>
    /// 日志契约
    /// </summary>
    public interface ILogAppService
    {
        /// <summary>
        /// 获取登录日志
        /// </summary>
        /// <param name="filters">过滤器</param>
        /// <returns></returns>
        Task<PagedList<LoginLogDto>> SearchLoginLogsAsync(LogFilters filters);

        /// <summary>
        /// 获取访问日志
        /// </summary>
        /// <param name="filters">过滤器</param>
        /// <returns></returns>
        Task<PagedList<VisitDto>> SearchVisitLogsAsync(LogFilters filters);
    }
}
