using System.Reflection;
using System.Runtime.Serialization;

namespace CSSC.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue(this Enum value)
        {
            var memberInfo = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            if (memberInfo != null)
            {
                var enumMemberAttribute = memberInfo.GetCustomAttribute<EnumMemberAttribute>();
                if (enumMemberAttribute != null)
                {
                    return enumMemberAttribute.Value;
                }
            }
            return value.ToString(); // Default to enum name if EnumMember attribute is not present
        }

        public static List<string> GetValuesWithDescriptions(this Type enumType)
        {
            return enumType.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? f.Name)
                .ToList();
        }
    }
}
