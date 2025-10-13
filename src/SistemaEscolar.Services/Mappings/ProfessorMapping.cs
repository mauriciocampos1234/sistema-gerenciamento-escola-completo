
using SistemaEscolar.Repositories.Entities;
using SistemaEscolar.Services.Models.Professor;

namespace SistemaEscolar.Services.Mappings
{
    public static class ProfessorMapping
    {
        public static Professor MapToProfessorR(this CriarProfessorRequest request, int usuarioId)
        {
            var professor = new Professor
            {
                Nome = request.Nome!,
                Email = request.Email,
                UsuarioId = usuarioId.ToString()

            };
            return professor;
        }
    }
}
