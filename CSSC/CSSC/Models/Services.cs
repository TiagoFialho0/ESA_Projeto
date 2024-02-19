using CSSC.Areas.Identity.Data;
using CSSC.Controllers;
using System.ComponentModel;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace CSSC.Models
{
    public class Services
    {
        [Key]
        [DisplayName("Número de Serviço")]
        public int IdServico { get; set; }

        [DisplayName("Número de Cliente")]
        public Guid ServIdUtilizador { get; set; }

        [DisplayName("Cliente")]
        public CSSCUser? csscUser { get; set; }

        [DisplayName("Número de Operador")]
        public Guid? ServIdOperador { get; set; }

        [DisplayName("Operador")]
        public CSSCUser? csscOperador { get; set; }

        [DisplayName("Marca do Veiculo")]
        public string? ServMarcaVeiculo { get; set; }

        [DisplayName("Modelo do Veiculo")]
        public string? ServModeloVeiculo { get; set; }

        [DisplayName("Matricula do Veiculo")]
        public string? ServMatriculaVeiculo { get; set; }

        [DisplayName("Classificação")]
        public int? ServClassificacao { get; set; }

        [DisplayName("Comentário")]
        public string? ServComentario { get; set; }

        [DisplayName("Prazo do serviço")]
        public DateTime ServPrazo { get; set; }

        [DisplayName("Estado do Serviço")]
        public string? EstadoDoServico { get; set; }
    }
}