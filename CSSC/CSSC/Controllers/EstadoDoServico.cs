using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace CSSC.Controllers
{
    public enum EstadoDoServico
    {
        [EnumMember(Value = "Em espera")]
        Espera,
        [EnumMember(Value = "Em reparação")]
        EmReparacao,
        [EnumMember(Value = "Reparação concluida")]
        ReparacaoConcluida,
        [EnumMember(Value = "Pronto para entrega")]
        ProntoParaEntrega,
        [EnumMember(Value = "Entregue")]
        Entregue,
        [EnumMember(Value = "Cancelado")]
        Cancelado
    }
}
