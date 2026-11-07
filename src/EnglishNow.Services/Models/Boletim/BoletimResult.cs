namespace EnglishNow.Services.Models.Boletim
{
    public class BoletimResult
    {
        public int Id { get; set; }

        public int AlunoId { get; set; }

        public required string AlunoNome { get; set; }

        public int TurmaId { get; set; }

        public int Semestre { get; set; }

        public int Ano { get; set; }

        public required string Periodo { get; set; }

        public required string Nivel { get; set; }

        public int ProfessorId { get; set; }

        public string? ProfessorNome { get; set; }

        public string TurmaDescricao => $"{Ano}/{Semestre} - {Nivel} - {Periodo}";

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
