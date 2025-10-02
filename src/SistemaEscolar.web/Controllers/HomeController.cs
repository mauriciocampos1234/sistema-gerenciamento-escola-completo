using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.web.Models;

namespace SistemaEscolar.web.Controllers
{
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
