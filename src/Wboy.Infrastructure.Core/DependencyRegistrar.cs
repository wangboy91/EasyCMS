using Autofac;
using Wboy.Infrastructure.Core.Cache;
using Wboy.Infrastructure.Core.Dependency;
using Wboy.Infrastructure.Core.Logging;

namespace Wboy.Infrastructure.Core
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder)
        {
            //--> 工厂配置
            ConfigureFactories();
            //--> log
            //builder.RegisterType<Log4NetLogFactory>().As<ILoggerFactory>().SingleInstance();
            //--> Cache
            builder.RegisterType<MemoryCacheService>().As<ICache>().Named<ICache>("init_cache").SingleInstance();

            builder.RegisterType<WebAppTypeFinder>().As<ITypeFinder>().SingleInstance();
            builder.RegisterType<AliLib.SmsSenderAli>().As<Message.ISmsSender> ().SingleInstance();

        }

        private void ConfigureFactories()
        {
            
            LoggerFactory.SetCurrent(new Log4NetLogFactory());
            CacheFactory.SetCurrent(new MemoryCacheFactory());

        }

        public int RegisterIndex => 0;
    }
}
