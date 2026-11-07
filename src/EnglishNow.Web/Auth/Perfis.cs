using EnglishNow.Services.Enums;

namespace EnglishNow.Web.Auth
{
    /// <summary>
    /// Nomes dos perfis (roles) usados nos cookies de login e no [Authorize(Roles = ...)].
    /// Seguem os nomes do enum Papel, que por sua vez espelha a tabela papel do banco.
    /// </summary>
    public static class Perfis
    {
        public const string Administrador = nameof(Papel.Administrador);

        public const string Professor = nameof(Papel.Professor);

        public const string Aluno = nameof(Papel.Aluno);

        public const string AdministradorOuProfessor = Administrador + "," + Professor;
    }
}
