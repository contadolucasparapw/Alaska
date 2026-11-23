using Microsoft.AspNetCore.Mvc;
using Alaska.API.Models;

namespace Alaska.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssentoController : ControllerBase
{
    [HttpGet]
    public List<Assento> ListarAssentos()
    {
        List<Assento> assentos = new List<Assento>();

        Assento assento1 = new Assento();
        assento1.Id = 1;
        assento1.SalaId = 1;
        assento1.Fileira = "A";
        assento1.Numero = 1;

        Assento assento2 = new Assento();
        assento2.Id = 2;
        assento2.SalaId = 1;
        assento2.Fileira = "A";
        assento2.Numero = 2;

        Assento assento3 = new Assento();
        assento3.Id = 3;
        assento3.SalaId = 1;
        assento3.Fileira = "B";
        assento3.Numero = 1;

        assentos.Add(assento1);
        assentos.Add(assento2);
        assentos.Add(assento3);

        return assentos;
    }
}