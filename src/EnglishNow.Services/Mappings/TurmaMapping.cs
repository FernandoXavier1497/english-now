using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Models.Turma;

namespace EnglishNow.Services.Mappings
{
    public static class TurmaMapping
    {
        public static Turma MapToTurma(this CriarTurmaRequest request)
        {
            return new Turma
            {
                Semestre = request.Semestre,
                Ano = request.Ano,
                Periodo = request.Periodo.Trim(),
                Nivel = request.Nivel.Trim(),
                ProfessorId = request.ProfessorId
            };
        }

        public static Turma MapToTurma(this EditarTurmaRequest request)
        {
            return new Turma
            {
                Id = request.Id,
                Semestre = request.Semestre,
                Ano = request.Ano,
                Periodo = request.Periodo.Trim(),
                Nivel = request.Nivel.Trim(),
                ProfessorId = request.ProfessorId
            };
        }

        public static TurmaResult MapToTurmaResult(this Turma turma)
        {
            return new TurmaResult
            {
                Id = turma.Id,
                Semestre = turma.Semestre,
                Ano = turma.Ano,
                Periodo = turma.Periodo,
                Nivel = turma.Nivel,
                ProfessorId = turma.ProfessorId,
                ProfessorNome = turma.ProfessorNome
            };
        }
    }
}
