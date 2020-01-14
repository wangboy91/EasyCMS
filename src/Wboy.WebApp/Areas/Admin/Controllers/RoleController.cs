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
    public class RoleController : BaseController
    {
        private readonly IRoleAppService _roleAppService;

        public RoleController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        #region 功能视图
        /// <summary>
        /// 角色管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 角色表单
        /// </summary>
        /// <returns></returns>
        public IActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 角色菜单权限
        /// </summary>
        /// <returns></returns>
        public IActionResult AuthenRoleMenuForm()
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
        public async Task<IActionResult> GetPageList(RoleFilters filters)
        {
            var result = await _roleAppService.SearchAsync(filters);
            return Json(result.ToJqGridRows());
        }
        /// <summary>
        /// 获取表格数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _roleAppService.FindAsync(id);
            return Json(result);
        }
        /// <summary>
        /// 获取角色的菜单树
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> AuthenRoleMenuTreeList(Guid roleId)
        {
            var result = await _roleAppService.AuthenRoleMenuTreeListAsync(roleId);
            return Content(result);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreRightFilter]
        public async Task<IActionResult> SaveForm(RoleDto dto)
        {
            try
            {
                if (dto.Id.HasGuidValue())
                {
                    await _roleAppService.UpdateAsync(dto);
                }
                else
                {
                    await _roleAppService.AddAsync(dto);
                }
                return JsonSuccessResult("保存成功");
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }
        }
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveForm(Guid id)
        {
            try
            {
                await _roleAppService.DeleteAsync(id);
                return JsonSuccessResult("删除成功");
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }
        }
        /// <summary>
        /// 设置角色菜单
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="menuIds">菜单ids</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AuthenRoleMenuForm(Guid roleId, IList<Guid> menuIds)
        {
            try
            {
                await _roleAppService.SetRoleMenusAsync(roleId, menuIds);
                return JsonSuccessResult("授权成功");
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }
        }
        #endregion
    }
}