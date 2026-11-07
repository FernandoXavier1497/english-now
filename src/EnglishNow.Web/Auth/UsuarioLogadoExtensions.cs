using System.Security.Claims;

namespace EnglishNow.Web.Auth
{
    public static class UsuarioLogadoExtensions
    {
        /// <summary>Id (tabela usuario) de quem está logado.</summary>
        public static int ObterUsuarioId(this ClaimsPrincipal user)
        {
            var valor = user.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(valor, out var id) ? id : 0;
        }
    }
}
