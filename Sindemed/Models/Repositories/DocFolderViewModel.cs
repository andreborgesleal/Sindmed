using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace DWM.Models.Repositories
{
    public class DocFolderViewModel : Repository
    {
        [DisplayName("ID")]
        public int docFolderId { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage="Descrição deve ser informada")]
        [StringLength(50, ErrorMessage="Descrição deve conter no máximo 50 caracteres")]
        public string descricao { get; set; }

        public string ind_fixo { get; set; }
    }
}