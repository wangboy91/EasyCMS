using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebApp.Extensions
{
    /// <summary>
    /// IIdentity扩展
    /// </summary>
    public static class IdentityExtention
    {
        /// <summary>
        /// 获取登录的用户ID
        /// </summary>
        /// <param name="identity">IIdentity</param>
        /// <returns></returns>
        public static UserAuthData GetLoginUser(this IIdentity identity)
        {
            var claim = (identity as ClaimsIdentity)?.FindFirst(UserAuthConstKey.AuthName);
            if (claim != null)
            {
                if (!string.IsNullOrEmpty(claim.Value))
                {
                    var json = Wboy.Infrastructure.Core.Security.DEncrypt.Decrypt(claim.Value);
                    var currentAuthData = Wboy.Infrastructure.Core.Serialized.JsonConvert.Deserialize<UserAuthData>(json);
                    return currentAuthData;
                }
            }
            return null;
        }
        public static IIdentity CreateLoginUser(Guid userId, string loginName, bool isSuperMan)
        {
            var apiAuthData = new UserAuthData()
            {
                UserId = userId,
                Name = loginName,
                IsSuperMan = isSuperMan
            };
            var json = Wboy.Infrastructure.Core.Serialized.JsonConvert.SerializeV2(apiAuthData);
            var token = Wboy.Infrastructure.Core.Security.DEncrypt.Encrypt(json);
            var authenType = CookieAuthenticationDefaults.AuthenticationScheme;
            var identity = new ClaimsIdentity(authenType);
            identity.AddClaim(new Claim(ClaimTypes.Name, loginName));
            identity.AddClaim(new Claim(UserAuthConstKey.AuthName, token));
            return identity;
        }

    }
    public class UserAuthData
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { set; get; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSuperMan { get; set; }


    }
    /// <summary>
    /// 客户端静态键
    /// </summary>
    public static class UserAuthConstKey
    {

        public static readonly string AuthName = "AUTH-WANG-TOKEN";
        //public static readonly string CookieDomain = ".sumxiang.com";
        //public static readonly string CookieDomain = "";
    }
}
