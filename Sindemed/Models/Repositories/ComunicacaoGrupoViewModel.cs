using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace DWM.Models.Repositories
{
    public class ComunicacaoGrupoViewModel : Repository
    {
        [DisplayName("Grupo Condômino")]
        [Required(ErrorMessage = "Por favor, preencha o campo grupo condômino")]
        public Nullable<int> grupoAssociadoId { get; set; }

        [DisplayName("Comunicacao")]
        [Required(ErrorMessage = "Por favor, preencha o campo comunicação")]
        public Nullable<int> comunicacaoId { get; set; }

        public string descricao { get; set; }

        public string cabecalho { get; set; }
    }
}