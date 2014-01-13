using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel;

namespace Sindemed.Models.Entidades
{
    [Table("Atendimento")]
    public class Atendimento
    {
        [Key, Column(Order = 0)]
        [DisplayName("ID")]
        public int chamadoId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("Dt_Atendimento")]
        public DateTime dt_atendimento { get; set; }

        [DisplayName("Fluxo")]
        public string fluxo { get; set; }

        [DisplayName("xml")]
        public string mensagem { get; set; }

        //public virtual Chamado Chamado { get; set; }
    }
}