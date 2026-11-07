namespace EnglishNow.Web.Models.Boletim
{
    public class MeuBoletimViewModel
    {
        public string? AlunoNome { get; set; }

        public IList<BoletimItemViewModel> Boletins { get; set; } = new List<BoletimItemViewModel>();
    }
}
