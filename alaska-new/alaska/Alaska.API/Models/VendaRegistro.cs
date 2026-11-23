using System.ComponentModel.DataAnnotations;

namespace Alaska.API.Models
{
    public class VendaRegistro
    {
        [Key]
        public int IdVenda { get; set; }
        public int IdFilme { get; set; }
        public string TituloFilme { get; set; } = string.Empty;
        public string Dia { get; set; } = string.Empty;
        public string Horario { get; set; } = string.Empty;
        public string TipoSessao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCompra { get; set; } = DateTime.Now;
    }
}
