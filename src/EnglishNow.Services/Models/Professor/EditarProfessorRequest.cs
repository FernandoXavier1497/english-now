﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Models.Professor
{
    public class EditarProfessorRequest
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public required string Nome { get; set; }

         public required string Email { get; set; }

         public required string Login { get; set; }

        public required string Senha { get; set; }


    }
}
