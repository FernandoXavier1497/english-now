namespace EnglishNow.Web.Models.Boletim
{
    public class BoletimItemViewModel
    {
        public int Id { get; set; }

        public required string AlunoNome { get; set; }

        public required string TurmaDescricao { get; set; }

        public string? ProfessorNome { get; set; }

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
    }
}
