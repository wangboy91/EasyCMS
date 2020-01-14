using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Wboy.Infrastructure.Core.Configuration;
using Wboy.Infrastructure.Core.Logging;

namespace Wboy.Infrastructure.Core
{
    public class SampleContext
    {
        public static string ConnectionString {set; get;}

        public static SampleEngine Initialize(ContainerBuilder builder)
        {
            if (Singleton<SampleEngine>.Instance == null)
            {
                //create engine
                var engine = CreateNSampleEngineInstance();
                //init engine
                engine.InitializeContainer(builder);
                Singleton<SampleEngine>.Instance = engine;
            }
           //init setting
            InitSettingConfig();
            return Singleton<SampleEngine>.Instance;
        }

        private static SampleEngine CreateNSampleEngineInstance()
        {
            return new SampleEngine();
        }
        private static void InitSettingConfig()
        {
            using (var scope = Current.BeginScope())
            {
                var setting = scope.Resolve<ISettingInitialization>();//Current.Resolve<ISettingInitialization>();
                if (setting != null && setting.Initialization())
                {
                    LoggerFactory.CreateLoger().Info("Setting-加载成功");
                }
            }
        }
        /// <summary>
        /// 获取当前上下文中的单例NSampleEngine实例
        /// </summary>
        public static SampleEngine Current
        {
            get
            {
                if (Singleton<SampleEngine>.Instance == null)
                {
                    Initialize(new ContainerBuilder());
                }
                return Singleton<SampleEngine>.Instance;
            }
        }
    }
}
