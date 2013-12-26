using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Sindemed.Models.Entidades
{
    [Table("Chamado")]
    public class Chamado
    {
        [Key]
        public int chamadoId { get; set; }

        public int associadoId { get; set; }

        public int areaAtendimentoId { get; set; }

        public DateTime dt_chamado { get; set; }

        public string assunto { get; set; }

        public string situacao { get; set; }

        public Nullable<int> usuarioId { get; set; }

        public string mensagemOriginal { get; set; }

        public virtual Associado Associado { get; set; }

        public virtual AreaAtendimento AreaAtendimento { get; set; }

    }
}