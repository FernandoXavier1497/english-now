using EnglishNow.Services.Models.Boletim;
using EnglishNow.Web.Models.Boletim;

namespace EnglishNow.Web.Mappings
{
    public static class BoletimMapping
    {
        public static BoletimItemViewModel MapToItemViewModel(this BoletimResult result)
        {
            return new BoletimItemViewModel
            {
                Id = result.Id,
                AlunoNome = result.AlunoNome,
                TurmaDescricao = result.TurmaDescricao,
                ProfessorNome = result.ProfessorNome,
                NotaBim1Escrita = result.NotaBim1Escrita,
                NotaBim1Leitura = result.NotaBim1Leitura,
                NotaBim1Conversacao = result.NotaBim1Conversacao,
                NotaBim1Final = result.NotaBim1Final,
                NotaBim2Escrita = result.NotaBim2Escrita,
                NotaBim2Leitura = result.NotaBim2Leitura,
                NotaBim2Conversacao = result.NotaBim2Conversacao,
                NotaBim2Final = result.NotaBim2Final,
                NotaFinalSemestre = result.NotaFinalSemestre,
                FaltasSemestre = result.FaltasSemestre
            };
        }

        public static LancarNotasViewModel MapToLancarNotasViewModel(this BoletimResult result)
        {
            var model = new LancarNotasViewModel
            {
                Id = result.Id,
                NotaBim1Escrita = result.NotaBim1Escrita,
                NotaBim1Leitura = result.NotaBim1Leitura,
                NotaBim1Conversacao = result.NotaBim1Conversacao,
                NotaBim2Escrita = result.NotaBim2Escrita,
                NotaBim2Leitura = result.NotaBim2Leitura,
                NotaBim2Conversacao = result.NotaBim2Conversacao,
                FaltasSemestre = result.FaltasSemestre
            };

            return model.PreencherExibicao(result);
        }

        /// <summary>Preenche os campos que só servem para desenhar a tela.</summary>
        public static LancarNotasViewModel PreencherExibicao(this LancarNotasViewModel model, BoletimResult result)
        {
            model.AlunoNome = result.AlunoNome;
            model.TurmaDescricao = result.TurmaDescricao;
            model.TurmaId = result.TurmaId;
            model.NotaBim1Final = result.NotaBim1Final;
            model.NotaBim2Final = result.NotaBim2Final;
            model.NotaFinalSemestre = result.NotaFinalSemestre;

            return model;
        }

        public static LancarNotasRequest MapToLancarNotasRequest(this LancarNotasViewModel model)
        {
            return new LancarNotasRequest
            {
                Id = model.Id,
                NotaBim1Escrita = model.NotaBim1Escrita,
                NotaBim1Leitura = model.NotaBim1Leitura,
                NotaBim1Conversacao = model.NotaBim1Conversacao,
                NotaBim2Escrita = model.NotaBim2Escrita,
                NotaBim2Leitura = model.NotaBim2Leitura,
                NotaBim2Conversacao = model.NotaBim2Conversacao,
                FaltasSemestre = model.FaltasSemestre
            };
        }
    }
}
