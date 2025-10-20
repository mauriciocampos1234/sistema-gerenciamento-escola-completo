using SistemaEscolar.Services.Models.Aluno;
using System.Collections.Generic;

namespace SistemaEscolar.Services
{
    public interface IAlunoService
    {
        CriarAlunoResult Criar(CriarAlunoRequest request);
        EditarAlunoResult Editar(EditarAlunoRequest request);
        ExcluirAlunoResult Excluir(int id);
        IList<AlunoResult> Listar();
        AlunoResult? ObterPorId(int id);
    }
}