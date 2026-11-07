using EnglishNow.Repositories;
using EnglishNow.Services.Mappings;
using EnglishNow.Services.Models.Turma;

namespace EnglishNow.Services
{
    public interface ITurmaService
    {
        CriarTurmaResult Criar(CriarTurmaRequest request);

        EditarTurmaResult Editar(EditarTurmaRequest request);

        ExcluirTurmaResult Excluir(int id);

        IList<TurmaResult> Listar();

        IList<TurmaResult> ListarPorProfessor(int professorId);

        TurmaResult? ObterPorId(int id);
    }

    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        private readonly IProfessorRepository _professorRepository;

        private readonly IBoletimRepository _boletimRepository;

        public TurmaService(ITurmaRepository turmaRepository, IProfessorRepository professorRepository, IBoletimRepository boletimRepository)
        {
            _turmaRepository = turmaRepository;

            _professorRepository = professorRepository;

            _boletimRepository = boletimRepository;
        }

        public CriarTurmaResult Criar(CriarTurmaRequest request)
        {
            var result = new CriarTurmaResult();

            var erro = Validar(request.Semestre, request.Ano, request.Periodo, request.Nivel, request.ProfessorId);

            if (erro != null)
            {
                result.MensagemErro = erro;

                return result;
            }

            var turmaId = _turmaRepository.Inserir(request.MapToTurma());

            if (!turmaId.HasValue)
            {
                result.MensagemErro = "Erro ao inserir a turma";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public EditarTurmaResult Editar(EditarTurmaRequest request)
        {
            var result = new EditarTurmaResult();

            var erro = Validar(request.Semestre, request.Ano, request.Periodo, request.Nivel, request.ProfessorId);

            if (erro != null)
            {
                result.MensagemErro = erro;

                return result;
            }

            if (_turmaRepository.ObterPorId(request.Id) == null)
            {
                result.MensagemErro = "Turma não existe";

                return result;
            }

            var affectedRows = _turmaRepository.Atualizar(request.MapToTurma());

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar a turma";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public ExcluirTurmaResult Excluir(int id)
        {
            var result = new ExcluirTurmaResult();

            if (_turmaRepository.ObterPorId(id) == null)
            {
                result.MensagemErro = "Turma não existe";

                return result;
            }

            //A turma não pode ser excluída enquanto tiver alunos matriculados (chave estrangeira em aluno_turma_boletim)
            if (_boletimRepository.ContarPorTurma(id) > 0)
            {
                result.MensagemErro = "Não é possível excluir a turma, pois ela possui alunos matriculados. Remova as matrículas antes.";

                return result;
            }

            var affectedRows = _turmaRepository.Apagar(id);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível excluir a turma";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public IList<TurmaResult> Listar()
        {
            return _turmaRepository.Listar().Select(t => t.MapToTurmaResult()).ToList();
        }

        public IList<TurmaResult> ListarPorProfessor(int professorId)
        {
            return _turmaRepository.ListarPorProfessor(professorId).Select(t => t.MapToTurmaResult()).ToList();
        }

        public TurmaResult? ObterPorId(int id)
        {
            var turma = _turmaRepository.ObterPorId(id);

            return turma?.MapToTurmaResult();
        }

        private string? Validar(int semestre, int ano, string periodo, string nivel, int professorId)
        {
            if (semestre != 1 && semestre != 2)
            {
                return "O semestre deve ser 1 ou 2";
            }

            if (ano < 2000 || ano > 2100)
            {
                return "Informe um ano válido";
            }

            if (string.IsNullOrWhiteSpace(periodo))
            {
                return "Período é obrigatório";
            }

            if (string.IsNullOrWhiteSpace(nivel))
            {
                return "Nível é obrigatório";
            }

            if (_professorRepository.ObterPorId(professorId) == null)
            {
                return "Professor não existe";
            }

            return null;
        }
    }
}
