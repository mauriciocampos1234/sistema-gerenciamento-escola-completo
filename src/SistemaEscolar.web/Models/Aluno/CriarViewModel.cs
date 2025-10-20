using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.web.Models.Aluno
{
    public class CriarViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string? Login { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string? Senha { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = null!;

        [Display(Name = "Email")]
        [EmailAddress]
        public string? Email { get; set; }
    }
}