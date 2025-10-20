using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Services;
using SistemaEscolar.web.Mappings;
using SistemaEscolar.web.Models.Aluno;

namespace SistemaEscolar.web.Controllers
{
    [Route("aluno")]
    public class AlunoController : Controller
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [Route("criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(CriarViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _alunoService.Criar(model.MapToCriarAlunoRequest());

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro ?? "Erro ao criar aluno.");
                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [Route("listar")]
        public IActionResult Listar()
        {
            var alunos = _alunoService.Listar();
            var vm = alunos.Select(a => a.MapToListarViewModel()).ToList();
            return View(vm);
        }

        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var aluno = _alunoService.ObterPorId(id);
            var model = aluno?.MapToEditarViewModel();
            return View(model);
        }

        [HttpPost]
        [Route("editar/{id}")]
        public IActionResult Editar(EditarViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var request = model.MapToEditarAlunoRequest();
            var result = _alunoService.Editar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro ?? "Erro ao editar aluno.");
                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [HttpPost]
        [Route("excluir/{id}")]
        public IActionResult Excluir(EditarViewModel model)
        {
            var result = _alunoService.Excluir(model.Id);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro ?? "Erro ao excluir aluno.");
                return View("Editar", model);
            }

            return RedirectToAction("Listar");
        }
    }
}