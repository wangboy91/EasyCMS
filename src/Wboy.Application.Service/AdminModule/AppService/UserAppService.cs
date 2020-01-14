using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Application.Service.AdminModule.IAppService;
using Wboy.Domain;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Domain.AdminModule.IRepository;
using Wboy.Infrastructure.Core;
using Wboy.Infrastructure.Core.Extension;
using Wboy.Infrastructure.Core.Tree;

namespace Wboy.Application.Service.AdminModule.AppService
{
    public class UserAppService : IUserAppService
    {
        #region 基础属性
        private readonly IUserRepository _userRepository;
        private readonly ILoginLogRepository _loginLogRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IPageViewRepository _pageViewRepository;

        public UserAppService(IUserRepository userRepository,
            ILoginLogRepository loginLogRepository,
            IRoleRepository roleRepository,
            IMenuRepository menuRepository,
            IPageViewRepository pageViewRepository)
        {
            _userRepository = userRepository;
            _loginLogRepository = loginLogRepository;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _pageViewRepository = pageViewRepository;
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<UserDto> FindAsync(Guid id)
        {
            var entity = await _userRepository.FindAsync(id);
            var dto = new UserDto(entity);
            return dto;
        }
        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        public async Task<PagedList<UserDto>> SearchAsync(UserFilters filters)
        {
            if (filters == null)
                return new PagedList<UserDto>(0, 0);
            var page = await _userRepository.GetPageList(filters.page-1, filters.rows, filters.keywords);
            return new PagedList<UserDto>()
            {
                Rows = page.Rows.Select(x => new UserDto(x)).ToList(),
                PageIndex = page.PageIndex,
                PageSize = page.PageSize,
                Records = page.Records
            };
        }
        /// <summary>
        /// 获取用户角色tree
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public async Task<string> GetUserRoleTreeList(Guid userId)
        {
            var userRoleIds = await _userRepository.GetUserRoleIdsAsync(userId);
            var roles = await _roleRepository.GetAllAsync();
            var treeList = roles.Select(x => new TreeModel()
            {
                id = x.Id.ToString(),
                text = x.Name,
                value = x.Id.ToString(),
                isexpand = true,
                complete = true,
                showcheck = true,
                checkstate = userRoleIds.Count(t => t == x.Id),
                hasChildren = false,
                parentId = "",
                img = "fa fa-group"
            }).ToList();
            return treeList.TreeToJson();
        }
        #endregion

        #region 操作数据
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(UserAddDto dto)
        {
            var entity = new UserEntity()
            {
                Id = Domain.NewId.NewSecuentialGuid(),
                LoginName = dto.LoginName,
                RealName = dto.RealName,
                Email = dto.Email,
                Password = dto.Password.MD5Encryption(),
                AddTime = DateTime.Now,
                IsDelete = false
            };
            if (entity.UserRoles.AnyOne())
            {
                entity.UserRoles.ForEach(r => r.UserId = entity.Id);
            }
            _userRepository.Insert(entity);
            return await _userRepository.UnitOfWork.CommitAsync() > 0 ? entity.Id : Guid.Empty;
        }
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(UserAddDto dto)
        {
            var entity = _userRepository.Get(dto.Id);
            if (entity == null)
                throw new NeedToShowFrontException("用户不存在");
            // entity.LoginName = dto.LoginName;
            entity.RealName = dto.RealName;
            entity.Email = dto.Email;
            return await _userRepository.UnitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> ChangePwd(Guid id, string oldPwd, string confirmPwd)
        {
            var entity = _userRepository.Get(id);
            if (entity == null)
                throw new NeedToShowFrontException("用户不存在");
            if (entity.Password != oldPwd.MD5Encryption())
            {
                throw new NeedToShowFrontException("原密码不正确");
            }
            entity.Password = confirmPwd.MD5Encryption();
            return await _userRepository.UnitOfWork.CommitAsync() > 0;
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdateStatusAsync(Guid id, bool isDelete)
        {
            var entity = _userRepository.Get(id);
            if (entity == null)
                throw new NeedToShowFrontException("用户不存在");
            entity.IsDelete = isDelete;
            return await _userRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = _userRepository.Get(id);
            if (entity == null)
                throw new NeedToShowFrontException("用户不存在");
            _userRepository.Delete(entity);
            return await _userRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ResetPwd(Guid id, string password)
        {
            var entity = _userRepository.Get(id);
            if (entity == null)
                throw new NeedToShowFrontException("用户不存在");
            entity.Password = password.MD5Encryption();
            return await _userRepository.UnitOfWork.CommitAsync() > 0;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<UserLoginDto> LoginAsync(LoginDto dto)
        {
            var result = new UserLoginDto();
            var entity = await _userRepository.GetAsyncByLoginName(dto.LoginName.Trim());

            if (entity == null)
            {
                result.Message = "账户不存在";
                result.Result = LoginResult.AccountNotExists;
            }
            else
            {
                if (entity.Password == dto.Password.MD5Encryption())
                {
                    result.LoginSuccess = true;
                    result.Message = "登录成功";
                    result.Result = LoginResult.Success;
                    result.User = new UserDto(entity);
                    var loginLog = new LoginLogEntity
                    {
                        Id = Guid.NewGuid(),
                        LoginName = dto.LoginName,
                        IP = dto.LoginIP,
                        AddTime = DateTime.Now,
                        UserId = entity.Id,
                        Message = result.Message
                    };
                    _loginLogRepository.Insert(loginLog);
                    await _loginLogRepository.UnitOfWork.CommitAsync();
                }
                else
                {
                    result.Message = "密码错误";
                    result.Result = LoginResult.WrongPassword;
                }
            }
            return result;
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<bool> GiveAsync(Guid userId, Guid roleId)
        {
            if (await _userRepository.AnyUserRoleAsync(userId, roleId))
                return true;
            _userRepository.InsertUserRole(new UserRoleEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                RoleId = roleId
            });
            return await _userRepository.UnitOfWork.CommitAsync() > 0;
        }

        /// <summary>
        /// 用户角色取消
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<bool> CancelAsync(Guid userId, Guid roleId)
        {
            var userRole = await _userRepository.GetUserRoleAsync(userId, roleId);
            if (userRole == null)
                return true;
            _userRepository.RemoveUserRole(userRole);
            return await _userRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 记录访问记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> VisitAsync(VisitDto dto)
        {
            var entity = new PageViewEntity()
            {
                Id = dto.Id,
                UserId = dto.UserId,
                IP = dto.Ip,
                LoginName = dto.LoginName,
                Url = dto.Url,
                AddTime = DateTime.Now
            };
            _pageViewRepository.Insert(entity);
            return await _pageViewRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 用户角色添加
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="roleIds">角色ids</param>
        /// <returns></returns>
        public async Task<bool> GiveAsync(Guid userId, IList<Guid> roleIds)
        {
            var olds = await _userRepository.GetUserRolesAsyns(userId);
            var oldIds = olds.Select(x => x.RoleId);
            var adds = roleIds.Where(x => !oldIds.Contains(x)).ToList();
            var removes = olds.Where(x => !roleIds.Contains(x.RoleId)).ToList();
            if (adds.AnyOne())
            {
                var entities = adds.Select(x =>
                                        new UserRoleEntity()
                                        {
                                            Id = Domain.NewId.NewSecuentialGuid(),
                                            UserId = userId,
                                            RoleId = x,
                                            AddTime = DateTime.Now
                                        })
                                    .ToList();
                _userRepository.InsertUserRoles(entities);
            }
            if (removes.AnyOne())
            {
                _userRepository.DeleteUserRoles(removes);
            }
            return await _userRepository.UnitOfWork.CommitAsync() > 0;
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public async Task<bool> HasRightAsync(Guid userId, string url)
        {
            var roleIds = await _userRepository.GetUserRoleIdsAsync(userId);
            var menuIds = await _roleRepository.GetMenuIdsByRoleIds(roleIds);
            return await _menuRepository.ExistByIdsAndUrl(menuIds, url);
        }

        /// <summary>
        /// 检测是否存在用户名
        /// </summary>
        /// <param name="userId">用户ID，可以为空</param>
        /// <param name="loginName">用户名</param>
        /// <returns></returns>
        public async Task<bool> ExistsLoginNameAsync(Guid? userId, string loginName)
        {
            return await _userRepository.ExistsLoginNameAsync(userId, loginName);
        }
        #endregion
    }
}
