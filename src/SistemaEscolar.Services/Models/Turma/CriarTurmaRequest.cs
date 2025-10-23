namespace SistemaEscolar.Services.Models.Turma
{
    public class CriarTurmaRequest
    {
        public required int Ano { get; set; }

        public required int Semestre { get; set; }

        public required int ProfessorId { get; set; }

        public required string Periodo { get; set; }
        
        public required string Nivel { get; set; }

    }
}
