﻿using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class GrupoMedicoController : RootController<GrupoAssociadoViewModel, GrupoAssociadoModel>
    {
        public override int _sistema_id() { return 2; }
        public override string getListName()
        {
            return "Listar Grupos de Associados";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewGrupoAssociado l = new ListViewGrupoAssociado();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        [AuthorizeFilter]
        public ActionResult ListGrupoAssociadoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupGrupoAssociadoModel l = new LookupGrupoAssociadoModel();
            return this.ListModal(index, pageSize, l, "Grupos de Associados", descricao);
        }
        [AuthorizeFilter]
        public ActionResult _ListGrupoAssociadoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupGrupoAssociadoFiltroModel l = new LookupGrupoAssociadoFiltroModel();
            return this.ListModal(index, pageSize, l, "Grupos de Associados", descricao);
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int grupoAssociadoId)
        {
            return _Edit(new GrupoAssociadoViewModel() { grupoAssociadoId = grupoAssociadoId });
        }
        #endregion


        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int grupoAssociadoId)
        {
            return Edit(grupoAssociadoId);
        }
        #endregion
    }
}