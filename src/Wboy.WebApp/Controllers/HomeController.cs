using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Service.AdminModule.IAppService;
using Wboy.Infrastructure.Core.Extension;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Controllers
{
    [IgnoreRightFilter]
    public class HomeController : BaseController
    {
        private readonly IUserAppService _userAppService;
        private readonly IMenuAppService _menuAppService;

        public HomeController(IUserAppService userAppService, 
            IMenuAppService menuAppService)
        {
            _userAppService = userAppService;
            _menuAppService = menuAppService;
        }

        #region 视图功能
        /// <summary>
        /// 默认页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Front()
        {
            return View();
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var curUser = User.Identity.GetLoginUser();

            if (curUser.IsSuperMan)
            {
                var myMenus = await _menuAppService.GetAllMenus();
                ViewBag.Menus = myMenus;
            }
            else
            {
                var myMenus = await _menuAppService.GetMyMenusAsync(curUser.UserId);
                ViewBag.Menus = myMenus;
            }
            var user = await _userAppService.FindAsync(curUser.UserId);
            ViewBag.Name = user.RealName;
            ViewBag.Account = user.LoginName;
            ViewBag.id = user.Id;
            return View();
        }
        /// <summary>
        /// 默认页
        /// </summary>
        /// <returns></returns>
        public IActionResult Default()
        {
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginDto
            {
                ReturnUrl = returnUrl,
                //LoginName = "superadmin",
                //Password = "123456"
            };
            return View(model);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto model)
        {
            model.ReturnUrl = "/home/index";
            //if (!ModelState.IsValid)
            //    return View(model);
            var connection = Request.HttpContext.Features.Get<IHttpConnectionFeature>();
            //var localIpAddress = connection.LocalIpAddress;    //本地IP地址
            //var localPort = connection.LocalPort;              //本地IP端口
            var remoteIpAddress = connection.RemoteIpAddress;  //远程IP地址
            //var remotePort = connection.RemotePort;            //本地IP端口
            model.LoginIP = remoteIpAddress.ToString();
            model.LoginIP = remoteIpAddress.ToString();
            var loginDto = await _userAppService.LoginAsync(model);
            if (loginDto.LoginSuccess)
            {
                var identity = IdentityExtention.CreateLoginUser(loginDto.User.Id, loginDto.User.LoginName, loginDto.User.IsSuperMan);
                var properties = new AuthenticationProperties() { IsPersistent = true };
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
                model.ReturnUrl = model.ReturnUrl.IsNotBlank() ? model.ReturnUrl : "/";
                return Success("登录成功",model.ReturnUrl);
            }
            return Error(loginDto.Message);
            //ModelState.AddModelError(loginDto.Result == LoginResult.AccountNotExists ? "LoginName" : "Password",
            //    loginDto.Message);
            //return View(model);
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        #endregion
        /// <summary>
        /// 错误页面
        /// </summary>
        /// <returns></returns>
        [IgnoreRightFilter]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var statusCode = HttpContext.Response.StatusCode;
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;
            if (statusCode >= 500)
            {
                Wboy.Infrastructure.Core.Logging.LoggerFactory.CreateLoger().Error("server Error", error);
            }
            var model = new CustomErrorModel()
            {
                StatusCode = statusCode,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = error?.Message
            };
            switch (model.StatusCode)
            {
                case 404:
                    model.Title = "页面未找到！";
                    break;
                case 500:
                    model.Title = "服务器内部错误";
                    break;

            }
            return View(model);
        }
    }
}