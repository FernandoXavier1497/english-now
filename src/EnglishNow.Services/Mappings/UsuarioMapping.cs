using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Enums;
using EnglishNow.Services.Models.Professor;

namespace EnglishNow.Services.Mappings
{
    public static class UsuarioMapping
    {
        public static Usuario MapToUsuario(this CriarProfessorRequest request)
        {
            var usuario = new Usuario
            {
                Login = request.Login,
                Senha = request.Senha,
                PapelId = (int)Papel.Professor
            };

            return usuario;
        }

        public static Usuario MapToUsuario(this EditarProfessorRequest request)
        {
            var usuario = new Usuario
            {
                Id = request.UsuarioId,
                Login = request.Login,
                Senha = request.Senha,
            };

            return usuario;
        }
    }
}
