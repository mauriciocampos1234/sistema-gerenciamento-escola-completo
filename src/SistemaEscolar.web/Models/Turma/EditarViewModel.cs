using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.web.Models.Turma
{
    public class EditarViewModel
    {
        public int Id { get; set; }

        public int Ano { get; set; }

        public int Semestre { get; set; }

        public int ProfessorId { get; set; }

        public required string Nivel { get; set; }

        public required string Periodo { get; set; }

        // Listas para popular os dropdowns(elementos de interface de usuário que mostram uma lista de opções quando clicados na view)
        public List<SelectListItem>? Professores { get; set; }

        public List<SelectListItem>? Niveis { get; set; }

        public List<SelectListItem>? Semestres { get; set; } // Adiciona a propriedade faltante

        public IList<AlunoTurmaViewModel>? Alunos { get; set; } // Lista de alunos disponíveis para adicionar à turma

        //Model add alunos da turma
        public required IList<AlunoTurmaViewModel> AlunosTurma { get; set; }

        public class AlunoTurmaViewModel
        {
            public int Id { get; set; }

            public required string Nome { get; set; }

            public required string Email { get; set; }

            public required string Login { get; set; }
        }
    }
}
