using EnglishNow.Services;
using EnglishNow.Web.Auth;
using EnglishNow.Web.Mappings;
using EnglishNow.Web.Models.Turma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Controllers
{
    [Route("turma")]
    [Authorize(Roles = Perfis.AdministradorOuProfessor)]
    public class TurmaController : Controller
    {
        private readonly ITurmaService _turmaService;
        private readonly IProfessorService _professorService;

        public TurmaController(ITurmaService turmaService, IProfessorService professorService)
        {
            _turmaService = turmaService;
            _professorService = professorService;
        }

        // Administrador vê todas as turmas; professor vê só as suas.
        [Route("listar")]
        public IActionResult Listar()
        {
            IList<EnglishNow.Services.Models.Turma.TurmaResult> turmas;

            if (User.IsInRole(Perfis.Administrador))
            {
                turmas = _turmaService.Listar();
            }
            else
            {
                var professor = _professorService.ObterPorUsuarioId(User.ObterUsuarioId());

                turmas = professor == null
                    ? new List<EnglishNow.Services.Models.Turma.TurmaResult>()
                    : _turmaService.ListarPorProfessor(professor.Id);
            }

            return View(turmas.Select(t => t.MapToListarViewModel()).ToList());
        }

        [Authorize(Roles = Perfis.Administrador)]
        [Route("criar")]
        public IActionResult Criar()
        {
            var model = new CriarViewModel
            {
                Professores = _professorService.Listar().MapToSelectList()
            };

            return View(model);
        }

        [Authorize(Roles = Perfis.Administrador)]
        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(CriarViewModel model)
        {
            model.Professores = _professorService.Listar().MapToSelectList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _turmaService.Criar(model.MapToCriarTurmaRequest());

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [Authorize(Roles = Perfis.Administrador)]
        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var turma = _turmaService.ObterPorId(id);

            if (turma == null)
            {
                return NotFound();
            }

            var model = turma.MapToEditarViewModel();

            model.Professores = _professorService.Listar().MapToSelectList();

            return View(model);
        }

        [Authorize(Roles = Perfis.Administrador)]
        [HttpPost]
        [Route("editar/{id}")]
        public IActionResult Editar(EditarViewModel model)
        {
            model.Professores = _professorService.Listar().MapToSelectList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _turmaService.Editar(model.MapToEditarTurmaRequest());

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [Authorize(Roles = Perfis.Administrador)]
        [HttpPost]
        [Route("excluir/{id}")]
        public IActionResult Excluir(EditarViewModel model)
        {
            var result = _turmaService.Excluir(model.Id);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                model.Professores = _professorService.Listar().MapToSelectList();

                return View("Editar", model);
            }

            return RedirectToAction("Listar");
        }
    }
}
