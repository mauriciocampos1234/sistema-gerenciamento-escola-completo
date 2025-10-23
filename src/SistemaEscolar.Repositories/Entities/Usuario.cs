namespace SistemaEscolar.Repositories.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string Login { get; set; }

        public required string Senha { get; set; }

        public int FuncaoId { get; set; }
    }
}
