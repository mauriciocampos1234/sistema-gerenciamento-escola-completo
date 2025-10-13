using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Services;
using SistemaEscolar.web.Mappings;
using SistemaEscolar.web.Models.Professor;

namespace SistemaEscolar.web.Controllers
{
    [Route("professor")] //Rota base para todas as ações deste controlador
    [Authorize] //Exige que só, no caso o usuário(Porfessor) esteja autenticado para acessar qualquer ação deste controlador
    public class ProfessorController : Controller
    {
        private readonly IProfessorService _professorService; // Declaração da variável de serviço de professor
        
        // Construtor que recebe as dependências necessárias via injeção de dependência no parametro
        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService; // Inicializa a variável de serviço de professor(Injeção de dependência)
        }


        // GET: Professor/Criar (Ao carregar a página de criação de professor)
        [Route("criar")]
        public IActionResult Criar()
        {
            
            return View();
        }


        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(CriarViewModel model)
        {
            // Verifica se o modelo recebido é válido
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Primeira Lógica para criar um professor usando o serviço de professor e os dados do modelo (ViewModel) fornecido pelo usuário
            //var result = _professorService.Criar(new Services.Models.Professor.CriarProfessorRequest
            //{
            //Login = model.Login!,
            //Senha = model.Senha!,
            //Nome = model.Nome!,
            //Email = model.Email!
            //});

            // Usando o Mapping para mapear o ViewModel para o Request
            var result = _professorService.Criar(model.MapToCriarProfessorRequest());

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);
                return View(model);
            }



            return RedirectToAction("Index", "Home");

        }
    }
}
