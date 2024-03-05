using System.Runtime.Serialization;

namespace CSSC.Controllers
{
    public enum TipoNotif
    {
        [EnumMember(Value = "Periodica")]
        Periódica,
        [EnumMember(Value = "Pontual")]
        Pontual        
    }
}
