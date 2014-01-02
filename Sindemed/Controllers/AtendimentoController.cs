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

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            return ListChamados(index, PageSize);
        }

        //[AuthorizeFilter]
        public ActionResult ListChamados(int? index, int? pageSize = 50, int? chamadoId = null, int? associadoId = null, 
                                    string nome_associado1 = null, string nome_associado = null, string data1 = null, string data2 = null, 
                                    int? areaAtendimento = null, string situacao = null)
        {
            DateTime _data1 = DateTime.Parse("01/" + DateTime.Today.ToString("MM/yyyy"));
            DateTime _data2 = DateTime.Parse(DateTime.Today.AddDays(1).ToString("dd/MM/yyyy"));

            if (data1 != null && data1 != "")
                _data1 = DateTime.Parse(data1);

            if (data2 != null && data2 != "")
                _data2 = DateTime.Parse(data2).AddDays(1);
            
            ListViewChamadoAdministracao l = new ListViewChamadoAdministracao();
            return _List(index, pageSize, "Browse", l, chamadoId, associadoId, _data1, _data2, areaAtendimento, situacao);
        }
        #endregion
	}
}