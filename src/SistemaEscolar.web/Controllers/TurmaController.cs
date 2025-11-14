using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Services;
using SistemaEscolar.Services.Models.Turma;
using SistemaEscolar.web.Mappings;
using SistemaEscolar.web.Models.Turma;
using System.Security.Claims;

namespace SistemaEscolar.Web.Controllers
{
    [Authorize]
    [Route("turma")]
    public class TurmaController : Controller
    {
        private readonly ITurmaService _turmaService;
        private readonly IProfessorService _professorService;
        private readonly IAlunoService _alunoService;

        public TurmaController(ITurmaService turmaService, IProfessorService professorService, IAlunoService alunoService)
        {
            _turmaService = turmaService;
            _professorService = professorService;
            _alunoService = alunoService;
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            var isAdmin = User.IsInRole("Administrador");
            var isProfessor = User.IsInRole("Professor");
            var isAluno = User.IsInRole("Aluno");

            var model = new ListarViewModel
            {
                ExibirBotaoInserir = isAdmin,
                ExibirBotaoEditar = isAdmin || isProfessor, // professor pode abrir tela para lançar notas
                ExibirBotaoBoletim = isAluno // apenas aluno vê link direto para seu boletim na listagem
            };

            IList<TurmaResult> turmas;

            if (isProfessor)
            {
                var usuarioId = ObterUsuarioId();
                turmas = _turmaService.ListarPorProfessor(usuarioId);
            }
            else if (isAluno)
            {
                var usuarioId = ObterUsuarioId();
                turmas = _turmaService.ListarPorAluno(usuarioId);
                model.AlunoId = ObterAlunoId(usuarioId);
            }
            else
            {
                turmas = _turmaService.Listar();
            }

            model.Turmas = turmas.Select(t => t.MapToTurmaViewModel()).ToList();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        [Route("criar")]
        public IActionResult Criar()
        {
            var vm = new CriarViewModel
            {
                Semestres = PreencherSemestres(),
                Professores = PreencherProfessores()
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [Route("criar")]
        public IActionResult Criar(CriarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Semestres = PreencherSemestres();
                model.Professores = PreencherProfessores();
                return View(model);
            }

            var request = model.MapToCriarTurmaRequest();
            var result = _turmaService.Criar(request);
            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);
                model.Semestres = PreencherSemestres();
                model.Professores = PreencherProfessores();
                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var turma = _turmaService.ObterPorId(id);
            if (turma == null) return RedirectToAction("Listar");

            var vm = turma.MapToEditarViewModel();
            vm.Semestres = PreencherSemestres();
            vm.Professores = PreencherProfessores();
            vm.PodeEditarApagarTurma = User.IsInRole("Administrador"); // somente admin altera estrutura

            var alunosTurma = _alunoService.ListarPorTurma(id);
            vm.AlunosTurma = alunosTurma.Select(a => a.MapToAlunoTurmaViewModel()).ToList();

            var alunos = _alunoService.Listar();
            vm.Alunos = alunos.Select(a => a.MapToAlunoTurmaViewModel()).ToList();

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [Route("editar/{id}")]
        public IActionResult Editar(EditarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Semestres = PreencherSemestres();
                model.Professores = PreencherProfessores();
                return View(model);
            }

            var request = model.MapToEditarTurmaRequest();
            var result = _turmaService.Editar(request);
            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);
                model.Semestres = PreencherSemestres();
                model.Professores = PreencherProfessores();
                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [Route("excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            var result = _turmaService.Excluir(id);
            if (!result.Sucesso)
            {
                TempData["MensagemErro"] = result.MensagemErro;
            }
            return RedirectToAction("Listar");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [Route("desassociar-aluno")]
        public IActionResult DesassociarAluno(int turmaId, int alunoId)
        {
            var result = _turmaService.DesassociarAlunoTurma(alunoId, turmaId);
            if (!result.Sucesso)
            {
                TempData["MensagemErro"] = result.MensagemErro;
            }
            return RedirectToAction("Editar", new { id = turmaId });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [Route("associar-alunos")]
        public IActionResult AssociarAlunos(int turmaId)
        {
            // Coleta checkboxes aluno_{id}
            var selecionados = Request.Form.Keys
                .Where(k => k.StartsWith("aluno_") && Request.Form[k] == "on")
                .Select(k => int.Parse(k.Split('_')[1]))
                .ToList();

            foreach (var alunoId in selecionados)
            {
                _turmaService.AssociarAlunoTurma(alunoId, turmaId);
            }

            return RedirectToAction("Editar", new { id = turmaId });
        }

        private int ObterUsuarioId()
        {
            var idStr = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            return int.TryParse(idStr, out var id) ? id : 0;
        }

        private int? ObterAlunoId(int usuarioId)
        {
            var aluno = _alunoService.ObterPorUsuarioId(usuarioId);
            return aluno?.Id;
        }

        private List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> PreencherSemestres() =>
            new()
            {
                new("1º Semestre", "1"),
                new("2º Semestre", "2"),
            };

        private List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> PreencherProfessores()
        {
            var profs = _professorService.Listar();
            return profs
                .Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = p.Nome, Value = p.Id.ToString() })
                .ToList();
        }
    }
}
