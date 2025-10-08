using SistemaEscolar.Repositories;
using SistemaEscolar.Services.Models.Usuario;

namespace SistemaEscolar.Services
{
    //Definição da interface IUsuarioService
    public interface IUsuarioService
    {
        ValidarLoginResult ValidarLogin(string login, string senha);
    }
    public class UsuarioService : IUsuarioService //Implementação da interface IUsuarioService
    {
        //Gerando construtor para injeção de dependência do repositório
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public ValidarLoginResult ValidarLogin(string login, string senha)
        {
            var result = new ValidarLoginResult();

            //result.Sucesso = true; //Simulação de login bem-sucedido

            // Agora é a hora de implementar a lógica real de validação de login
            if (String.IsNullOrWhiteSpace(login) || String.IsNullOrWhiteSpace(senha))
            {
                result.Sucesso = false;
                result.MensagemErro = "E-mail e senha são obrigatórios.";
                return result;
            }

            //Aqui você chamaria o repositório para obter o usuário do banco de dados
            //var usuario = new UsuarioRepository().ObterPorLogin(login);

            //Agora usando injeção de dependência
            var usuario = _usuarioRepository.ObterPorLogin(login);

            //Verificando se o usuário foi encontrado
            if (usuario == null)
            {
                result.Sucesso = false;
                result.MensagemErro = "Usuário não encontrado.";
                return result;
            }

            //Verificando se a senha está correta
            if (usuario.Senha != senha)
            {
                result.Sucesso = false;
                result.MensagemErro = "Senha incorreta.";
                return result;
            }
            //Se tudo estiver ok, o login é bem-sucedido
            result.Sucesso = true;

            return result;
        }
    }
}
