using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Models.Professor
{
    public class CriarViewModel
    {
        [Required(ErrorMessage ="O campo login é obrigattório.")]
       public string? Login { get; set; }

        [Required(ErrorMessage ="O campo senha é obrigatório.")]

        public string? Senha { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]

        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório.")]

        public string? Email { get; set; }

    }
}
