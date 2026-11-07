using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EnglishNow.Web.Models.Boletim
{
    public class LancarNotasViewModel
    {
        public int Id { get; set; }

        // Dados só de exibição: são recarregados do banco a cada requisição.
        [BindNever]
        [ValidateNever]
        public string AlunoNome { get; set; } = string.Empty;

        [BindNever]
        [ValidateNever]
        public string TurmaDescricao { get; set; } = string.Empty;

        [BindNever]
        [ValidateNever]
        public int TurmaId { get; set; }

        [BindNever]
        [ValidateNever]
        public decimal? NotaBim1Final { get; set; }

        [BindNever]
        [ValidateNever]
        public decimal? NotaBim2Final { get; set; }

        [BindNever]
        [ValidateNever]
        public decimal? NotaFinalSemestre { get; set; }

        [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
        public decimal? NotaBim1Escrita { get; set; }

        [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
        public decimal? NotaBim1Leitura { get; set; }

        [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
        public decimal? NotaBim1Conversacao { get; set; }

        [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
        public decimal? NotaBim2Escrita { get; set; }

        [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
        public decimal? NotaBim2Leitura { get; set; }

        [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
        public decimal? NotaBim2Conversacao { get; set; }

        [Range(0, 999, ErrorMessage = "Informe um número de faltas válido.")]
        public int? FaltasSemestre { get; set; }
    }
}
