using SistemaEscolar.Services.Models.Aluno;
using SistemaEscolar.web.Models.Aluno;

namespace SistemaEscolar.web.Mappings
{
    public static class AlunoMapping
    {
        public static CriarAlunoRequest MapToCriarAlunoRequest(this CriarViewModel model)
        {
            return new CriarAlunoRequest
            {
                Login = model.Login,
                Senha = model.Senha,
                Nome = model.Nome,
                Email = model.Email
            };
        }

        public static EditarAlunoRequest MapToEditarAlunoRequest(this EditarViewModel model)
        {
            return new EditarAlunoRequest
            {
                Id = model.Id,
                UsuarioId = model.UsuarioId,
                Login = model.Login,
                Senha = model.Senha,
                Nome = model.Nome,
                Email = model.Email
            };
        }

        public static ListarViewModel MapToListarViewModel(this AlunoResult model)
        {
            return new ListarViewModel
            {
                Id = model.Id,
                Nome = model.Nome,
                Email = model.Email,
                Login = model.Login
            };
        }

        public static EditarViewModel MapToEditarViewModel(this AlunoResult model)
        {
            return new EditarViewModel
            {
                Id = model.Id,
                UsuarioId = model.UsuarioId,
                Nome = model.Nome,
                Email = model.Email,
                Login = model.Login ?? string.Empty,
                Senha = model.Senha ?? string.Empty
            };
        }
    }
}