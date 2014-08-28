using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWM.Models.Entidades
{
    [Table("Comunicacao")]
    public class Comunicacao
    {
        [Key]
        [DisplayName("ID")]
        public int comunicacaoId { get; set; }

        [DisplayName("Data")]
        public DateTime dt_comunicacao { get; set; }

        [DisplayName("Dt_Publicacao")]
        public DateTime dt_publicacao { get; set; }

        [DisplayName("Dt_Expiracao")]
        public DateTime dt_expiracao { get; set; }

        [DisplayName("Cabeçalho")]
        public string cabecalho { get; set; }

        [DisplayName("Resumo")]
        public string resumo { get; set; }

        [DisplayName("xml")]
        public string mensagemDetalhada { get; set; }

        [DisplayName("Arq_Imagem_300x200")]
        public string arq_imagem_300x200 { get; set; }

        [DisplayName("Arq_Imagem_100x100")]
        public string arq_imagem_100x100 { get; set; }

        [DisplayName("Arq_Imagem_400x300")]
        public string arq_imagem_400x300 { get; set; }



    }
}

