using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace Wboy.Infrastructure.Core.Dependency
{
    public class SampleDependencyScope : IDisposable
    {
        private bool _disposed;
        private IContainer _container;
        private ILifetimeScope _nSampleDependencyScope;

        public SampleDependencyScope(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this._container = container;
            this._nSampleDependencyScope = container.BeginLifetimeScope();
        }

        ~SampleDependencyScope()
        {
            //就算是gc调用了我的终结器，我也不释放scope
            //所有此类必须手动释放调用Dispose
            this.Dispose(false);
        }

        public T Resolve<T>(string key = "") where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                return _nSampleDependencyScope.Resolve<T>();
            }
            return _nSampleDependencyScope.ResolveKeyed<T>(key);
        }

        public object Resolve(Type type)
        {
            return _nSampleDependencyScope.Resolve(type);
        }

        public T[] ResolveAll<T>(string key = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                return _nSampleDependencyScope.Resolve<IEnumerable<T>>().ToArray();
            }
            return _nSampleDependencyScope.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }
        public object ResolveOptional(Type serviceType)
        {
            return _nSampleDependencyScope.ResolveOptional(serviceType);
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

        /// <summary>
        /// 尝试从autofac容器中获取指定类型的事例，如果获取失败返回false
        /// </summary>
        /// <param name="serviceType">获取实例类型</param>
        /// <param name="instance">实例</param>
        /// <returns></returns>
        public bool TryResolve(Type serviceType, out object instance)
        {
            return _nSampleDependencyScope.TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// 类型是否已经注册到autofac容器
        /// </summary>
        /// <param name="serviceType">类型</param>
        /// <returns></returns>
        public bool IsRegistered(Type serviceType)
        {
            return _nSampleDependencyScope.IsRegistered(serviceType);
        }

        public void Dispose()
        {
            //真正回收资源
            this.Dispose(true);
            //当gc调用时告诉它不要调用我的终结器，以免在我不需要释放scope的时候释放scope
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing && this._nSampleDependencyScope != null)
                {
                    this._nSampleDependencyScope.Dispose();
                }
                this._disposed = true;
            }
        }
    }
}
