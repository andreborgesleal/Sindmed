using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Sindemed.Models.Entidades
{
    [Table("Atendimento")]
    public class Atendimento
    {
        [Key, Column(Order = 0)]
        public int chamadoId { get; set; }

        [Key, Column(Order = 1)]
        public DateTime dt_atendimento { get; set; }

        public string fluxo { get; set; }

        public string mensagem { get; set; }

        //public virtual Chamado Chamado { get; set; }
    }
}