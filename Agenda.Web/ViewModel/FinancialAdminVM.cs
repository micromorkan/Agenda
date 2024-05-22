using System.ComponentModel.DataAnnotations;

namespace Agenda.Web.ViewModel
{
    public class FinancialAdminVM
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nome não pode ter menos de 1 ou mais de 50 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Ativo")]
        public bool? Active { get; set; }
    }
}
