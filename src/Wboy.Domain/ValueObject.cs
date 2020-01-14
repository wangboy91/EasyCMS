using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NSample.Domain.SeedWork
{
    /// <summary>
    /// 领域对象中的值类型的基类
    /// </summary>
    /// <typeparam name="TValueObject">值类型</typeparam>
    public class ValueObject<TValueObject> :IEquatable<TValueObject> 
        where TValueObject : ValueObject<TValueObject>
    {

        public bool Equals(TValueObject other)
        {
            if (Object.Equals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;

            var publicProperties = this.GetType().GetProperties();

            if (publicProperties.Any())
            {
                return publicProperties.All(p =>
                {
                    var left = p.GetValue(this, null);
                    var right = p.GetValue(other, null);
                    
                    //验证当前对象
                    if (left is TValueObject)
                    {
                        return Object.ReferenceEquals(left, right);
                    }
                    else
                    {
                        return left.Equals(right);
                    }
                });
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (Object.Equals(obj, null)) return false;
            if (Object.ReferenceEquals(this, obj)) return true;

            var item = obj as ValueObject<TValueObject>;
            if (item != null)
                return Equals((TValueObject) item);
            else
                return false;
        }

        public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            if (Object.Equals(left, null))
            {
                return Object.Equals(right, null) ? true : false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            var hashCode = 31;
            var changeMultiplier = false;
            var index = 1;

            //compare all public properties
            var publicProperties = this.GetType().GetProperties();

            if (publicProperties.Any())
            {
                foreach (var item in publicProperties)
                {
                    var value = item.GetValue(this, null);

                    if (value != null)
                    {

                        hashCode = hashCode * ((changeMultiplier) ? 59 : 114) + value.GetHashCode();

                        changeMultiplier = !changeMultiplier;
                    }
                    else
                        hashCode = hashCode ^ (index * 13);//only for support {"a",null,null,"a"} <> {null,"a","a",null}
                }
            }

            return hashCode;
        }
    }
}
