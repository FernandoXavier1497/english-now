namespace EnglishNow.Web.Models.Turma
{
    public class ListarViewModel
    {
        public int Id { get; set; }

        public int Ano { get; set; }

        public int Semestre { get; set; }

        public required string Nivel { get; set; }

        public required string Periodo { get; set; }

        public string? ProfessorNome { get; set; }
    }
}
