using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("Comunicacao")]
    public class Comunicacao
    {
        [Key]
        public int comunicacaoId { get; set; }

        public DateTime dt_comunicacao { get; set; }

        public string cabecalho { get; set; }

        public string resumo { get; set; }

        public string mensagemDetalhada { get; set; }
    }
}