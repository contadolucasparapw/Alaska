using Microsoft.AspNetCore.Mvc;
using Alaska.API.Models;
using Alaska.API.Enums;
using System.Linq;

namespace Alaska.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    [HttpGet]
    public IActionResult ListarPessoas()
    {
        List<Pessoa> pessoas = new List<Pessoa>();

        Pessoa pessoa1 = new Pessoa();
        pessoa1.Id = 1;
        pessoa1.Nome = "Carlos";
        pessoa1.Email = "carlos@email.com";
        pessoa1.Nivel = NivelUsuario.Cliente;

        Pessoa pessoa2 = new Pessoa();
        pessoa2.Id = 2;
        pessoa2.Nome = "Ana";
        pessoa2.Email = "ana@email.com";
        pessoa2.Nivel = NivelUsuario.Administrador;

        pessoas.Add(pessoa1);
        pessoas.Add(pessoa2);

        var dadosPublicos = pessoas.Select(pessoa => new
        {
            pessoa.Id,
            pessoa.Nome,
            pessoa.Email,
            pessoa.Nivel
        });

        return Ok(dadosPublicos);
    }
}