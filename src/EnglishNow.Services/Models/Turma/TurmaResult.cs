namespace EnglishNow.Services.Models.Turma
{
    public class TurmaResult
    {
        public int Id { get; set; }

        public int Semestre { get; set; }

        public int Ano { get; set; }

        public required string Periodo { get; set; }

        public required string Nivel { get; set; }

        public int ProfessorId { get; set; }

        public string? ProfessorNome { get; set; }

        /// <summary>Ex.: "2025/1 - Intermediate - Noite"</summary>
        public string Descricao => $"{Ano}/{Semestre} - {Nivel} - {Periodo}";
    }
}
