namespace Alaska.API.Models;

public class Reserva
{
    public int Id { get; set; }
    public int SessaoId { get; set; }
    public DateTime DataCriacao { get; set; }
    public Sessao Sessao { get; set; } = null!;
    public ICollection<ReservaAssento> ReservaAssentos { get; set; } = new List<ReservaAssento>();
}