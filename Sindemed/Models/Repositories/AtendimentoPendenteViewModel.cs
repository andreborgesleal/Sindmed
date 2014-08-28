using App_Dominio.Component;
using App_Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DWM.Models.Repositories
{
    public class AtendimentoPendenteViewModel : Repository, IReportRepository<AtendimentoPendenteViewModel>
    {
        public int associadoId { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string crm { get; set; }
        public string email { get; set; }
        public string ind_proprietario { get; set; }
        public DateTime dt_chamado { get; set; }
        public string dt_chamado2 { get; set; }
        public System.Nullable<DateTime> dt_atendimento { get; set; }
        public int chamadoId { get; set; }
        public int atraso { get; set; }
        public int areaAtendimentoId { get; set; }
        public string descricao_areaAtendimento { get; set; }
        public string assunto { get; set; }
        public int? usuarioId { get; set; }
        public string usuario_nome { get; set; }

        #region métodos da Interface
        public object getValueColumn1()
        {
            return dt_chamado2;
        }
        
        public object getValueColumn2()
        {
            return nome;
        }

        public void ClearColumn1()
        {
            dt_chamado2 = null;
        }

        public void ClearColumn2()
        {
            nome = null;
        }

        public AtendimentoPendenteViewModel getKey(object group = null, object subGroup = null)
        {
            return new AtendimentoPendenteViewModel()
            {
                dt_chamado2 = (string)group ?? "",
                cpf = (string)subGroup ?? ""
            };
        }

        public AtendimentoPendenteViewModel Create(AtendimentoPendenteViewModel key, IEnumerable<AtendimentoPendenteViewModel> list)
        {
            return new AtendimentoPendenteViewModel();
        }

        #endregion
    }
}