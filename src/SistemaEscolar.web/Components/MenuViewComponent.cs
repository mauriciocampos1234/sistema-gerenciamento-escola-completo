using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.web.Models.Menu;

namespace SistemaEscolar.web.Components
{
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menu = new MenuViewModel
            {
                Ativo = ViewData["Menu"] as Menu? ?? Menu.Home,
                MenuProfessorVisivel = User.IsInRole("Administrador")
            };

            return View(menu);
        }
    }
}
