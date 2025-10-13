namespace SistemaEscolar.Repositories.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string Login { get; set; }

        public required string Senha { get; set; }

        public int FuncaolId { get; set; } // Corrigido: tornada pública para acesso ao set
    }
}
