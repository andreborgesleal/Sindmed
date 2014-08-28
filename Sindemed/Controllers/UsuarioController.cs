using App_Dominio.Controllers;
using App_Dominio.Negocio;
using App_Dominio.Repositories;
using App_Dominio.Security;
using DWM.Models;
using DWM.Models.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DWM.Controllers
{
    public class UsuarioController : RootController<UsuarioRepository, UsuarioModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED ; }
        public override string getListName()
        {
            return "Listar Usuários";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewUsuario l = new ListViewUsuario();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
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
            if (ViewBag.ValidateRequest)
            {
                LookupUsuarioFiltroModel l = new LookupUsuarioFiltroModel();
                return this.ListModal(index, pageSize, l, "Usuáiros", descricao);
            }
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult ListUsuarioMedicoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupUsuarioMedicoModel l = new LookupUsuarioMedicoModel();
            return this.ListModal(index, pageSize, l, "Usuários", descricao);
        }

        [AuthorizeFilter]
        public ActionResult _ListUsuarioMedicoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupUsuarioMedicoFiltroModel l = new LookupUsuarioMedicoFiltroModel();
                return this.ListModal(index, pageSize, l, "Usuáiros", descricao, Sistema.SINDMED);
            }
            else
                return View();
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