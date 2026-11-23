using Alaska.API.Enums;

namespace Alaska.API.Models;

public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public NivelUsuario Nivel { get; set; }
}