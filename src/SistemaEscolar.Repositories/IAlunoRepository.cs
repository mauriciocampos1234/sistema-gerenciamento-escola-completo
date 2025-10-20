using SistemaEscolar.Repositories.Entities;

namespace SistemaEscolar.Repositories
{
    public interface IAlunoRepository
    {
        int? Inserir(Aluno aluno);
        int? Atualizar(Aluno aluno);
        IList<Aluno> Listar();
        Aluno? ObterPorId(int id);
        int? Apagar(int id);

        // Opcional: caso você já tenha relacionamento com turmas
        //IList<Aluno> ListarPorTurma(int turmaId);
    }
}