using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Wboy.Application.Service.AdminModule.IAppService;
using Wboy.Infrastructure.Core;

namespace WebApp.Extensions
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class RightFilter : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isIgnored = context.ActionDescriptor.FilterDescriptors.Any(f => f.Filter is IgnoreRightFilter);

            if (isIgnored) return;


            var httpContext = context.HttpContext;
            //var identity = httpContext.User.Identity;
            var curLoginUser= httpContext.User.Identity.GetLoginUser();
            if (curLoginUser.IsSuperMan) //超级管理员
                return;
            var routeData = context.RouteData.Values;
            var area = routeData["area"];
            var controller = routeData["controller"];
            var action = routeData["action"];
            var url =area==null ? $"/{controller}/{action}" : $"/{area}/{controller}/{action}";
            var userService = SampleContext.Current.Resolve<IUserAppService>();
            var hasRight = userService.HasRightAsync(curLoginUser.UserId, url).Result;
            if (hasRight) //有访问资源的权限
                return;

            var isAjax = httpContext.Request.Headers["X-Requested-With"].ToString()
                .Equals("XMLHttpRequest", StringComparison.CurrentCultureIgnoreCase);

            IActionResult result;
            if (isAjax)
            {
                //var msgobj = new ResultMessage(StateCode.Fail, "您没有权限！访问相应资源");
                //var msg = Infrastructure.Core.Serialized.JsonConvert.SerializeV2(msgobj, true);
                result = new ContentResult
                {
                    Content = "您没有权限！访问相应资源",
                    ContentType = "application/json; charset=utf-8",
                    StatusCode = 200,
                };
            }
            else
            {
                result = new ViewResult
                {
                    ViewName = "~/Views/Shared/NoRight.cshtml",
                };
            }
            context.Result = result;
        }
    }
}