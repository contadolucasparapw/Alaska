namespace Alaska.API.Models;

public class Assento{
    public int Id {get;set;}
    public int SalaId {get;set;}
    public string Fileira {get;set;} = string.Empty;
    public int Numero {get;set;} 
    public Sala Sala { get; set; } = null!;
    public ICollection<ReservaAssento> ReservaAssentos { get; set; } = new List<ReservaAssento>();
}