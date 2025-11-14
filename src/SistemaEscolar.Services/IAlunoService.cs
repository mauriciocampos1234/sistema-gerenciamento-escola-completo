using SistemaEscolar.Services.Models.Aluno;

namespace SistemaEscolar.Services
{
    public interface IAlunoService
    {
        CriarAlunoResult Criar(CriarAlunoRequest request);

        EditarAlunoResult Editar(EditarAlunoRequest request);

        ExcluirAlunoResult Excluir(int id);

        IList<AlunoResult> Listar();

        IList<AlunoResult> ListarPorTurma(int turmaId);

        IList<AlunoResult> ListarPorProfessor(int usuarioId);

        IList<AlunoResult> ListarPorAluno(int usuarioId);

        AlunoResult? ObterPorId(int id);

        AlunoResult? ObterPorUsuarioId(int usuarioId);
        IList<AlunoResult>? ListarPorProfessor(string usuarioId);
    }
}
