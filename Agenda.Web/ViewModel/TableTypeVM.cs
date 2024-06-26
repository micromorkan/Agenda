﻿using System.ComponentModel.DataAnnotations;

namespace Agenda.Web.ViewModel
{
    public class TableTypeVM
    {
        public int Id { get; set; }

        [Display(Name = "Tabela")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string TableTax { get; set; }

        [Display(Name = "Taxa Adesão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string MembershipFee { get; set; }

        [Display(Name = "Taxa Restante")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string RemainingRate { get; set; }

        [Display(Name = "Taxa Comissão Representante")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CommissionFee { get; set; }

        [Display(Name = "Taxa Comissão Gerente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string ManagerFee { get; set; }
    }
}
