using Microsoft.AspNetCore.Mvc;
using Alaska.API.Models;

namespace Alaska.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalaController : ControllerBase
{
[HttpGet]
public List<Sala> ListarSalas()
{
    List<Sala> salas = new List<Sala>();
        Sala sala1 = new Sala();
        sala1.Id = 1;
        sala1.Capacidade = 20;

        Sala sala2 = new Sala();
        sala2.Id = 2;
        sala2.Capacidade= 30;
        
    salas.Add(sala1);
    salas.Add(sala2);
    return salas;
}
}