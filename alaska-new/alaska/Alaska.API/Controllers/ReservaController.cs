using Microsoft.AspNetCore.Mvc;
using Alaska.API.Models;

namespace Alaska.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservaController : ControllerBase
{
[HttpGet]
public List<Reserva> ListarReservas()
{
    List<Reserva> reservas = new List<Reserva>();
    Reserva reserva1 = new Reserva();
    reserva1.Id = 1;
    reserva1.SessaoId = 1;
    reserva1.DataCriacao = DateTime.Now;

    Reserva reserva2 = new Reserva();
    reserva2.Id = 2;
    reserva2.SessaoId = 2;
    reserva2.DataCriacao = DateTime.Now;

    reservas.Add(reserva1);
    reservas.Add(reserva2);
    return reservas;
}
}