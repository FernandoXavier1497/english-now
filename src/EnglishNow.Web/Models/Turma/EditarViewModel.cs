using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnglishNow.Web.Models.Turma
{
    public class EditarViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo ano é obrigatório.")]
        [Range(2000, 2100, ErrorMessage = "Informe um ano válido.")]
        public int? Ano { get; set; }

        [Required(ErrorMessage = "O campo semestre é obrigatório.")]
        [Range(1, 2, ErrorMessage = "O semestre deve ser 1 ou 2.")]
        public int? Semestre { get; set; }

        [Required(ErrorMessage = "O campo nível é obrigatório.")]
        [StringLength(30, ErrorMessage = "O campo nível deve ter no máximo 30 caracteres.")]
        public string? Nivel { get; set; }

        [Required(ErrorMessage = "O campo período é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo período deve ter no máximo 150 caracteres.")]
        public string? Periodo { get; set; }

        [Required(ErrorMessage = "Selecione o professor.")]
        public int? ProfessorId { get; set; }

        [BindNever]
        [ValidateNever]
        public IList<SelectListItem> Professores { get; set; } = new List<SelectListItem>();
    }
}
