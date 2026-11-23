using Microsoft.AspNetCore.Mvc;
using Alaska.API.Models;

namespace Alaska.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessaoController : ControllerBase
{
[HttpGet]
public List<Sessao> ListarSessoes()
{
    List<Sessao> sessoes = new List<Sessao>();
        Sessao sessao1 = new Sessao();
        sessao1.Id = 1;
        sessao1.FilmeId = 1;
        sessao1.SalaId = 1;
        sessao1.QuantidadeMinima = 20;
        sessao1.DataHora = new DateTime(2026, 11, 23, 18, 0, 0);

        Sessao sessao2 = new Sessao();
        sessao2.Id = 2;
        sessao2.FilmeId = 2;
        sessao2.SalaId = 2;
        sessao2.QuantidadeMinima = 20;
        sessao2.DataHora = new DateTime(2026, 11, 23, 18, 0, 0);

    sessoes.Add(sessao1);
    sessoes.Add(sessao2);
    return sessoes;
}
}