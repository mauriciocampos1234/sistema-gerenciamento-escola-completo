using SistemaEscolar.Services.Models.Boletim;

namespace SistemaEscolar.web.Models.Boletim
{
    public class EditarViewModel
    {
        public int BoletimId { get; set; }

        public int? TurmaId { get; set; }

        public int? AlunoId { get; set; } // adicionado para redirecionar corretamente

        public string? NomeAluno { get; set; }

        public string? DescricaoTurma { get; set; }

        public decimal? NotaBim1Escrita { get; set; }

        public decimal? NotaBim1Leitura { get; set; }

        public decimal? NotaBim1Conversacao { get; set; }

        public decimal? NotaBim1Final { get; set; }

        public decimal? NotaBim2Escrita { get; set; }

        public decimal? NotaBim2Leitura { get; set; }

        public decimal? NotaBim2Conversacao { get; set; }

        public decimal? NotaBim2Final { get; set; }

        public decimal? NotaFinalSemestre { get; set; }

        public int? FaltasSemestre { get; set; }

        public bool PermiteEdicao { get; set; }

        internal AtualizarBoletimRequest MapToAtualizarBoletimRequest()
        {
            return new AtualizarBoletimRequest
            {
                BoletimId = BoletimId,
                NotaBim1Escrita = NotaBim1Escrita,
                NotaBim1Leitura = NotaBim1Leitura,
                NotaBim1Conversacao = NotaBim1Conversacao,
                NotaBim1Final = NotaBim1Final,
                NotaBim2Escrita = NotaBim2Escrita,
                NotaBim2Leitura = NotaBim2Leitura,
                NotaBim2Conversacao = NotaBim2Conversacao,
                NotaBim2Final = NotaBim2Final,
                NotaFinalSemestre = NotaFinalSemestre,
                FaltasSemestre = FaltasSemestre
            };
        }
    }
}
