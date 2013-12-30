using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace Sindemed.Models.Repositories
{
    public class AssociadoGrupoViewModel : Repository
    {
        [DisplayName("Grupo Associado")]
        [Required(ErrorMessage="Por favor, preencha o campo grupo associado")]
        public Nullable<int> grupoAssociadoId { get; set; }

        [DisplayName("Associado")]
        [Required(ErrorMessage = "Por favor, preencha o campo associado")]
        public Nullable<int> associadoId { get; set; }

        public string descricao { get; set; }

        public string nome { get; set; }

    }
}