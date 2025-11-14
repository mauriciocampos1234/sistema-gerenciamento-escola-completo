namespace SistemaEscolar.Services.Models.Aluno
{
    // Remova esta definição duplicada se já existir outra classe AlunoResult no mesmo namespace.
    // Certifique-se de que só exista UMA definição de AlunoResult no namespace SistemaEscolar.Services.Models.Aluno.
    public class AlunoResult
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public string? Login { get; set; }

        public string? Senha { get; set; }
    }
}
