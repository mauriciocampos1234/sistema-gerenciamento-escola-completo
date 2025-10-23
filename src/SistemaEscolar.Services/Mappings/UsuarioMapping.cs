using SistemaEscolar.Repositories.Entities;
using SistemaEscolar.Services.Enums;
using SistemaEscolar.Services.Models.Aluno;
using SistemaEscolar.Services.Models.Professor;
using SistemaEscolar.Services.Models.Usuario;


namespace SistemaEscolar.Services.Mappings
{
    public static class UsuarioMapping
    {
        public static Usuario MapToUsuario(this CriarProfessorRequest request)
        {
            var usuario = new Usuario
            {
                Login = request.Login,
                Senha = request.Senha,
                FuncaoId = (int)Funcao.Professor
            };

            return usuario;
        }

        public static Usuario MapToUsuario(this EditarProfessorRequest request)
        {
            var usuario = new Usuario
            {
                Id = request.UsuarioId,
                Login = request.Login,
                Senha = request.Senha
            };

            return usuario;
        }

        public static Usuario MapToUsuario(this CriarAlunoRequest request)
        {
            var usuario = new Usuario
            {
                Login = request.Login,
                Senha = request.Senha,
                FuncaoId = (int)Funcao.Aluno
            };

            return usuario;
        }

        public static Usuario MapToUsuario(this EditarAlunoRequest request)
        {
            var usuario = new Usuario
            {
                Id = request.UsuarioId,
                Login = request.Login,
                Senha = request.Senha
            };

            return usuario;
        }

        public static UsuarioResult MapToUsuarioResult(this Usuario usuario)
        {
            var usuarioResult = new UsuarioResult
            {
                Id = usuario.Id,
                Login = usuario.Login,
                Funcao = (Funcao)usuario.FuncaoId
            };

            return usuarioResult;
        }
    }
}