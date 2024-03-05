using MessagePack;
using System.ComponentModel;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace CSSC.Models
{
    public class Notificacao
    {
        [DisplayName("Número de Serviço")]
        public int IdServico { get; set; }

        [Key]
        public int IdNotif {  get; set; }

        [DisplayName("Data Inicial")]
        public DateTime DataInicial { get; set; }

        [DisplayName("Intervalo de Tempo")]
        public int? IntervaloDeEnvio { get; set; }

        [DisplayName("Tipo de Notificação")]
        public string? TipoDeNotif { get; set; }    
    }
}
