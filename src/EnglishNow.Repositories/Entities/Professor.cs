﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories.Entities
{
    public class Professor
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; } 

    }
}
