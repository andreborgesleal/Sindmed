using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("DocClick")]
    public class DocClick
    {
        [Key, Column(Order = 0)]
        [DisplayName("DocID")]
        public int docId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("AssociadoID")]
        public int associadoId { get; set; }

        [DisplayName("Dt_Click")]
        public DateTime dt_click { get; set; }
    }
}