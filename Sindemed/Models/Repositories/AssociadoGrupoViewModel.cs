using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace DWM.Models.Repositories
{
    public class AssociadoGrupoViewModel : Repository
    {
        [DisplayName("Grupo Condômino")]
        [Required(ErrorMessage = "Por favor, preencha o campo grupo condômino")]
        public int grupoAssociadoId { get; set; }

        [DisplayName("Condômino")]
        [Required(ErrorMessage = "Por favor, preencha o campo condômino")]
        public int associadoId { get; set; }

        public string descricao { get; set; }

        public string nome { get; set; }

    }
}