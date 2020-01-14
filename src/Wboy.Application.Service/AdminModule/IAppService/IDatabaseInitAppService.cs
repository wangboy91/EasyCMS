using System.Threading.Tasks;

namespace Wboy.Application.Service.AdminModule.IAppService
{
    /// <summary>
    /// 数据库初始化契约
    /// </summary>
    public interface IDatabaseInitAppService
    {
        /// <summary>
        /// 初始化数据库数据
        /// </summary>
        Task<bool> InitAsync();

        /// <summary>
        /// 初始化路径码
        /// </summary>
        Task<bool> InitPathCodeAsync();
    }
}
