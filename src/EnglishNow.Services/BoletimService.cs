using EnglishNow.Repositories;
using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Mappings;
using EnglishNow.Services.Models;
using EnglishNow.Services.Models.Boletim;

namespace EnglishNow.Services
{
    public interface IBoletimService
    {
        IList<BoletimResult> ListarPorTurma(int turmaId);

        IList<BoletimResult> ListarPorAluno(int alunoId);

        BoletimResult? ObterPorId(int id);

        BaseResult Matricular(int alunoId, int turmaId);

        BaseResult LancarNotas(LancarNotasRequest request);

        BaseResult RemoverMatricula(int id);
    }

    public class BoletimService : IBoletimService
    {
        public const decimal NotaMinima = 0m;

        public const decimal NotaMaxima = 10m;

        private readonly IBoletimRepository _boletimRepository;

        private readonly IAlunoRepository _alunoRepository;

        private readonly ITurmaRepository _turmaRepository;

        public BoletimService(IBoletimRepository boletimRepository, IAlunoRepository alunoRepository, ITurmaRepository turmaRepository)
        {
            _boletimRepository = boletimRepository;

            _alunoRepository = alunoRepository;

            _turmaRepository = turmaRepository;
        }

        public IList<BoletimResult> ListarPorTurma(int turmaId)
        {
            return _boletimRepository.ListarPorTurma(turmaId).Select(b => b.MapToBoletimResult()).ToList();
        }

        public IList<BoletimResult> ListarPorAluno(int alunoId)
        {
            return _boletimRepository.ListarPorAluno(alunoId).Select(b => b.MapToBoletimResult()).ToList();
        }

        public BoletimResult? ObterPorId(int id)
        {
            return _boletimRepository.ObterPorId(id)?.MapToBoletimResult();
        }

        public BaseResult Matricular(int alunoId, int turmaId)
        {
            var result = new BaseResult();

            if (_alunoRepository.ObterPorId(alunoId) == null)
            {
                result.MensagemErro = "Aluno não existe";

                return result;
            }

            if (_turmaRepository.ObterPorId(turmaId) == null)
            {
                result.MensagemErro = "Turma não existe";

                return result;
            }

            if (_boletimRepository.Existe(alunoId, turmaId))
            {
                result.MensagemErro = "Esse aluno já está matriculado nessa turma";

                return result;
            }

            var id = _boletimRepository.Inserir(new Boletim { AlunoId = alunoId, TurmaId = turmaId });

            if (!id.HasValue)
            {
                result.MensagemErro = "Erro ao matricular o aluno";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public BaseResult LancarNotas(LancarNotasRequest request)
        {
            var result = new BaseResult();

            if (_boletimRepository.ObterPorId(request.Id) == null)
            {
                result.MensagemErro = "Matrícula não existe";

                return result;
            }

            var notas = new[]
            {
                request.NotaBim1Escrita, request.NotaBim1Leitura, request.NotaBim1Conversacao,
                request.NotaBim2Escrita, request.NotaBim2Leitura, request.NotaBim2Conversacao
            };

            if (notas.Any(n => n.HasValue && (n.Value < NotaMinima || n.Value > NotaMaxima)))
            {
                result.MensagemErro = $"As notas devem estar entre {NotaMinima} e {NotaMaxima}";

                return result;
            }

            if (request.FaltasSemestre.HasValue && request.FaltasSemestre.Value < 0)
            {
                result.MensagemErro = "As faltas não podem ser negativas";

                return result;
            }

            var boletim = request.MapToBoletim();

            CalcularMedias(boletim);

            _boletimRepository.Atualizar(boletim);

            result.Sucesso = true;

            return result;
        }

        public BaseResult RemoverMatricula(int id)
        {
            var result = new BaseResult();

            if (_boletimRepository.ObterPorId(id) == null)
            {
                result.MensagemErro = "Matrícula não existe";

                return result;
            }

            var affectedRows = _boletimRepository.Apagar(id);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível remover a matrícula";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        /// <summary>
        /// Regra de cálculo das médias (ajuste aqui se a escola usar outra):
        /// - média do bimestre = média simples de escrita, leitura e conversação (só se as três estiverem lançadas);
        /// - nota final do semestre = média simples das médias dos dois bimestres (só se os dois estiverem fechados).
        /// </summary>
        public static void CalcularMedias(Boletim boletim)
        {
            boletim.NotaBim1Final = Media(boletim.NotaBim1Escrita, boletim.NotaBim1Leitura, boletim.NotaBim1Conversacao);

            boletim.NotaBim2Final = Media(boletim.NotaBim2Escrita, boletim.NotaBim2Leitura, boletim.NotaBim2Conversacao);

            boletim.NotaFinalSemestre = Media(boletim.NotaBim1Final, boletim.NotaBim2Final);
        }

        private static decimal? Media(params decimal?[] notas)
        {
            if (notas.Any(n => !n.HasValue))
            {
                return null;
            }

            return Math.Round(notas.Average(n => n!.Value), 2, MidpointRounding.AwayFromZero);
        }
    }
}
