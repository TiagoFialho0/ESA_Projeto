using CSSC.Areas.Identity.Data;
using CSSC.Controllers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace CSSC.Models
{
    public class Services
    {
        /// <summary>
        /// Id do serviço
        /// </summary>
        /// <return>Int do Id do serviço</return>
        [Key]
        [DisplayName("Número de Serviço")]
        public int IdServico { get; set; }

        /// <summary>
        /// Id do utilizador associado ao serviço
        /// </summary>
        /// <return>Guid do utilizador associado</return>
        [DisplayName("Email do Cliente")]
        public Guid ServIdUtilizador { get; set; }

        /// <summary>
        /// Utilizador associado ao serviço
        /// </summary>
        /// <return>CSSCUser associado ao serviço</return>
        [DisplayName("Cliente")]
        public CSSCUser? csscUser { get; set; }

        /// <summary>
        /// Id do operador associado ao serviço
        /// </summary>
        /// <return>Guid do operador associado</return>
        [DisplayName("Número de Operador")]
        public Guid? ServIdOperador { get; set; }

        /// <summary>
        /// Operador associado ao serviço
        /// </summary>
        /// <return>CSSCUser associado ao serviço</return>
        [DisplayName("Operador")]
        public CSSCUser? csscOperador { get; set; }

        /// <summary>
        /// Marca do veiculo
        /// </summary>
        /// <return>String da marca do veiculo</return>
        [DisplayName("Marca do Veiculo")]
        public string? ServMarcaVeiculo { get; set; }

        /// <summary>
        /// Modelo do veiculo
        /// </summary>
        /// <return>String do modelo do veículo</return>
        [DisplayName("Modelo do Veiculo")]
        public string? ServModeloVeiculo { get; set; }

        /// <summary>
        /// Matrícula do veiculo
        /// </summary>
        /// <return>String da matriculo do veículo</return>
        [DisplayName("Matricula do Veiculo")]
        public string? ServMatriculaVeiculo { get; set; }

        /// <summary>
        /// Classificação final do serviço
        /// </summary>
        /// <return>Int correspondente à avaliação do serviço</return>
        [DisplayName("Classificação")]
        public int? ServClassificacao { get; set; }

        /// <summary>
        /// Comentário opcional ao serviço final
        /// </summary>
        /// <return>String com o comentário opcional do serviço</return>
        [DisplayName("Comentário")]
        public string? ServComentario { get; set; }

        /// <summary>
        /// Data em que o serviço vai ser realizado
        /// </summary>
        /// <return>DateTime com a data do serviço</return>
        [DisplayName("Data de Início")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ServDataInicio { get; set; }

        /// <summary>
        /// Estado atual do serviço
        /// </summary>
        /// <return>String que apresenta o estado atual do serviço</return>
        [DisplayName("Estado do Serviço")]
        public string? EstadoDoServico { get; set; }

        /// <summary>
        /// Breve descrição do serviço
        /// </summary>
        /// <return>String que apresenta uma breve descrição do serviço</return>
        [DisplayName("Descrição do Serviço")]
        public string? DescricaoDoServico { get; set; }
    }
}