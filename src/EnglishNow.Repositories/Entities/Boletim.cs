namespace EnglishNow.Repositories.Entities
{
    /// <summary>
    /// Matrícula de um aluno em uma turma, com as notas e faltas (tabela aluno_turma_boletim).
    /// </summary>
    public class Boletim
    {
        public int Id { get; set; }

        public int AlunoId { get; set; }

        public int TurmaId { get; set; }

        public string? AlunoNome { get; set; }

        public Turma? Turma { get; set; }

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
