using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wboy.Infrastructure.Core.Util.WebControl;
using WebApp.Extensions;

namespace WebApp
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 序列化json 返回自定义的错误状态对象
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected internal virtual ContentResult JsonErrorResult(string message)
        {
            var jsonStr = Wboy.Infrastructure.Core.Serialized.JsonConvert.SerializeV2(new ResultMessage(StateCode.Fail, message), true);
            return Content(jsonStr);
        }
        /// <summary>
        /// 序列化json 返回自定义的成功状态对象
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected internal virtual ContentResult JsonSuccessResult(string message)
        {
            var jsonStr = Wboy.Infrastructure.Core.Serialized.JsonConvert.SerializeV2(new ResultMessage(StateCode.Success, message), true);
            return Content(jsonStr);
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult ToJsonResult(object data)
        {
            var jsonStr = Wboy.Infrastructure.Core.Serialized.JsonConvert.SerializeV2(data, true);
            return Content(jsonStr);
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual JsonResult Success(string message)
        {
            return Json(new AjaxResult { Type = ResultType.Success, Message = message });
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual JsonResult Success(string message, object data)
        {
            return Json(new AjaxResult { Type = ResultType.Success, Message = message, Resultdata = data });
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual JsonResult Error(string message)
        {
            return Json(new AjaxResult { Type = ResultType.Error, Message = message });
        }
    }
}
