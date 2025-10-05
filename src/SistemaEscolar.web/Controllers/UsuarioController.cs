using Microsoft.AspNetCore.Mvc;

namespace SistemaEscolar.web.Controllers
{
    public class UsuarioController : Controller
    {
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
