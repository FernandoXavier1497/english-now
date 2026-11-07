namespace EnglishNow.Services.Models.Boletim
{
    /// <summary>
    /// Notas lançadas pelo professor. As médias (bimestre e semestre) não vêm da tela: são calculadas pelo serviço.
    /// </summary>
    public class LancarNotasRequest
    {
        public int Id { get; set; }

        public decimal? NotaBim1Escrita { get; set; }
        public decimal? NotaBim1Leitura { get; set; }
        public decimal? NotaBim1Conversacao { get; set; }

        public decimal? NotaBim2Escrita { get; set; }
        public decimal? NotaBim2Leitura { get; set; }
        public decimal? NotaBim2Conversacao { get; set; }

        public int? FaltasSemestre { get; set; }
    }
}
