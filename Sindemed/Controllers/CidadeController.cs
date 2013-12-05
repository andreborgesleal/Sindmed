using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class CidadeController : RootController<CidadeViewModel, CidadeModel>
    {
        public override int _sistema_id() { return 2; }

        #region List
        [AuthorizeFilter]
        public override string getListName()
        {
            return "Listagem de Cidades";
        }
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewCidade l = new ListViewCidade();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        [AuthorizeFilter]
        public ActionResult ListCidadeModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupCidadeModel l = new LookupCidadeModel();
            return this.ListModal(index, pageSize, l, "Cidades", descricao);
        }
        [AuthorizeFilter]
        public ActionResult _ListCidadeModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupCidadeFiltroModel l = new LookupCidadeFiltroModel();
            return this.ListModal(index, pageSize, l, "Cidades", descricao);
        }
        #endregion
	}
}