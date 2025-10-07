using SistemaEscolar.Services.Models.Usuario;

namespace SistemaEscolar.Services
{
    public class UsuarioService
    {
        public ValidarLoginResult ValidarLogin(string email, string senha)
        {
            var result = new ValidarLoginResult();

            result.Sucesso = true;

            return result;
        }
    }
}
