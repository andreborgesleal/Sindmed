using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("Comunicacao")]
    public class Comunicacao
    {
        [Key]
        [DisplayName("ID")]
        public int comunicacaoId { get; set; }

        [DisplayName("Data")]
        public DateTime dt_comunicacao { get; set; }

        [DisplayName("Cabeçalho")]
        public string cabecalho { get; set; }

        [DisplayName("Resumo")]
        public string resumo { get; set; }

        [DisplayName("xml")]
        public string mensagemDetalhada { get; set; }
    }
}

