using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System;

namespace DWM.Models.Entidades
{
    [Table("AcessoExterno")]
    public class AcessoExterno
    {
        [Key]
        [DisplayName("ID")]
        public int acessoId { get; set; }

        [DisplayName("Torre")]
        public string torreId { get; set; }

        [DisplayName("Unidade")]
        public System.Nullable<int> unidadeId { get; set; }

        [DisplayName("Pessoa ID")]
        public System.Nullable<int> pessoaId { get; set; }

        [DisplayName("Interfone")]
        public string ind_interfone { get; set; }

        [DisplayName("Dt_Entrada_Inicio")]
        public System.Nullable<DateTime> dt_entrada_inicio { get; set; }

        [DisplayName("Dt_Entrada_Final")]
        public System.Nullable<DateTime> dt_entrada_final { get; set; }

        [DisplayName("Dt_Chegada")]
        public System.Nullable<DateTime> dt_chegada { get; set; }

        [DisplayName("Dt_Saida")]
        public System.Nullable<DateTime> dt_saida { get; set; }

        [DisplayName("Observacao")]
        public string observacao { get; set; }

        [DisplayName("Pessoa")]
        public virtual Pessoa Pessoa { get; set; }
    }
}