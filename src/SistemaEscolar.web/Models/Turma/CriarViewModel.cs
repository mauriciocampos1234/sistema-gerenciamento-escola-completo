using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.web.Models.Turma
{
    public class CriarViewModel
    {
        [Required(ErrorMessage = "O campo Ano é obrigatório.")]
        public int? Ano { get; set; }

        [Required(ErrorMessage = "O campo Semestre é obrigatório.")]
        public int Semestre { get; set; }

        [Required(ErrorMessage = "O campo Professor é obrigatório.")]
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = "O campo Nível é obrigatório.")]
        public string? Nivel { get; set; }

        [Required(ErrorMessage = "O campo Período é obrigatório.")]
        public string? Periodo { get; set; }


        // Listas para popular os dropdowns(elementos de interface de usuário que mostram uma lista de opções quando clicados na view)
        public List<SelectListItem>? Semestres { get; set; }

        public List<SelectListItem>? Professores { get; set; }

    }
}
