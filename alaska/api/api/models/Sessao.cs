namespace appdomateus.models
{
    public class Sessao
    {
        public int IdSessao { get; set; }
        public int IdFilme { get; set; }
        public int IdSala { get; set; }
        public string NomeSala { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public decimal Preco { get; set; }
        public int Status { get; set; }
        public int IngressosDisponiveis { get; set; }
    }
}
