using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Wboy.Infrastructure.Core.Extension
{
    /// <summary>
    ///     枚举扩展方法类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionForEnum(this object value)
        {
            try
            {
                if (value == null) return string.Empty;
                var type = value.GetType();
                var field = type.GetField(Enum.GetName(type, value));

                if (field == null) return value.ToString();

                var des = CustomAttributeData.GetCustomAttributes(type.GetMember(field.Name)[0]);

                return des.AnyOne() && des[0].ConstructorArguments.AnyOne()
                    ? des[0].ConstructorArguments[0].Value.ToString()
                    : value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 判断序列是否包含任何元素，如果序列为空，则返回False
        /// </summary>
        /// <typeparam name="T">序列类型</typeparam>
        /// <param name="source">序列</param>
        /// <returns></returns>
        private static bool AnyOne<T>(this IEnumerable<T> source)
        {
            return source != null ? source.Any() : false;
        }
    }
}
