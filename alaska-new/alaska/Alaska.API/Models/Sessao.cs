namespace Alaska.API.Models;

public class Sessao
{
    public int Id { get; set; }
    public int FilmeId { get; set; }
    public int SalaId { get; set; }
    public DateTime DataHora{get;set;}
    public int QuantidadeMinima{get;set;}
    public Filme Filme { get; set; } = null!;
    public Sala Sala { get; set; } = null!;
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}