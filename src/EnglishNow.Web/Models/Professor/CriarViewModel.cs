using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Models.Professor
{
    public class CriarViewModel
    {
        [Required(ErrorMessage ="O campo login é obrigatório.")]
       [StringLength(50, ErrorMessage = "O campo login deve ter no máximo 50 caracteres.")]
        public string? Login { get; set; }

        [Required(ErrorMessage ="O campo senha é obrigatório.")]

        [StringLength(50, ErrorMessage = "O campo senha deve ter no máximo 50 caracteres.")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]

        [StringLength(200, ErrorMessage = "O campo nome deve ter no máximo 200 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório.")]

        [StringLength(45, ErrorMessage = "O campo e-mail deve ter no máximo 45 caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string? Email { get; set; }

    }
}
