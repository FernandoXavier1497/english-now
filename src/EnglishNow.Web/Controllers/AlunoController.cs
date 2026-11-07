using Microsoft.AspNetCore.Mvc;
using EnglishNow.Web.Models.Aluno;
using Microsoft.AspNetCore.Authorization;
using EnglishNow.Services;
using EnglishNow.Web.Auth;
using EnglishNow.Web.Mappings;

namespace EnglishNow.Web.Controllers
{
    [Route("aluno")]
    [Authorize(Roles = Perfis.Administrador)]
    public class AlunoController : Controller
    {
        private readonly IAlunoService _alunoService;
        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [Route("criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(CriarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //Cria o aluno.
            var result = _alunoService.Criar(model.MapToCriarAlunoRequest());

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [Route("listar")]
        public IActionResult Listar()
        {
            var alunos = _alunoService.Listar();

            var result = alunos.Select(c => c.MapToListarViewModel()).ToList();

            return View(result);
        }

        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var aluno = _alunoService.ObterPorId(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno.MapToEditarViewModel());
        }

        [Route("editar/{id}")]
        [HttpPost]
        public IActionResult Editar(EditarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = model.MapToEditarAlunoRequest();

            var result = _alunoService.Editar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [Route("excluir/{id}")]
        [HttpPost]
        public IActionResult Excluir(EditarViewModel model)
        {
            var result = _alunoService.Excluir(model.Id);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                //Volta para a tela de edição mostrando o motivo (não existe view "Excluir")
                return View("Editar", model);
            }

            return RedirectToAction("Listar");
        }
    }
}
