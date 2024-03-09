using MessagePack;
using System.ComponentModel;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace CSSC.Models
{
    public class Notificacao
    {
        /// <summary>
        /// Id do serviço para associar a notificação
        /// </summary>
        /// <return>Int do Id do serviço associado</return>
        [DisplayName("Número de Serviço")]
        public int IdServico { get; set; }

        /// <summary>
        /// Id da notificação
        /// </summary>
        /// <return>Int do Id da notificação</return>
        [Key]
        public int IdNotif {  get; set; }

        /// <summary>
        /// Data para o envio da notificação
        /// </summary>
        /// <return>DateTime da data do envio da notificação</return>
        [DisplayName("Data Inicial")]
        public DateTime DataInicial { get; set; }

        /// <summary>
        /// Intervalo de tempo (em meses) para o envio da notificação, caso seja periódica
        /// </summary>
        /// <return>Int do intervalo de tempo (em meses)9</return>
        [DisplayName("Intervalo de Tempo")]
        public int? IntervaloDeEnvio { get; set; }

        /// <summary>
        /// Tipo de notificação (periódica ou pontual)
        /// </summary>
        /// <return>String a identificar o tipo de notificação</return>
        [DisplayName("Tipo de Notificação")]
        public string? TipoDeNotif { get; set; }    
    }
}
