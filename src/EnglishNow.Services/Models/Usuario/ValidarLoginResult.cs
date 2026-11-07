using EnglishNow.Services.Enums;

namespace EnglishNow.Services.Models.Usuario
{
    public class ValidarLoginResult : BaseResult
    {
        public int UsuarioId { get; set; }

        public string? Login { get; set; }

        public Papel Papel { get; set; }
    }
}
