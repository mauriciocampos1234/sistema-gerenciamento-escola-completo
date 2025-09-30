using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.web.Models.Erro;

namespace SistemaEscolar.web.Controllers
{
    public class ErroController : Controller
    {
        public IActionResult Index()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var model = new ErroViewModel
            {
                // Operador Ternário (? = Se) (: = Caso ao contrário)
                MensagemErro = exceptionHandlerFeature == null ? "Erro inesperado" : exceptionHandlerFeature.Error.Message
            };
            
            return View(model);
        }
    }
}
