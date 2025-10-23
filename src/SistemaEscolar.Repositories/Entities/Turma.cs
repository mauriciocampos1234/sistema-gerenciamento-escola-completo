namespace SistemaEscolar.Repositories.Entities
{
    public class Turma
    {
        public int Id { get; set; }

        public int Semestre { get; set; }

        public int Ano { get; set; }

        public required string Periodo { get; set; }

        public required string Nivel { get; set; }

        public int ProfessorId { get; set; }

        public Professor? Professor { get; set; }
        //Não podemos chamar o nome do professor pois ela é uma Entitie de turma, dessa Forma vamos vincular uma Entitie(Professor) aqui na Entitie(Turma)
    }
}
