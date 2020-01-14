namespace Wboy.Application.Dto.AdminModule.Dto
{
    /// <summary>
    /// 登录DTO
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// 登陆成功后跳转的地址
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string LoginIP { get; set; }
    }
}
