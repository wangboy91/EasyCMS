
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Infrastructure.Core;

namespace Wboy.Application.Service.AdminModule.IAppService
{
    /// <summary>
    /// 角色契约
    /// </summary>
    public interface IRoleAppService
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto">角色模型</param>
        /// <returns></returns>
        Task<Guid> AddAsync(RoleDto dto);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="dto">角色模型</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(RoleDto dto);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<RoleDto> FindAsync(Guid id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<Guid> ids);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        Task<PagedList<RoleDto>> SearchAsync(RoleFilters filters);

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <returns></returns>
        Task<List<TreeDto>> GetTreesAsync();

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <returns></returns>
        Task<bool> SetRoleMenusAsync(List<RoleMenuDto> datas);
        /// <summary>
        /// 获取角色的菜单树
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<string> AuthenRoleMenuTreeListAsync(Guid roleId);

        /// <summary>
        /// 清空该角色下的所有权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        Task<bool> ClearRoleMenusAsync(Guid roleId);
        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="menuIds">菜单ids</param>
        /// <returns></returns>
        Task<bool> SetRoleMenusAsync(Guid roleId, IList<Guid> menuIds);
    }
}
