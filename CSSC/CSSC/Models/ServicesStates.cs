using CSSC.Areas.Identity.Data;
using System.ComponentModel;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace CSSC.Models
{
    public class ServicesStates
    {
        [Key]
        [DisplayName("Código Estado de Serviço")]
        public int IdEstadoServico { get; set; }


        [DisplayName("Número de Serviço")]
        public int ServIdServico { get; set; }

 
        [DisplayName("Serviço")]
        public Services? services { get; set; }

        [DisplayName("Estado do Serviço")]
        public string EstadoDoServico { get; set; }

        [DisplayName("Data de alteração")]
        public DateTime alteracaoEstado { get; set; }

        //[DisplayName("Estado atual")]
        //public bool estadoAtual { get; set; }

    }
}

