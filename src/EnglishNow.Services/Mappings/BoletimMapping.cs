using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Models.Boletim;

namespace EnglishNow.Services.Mappings
{
    public static class BoletimMapping
    {
        public static BoletimResult MapToBoletimResult(this Boletim boletim)
        {
            return new BoletimResult
            {
                Id = boletim.Id,
                AlunoId = boletim.AlunoId,
                AlunoNome = boletim.AlunoNome ?? string.Empty,
                TurmaId = boletim.TurmaId,
                Semestre = boletim.Turma?.Semestre ?? 0,
                Ano = boletim.Turma?.Ano ?? 0,
                Periodo = boletim.Turma?.Periodo ?? string.Empty,
                Nivel = boletim.Turma?.Nivel ?? string.Empty,
                ProfessorId = boletim.Turma?.ProfessorId ?? 0,
                ProfessorNome = boletim.Turma?.ProfessorNome,
                NotaBim1Escrita = boletim.NotaBim1Escrita,
                NotaBim1Leitura = boletim.NotaBim1Leitura,
                NotaBim1Conversacao = boletim.NotaBim1Conversacao,
                NotaBim1Final = boletim.NotaBim1Final,
                NotaBim2Escrita = boletim.NotaBim2Escrita,
                NotaBim2Leitura = boletim.NotaBim2Leitura,
                NotaBim2Conversacao = boletim.NotaBim2Conversacao,
                NotaBim2Final = boletim.NotaBim2Final,
                NotaFinalSemestre = boletim.NotaFinalSemestre,
                FaltasSemestre = boletim.FaltasSemestre
            };
        }

        public static Boletim MapToBoletim(this LancarNotasRequest request)
        {
            return new Boletim
            {
                Id = request.Id,
                NotaBim1Escrita = request.NotaBim1Escrita,
                NotaBim1Leitura = request.NotaBim1Leitura,
                NotaBim1Conversacao = request.NotaBim1Conversacao,
                NotaBim2Escrita = request.NotaBim2Escrita,
                NotaBim2Leitura = request.NotaBim2Leitura,
                NotaBim2Conversacao = request.NotaBim2Conversacao,
                FaltasSemestre = request.FaltasSemestre
            };
        }
    }
}
