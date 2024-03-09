namespace CSSC.Models
{
    public class CalendarViewModel
    {
        /// <summary>
        /// Meses apresentados no calendário
        /// </summary>
        /// <return>List com os nomes dos meses presentes no calendário</return>
        public List<string> Months { get; set; }

        /// <summary>
        /// Serviços a decorrer nos meses
        /// </summary>
        /// <return>List com Lists dos serviços</return>
        public List<List<Services>> Data { get; set; }

        /// <summary>
        /// Data inicial do calendário
        /// </summary>
        /// <return>DateTime inicial do calendário</return>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Data final do calendário
        /// </summary>
        /// <return>DateTime final do calendário</return>
        public DateTime EndDate { get; set; }
    }
}
