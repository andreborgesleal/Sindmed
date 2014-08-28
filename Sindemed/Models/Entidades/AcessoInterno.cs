using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("AcessoInterno")]
    public class AcessoInterno
    {
        [Key]
        [DisplayName("ID")]
        public int protocoloId { get; set; }

        [DisplayName("Associado")]
        public int associadoId { get; set; }

        [DisplayName("Arquivo")]
        public string fileId { get; set; }

        [DisplayName("Dt_Protocolo")]
        public System.DateTime dt_protocolo { get; set; }

        [DisplayName("Dt_Formulario")]
        public System.DateTime dt_formulario { get; set; }

        [DisplayName("Ind_Motivo_Solicitacao")]
        public string ind_motivo_solicitacao { get; set; }

        [DisplayName("Qte_Dispositivos")]
        public int qte_dispositivos { get; set; }

        [DisplayName("Vr_Unitario")]
        public Nullable<decimal> vr_unitario { get; set; }

        [DisplayName("Observacao")]
        public string observacao { get; set; }

    }
}