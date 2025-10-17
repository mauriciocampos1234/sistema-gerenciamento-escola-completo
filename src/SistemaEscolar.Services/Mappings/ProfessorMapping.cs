
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
                UsuarioId = usuarioId

            };
            return professor;
        }

        // Retornar um ProfessorResult
        public static ProfessorResult MapToProfessorResult(this Professor professor)
            {
            var result = new ProfessorResult
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Email = professor.Email,
                Login = professor.Usuario?.Login,
                Senha = professor.Usuario?.Senha,
                //UsuarioId = professor.UsuarioId
            };
            return result;
        }

        
        public static Professor MapToProfessor(this EditarProfessorRequest request)
        {
            var professor = new Professor
            {
                Id = request.Id,
                Nome = request.Nome!,
                Email = request.Email
            };
            return professor;
        }
    }
}
