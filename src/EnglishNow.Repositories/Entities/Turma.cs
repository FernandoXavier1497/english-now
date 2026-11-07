namespace EnglishNow.Repositories.Entities
{
    public class Turma
    {
        public int Id { get; set; }

        public int Semestre { get; set; }

        public int Ano { get; set; }

        public required string Periodo { get; set; }

        public required string Nivel { get; set; }

        public int ProfessorId { get; set; }

        public string? ProfessorNome { get; set; }
    }
}
