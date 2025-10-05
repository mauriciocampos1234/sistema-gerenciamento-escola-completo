using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.web.Models.Usuario
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo usuário é obrigatório.")]
        public string? Usuario { get; set; } //? indica que a propriedade pode ser nula (nullable reference type)

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string? Senha { get; set; } //? indica que a propriedade pode ser nula (nullable reference type)

        public bool LembrarMe { get; set; } //Não precisa ser nullable, pois o valor padrão de um bool é false
    }
}
