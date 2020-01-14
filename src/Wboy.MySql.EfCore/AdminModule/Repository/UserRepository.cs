using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Domain.AdminModule.IRepository;
using Wboy.Infrastructure.Core;
using Wboy.Infrastructure.Core.Extension;

namespace Wboy.MySql.EfCore.AdminModule.Repository
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(EfUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        private EfUnitOfWork CurrentUnitOfWork => UnitOfWork as EfUnitOfWork;
        DbSet<UserRoleEntity> _userRoleDbset;
        public DbSet<UserRoleEntity> UserRoleDbset => _userRoleDbset ?? (_userRoleDbset = CurrentUnitOfWork.Set<UserRoleEntity>());

        #region 获取数据
        /// <summary>
        /// 通过ids获取
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IQueryable<UserEntity> GetByIds(IEnumerable<Guid> ids)
        {
            return _dbSet.Where(x => ids.Contains(x.Id));
        }
        /// <summary>
        /// 异步获取通过登录名
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <returns></returns>
        public Task<UserEntity> GetAsyncByLoginName(string loginName)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.LoginName == loginName);
        }
        /// <summary>
        /// 异步获取用户角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public Task<UserRoleEntity> GetUserRoleAsync(Guid userId, Guid roleId)
        {
            return UserRoleDbset.FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);
        }
        /// <summary>
        /// 异步获取用户角色ids
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public async Task<IList<Guid>> GetUserRoleIdsAsync(Guid userId)
        {
            return await UserRoleDbset.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public async Task<IList<UserRoleEntity>> GetUserRolesAsyns(Guid userId)
        {
            return await UserRoleDbset.Where(x => x.UserId == userId).ToListAsync();
        }

        /// <summary>
        /// 异步获取用户分页列表
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页值</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public async Task<PagedList<UserEntity>> GetPageList(int pageIndex, int pageSize, string keyword)
        {
            var query = _dbSet.Where(x => !x.IsSuperMan);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.LoginName.Contains(keyword) || x.RealName.Contains(keyword));
            }
            return await query.OrderByDescending(x => x.AddTime)
                               .PagingAsync(pageIndex, pageSize);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="entity"></param>
        public void InsertUserRole(UserRoleEntity entity)
        {
            UserRoleDbset.Add(entity);
        }
        /// <summary>
        /// 批量添加用户角色
        /// </summary>
        /// <param name="entities"></param>
        public void InsertUserRoles(IList<UserRoleEntity> entities)
        {
            UserRoleDbset.AddRange(entities);
        }
        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveUserRole(UserRoleEntity entity)
        {
            UserRoleDbset.Remove(entity);
        }

        public void DeleteUserRoles(IList<UserRoleEntity> entities)
        {
            UserRoleDbset.RemoveRange(entities);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 异步判断用户是否存在角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<bool> AnyUserRoleAsync(Guid userId, Guid roleId)
        {
            return await UserRoleDbset.AnyAsync(x => x.UserId == userId && x.RoleId == roleId);
        }
        /// <summary>
        /// 检测是否存在用户名
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="loginName">登录名</param>
        /// <returns></returns>
        public async Task<bool> ExistsLoginNameAsync(Guid? userId, string loginName)
        {
            var query = _dbSet.Where(x => !x.IsDelete && x.LoginName == loginName);
            if (userId.HasGuidValue())
            {
                query.Where(x => x.Id == userId.Value);
            }
            return await query.AnyAsync();
        }
        #endregion
    }
}
