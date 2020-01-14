using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Wboy.Infrastructure.Core.Dependency;
using Wboy.Infrastructure.Core.Logging;

namespace Wboy.Infrastructure.Core
{
    public class SampleEngine
    {
        private IContainer _applicationContainer;
        public IContainer Container => _applicationContainer;
        public void InitializeContainer(ContainerBuilder builder)
        {
            var typeFinder = new WebAppTypeFinder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
            {
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
                //Trace.TraceInformation("依赖注册:" + drType.FullName);
            }
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.RegisterIndex).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder);

            var container = builder.Build();
            this._applicationContainer = container;

            LoggerFactory.CreateLoger().Info("Initialize SampleEngine Container Success!");
        }

        public SampleDependencyScope BeginScope()
        {
            return new SampleDependencyScope(_applicationContainer);
        }

        public T Resolve<T>() where T : class
        {
            return _applicationContainer.Resolve<T>();
        }
        public T[] ResolveAll<T>(string key = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                return _applicationContainer.Resolve<IEnumerable<T>>().ToArray();
            }
            return _applicationContainer.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }
        /// <summary>
        /// 获取一个未在autofac中未注册的实例
        /// 创建实例时，会自动为该实例初始化构造函数中参数，参数是从容器中获得
        /// </summary>
        /// <param name="type">未注册的实例</param>
        /// <returns></returns>
        public object ResolveUnregistered(Type type)
        {
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null) throw new Exception("未知依赖");
                        parameterInstances.Add(service);
                    }
                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
                catch (Exception)
                {

                }
            }
            throw new Exception("没有为构造函数在容器中找到依赖");
        }
        public object Resolve(Type type)
        {
            return _applicationContainer.Resolve(type);
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            return _applicationContainer.TryResolve(serviceType, out instance);
        }

        public bool IsRegistered(Type serviceType)
        {
            return _applicationContainer.IsRegistered(serviceType);
        }
    }
}
