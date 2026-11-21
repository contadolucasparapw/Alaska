using Microsoft.AspNetCore.Mvc;
using appdomateus.models;

namespace appdomateus.Controllers
{
    [ApiController]
    [Route("api")]
    public class CinemaController : ControllerBase
    {
        private static readonly List<Filme> Filmes = new()
        {
            new Filme
            {
                Id = 1,
                Titulo = "Duna: Parte Dois",
                Duracao = 166,
                Classificacao = 14,
                DataLancamento = "2026-11-01",
                PosterUrl = "https://image.tmdb.org/t/p/w500/8b8R8l88Qje9dn9OE8PY05Nxl1X.jpg",
                Status = 0
            },
            new Filme
            {
                Id = 2,
                Titulo = "Interestelar",
                Duracao = 169,
                Classificacao = 10,
                DataLancamento = "2026-12-15",
                PosterUrl = "https://image.tmdb.org/t/p/w500/nCbkOyOMTEwlEV0LtCOvCnwEONA.jpg",
                Status = 1
            },
            new Filme
            {
                Id = 3,
                Titulo = "Stelarinter",
                Duracao = 169,
                Classificacao = 10,
                DataLancamento = "2026-12-15",
                PosterUrl = "https://image.tmdb.org/t/p/w500/nCbkOyOMTEwlEV0LtCOvCnwEONA.jpg",
                Status = 2
            },
            new Filme
            {
                Id = 4,
                Titulo = "Filme 4",
                Duracao = 169,
                Classificacao = 10,
                DataLancamento = "2026-12-15",
                PosterUrl = "https://image.tmdb.org/t/p/w500/nCbkOyOMTEwlEV0LtCOvCnwEONA.jpg",
                Status = 3
            },
            new Filme
            {
                Id = 5,
                Titulo = "Minha mãe é uma peça",
                Duracao = 169,
                Classificacao = 10,
                DataLancamento = "2026-12-15",
                PosterUrl = "https://image.tmdb.org/t/p/w500/nCbkOyOMTEwlEV0LtCOvCnwEONA.jpg",
                Status = 4
            },  
            };

        private static readonly List<Categoria> Categorias = new()
        {
            new Categoria { Id = 1, Nome = "Ficção Científica" },
            new Categoria { Id = 2, Nome = "Aventura" }
        };

        private static readonly List<CategoriaFilme> CategoriasFilmes = new()
        {
            new CategoriaFilme { IdFilme = 1, IdCategoria = 1 },
            new CategoriaFilme { IdFilme = 1, IdCategoria = 2 },
            new CategoriaFilme { IdFilme = 2, IdCategoria = 1 }
        };

        private static readonly List<Sessao> Sessoes = new()
        {
            new Sessao
            {
                IdSessao = 101,
                IdFilme = 1,
                IdSala = 1,
                NomeSala = "Sala IMAX 1",
                DataHora = DateTime.Now.AddHours(3),
                Preco = 38.00m,
                Status = 1,
                IngressosDisponiveis = 45
            },
            new Sessao
            {
                IdSessao = 102,
                IdFilme = 1,
                IdSala = 2,
                NomeSala = "Sala VIP 2",
                DataHora = DateTime.Now.AddDays(1).AddHours(2),
                Preco = 52.00m,
                Status = 1,
                IngressosDisponiveis = 20
            }
        };

        [HttpGet("Filme")]
        public IActionResult GetFilmes() => Ok(Filmes);

        [HttpGet("Categoria")]
        public IActionResult GetCategorias() => Ok(Categorias);

        [HttpGet("CategoriaFilme")]
        public IActionResult GetCategoriaFilme() => Ok(CategoriasFilmes);

        [HttpGet("Sessao/filme/{idFilme}")]
        public IActionResult GetSessoesPorFilme(int idFilme)
        {
            var resultado = Sessoes.Where(s => s.IdFilme == idFilme).ToList();
            return Ok(resultado);
        }

        [HttpPost("Venda")]
        public IActionResult RealizarVenda([FromBody] VendaRequest venda)
        {
            var sessao = Sessoes.FirstOrDefault(s => s.IdSessao == venda.IdSessao);
            if (sessao == null)
                return NotFound(new { mensagem = "Sessão não encontrada." });

            if (venda.Quantidade < 1 || venda.Quantidade > sessao.IngressosDisponiveis)
                return BadRequest(new { mensagem = "Quantidade de ingressos inválida ou indisponível." });

            sessao.IngressosDisponiveis -= venda.Quantidade;
            return Ok(new { mensagem = "Compra realizada com sucesso!", ingressosRestantes = sessao.IngressosDisponiveis });
        }
    }
}
