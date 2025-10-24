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
    }
}
