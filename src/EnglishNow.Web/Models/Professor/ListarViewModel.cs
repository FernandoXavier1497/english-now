﻿using Org.BouncyCastle.Bcpg.OpenPgp;

namespace EnglishNow.Web.Models.Professor
{
    public class ListarViewModel
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string Login { get; set; }


    }
}
