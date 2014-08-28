using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;
using System.Collections.Generic;

namespace DWM.Models.Repositories
{
    public class AtendimentoViewModel : Repository
    {
        [DisplayName("ID")]
        public int chamadoId { get; set; }

        [DisplayName("Dt.Atendimento")]
        public DateTime dt_atendimento { get; set; }

        /// <summary>
        /// 1-Associado/Sindmed ou 2-Sindmed/Associado
        /// </summary>
        [DisplayName("Fluxo")]
        public string fluxo { get; set; }

        [DisplayName("Mensagem")]
        public string mensagemResposta { get; set; }
        public AssociadoDocumentoViewModel anexo { get; set; }

        [DisplayName("Chamado")]
        public ChamadoViewModel chamado { get; set; }

        public IEnumerable<AtendimentoViewModel> atendimentos { get; set; }
    }
}