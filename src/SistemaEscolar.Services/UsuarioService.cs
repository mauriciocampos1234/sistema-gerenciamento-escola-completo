using SistemaEscolar.Services.Models.Usuario;

namespace SistemaEscolar.Services
{
    //Definição da interface IUsuarioService
    public interface IUsuarioService
    {
        ValidarLoginResult ValidarLogin(string email, string senha);
    }
    public class UsuarioService : IUsuarioService //Implementação da interface IUsuarioService
    {
        public ValidarLoginResult ValidarLogin(string email, string senha)
        {
            var result = new ValidarLoginResult();

            result.Sucesso = true;

            return result;
        }
    }
}
