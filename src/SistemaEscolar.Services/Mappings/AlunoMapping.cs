using SistemaEscolar.Repositories.Entities;
using SistemaEscolar.Services.Enums;
using SistemaEscolar.Services.Models.Aluno;

namespace SistemaEscolar.Services.Mappings
{
    public static class AlunoMapping
    {
        public static Repositories.Entities.Usuario MapToUsuario(this CriarAlunoRequest request)
        {
            return new Repositories.Entities.Usuario
            {
                Login = request.Login!,
                Senha = request.Senha!,
                FuncaolId = (int)Funcao.Aluno
            };
        }

        public static Repositories.Entities.Usuario MapToUsuario(this EditarAlunoRequest request)
        {
            return new Repositories.Entities.Usuario
            {
                Id = request.UsuarioId,
                Login = request.Login!,
                Senha = request.Senha!
            };
        }

        public static Repositories.Entities.Aluno MapToAlunoR(this CriarAlunoRequest request, int usuarioId)
        {
            return new Repositories.Entities.Aluno
            {
                Nome = request.Nome,
                Email = request.Email ?? string.Empty,
                UsuarioId = usuarioId
            };
        }

        public static Repositories.Entities.Aluno MapToAluno(this EditarAlunoRequest request)
        {
            return new Repositories.Entities.Aluno
            {
                Id = request.Id,
                Nome = request.Nome,
                Email = request.Email ?? string.Empty,
                UsuarioId = request.UsuarioId
            };
        }

        public static AlunoResult MapToAlunoResult(this Repositories.Entities.Aluno aluno)
        {
            return new AlunoResult
            {
                Id = aluno.Id,
                UsuarioId = aluno.UsuarioId,
                Nome = aluno.Nome,
                Email = aluno.Email,
                Login = aluno.Usuario?.Login,
                Senha = aluno.Usuario?.Senha
            };
        }
    }
}