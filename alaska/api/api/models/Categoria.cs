namespace appdomateus.models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }

    public class CategoriaFilme
    {
        public int IdFilme { get; set; }
        public int IdCategoria { get; set; }
    }
}
