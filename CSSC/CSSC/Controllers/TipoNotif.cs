using System.Runtime.Serialization;

namespace CSSC.Controllers
{
        public enum TipoNotif
        {
            [EnumMember(Value = "Periódica")]
            Periodica,
            [EnumMember(Value = "Pontual")]
            Pontual        }
    
}
