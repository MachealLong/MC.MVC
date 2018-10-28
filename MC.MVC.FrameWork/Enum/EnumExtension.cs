using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MC.MVC.Framework.Enum
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的description
        /// </summary>
        /// <param name="myEnum">枚举</param>
        /// <returns></returns>
        public static string GetDescription(this System.Enum myEnum)
        {
            var attr = GetCustomAttribute<DescriptionAttribute>(myEnum).FirstOrDefault();
            if (attr != null)
            {
                return attr.Description;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取制定类型特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="myEnum"></param>
        /// <returns></returns>
        public static IList<T> GetCustomAttribute<T>(this System.Enum myEnum) where T : Attribute
        {
            Type myEnumType = myEnum.GetType();
            string myEnumName = System.Enum.GetName(myEnumType, myEnum);
            FieldInfo field = myEnumType.GetField(myEnumName);
            object[] attributes = field.GetCustomAttributes(typeof(T), false);
            return attributes.Select(o => (T) o).ToList();
        }
    }
}
