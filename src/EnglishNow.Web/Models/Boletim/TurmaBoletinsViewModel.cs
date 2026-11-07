using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnglishNow.Web.Models.Boletim
{
    public class TurmaBoletinsViewModel
    {
        public int TurmaId { get; set; }

        public required string TurmaDescricao { get; set; }

        public string? ProfessorNome { get; set; }

        public IList<BoletimItemViewModel> Boletins { get; set; } = new List<BoletimItemViewModel>();

        /// <summary>Alunos que ainda não estão matriculados nesta turma.</summary>
        public IList<SelectListItem> AlunosDisponiveis { get; set; } = new List<SelectListItem>();
    }
}
