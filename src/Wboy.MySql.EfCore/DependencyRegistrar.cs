
using Autofac;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using Wboy.Infrastructure.Core.Cache;
using Wboy.Infrastructure.Core.Configuration;
using Wboy.Infrastructure.Core.Dependency;
using Wboy.MySql.EfCore.Configuration;

namespace Wboy.MySql.EfCore
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder)
        {
            builder.Register(x => new EfUnitOfWork());
            #region 系统管理模块
            builder.RegisterType<AdminModule.Repository.AreaRepository>().As<Domain.AdminModule.IRepository.IAreaRepository>();
            builder.RegisterType<AdminModule.Repository.LoginLogRepository>().As<Domain.AdminModule.IRepository.ILoginLogRepository>();
            builder.RegisterType<AdminModule.Repository.MenuRepository>().As<Domain.AdminModule.IRepository.IMenuRepository>();
            builder.RegisterType<AdminModule.Repository.PageViewRepository>().As<Domain.AdminModule.IRepository.IPageViewRepository>();
            builder.RegisterType<AdminModule.Repository.PathCodeRepository>().As<Domain.AdminModule.IRepository.IPathCodeRepository>();
            builder.RegisterType<AdminModule.Repository.RoleRepository>().As<Domain.AdminModule.IRepository.IRoleRepository>();
            builder.RegisterType<AdminModule.Repository.UserRepository>().As<Domain.AdminModule.IRepository.IUserRepository>();
            #endregion

            #region 平台模块
            builder.RegisterType<PlatformModule.Repository.SystemSettingRepository>().As<Domain.PlatformModule.IRepository.ISystemSettingRepository>();
            builder.RegisterType<DbSettingInitialization>().As<ISettingInitialization>().SingleInstance();
            builder.RegisterGeneric(typeof(DbSettingProvider<>)).As(typeof(ISettingProvider<>)).WithParameter(ResolvedParameter.ForNamed<ICache>("init_cache"));

            builder.RegisterType<PlatformModule.Repository.SystemSettingRepository>().As<Domain.PlatformModule.IRepository.ISystemSettingRepository>();
            builder.RegisterType<PlatformModule.Repository.OssFileRepository>().As<Domain.PlatformModule.IRepository.IOssFileRepository>();
            #endregion

        }


        public int RegisterIndex => 1;
    }
}
