using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wboy.Application.Service.AdminModule.IAppService;
using Wboy.Domain;
using Wboy.Domain.AdminModule;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Domain.AdminModule.IRepository;
using Wboy.Infrastructure.Core.Extension;

namespace Wboy.Application.Service.AdminModule.AppService
{
    public class DatabaseInitAppService : IDatabaseInitAppService
    {
        private readonly IAdminConfigRepository _adminConfigRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPathCodeRepository _pathCodeRepository;

        public DateTime Now => new DateTime(2018, 02, 10, 0, 0, 0);
        public DatabaseInitAppService(IAdminConfigRepository adminConfigRepository,
            IUserRepository userRepository,
            IMenuRepository menuRepository,
            IRoleRepository roleRepository,
            IPathCodeRepository pathCodeRepository)
        {
            _adminConfigRepository = adminConfigRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _pathCodeRepository = pathCodeRepository;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public async Task<bool> InitAsync()
        {
            try
            {
                if (_adminConfigRepository.ExistDataInited())
                    return false;
                #region 用户
                var admin = new UserEntity
                {
                    Id = Guid.Parse("f5cf28ca-d5c4-c12f-804f-08d614d69906"),
                    LoginName = "superadmin",
                    RealName = "超级管理员",
                    Password = "admin".MD5Encryption(),
                    Email = "wangboy91@qq.com",
                    IsSuperMan = true,
                    AddTime = Now
                };
                var guest = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    LoginName = "guest",
                    RealName = "游客",
                    Password = "123456".MD5Encryption(),
                    Email = "wangboy91@qq.com",
                    AddTime = Now
                };
                //用户
                var user = new List<UserEntity>
                {
                    admin,
                    guest
                };
                #endregion

                #region 菜单

                var system = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "系统设置",
                    Url = "#",
                    AddTime = Now,
                    Order = 1,
                    Code = "AA",
                    PathCode = "AA",
                    Type = MenuType.Module
                };//1
                var menuMgr = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = system.Id,
                    Name = "菜单管理",
                    Url = "/Admin/Menu/Index",
                    AddTime = Now,
                    Order = 2,
                    Code = "AA",
                    PathCode = "AAAA",
                    Type =MenuType.Menu
                };//2
                var roleMgr = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = system.Id,
                    Name = "角色管理",
                    Url = "/Admin/Role/Index",
                    AddTime = Now,
                    Order = 3,
                    Code = "AB",
                    PathCode = "AAAB",
                    Type = MenuType.Menu
                };//3
                var userMgr = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = system.Id,
                    Name = "用户管理",
                    Url = "/Admin/User/Index",
                    AddTime = Now,
                    Order = 4,
                    Code = "AC",
                    PathCode = "AAAC",
                    Type = MenuType.Menu
                };//4
                var userRoleMgr = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = userMgr.Id,
                    Name = "用户授权",
                    Url = "/Admin/User/Authen",
                    AddTime = Now,
                    Order = 5,
                    Code = "AD",
                    PathCode = "AAAD",
                    Type = MenuType.Menu
                };//5
                var giveRight = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = userRoleMgr.Id,
                    Name = "授权",
                    Url = "/Admin/User/GiveRight",
                    AddTime = Now,
                    Order = 1,
                    Code = "AA",
                    PathCode = "AAADAA",
                    Type = MenuType.Button
                };
                var cancelRight = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = userRoleMgr.Id,
                    Name = "取消授权",
                    Url = "/Admin/User/CancelRight",
                    AddTime = Now,
                    Order = 2,
                    Code = "AB",
                    PathCode = "AAADAB",
                    Type = MenuType.Button
                };
                var roleMenuMgr = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = system.Id,
                    Name = "角色授权",
                    Url = "/Admin/Role/Authen",
                    AddTime = Now,
                    Order = 6,
                    Code = "AE",
                    PathCode = "AAAE",
                    Type = MenuType.Menu
                };//6
                var log = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "日志查看",
                    Url = "#",
                    AddTime = Now,
                    Order = 2,
                    Code = "AB",
                    PathCode = "AB",
                    Type = MenuType.Module
                };//9
                var logLogin = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = log.Id,
                    Name = "登录日志",
                    Url = "/Admin/Log/Logins",
                    AddTime = Now,
                    Order = 1,
                    Code = "AA",
                    PathCode = "ABAA",
                    Type = MenuType.Menu
                };//10
                var logView = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = log.Id,
                    Name = "访问日志",
                    Url = "/Admin/Log/Visits",
                    AddTime = Now,
                    Order = 2,
                    Code = "AB",
                    PathCode = "ABAB",
                    Type = MenuType.Menu
                };//11

                //菜单
                var menus = new List<MenuEntity>
                {
                    system,
                    menuMgr,
                    roleMgr,
                    userMgr,
                    userRoleMgr,
                    giveRight,
                    cancelRight,
                    roleMenuMgr,
                    log,
                    logLogin,
                    logView
                };
                var menuBtns = GetMenuButtons(menuMgr.Id, "Menu", "菜单", "AAAA", 12);//14
                var rolwBtns = GetMenuButtons(roleMgr.Id, "Role", "角色", "AAAB", 15);//17
                var userBtns = GetMenuButtons(userMgr.Id, "User", "用户", "AAAC", 18);//20

                menus.AddRange(menuBtns);//14
                menus.AddRange(rolwBtns);//17
                menus.AddRange(userBtns);//20
                menus.Add(new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = roleMenuMgr.Id,
                    Order = 6,
                    Name = "授权",
                    Type = MenuType.Button,
                    Url = "/Admin/Role/SetRoleMenus",
                    AddTime = Now,
                    Code = "AA",
                    PathCode = "AAACAA"
                });
                menus.Add(new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = roleMenuMgr.Id,
                    Order = 6,
                    Name = "清空权限",
                    Type = MenuType.Button,
                    Url = "/Admin/Role/ClearRoleMenus",
                    AddTime = Now,
                    Code = "AB",
                    PathCode = "AAACAB"
                });
                //示例页面
                var page = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "示例页面",
                    Url = "#",
                    AddTime = Now,
                    Order = 3,
                    Code = "AC",
                    PathCode = "AC",
                    Type = (MenuType)1
                };
                var pageButton = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = page.Id,
                    Name = "按钮",
                    Url = "/Admin/Pages/Buttons",
                    AddTime = Now,
                    Order = 0,
                    Code = "AA",
                    PathCode = "ACAA",
                    Type = MenuType.Menu
                };
                var pageForm = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = page.Id,
                    Name = "表单",
                    Url = "/Admin/Pages/Form",
                    AddTime = Now,
                    Order = 1,
                    Code = "AB",
                    PathCode = "ACAB",
                    Type = MenuType.Menu
                };
                var pageFormAdvance = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = page.Id,
                    Name = "高级表单",
                    Url = "/Admin/Pages/FormAdvance",
                    AddTime = Now,
                    Order = 2,
                    Code = "AC",
                    PathCode = "ACAC",
                    Type = MenuType.Menu
                };
                var pageTable = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = page.Id,
                    Name = "表格",
                    Url = "/Admin/Pages/Tables",
                    AddTime = Now,
                    Order = 3,
                    Code = "AD",
                    PathCode = "ACAD",
                    Type = MenuType.Menu
                };
                var pageTabs = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = page.Id,
                    Name = "选项卡",
                    Url = "/Admin/Pages/Tabs",
                    AddTime = Now,
                    Order = 4,
                    Code = "AE",
                    PathCode = "ACAE",
                    Type = MenuType.Menu
                };
                var pageFonts = new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = page.Id,
                    Name = "字体",
                    Url = "/Admin/Pages/FontAwesome",
                    AddTime = Now,
                    Order = 5,
                    Code = "AF",
                    PathCode = "ACAF",
                    Type = MenuType.Menu
                };

                menus.Add(page);
                menus.Add(pageButton);
                menus.Add(pageForm);
                menus.Add(pageFormAdvance);
                menus.Add(pageTable);
                menus.Add(pageTabs);
                menus.Add(pageFonts);

                #endregion

                #region 角色
                var superAdminRole = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "超级管理员",
                    Description = "超级管理员"
                };
                var guestRole = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "guest",
                    Description = "游客"
                };
                var roles = new List<RoleEntity>
                {
                    superAdminRole,
                    guestRole
                };
                #endregion

                #region 用户角色关系
                var userRoles = new List<UserRoleEntity>
                {
                    new UserRoleEntity
                    {
                        Id = Guid.NewGuid(),
                        UserId = admin.Id,
                        RoleId = superAdminRole.Id,
                        AddTime = Now
                    },
                    new UserRoleEntity
                    {
                        Id = Guid.NewGuid(),
                        UserId = guest.Id,
                        RoleId = guestRole.Id,
                        AddTime = Now
                    }
                };
                #endregion

                #region 角色菜单权限关系
                var roleMenus = new List<RoleMenuEntity>();
                //管理员授权(管理员有所有权限)
                menus.ForEach(m =>
                {
                    roleMenus.Add(new RoleMenuEntity
                    {
                        Id = Guid.NewGuid(),
                        RoleId = superAdminRole.Id,
                        MenuId = m.Id,
                        AddTime = Now
                    });
                });
                //guest授权(guest只有查看权限，没有按钮操作权限)
                menus.Where(item => item.Type != MenuType.Button).ForEach(m =>
                {
                    roleMenus.Add(new RoleMenuEntity
                    {
                        Id = Guid.NewGuid(),
                        RoleId = guestRole.Id,
                        MenuId = m.Id,
                        AddTime = Now
                    });
                });
                #endregion

                #region 系统配置
                var systemConfig = new AdminConfigEntity
                {
                    Id = Guid.NewGuid(),
                    SystemName = "Boxhi Manager",
                    IsDataInited = true,
                    DataInitedDate = Now,
                    AddTime = Now,
                    IsDelete = false
                };
                #endregion

                _menuRepository.Insert(menus);
                _userRepository.Insert(user);
                _roleRepository.Insert(roles);
                _roleRepository.InsertRoleMenus(roleMenus);
                _userRepository.InsertUserRoles(userRoles);
                _adminConfigRepository.Insert(systemConfig);

                _menuRepository.UnitOfWork.Commit();
                _roleRepository.UnitOfWork.Commit();
                _userRepository.UnitOfWork.Commit();
                return await _adminConfigRepository.UnitOfWork.CommitAsync() > 0;

            }
            catch (Exception ex)
            {
                //todo log
            }
            return false;
        }

        /// <summary>
        /// 获取菜单的基础按钮
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="controllerShowName">菜单显示名称</param>
        /// <param name="parentPathCode">父级路径码</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        private IEnumerable<MenuEntity> GetMenuButtons(Guid parentId, string controllerName, string controllerShowName, string parentPathCode, int order)
        {
            return new List<MenuEntity>
            {
                new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = parentId,
                    Name = string.Concat("添加",controllerShowName),
                    Url = string.Format("/Admin/{0}/From",controllerName),
                    AddTime = Now,
                    Order = order,
                    Code = "AA",
                    PathCode = parentPathCode+"AA",
                    Type = MenuType.Button
                },
                new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = parentId,
                    Name = string.Concat("修改",controllerShowName),
                    Url = string.Format("/Admin/{0}/From",controllerName),
                    AddTime = Now,
                    Order = order+1,
                    Code = "AB",
                    PathCode = parentPathCode+"AB",
                    Type = MenuType.Button
                },
                new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = parentId,
                    Name = string.Concat("删除",controllerShowName),
                    Url = string.Format("/Admin/{0}/RemoveForm",controllerName),
                    AddTime = Now,
                    Order = order+2,
                    Code = "AC",
                    PathCode = parentPathCode+"AC",
                    Type = MenuType.Button
                }
            };
        }

        /// <summary>
        /// 初始化路径码
        /// </summary>
        public async Task<bool> InitPathCodeAsync()
        {
            //生成路径码
            var codes = new List<string>(26);
            for (var i = 65; i <= 90; i++)
            {
                codes.Add(((char)i).ToString());
            }
            //求组合
            var list = (from a in codes
                        from b in codes
                        select new PathCodeEntity
                        {
                            Code = a + b,
                            Len = 2
                        }).OrderBy(item => item.Code).ToList();
            var all = await _pathCodeRepository.GetAllAsync();
            _pathCodeRepository.Delete(all);
            _pathCodeRepository.Insert(list);
            return await _pathCodeRepository.UnitOfWork.CommitAsync() > 0;
        }
    }
}
