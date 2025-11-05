using EnglishNow.Services;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.web.Mappings;

namespace SistemaEscolar.web.Controllers
{
    [Route("boletim")]
    public class BoletimController : Controller
    {
        private readonly IBoletimService _boletimService;

        public BoletimController(IBoletimService boletimService)
        {
            _boletimService = boletimService;
        }

        [Route("editar/{alunoId}/{turmaId}")]
        public IActionResult Editar(int alunoId, int turmaId)
        {
            var result = _boletimService.ObterBoletimPorAlunoTurma(alunoId, turmaId);

            if (result == null)
            {
                RedirectToAction("Editar", "Turma", new { id = turmaId });
            }

            var model = result!.MapToEditarViewModel();

            //model.PermitirEdicao = User.IsInRole("Administrador") || User.IsInRole("Professor");

            return View(model);
        }


    }
}
