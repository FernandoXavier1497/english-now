namespace EnglishNow.Services.Models.Turma
{
    public class EditarTurmaRequest
    {
        public int Id { get; set; }

        public int Semestre { get; set; }

        public int Ano { get; set; }

        public required string Periodo { get; set; }

        public required string Nivel { get; set; }

        public int ProfessorId { get; set; }
    }
}
