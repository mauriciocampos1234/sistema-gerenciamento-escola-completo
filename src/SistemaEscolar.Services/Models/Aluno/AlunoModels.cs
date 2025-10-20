using System.Collections.Generic;

namespace SistemaEscolar.Services.Models.Aluno
{
    public class CriarAlunoRequest
    {
        public string? Login { get; set; }
        public string? Senha { get; set; }
        public string Nome { get; set; } = null!;
        public string? Email { get; set; }
    }

    public class EditarAlunoRequest
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Login { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string? Email { get; set; }
    }

    public class AlunoResult
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = null!;
        public string? Email { get; set; }
        public string? Login { get; set; }
        public string? Senha { get; set; }
    }

    public class CriarAlunoResult
    {
        public bool Sucesso { get; set; } = false;
        public string? MensagemErro { get; set; }
    }

    public class EditarAlunoResult
    {
        public bool Sucesso { get; set; } = false;
        public string? MensagemErro { get; set; }
    }

    public class ExcluirAlunoResult
    {
        public bool Sucesso { get; set; } = false;
        public string? MensagemErro { get; set; }
    }
}