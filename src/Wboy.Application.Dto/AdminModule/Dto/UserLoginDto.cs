
namespace Wboy.Application.Dto.AdminModule.Dto
{
    /// <summary>
    /// 登陆返回数据
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// 是否登陆成功
        /// </summary>
        public bool LoginSuccess { get; set; }
        /// <summary>
        /// 登陆结果
        /// </summary>
        public LoginResult Result { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回的登陆用户数据
        /// </summary>
        public UserDto User { get; set; }
    }
    /// <summary>
    /// 登陆结果
    /// </summary>
    public enum LoginResult
    {
        /// <summary>
        /// 账号不存在
        /// </summary>
        AccountNotExists = 1,

        /// <summary>
        /// 登陆密码错误
        /// </summary>
        WrongPassword = 2,

        /// <summary>
        /// 登陆成功
        /// </summary>
        Success = 3
    }
}
