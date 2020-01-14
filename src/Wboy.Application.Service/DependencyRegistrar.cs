using System;
using Autofac;
using Wboy.Infrastructure.Core.Dependency;

namespace Wboy.Application.Service
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder)
        {

            #region 系统管理模块
            builder.RegisterType<AdminModule.AppService.DatabaseInitAppService>().As<AdminModule.IAppService.IDatabaseInitAppService>();
            builder.RegisterType<AdminModule.AppService.LogAppService>().As<AdminModule.IAppService.ILogAppService>();
            builder.RegisterType<AdminModule.AppService.MenuAppService>().As<AdminModule.IAppService.IMenuAppService>();
            builder.RegisterType<AdminModule.AppService.RoleAppService>().As<AdminModule.IAppService.IRoleAppService>();
            builder.RegisterType<AdminModule.AppService.UserAppService>().As<AdminModule.IAppService.IUserAppService>();
            #endregion

            #region 平台管理模块

            builder.RegisterType<AdminModule.AppService.SystemSettingAppService>().As<AdminModule.IAppService.ISystemSettingAppService>();

            #endregion
        }
        public int RegisterIndex => 2;
    }
}
