using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class AtendimentoController : ProcessController<AtendimentoViewModel, AtendimentoModel>
    {
        public override int _sistema_id() { return (int)Sindemed.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Atendimentos";
        }

        public override bool mustListOnLoad()
        {
            return false;
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            throw new NotImplementedException();
        }

        [AuthorizeFilter]
        public ActionResult List(int? index, int? pageSize = 50, int? chamadoId = null,
                                    int? associadoId = null, string data1 = null, string data2 = null, int? areaAtendimento = null, string situacao = null)
        {
            ListViewChamadoAdministracao l = new ListViewChamadoAdministracao();
            return _List(index, pageSize, "Browse", l, chamadoId, associadoId, DateTime.Parse(data1), DateTime.Parse(data2), areaAtendimento, situacao);
        }
        #endregion
	}
}