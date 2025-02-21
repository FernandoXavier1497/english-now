using EnglishNow.Web.Models.Erro;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Controllers
{
    public class ErroController : Controller
    {
        public IActionResult Index()
        {
            var exceptionHandlerfeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var model = new ErroViewModel
            {
                MensagemErro = exceptionHandlerfeature == null ? "Erro inesperado" : exceptionHandlerfeature.Error.Message
            };

            return View(model);
        }
    }
}
