using EnglishNow.Services;
using EnglishNow.Services.Models.Turma;
using EnglishNow.Web.Auth;
using EnglishNow.Web.Mappings;
using EnglishNow.Web.Models.Boletim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnglishNow.Web.Controllers
{
    [Authorize]
    public class BoletimController : Controller
    {
        private readonly IBoletimService _boletimService;
        private readonly ITurmaService _turmaService;
        private readonly IAlunoService _alunoService;
        private readonly IProfessorService _professorService;

        public BoletimController(
            IBoletimService boletimService,
            ITurmaService turmaService,
            IAlunoService alunoService,
            IProfessorService professorService)
        {
            _boletimService = boletimService;
            _turmaService = turmaService;
            _alunoService = alunoService;
            _professorService = professorService;
        }

        // Alunos matriculados na turma e suas notas (administrador ou professor da turma).
        [Authorize(Roles = Perfis.AdministradorOuProfessor)]
        [Route("turma/{turmaId:int}/boletins")]
        public IActionResult Listar(int turmaId)
        {
            var turma = _turmaService.ObterPorId(turmaId);

            if (turma == null)
            {
                return NotFound();
            }

            if (!PodeAcessarTurma(turma.ProfessorId))
            {
                return Forbid();
            }

            return View(MontarTurmaBoletins(turma));
        }

        [Authorize(Roles = Perfis.Administrador)]
        [HttpPost]
        [Route("turma/{turmaId:int}/matricular")]
        public IActionResult Matricular(int turmaId, int? alunoId)
        {
            if (!alunoId.HasValue)
            {
                TempData["Erro"] = "Selecione um aluno para matricular.";
            }
            else
            {
                var result = _boletimService.Matricular(alunoId.Value, turmaId);

                if (!result.Sucesso)
                {
                    TempData["Erro"] = result.MensagemErro;
                }
            }

            return RedirectToAction("Listar", new { turmaId });
        }

        [Authorize(Roles = Perfis.AdministradorOuProfessor)]
        [Route("boletim/{id:int}/notas")]
        public IActionResult LancarNotas(int id)
        {
            var boletim = _boletimService.ObterPorId(id);

            if (boletim == null)
            {
                return NotFound();
            }

            if (!PodeAcessarTurma(boletim.ProfessorId))
            {
                return Forbid();
            }

            return View(boletim.MapToLancarNotasViewModel());
        }

        [Authorize(Roles = Perfis.AdministradorOuProfessor)]
        [HttpPost]
        [Route("boletim/{id:int}/notas")]
        public IActionResult LancarNotas(int id, LancarNotasViewModel model)
        {
            var boletim = _boletimService.ObterPorId(id);

            if (boletim == null)
            {
                return NotFound();
            }

            if (!PodeAcessarTurma(boletim.ProfessorId))
            {
                return Forbid();
            }

            model.Id = id;
            model.PreencherExibicao(boletim);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _boletimService.LancarNotas(model.MapToLancarNotasRequest());

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar", new { turmaId = boletim.TurmaId });
        }

        [Authorize(Roles = Perfis.Administrador)]
        [HttpPost]
        [Route("boletim/{id:int}/remover")]
        public IActionResult Remover(int id)
        {
            var boletim = _boletimService.ObterPorId(id);

            if (boletim == null)
            {
                return NotFound();
            }

            var result = _boletimService.RemoverMatricula(id);

            if (!result.Sucesso)
            {
                TempData["Erro"] = result.MensagemErro;
            }

            return RedirectToAction("Listar", new { turmaId = boletim.TurmaId });
        }

        // Boletim do aluno logado (somente leitura).
        [Authorize(Roles = Perfis.Aluno)]
        [Route("meu-boletim")]
        public IActionResult MeuBoletim()
        {
            var aluno = _alunoService.ObterPorUsuarioId(User.ObterUsuarioId());

            var model = new MeuBoletimViewModel
            {
                AlunoNome = aluno?.Nome,
                Boletins = aluno == null
                    ? new List<BoletimItemViewModel>()
                    : _boletimService.ListarPorAluno(aluno.Id).Select(b => b.MapToItemViewModel()).ToList()
            };

            return View(model);
        }

        // Administrador acessa qualquer turma; professor só as turmas dele.
        private bool PodeAcessarTurma(int professorIdDaTurma)
        {
            if (User.IsInRole(Perfis.Administrador))
            {
                return true;
            }

            var professor = _professorService.ObterPorUsuarioId(User.ObterUsuarioId());

            return professor != null && professor.Id == professorIdDaTurma;
        }

        private TurmaBoletinsViewModel MontarTurmaBoletins(TurmaResult turma)
        {
            var boletins = _boletimService.ListarPorTurma(turma.Id);

            var jaMatriculados = boletins.Select(b => b.AlunoId).ToHashSet();

            var disponiveis = _alunoService.Listar()
                .Where(a => !jaMatriculados.Contains(a.Id))
                .OrderBy(a => a.Nome)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Nome })
                .ToList();

            return new TurmaBoletinsViewModel
            {
                TurmaId = turma.Id,
                TurmaDescricao = turma.Descricao,
                ProfessorNome = turma.ProfessorNome,
                Boletins = boletins.Select(b => b.MapToItemViewModel()).ToList(),
                AlunosDisponiveis = disponiveis
            };
        }
    }
}
