using CSSC.Areas.Identity.Data;
using System.ComponentModel;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace CSSC.Models
{
    public class ServicesStates
    {
        /// <summary>
        /// O id do estado de serviço.
        /// </summary>
        [Key]
        [DisplayName("Código Estado de Serviço")]
        public int IdEstadoServico { get; set; }

        /// <summary>
        /// O id do serviço.
        /// </summary>
        [DisplayName("Número de Serviço")]
        public int ServIdServico { get; set; }

        /// <summary>
        /// O serviço associado ao estado.
        /// </summary>
        [DisplayName("Serviço")]
        public Services? services { get; set; }

        /// <summary>
        /// O estado do serviço.
        /// </summary>
        [DisplayName("Estado do Serviço")]
        public string EstadoDoServico { get; set; }

        /// <summary>
        /// A data de alteração do estado.
        /// </summary>
        [DisplayName("Data de alteração")]
        public DateTime alteracaoEstado { get; set; }

        //[DisplayName("Estado atual")]
        //public bool estadoAtual { get; set; }

    }
}

