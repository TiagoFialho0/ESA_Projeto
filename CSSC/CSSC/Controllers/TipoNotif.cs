using System.Runtime.Serialization;

namespace CSSC.Controllers
{
    /// <summary>
    /// Enumerado para representar tipos de notificação
    /// </summary>
    public enum TipoNotif
    {
        [EnumMember(Value = "Periodica")]
        Periódica,
        [EnumMember(Value = "Pontual")]
        Pontual        
    }
}
