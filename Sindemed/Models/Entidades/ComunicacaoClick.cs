using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWM.Models.Entidades
{
    [Table("ComunicacaoClick")]
    public class ComunicacaoClick
    {
        [Key, Column(Order = 0)]
        [DisplayName("ComunicacaoID")]
        public int comunicacaoId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("AssociadoID")]
        public int associadoId { get; set; }

        [DisplayName("Dt_Click")]
        public DateTime dt_click { get; set; }

    }
}