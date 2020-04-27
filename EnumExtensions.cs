using System;
using System.ComponentModel;
using System.Reflection;

namespace Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// public enum Duration 
        //{ 
        //    [Description("Enum1")]
        //        Enum1,
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}
