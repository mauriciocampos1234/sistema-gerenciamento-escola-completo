using SistemaEscolar.Services.Models.Turma;
using SistemaEscolar.web.Models.Turma;

namespace SistemaEscolar.web.Mappings
{
    public static class TurmaMapping
    {
        public static CriarTurmaRequest MapToCriarTurmaRequest(this CriarViewModel model)
        {
            var request = new CriarTurmaRequest
            {
                Ano = model.Ano!.Value,
                Semestre = model.Semestre,
                ProfessorId = model.ProfessorId,
                Periodo = model.Periodo!,
                Nivel = model.Nivel!
            };

            return request;
        }

        public static TurmaViewModel MapToTurmaViewModel(this TurmaResult model)
        {
            var ViewModel = new TurmaViewModel
            {
                Id = model.Id,
                SemestreAno = $"{model.Semestre}° Semestre/{model.Ano}",
                Professor = model.ProfessorNome ?? string.Empty, // Corrigido para evitar atribuição nula
                Nivel = model.Nivel,
                Periodo = model.Periodo
            };
            return ViewModel;
        }

        public static EditarViewModel MapToEditarViewModel(this TurmaResult model)
        {
            var viewModel = new EditarViewModel
            {
                Id = model.Id,
                Ano = model.Ano,
                Semestre = model.Semestre,
                ProfessorId = model.ProfessorId,
                Nivel = model.Nivel,
                Periodo = model.Periodo,
                AlunosTurma = new List<EditarViewModel.AlunoTurmaViewModel>() // Adicionado para corrigir CS9035
            };

            return viewModel;
        }

        public static EditarTurmaRequest MapToEditarTurmaRequest(this EditarViewModel model)
        {
            var request = new EditarTurmaRequest
            {
                Id = model.Id,
                Ano = model.Ano,
                Semestre = model.Semestre,
                ProfessorId = model.ProfessorId,
                Nivel = model.Nivel,
                Periodo = model.Periodo
            };

            return request;
        }
    }
}