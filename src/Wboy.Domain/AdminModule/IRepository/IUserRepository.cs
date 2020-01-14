using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Infrastructure.Core;

namespace Wboy.Domain.AdminModule.IRepository
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        #region 获取数据
        /// <summary>
        /// 通过ids获取
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IQueryable<UserEntity> GetByIds(IEnumerable<Guid> ids);

        /// <summary>
        /// 异步获取通过登录名
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <returns></returns>
        Task<UserEntity> GetAsyncByLoginName(string loginName);
        /// <summary>
        /// 异步获取用户角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<UserRoleEntity> GetUserRoleAsync(Guid userId, Guid roleId);
        /// <summary>
        /// 异步获取用户角色ids
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task<IList<Guid>> GetUserRoleIdsAsync(Guid userId);
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task<IList<UserRoleEntity>> GetUserRolesAsyns(Guid userId);
        /// <summary>
        /// 异步获取用户分页列表
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页值</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        Task<PagedList<UserEntity>> GetPageList(int pageIndex, int pageSize, string keyword);
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="entity"></param>
        void InsertUserRole(UserRoleEntity entity);
        /// <summary>
        /// 批量添加用户角色
        /// </summary>
        /// <param name="entities"></param>
        void InsertUserRoles(IList<UserRoleEntity> entities);
        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="entity"></param>
        void RemoveUserRole(UserRoleEntity entity);
        void DeleteUserRoles(IList<UserRoleEntity> entities);
        #endregion

        #region 验证数据
        /// <summary>
        /// 异步判断用户是否存在角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<bool> AnyUserRoleAsync(Guid userId, Guid roleId);
        /// <summary>
        /// 检测是否存在用户名
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="loginName">登录名</param>
        /// <returns></returns>
        Task<bool> ExistsLoginNameAsync(Guid? userId, string loginName);
        #endregion
    }
}
