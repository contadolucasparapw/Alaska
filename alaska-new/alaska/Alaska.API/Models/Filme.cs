namespace Alaska.API.Models;

public class Filme{
    public int Id {get;set;}
    public string Titulo {get;set;} = string.Empty;
    public int Duracao {get;set;}
    public string Classificacao { get; set; } = string.Empty;
    public DateTime DataLancamento { get; set; }
    public string PosterUrl { get; set; }
    public int Status { get; set; }
    public ICollection<Sessao> Sessoes { get; set; } = new List<Sessao>();
} 