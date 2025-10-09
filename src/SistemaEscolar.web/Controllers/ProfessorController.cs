using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.web.Models.Professor;
using System;

namespace SistemaEscolar.web.Controllers
{
    [Route("professor")] //Rota base para todas as ações deste controlador
    [Authorize] //Exige que só, no caso o usuário(Porfessor) esteja autenticado para acessar qualquer ação deste controlador
    public class ProfessorController : Controller
    {
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

            // Lógica para criar um professor
            


            return RedirectToAction("Index", "Home");

        }
    }
}
