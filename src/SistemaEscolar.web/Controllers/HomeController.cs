using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaEscolar.web.Controllers
{
    // Autorização somente para usuários autenticados (F5 para abrir e testar para tentar acessar a home. Resulado tem que ser não acessado)
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //throw new Exception("Ocorreu um erro ao carregar a homePage"); //Usado para testar a pagina de erro personalizada

            return View();
        }
    }
}