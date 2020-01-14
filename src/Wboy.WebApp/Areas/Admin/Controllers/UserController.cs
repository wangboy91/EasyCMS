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
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Form()
        {
            return View();
        }

        public IActionResult ResetForm()
        {
            return View();
        }
        public IActionResult ChangePwd(Guid id)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult SetUserRoleForm()
        {
            return View();
        }

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> GetPageList(UserFilters filters)
        {
            var result = await _userAppService.SearchAsync(filters);
            return Json(result.ToJqGridRows());
        }
        /// <summary>
        /// 获取用户表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userAppService.FindAsync(id);
            return Json(result);
        }
        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> UserRoleTreeList(Guid userId)
        {
            var result = await _userAppService.GetUserRoleTreeList(userId);
            return Content(result);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [IgnoreRightFilter]
        public async Task<IActionResult> ChangePwdForm(Guid id,string oldPwd,string pwd,string confirmPwd)
        {
            try
            {
                if (id.HasGuidValue())
                {
                    if(pwd!=confirmPwd)
                        return JsonErrorResult("两次密码不一致");
                    await _userAppService.ChangePwd(id, oldPwd, confirmPwd);
                    return JsonSuccessResult("修改成功");
                }
                else
                {
                    return JsonErrorResult("请登录");
                }
                
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }
        }
        [HttpPost]
        [IgnoreRightFilter]
        public async Task<IActionResult> SaveForm(UserAddDto dto)
        {
            try
            {
                if (dto.Id.HasGuidValue())
                {
                    await _userAppService.UpdateAsync(dto);
                }
                else
                {
                    await _userAppService.AddAsync(dto);
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
                await _userAppService.DeleteAsync(id);
                return JsonSuccessResult("删除成功");
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid id, bool status)
        {
            try
            {
                await _userAppService.UpdateStatusAsync(id, status);
                return JsonSuccessResult("用户状态修改成功");
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }
        }
        [HttpPost]
        [IgnoreRightFilter]
        public async Task<IActionResult> ResetPwd(Guid id, string password)
        {
            try
            {
                await _userAppService.ResetPwd(id, password);
                return JsonSuccessResult("用户密码重置成功");
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SetUserRoleForm(Guid userId, IList<Guid> roleIds)
        {
            try
            {
                await _userAppService.GiveAsync(userId, roleIds);
                return JsonSuccessResult("用户设置角色成功");
            }
            catch (Exception ex)
            {
                return JsonErrorResult(ex.Message);
            }
        }
        #endregion
    }
}