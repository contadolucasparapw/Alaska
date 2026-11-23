namespace Alaska.API.Models;

public class Sala{
    public int Id {get;set;}
    public int Capacidade {get;set;}
    public ICollection<Sessao> Sessoes { get; set; } = new List<Sessao>();
    public ICollection<Assento> Assentos { get; set; } = new List<Assento>();
}