using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace DWM.Models.Repositories
{
    public class DocInternoViewModel : Repository
    {
        [DisplayName("ID")]
        public int docId { get; set; }

        [DisplayName("Pasta")]
        [Required(ErrorMessage="Pasta deve ser informada")]
        public int docFolderId { get; set; }

        [DisplayName("Pasta")]
        public string nome_pasta { get; set; }

        [DisplayName("Arquivo")]
        public string arquivo { get; set; }

        [DisplayName("Dt.Arquivo")]
        public DateTime dt_arquivo { get; set; }

        [DisplayName("Dt.Tarja [Novo]")]
        public System.Nullable<DateTime> dt_novo { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage="Nome do arquivo deve ser informado")]
        [StringLength(60, ErrorMessage="Nome do arquivo deve conter no máximo 60 caracteres")]
        public string nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage="Descrição do arquivo deve ser informada")]
        [StringLength(255, ErrorMessage="Descrição do arquivo deve conter no máximo 255 caracteres")]
        public string descricao { get; set; }

        [DisplayName("Enfatizar")]
        public string ind_enfatizar { get; set; }
    }
}