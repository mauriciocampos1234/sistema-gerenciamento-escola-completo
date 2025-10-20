namespace SistemaEscolar.web.Models.Aluno
{
    public class ListarViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Email { get; set; }
        public string? Login { get; set; }
    }
}