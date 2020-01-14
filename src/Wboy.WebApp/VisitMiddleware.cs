using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Service.AdminModule.IAppService;
using Wboy.Infrastructure.Core;
using WebApp.Extensions;

namespace WebApp
{
    public class VisitMiddleware
    {
        readonly RequestDelegate _next;

        public VisitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //var authData = context.Request.Headers[WebAuthConstKey.AuthName];
            //if (!string.IsNullOrEmpty(authData))
            //{
            //    try
            //    {
            //        var base64 = Wboy.Infrastructure.Core.Security.DEncrypt.Base64Decrypt(authData);
            //        var json = Wboy.Infrastructure.Core.Security.DEncrypt.Decrypt(base64);
            //        var currentAuthData = Wboy.Infrastructure.Core.Serialized.JsonConvert.Deserialize<WebAuthData>(json);
            //        context.Items[WebAuthConstKey.AuthName] = currentAuthData;

            //    }
            //    catch (Exception ex)
            //    {
            //        Wboy.Infrastructure.Core.Logging.LoggerFactory.CreateLoger().Error("AuthenticationHandlerError", ex);
            //        // ignored
            //    }
            //}
            //进入下一个中间件过程
            await _next(context);

            #region 记录记录访问记录

            //访问记与操作日志都在这里添加

            try
            {
                var userService = SampleContext.Current.Resolve<IUserAppService>();

                var connection = context.Features.Get<IHttpConnectionFeature>();
                var user = context.User;
                var isLogined = user?.Identity != null && user.Identity.IsAuthenticated;
                var visit = new VisitDto
                {
                    Ip = connection.RemoteIpAddress.ToString(),
                    LoginName = isLogined ? user.Identity.Name : string.Empty,
                    Url = context.Request.Path,
                    UserId = isLogined ? user.Identity.GetLoginUser().UserId : Guid.Empty,
                };
                await userService.VisitAsync(visit);
            }
            catch (Exception ex)
            {
                throw new NeedToShowFrontException(ex.Message);
                //eat exception
            }

            #endregion
        }
    }
}
