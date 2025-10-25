namespace SistemaEscolar.web.Models.Menu
{
    public class MenuViewModel
    {
        public Menu Ativo { get; set; }
    }

    //Enum para o menu
    public enum Menu
    { 
        Home,
        Professor,
        Aluno,
        Turma
    }
}
