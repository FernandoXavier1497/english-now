namespace EnglishNow.Web.Helpers
{
    public static class Formatacao
    {
        /// <summary>Nota com até 2 casas (ex.: 7,5 / 8,25); "-" quando ainda não foi lançada.</summary>
        public static string Nota(this decimal? nota)
        {
            return nota.HasValue ? nota.Value.ToString("0.0#") : "-";
        }

        public static string Faltas(this int? faltas)
        {
            return faltas.HasValue ? faltas.Value.ToString() : "-";
        }
    }
}
