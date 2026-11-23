namespace Alaska.API.Models;

public class ReservaAssento{
    public int Id {get;set;}
    public int ReservaId {get;set;}
    public int AssentoId {get;set;}
    public Reserva Reserva { get; set; } = null!;
    public Assento Assento { get; set; } = null!;
}