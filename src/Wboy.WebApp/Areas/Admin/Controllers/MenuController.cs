using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Application.Service.AdminModule.IAppService;
using Wboy.Infrastructure.Core.Extension;
using WebApp.Extensions;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : BaseController
    {
        private readonly IMenuAppService _menuAppService;
        public MenuController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

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
        public async Task<IActionResult> GetPageList(MenuFilters filters)
        {
            var result = await _menuAppService.SearchAsync(filters);
            return Json(result.ToJqGridRows());
        }
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public IActionResult GetTreeList(string keyword)
        {
            var result = _menuAppService.GetTreeListJson(keyword);
            return Content(result);
        }
        /// <summary>
        /// 获取单个菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _menuAppService.FindAsync(id);
            return Json(result);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [IgnoreRightFilter]
        public async Task<IActionResult> SaveForm(MenuDto dto)
        {
            try
            {
                if (dto.Id.HasGuidValue())
                {
                    await _menuAppService.UpdateAsync(dto);
                }
                else
                {
                    await _menuAppService.AddAsync(dto);
                }
                return JsonSuccessResult("保存成功");
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> RemoveForm(Guid id)
        {
            try
            {
                await _menuAppService.DeleteAsync(id);
                return JsonSuccessResult("删除成功");
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }
        }
        #endregion
    }
}