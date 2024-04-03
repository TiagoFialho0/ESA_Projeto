using System.Runtime.Serialization;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace CSSC.Controllers
{
    /// <summary>
    /// Enumerado para representar o código do país com base no formato NIF
    /// </summary>
    public enum PaiseNIF
    {
        [EnumMember(Value = "pt")]
        PT,
        [EnumMember(Value = "esp")]
        ESP,
        [EnumMember(Value = "fr")]
        FR
    }

    /*public static class EnumExtensions
    {
        private static readonly Dictionary<PaiseNIF, string> ImagePaths = new Dictionary<PaiseNIF, string>
        {
            { PaiseNIF.PT, "/CSSC/wwwroot/images/icons8-portugal-96.png" },
            { PaiseNIF.ESP, "/CSSC/wwwroot/images/icons8-portugal-96.png" }, 
            { PaiseNIF.FR, "/CSSC/wwwroot/images/icons8-portugal-96.png" }
        };

        public static string? GetImagePath(this PaiseNIF value)
        {
            return ImagePaths.TryGetValue(value, out var path) ? path : null;
        }
    }*/
}
