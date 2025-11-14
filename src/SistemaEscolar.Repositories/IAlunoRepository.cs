using SistemaEscolar.Repositories.Entities;

namespace SistemaEscolar.Repositories
{
    public interface IAlunoRepository
    {
        int? Inserir(Aluno aluno);

        int? Atualizar(Aluno aluno);

        int? Apagar(int id);

        IList<Aluno> Listar();

        IList<Aluno> ListarPorTurma(int turmaId);

        IList<Aluno> ListarPorProfessor(int usuarioId);

        IList<Aluno> ListarPorAluno(int usuarioId);

        Aluno? ObterPorId(int id);

        Aluno? ObterPorUsuarioId(int usuarioId);
    }
}