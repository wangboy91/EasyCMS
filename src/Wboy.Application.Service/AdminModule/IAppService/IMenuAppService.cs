﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Infrastructure.Core;

namespace Wboy.Application.Service.AdminModule.IAppService
{
    /// <summary>
    /// 菜单契约
    /// </summary>
    public interface IMenuAppService
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        Task<bool> AddAsync(MenuDto dto);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(MenuDto dto);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<MenuDto> FindAsync(Guid id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        Task<PagedList<MenuDto>> SearchAsync(MenuFilters filters);

        /// <summary>
        /// 获取用户拥有的权限菜单（不包含按钮）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<List<MenuDto>> GetMyMenusAsync(Guid userId);

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        Task<List<TreeDto>> GetTreesAsync();

        /// <summary>
        /// 通过角色ID获取拥有的菜单权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        Task<List<MenuDto>> GetMenusByRoleIdAsync(Guid roleId);
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        string GetTreeListJson(string keyword);
        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
       Task<bool> DeleteAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<MenuDto>> GetAllMenus();
    }
}
