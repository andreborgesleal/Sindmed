using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("DocFolder")]
    public class DocFolder
    {
        [Key]
        [DisplayName("ID")]
        public int docFolderId { get; set; }

        [DisplayName("Descricao")]
        public string descricao { get; set; }

        [DisplayName("Ind_Fixo")]
        public string ind_fixo { get; set; }
    }
}