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
    /// <summary>
    /// 系统配置
    /// </summary>
    [Area("Admin")]
    public class SystemSettingController : BaseController
    {
        #region 基础属性
        private readonly ISystemSettingAppService _systemSettingAppService;
        public SystemSettingController(ISystemSettingAppService systemSettingAppService)
        {
            _systemSettingAppService = systemSettingAppService;
        }
        #endregion

        #region 视图功能
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> GetPageList(BaseFilter filters)
        {
            var result = await _systemSettingAppService.SearchAsync(filters);
            return Json(result.ToJqGridRows());
        }
        [IgnoreRightFilter]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _systemSettingAppService.FindAsync(id);
            return Json(result);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [IgnoreRightFilter]
        public async Task<IActionResult> SaveForm(Guid id, string value, string description)
        {
            try
            {
                var result = await _systemSettingAppService.UpdateAsync(id, value, description);
                return JsonSuccessResult("保存成功");
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }
            
        }
        #endregion
    }
}