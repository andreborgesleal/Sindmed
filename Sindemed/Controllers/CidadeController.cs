﻿using App_Dominio.Controllers;
using App_Dominio.Security;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System.Web.Mvc;

namespace DWM.Controllers
{
    public class CidadeController : RootController<CidadeViewModel, CidadeModel>
    {
        public override int _sistema_id() { return (int)DWM.Models.Enumeracoes.Sistema.SINDMED ; }
        public override string getListName()
        {
            return "Listagem de Cidades";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewCidade l = new ListViewCidade();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
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
            if (ViewBag.ValidateRequest)
            {
                LookupCidadeFiltroModel l = new LookupCidadeFiltroModel();
                return this.ListModal(index, pageSize, l, "Cidades", descricao);
            }
            else
                return View();
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int cidadeId)
        {
            return _Edit(new CidadeViewModel() { cidadeId = cidadeId });
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int cidadeId)
        {
            return Edit(cidadeId);
        }
        #endregion
	}
}