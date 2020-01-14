using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Infrastructure.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AttributeSetting : Attribute
    {

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { set; get; }
    }
}
