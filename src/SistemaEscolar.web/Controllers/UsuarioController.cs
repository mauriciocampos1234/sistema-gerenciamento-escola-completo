using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Services;
using SistemaEscolar.web.Models.Usuario;
using System.Security.Claims;

namespace SistemaEscolar.web.Controllers
{
    public class UsuarioController : Controller
    {
        // Desaclopando a UsuarioService.cs da UsuarioController.cs (Reconhecendo somente Contratos(Interface)
        // private readonly só pode ser criada no construtor da classe UsuarioController e não pode ser alterada depois
        private readonly IUsuarioService _usuarioService; //Declara uma variável privada para o serviço de usuário e o _ indica que é uma variável privada(Só essa classe consegue enxergar, Padrão de código)
        public UsuarioController(IUsuarioService usuarioService) //Injeção de dependência do serviço de usuário (IUsuarioService)
        {
            _usuarioService = usuarioService; //Atribui o serviço injetado a uma variável privada como parâmetro do construtor
        }


        [Route("login")] //Aqui somente irá renderizar a view (Tela de login) quando for chamado o /login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost] //Indica que esse método responde a requisições HTTP POST
        [Route("login")] //Aqui vai disparar quando for chamado o /login quando o usuário clicar no botão de login após ter inserido seus dados
        public IActionResult Login(LoginViewModel model) //IActionResult Login recebe o parametro model do tipo LoginViewModel (MVC)
        {
            if (!ModelState.IsValid) //Verifica se o modelo recebido é válido (Se passou nas validações)
            {
                return View(model); //Se não for válido, retorna a mesma view de login com os dados preenchidos (model) para que o usuário possa corrigir
            }

            //Validar e Instânciar  o UsuarioService.cs criado em SistemaEscolar.Servives na UsuarioController.cs
            //var usuarioService = new Services.UsuarioService(); //Instancia o serviço de usuário (Não é mais necessário por causa da injeção de dependência)
            var result = _usuarioService.ValidarLogin(model.Usuario!, model.Senha!); //Chama o método ValidarLogin do serviço, passando o usuário e a senha e o ! nos atributos do model indica que eles não podem ser nulos (nullable reference type)

            if (!result.Sucesso) //Verifica se a validação do login foi malsucedida
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!); //Adiciona um erro ao ModelState para exibir na view (string.Empty indica que o erro não está associado a um campo específico) e o ! indica que MensagemErro não pode ser nulo
                return View(model); //Retorna a mesma view de login com os dados preenchidos (model) para que o usuário possa tentar novamente
            }


            //Montagem da mecanica de autenticação(lOGIN) (Fazer a validação do usuário no banco de dados)
            var clains = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Usuario!) //ClaimTypes.NameIdentifier é o identificador único do usuário (Pode ser o ID ou o nome de usuário)
                };

            var claimsIdentity = new ClaimsIdentity(clains, CookieAuthenticationDefaults.AuthenticationScheme); //Cria uma identidade de usuário com os claims e o esquema de autenticação baseado em cookies

            var properties = new AuthenticationProperties
            {
                AllowRefresh = true, //Permite que o cookie de autenticação seja renovado
                IsPersistent = model.LembrarMe //Define se o cookie de autenticação deve ser persistente (LembrarMe)
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties); //Realiza o login do usuário, criando o cookie de autenticação

            return RedirectToAction("Index", "Home"); //Se for válido, redireciona para a ação Index do controlador Home
        }

        [HttpPost]
        [Route("logout")] //Rota para o logout
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //Realiza o logout do usuário, removendo o cookie de autenticação
            return RedirectToAction("Login", "Usuario"); //Redireciona para a tela de login após o logout
        }
    }
}
