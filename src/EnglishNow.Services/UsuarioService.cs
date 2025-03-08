using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnglishNow.Services.Models.Usuario;

namespace EnglishNow.Services
{
    public interface IUsuarioService
    {
        ValidarLoginResult ValidarLogin(string usuario, string senha);
    }
    public class UsuarioService : IUsuarioService
    {
        public ValidarLoginResult ValidarLogin(string usuario, string senha)
        {
            var result = new ValidarLoginResult();

            result.Sucesso = true;

            return result;
        }
    }
}
