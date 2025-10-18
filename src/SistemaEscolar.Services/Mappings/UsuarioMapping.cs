using SistemaEscolar.Repositories.Entities;
using SistemaEscolar.Services.Enums;
using SistemaEscolar.Services.Models.Professor;

namespace SistemaEscolar.Services.Mappings
{
    public static class UsuarioMapping
    {
        public static Usuario MapToUsuario(this CriarProfessorRequest request)
        {
            var usuario = new Usuario
            {
                Login = request.Login!,
                Senha = request.Senha!,
                FuncaolId = (int)Funcao.Professor // Definindo a função como Professor
            };
            return usuario;
        }

        public static Usuario MapToUsuario(this EditarProfessorRequest request)
        {
            var usuario = new Usuario
            {
                Id = request.UsuarioId,
                Login = request.Login!,
                Senha = request.Senha!
            };
            return usuario;
        }
    }
}
