namespace appdomateus.models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
        public int Duracao { get; set; }
        public int Classificacao { get; set; }
        public string DataLancamento { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
