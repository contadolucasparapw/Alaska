using Microsoft.AspNetCore.Mvc;
using Alaska.API.Models;
using Alaska.API.Data;

namespace Alaska.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmeController : ControllerBase
{
    private readonly AlaskaContext _context;

    public FilmeController(AlaskaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult BuscarFilmes()
    {
        var filmes = _context.Filmes.ToList();

        return Ok(filmes);
    }

    [HttpPost]
    public IActionResult CadastrarFilme([FromBody] Filme filme)
    {
        _context.Filmes.Add(filme);
        _context.SaveChanges();

        return Ok(filme);
    }
}