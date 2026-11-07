using EnglishNow.Services.Models.Professor;
using EnglishNow.Services.Models.Turma;
using EnglishNow.Web.Models.Turma;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnglishNow.Web.Mappings
{
    public static class TurmaMapping
    {
        public static CriarTurmaRequest MapToCriarTurmaRequest(this CriarViewModel model)
        {
            return new CriarTurmaRequest
            {
                Ano = model.Ano!.Value,
                Semestre = model.Semestre!.Value,
                Nivel = model.Nivel!,
                Periodo = model.Periodo!,
                ProfessorId = model.ProfessorId!.Value
            };
        }

        public static EditarTurmaRequest MapToEditarTurmaRequest(this EditarViewModel model)
        {
            return new EditarTurmaRequest
            {
                Id = model.Id,
                Ano = model.Ano!.Value,
                Semestre = model.Semestre!.Value,
                Nivel = model.Nivel!,
                Periodo = model.Periodo!,
                ProfessorId = model.ProfessorId!.Value
            };
        }

        public static ListarViewModel MapToListarViewModel(this TurmaResult result)
        {
            return new ListarViewModel
            {
                Id = result.Id,
                Ano = result.Ano,
                Semestre = result.Semestre,
                Nivel = result.Nivel,
                Periodo = result.Periodo,
                ProfessorNome = result.ProfessorNome
            };
        }

        public static EditarViewModel MapToEditarViewModel(this TurmaResult result)
        {
            return new EditarViewModel
            {
                Id = result.Id,
                Ano = result.Ano,
                Semestre = result.Semestre,
                Nivel = result.Nivel,
                Periodo = result.Periodo,
                ProfessorId = result.ProfessorId
            };
        }

        public static IList<SelectListItem> MapToSelectList(this IEnumerable<ProfessorResult> professores)
        {
            return professores
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nome })
                .ToList();
        }
    }
}
