﻿using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class AreaAtuacaoController : RootController<AreaAtuacaoViewModel, AreaAtuacaoModel>
    {
        public override int _sistema_id() { return 2; }
        public override string getListName()
        {
            return "Listar Áreas de Atuação";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewAreaAtuacao l = new ListViewAreaAtuacao();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        [AuthorizeFilter]
        public ActionResult ListAreaAtuacaoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupAreaAtuacaoModel l = new LookupAreaAtuacaoModel();
            return this.ListModal(index, pageSize, l, "Áreas de Atuação", descricao);
        }
        [AuthorizeFilter]
        public ActionResult _ListAreaAtuacaoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupAreaAtuacaoFiltroModel l = new LookupAreaAtuacaoFiltroModel();
            return this.ListModal(index, pageSize, l, "Áreas de Atuação", descricao);
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int areaAtuacaoId)
        {
            return _Edit(new AreaAtuacaoViewModel() { areaAtuacaoId = areaAtuacaoId });
        }
        #endregion


        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int areaAtuacaoId)
        {
            return Edit(areaAtuacaoId);
        }
        #endregion
	}
}