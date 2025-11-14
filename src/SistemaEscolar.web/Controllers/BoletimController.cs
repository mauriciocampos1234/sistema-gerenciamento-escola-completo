using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Services;
using SistemaEscolar.web.Mappings;
using SistemaEscolar.web.Models.Boletim;

namespace SistemaEscolar.Web.Controllers
{
    [Authorize]
    [Route("boletim")]
    public class BoletimController : Controller
    {
        private readonly IBoletimService _boletimService;
        private readonly IAlunoService _alunoService;

        public BoletimController(IBoletimService boletimService, IAlunoService alunoService)
        {
            _boletimService = boletimService;
            _alunoService = alunoService;
        }

        [HttpGet]
        [Route("editar")]
        public IActionResult Editar(int turmaId, int? alunoId)
        {
            int resolvedAlunoId = alunoId ?? 0;

            // Se alunoId não informado e usuário for Aluno, descobrir pelo usuário logado
            if ((resolvedAlunoId == 0) && User.IsInRole("Aluno"))
            {
                var usuarioIdStr = User.FindFirst("Id")?.Value;
                if (int.TryParse(usuarioIdStr, out var usuarioId))
                {
                    var aluno = _alunoService.ObterPorUsuarioId(usuarioId);
                    if (aluno != null)
                    {
                        resolvedAlunoId = aluno.Id;
                    }
                }
            }

            if (resolvedAlunoId == 0)
            {
                return RedirectToAction("Editar", "Turma", new { id = turmaId });
            }

            var boletim = _boletimService.ObterBoletimPorAlunoTurma(resolvedAlunoId, turmaId);
            if (boletim == null)
            {
                return RedirectToAction("Editar", "Turma", new { id = turmaId });
            }

            var vm = boletim.MapToEditarViewModel();
            vm.PermiteEdicao = User.IsInRole("Administrador") || User.IsInRole("Professor");

            return View(vm);
        }

        [HttpPost]
        [Route("editar")]
        [Authorize(Roles = "Administrador,Professor")]
        public IActionResult Editar(EditarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = model.AtualizarBoletimRequest();
            var result = _boletimService.Atualizar(request);
            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);
                return View(model);
            }

            // Agora redireciona para edição da turma
            return RedirectToAction("Editar", "Turma", new { id = model.TurmaId });
        }
    }
}
