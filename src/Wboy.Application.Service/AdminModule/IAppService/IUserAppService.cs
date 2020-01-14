using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Infrastructure.Core;

namespace Wboy.Application.Service.AdminModule.IAppService
{
    /// <summary>
    /// 用户契约
    /// </summary>
    public interface IUserAppService
    {

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dto">用户模型</param>
        /// <returns></returns>
        Task<Guid> AddAsync(UserAddDto dto);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="dto">用户模型</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UserAddDto dto);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPwd"></param>
        /// <param name="confirmPwd"></param>
        /// <returns></returns>
        Task<bool> ChangePwd(Guid id, string oldPwd, string confirmPwd);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<UserDto> FindAsync(Guid id);

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns></returns>
        Task<UserLoginDto> LoginAsync(LoginDto dto);
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> UpdateStatusAsync(Guid id, bool isDelete);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> ResetPwd(Guid id, string password);

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        Task<bool> GiveAsync(Guid userId, Guid roleId);

        /// <summary>
        /// 用户角色取消
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        Task<bool> CancelAsync(Guid userId, Guid roleId);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        Task<PagedList<UserDto>> SearchAsync(UserFilters filters);

        /// <summary>
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        Task<bool> HasRightAsync(Guid userId, string url);

        /// <summary>
        /// 记录访问记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> VisitAsync(VisitDto dto);

        /// <summary>
        /// 检测是否存在用户名
        /// </summary>
        /// <param name="userId">用户ID，可以为空</param>
        /// <param name="loginName">用户名</param>
        /// <returns></returns>
        Task<bool> ExistsLoginNameAsync(Guid? userId, string loginName);
        /// <summary>
        /// 获取用户角色tree
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task<string> GetUserRoleTreeList(Guid userId);
        /// <summary>
        /// 用户角色添加
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="roleIds">角色ids</param>
        /// <returns></returns>
        Task<bool> GiveAsync(Guid userId, IList<Guid> roleIds);
    }
}
