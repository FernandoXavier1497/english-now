using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnglishNow.Repositories;
using EnglishNow.Services.Models.Usuario;

namespace EnglishNow.Services
{
    public interface IUsuarioService
    {
        ValidarLoginResult ValidarLogin(string login, string senha);
    }
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public ValidarLoginResult ValidarLogin(string login, string senha)
        {
            var result = new ValidarLoginResult();

            if (string.IsNullOrEmpty(login))
            {
                result.MensagemErro = "Usuário é obrigatório";

                return result;
            }

            if (string.IsNullOrEmpty(senha))
            {
                result.MensagemErro = "Senha é obrigatório";
                return result;
            }

            var usuario = _usuarioRepository.ObterPorLogin(login);

            if (usuario == null)
            {
                result.MensagemErro = "Usuário não encontrado";
                return result;
            }

            if (usuario.Senha != senha) 
            {
                result.MensagemErro = "Senha inválida";
                return result;
            }

            //se chegou até aqui é por quê funcionou
            result.Sucesso = true;

            return result;
        }
    }
}
