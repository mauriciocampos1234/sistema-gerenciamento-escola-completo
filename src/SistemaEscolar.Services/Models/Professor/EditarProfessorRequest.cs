namespace SistemaEscolar.Services.Models.Professor
{
    public class EditarProfessorRequest
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string Login { get; set; }

        public required string Senha { get; set; }
    }
}
