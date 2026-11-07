using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    // Para onde o usuário vai quando tenta abrir uma página que o perfil dele não permite
    [Route("acesso-negado")]
    public IActionResult AcessoNegado()
    {
        return View();
    }
}
