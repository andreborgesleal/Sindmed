using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace Sindemed.Models.Repositories
{
    public class MedicoViewModel : AssociadoViewModel
    {
        [DisplayName("UF CRM")]
        [Required(ErrorMessage = "UF do CRM deve ser informada")]
        [StringLength(2, ErrorMessage = "A UF do CRM deve possuir 2 caracteres", MinimumLength = 2)]
        public string ufCRM { get; set; }

        [Required(ErrorMessage = "CRM deve ser informado")]
        [StringLength(6, ErrorMessage = "O Número do CRM deve possuir no máximo 6 dígitos")]
        [DisplayName("CRM")]
        public string CRM { get; set; }

        [DisplayName("UF CRM 2")]
        [Required(ErrorMessage = "UF CRM deve ser informada")]
        [StringLength(2, ErrorMessage = "A UF do CRM 2 deve possuir 2 caracteres", MinimumLength = 2)]
        public string ufCRM_Seg { get; set; }

        [DisplayName("CRM 2")]
        [StringLength(6, ErrorMessage = "O Número do CRM 2 deve possuir no máximo 6 dígitos")]
        [Required(ErrorMessage = "CRM 2 deve ser informado")]
        public string CRM_Seg { get; set; }

        [DisplayName("Especialidade")]
        [Required(ErrorMessage = "Informe a Especialidade médica")]
        public Nullable<int> especialidade1Id { get; set; }

        [DisplayName("Especialidade 2")]
        public Nullable<int> especialidade2Id { get; set; }

        [DisplayName("Especialidade 3")]
        public Nullable<int> especialidade3Id { get; set; }

    }
}