using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alaska.API.Data;
using Alaska.API.Models;

namespace Alaska.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase // <-- Abre a classe aqui
    {
        private readonly AlaskaContext _context;

        public VendaController(AlaskaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetVendas()
        {
            var vendas = await _context.Vendas
                .OrderByDescending(v => v.DataCompra)
                .ToListAsync();
            return Ok(vendas);
        }

        [HttpPost] // <-- Deve ficar DENTRO das chaves da classe VendaController
        public async Task<IActionResult> RealizarVenda([FromBody] VendaRegistro venda)
        {
            if (venda.Quantidade < 1)
                return BadRequest(new { mensagem = "Quantidade inválida." });

            venda.DataCompra = DateTime.Now;
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Compra realizada com sucesso!", venda });
        }
    } 
}
