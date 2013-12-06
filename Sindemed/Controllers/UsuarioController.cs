using App_Dominio.Controllers;
using App_Dominio.Negocio;
using App_Dominio.Repositories;
using App_Dominio.Security;
using Sindemed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class UsuarioController : RootController<UsuarioRepository, UsuarioModel>
    {
        public override int _sistema_id() { return 2; }
        public override string getListName()
        {
            return "Listar Usuários";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewUsuario l = new ListViewUsuario();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        [AuthorizeFilter]
        public ActionResult ListUsuarioModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupUsuarioModel l = new LookupUsuarioModel();
            return this.ListModal(index, pageSize, l, "Usuários", descricao);
        }

        [AuthorizeFilter]
        public ActionResult _ListUsuarioModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupUsuarioFiltroModel l = new LookupUsuarioFiltroModel();
            return this.ListModal(index, pageSize, l, "Usuáiros", descricao);
        }
        #endregion

        #region Typeahead
        public JsonResult GetNames(string term)
        {
            return JSonTypeahead(term, new ListViewUsuario());
        }
        #endregion
	}
}