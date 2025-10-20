using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.web.Models.Aluno
{
    public class EditarViewModel
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; } = null!;

        [Required]
        [Display(Name = "Senha")]
        public string Senha { get; set; } = null!;

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = null!;

        [Display(Name = "Email")]
        [EmailAddress]
        public string? Email { get; set; }
    }
}