using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("DocInterno")]
    public class DocInterno
    {
        [Key]
        [DisplayName("ID")]
        public int docId { get; set; }

        [DisplayName("Folder")]
        public int docFolderId { get; set; }

        [DisplayName("arquivo")]
        public string arquivo { get; set; }

        [DisplayName("Dt_Arquivo")]
        public DateTime dt_arquivo { get; set; }

        [DisplayName("Dt_Novo")]
        public System.Nullable<DateTime> dt_novo { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("Descricao")]
        public string descricao { get; set; }

        [DisplayName("Ind_Enfatizar")]
        public string ind_enfatizar { get; set; }
    }
}