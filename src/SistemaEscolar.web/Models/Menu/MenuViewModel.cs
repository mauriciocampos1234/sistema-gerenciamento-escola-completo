namespace SistemaEscolar.web.Models.Menu
{
    public enum Menu
    {
        Home,
        Aluno,
        Professor,
        Turma
    }

    public class MenuViewModel
    {
        public Menu Ativo { get; set; }
        public bool MenuProfessorVisivel { get; set; }
    }
}
