using System.Reflection;
using System.Runtime.Serialization;

namespace CSSC.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Obtém o valor associado a um membro de enumeração, utilizando o atributo EnumMember, se presente.
        /// </summary>
        /// <param name="value">O valor de enumeração.</param>
        /// <returns>
        /// O valor associado ao membro de enumeração, conforme especificado pelo atributo EnumMember, se presente.
        /// Caso contrário, retorna o nome do membro de enumeração.
        /// </returns>
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

        /// <summary>
        /// Obtém uma lista de valores de enumeração com descrições, utilizando o atributo EnumMember, se presente.
        /// </summary>
        /// <param name="enumType">O tipo de enumeração.</param>
        /// <returns>
        /// Uma lista de valores de enumeração com descrições, conforme especificado pelo atributo EnumMember, se presente.
        /// Caso contrário, retorna uma lista contendo os nomes dos membros de enumeração.
        /// </returns>
        public static List<string> GetValuesWithDescriptions(this Type enumType)
        {
            return enumType.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? f.Name)
                .ToList();
        }
    }
}
