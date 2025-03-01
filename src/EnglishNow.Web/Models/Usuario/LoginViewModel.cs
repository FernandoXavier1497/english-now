using System.ComponentModel.DataAnnotations;

namespace EnglishNow.Web.Models.Usuario
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo Usuário é obrigatorio")]
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatorio")]
        public string? Senha { get; set; }

        public bool LembrarMe { get; set; }
    }
}
