using CSSC.Areas.Identity.Data;
using CSSC.Controllers;
using System.ComponentModel;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace CSSC.Models
{
    public class Services
    {
        [Key]
        [DisplayName("Id do serviço")]
        public int IdServico { get; set; }
        public Guid ServIdUtilizador { get; set; }
        public CSSCUser? csscUser { get; set; }
        public string? ServMarcaVeiculo { get; set; }
        public string? ServModeloVeiculo { get; set; }
        public string? ServMatriculaVeiculo { get; set; }
        public int? ServClassificacao { get; set; }
        public string? ServComentario { get; set; }
        [DisplayName("Prazo do serviço")]
        public DateTime ServPrazo { get; set; }
        public string? EstadoDoServico { get; set; }
    }
}
