using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("Comentario")]
    public class Comentario
    {
        [Key]
        [DisplayName("ID")]
        public int comentarioId { get; set; }

        [DisplayName("Associado")]
        public int associadoId { get; set; }

        [DisplayName("Comounicacao")]
        public int comunicacaoId { get; set; }

        [DisplayName("Dt_Comentario")]
        public DateTime dt_comentario { get; set; }

        [DisplayName("Descricao")]
        public string descricao { get; set; }
    }
}