using SistemaEscolar.Services.Enums;

namespace SistemaEscolar.Services.Models.Usuario
{
    public class ValidarLoginResult : BaseResult
    {
        public UsuarioResult? Usuario { get; set; }
    }

    public class UsuarioResult
    {
        public int Id { get; set; }

        public required string Login { get; set; }

        public Funcao Funcao { get; set; }
    }
}