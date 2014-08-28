using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace DWM.Models.Repositories
{
    public class AcessoInternoViewModel : Repository
    {
        [DisplayName("Nº Protocolo")]
        public int protocoloId { get; set; }

        [DisplayName("Associado")]
        public int associadoId { get; set; }

        public string nome_associado { get; set; }

        [DisplayName("Arquivo")]
        public string fileId { get; set; }

        [DisplayName("Dt.Protocolo")]
        public System.DateTime dt_protocolo { get; set; }

        [DisplayName("Dt.Formulario")]
        [Required(ErrorMessage="Data do preenchimento do formulário deve ser preenchida")]
        public System.DateTime dt_formulario { get; set; }

        [DisplayName("Motivo Solicitação")]
        public string ind_motivo_solicitacao { get; set; }

        [DisplayName("Qte Dispositivos")]
        public int qte_dispositivos { get; set; }

        [DisplayName("Vr.Unitário")]
        public Nullable<decimal> vr_unitario { get; set; }

        [DisplayName("Observação")]
        public string observacao { get; set; }

        [DisplayName("Cofirmação de proprietário/inquilino")]
        public string ind_proprietario_confirmacao { get; set; }
    }
}