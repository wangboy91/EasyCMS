using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Application.Service.AdminModule.IAppService;
using WebApp.Extensions;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SystemLogController : BaseController
    {
        private readonly ILogAppService _logService;

        public SystemLogController(ILogAppService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <returns></returns>
        public IActionResult Logins()
        {
            return View();
        }

        /// <summary>
        /// 访问记录
        /// </summary>
        /// <returns></returns>
        public IActionResult Visits()
        {
            return View();
        }

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> LoginsList(LogFilters filters)
        {
            var result = await _logService.SearchLoginLogsAsync(filters);
            return Json(result.ToJqGridRows());
        }

        /// <summary>
        /// 访问记录
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> VisitsList(LogFilters filters)
        {
            var result = await _logService.SearchVisitLogsAsync(filters);
            return Json(result.ToJqGridRows());
        }
    }
}