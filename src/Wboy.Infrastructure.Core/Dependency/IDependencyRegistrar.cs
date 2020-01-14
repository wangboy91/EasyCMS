using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Wboy.Infrastructure.Core.Dependency
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder);

        /// <summary>
        /// 注册顺序，升序
        /// </summary>
        int RegisterIndex { get; }
    }
}
