﻿using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Enumeracoes;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class ComunicacaoGrupoController : RootController<ComunicacaoGrupoViewModel, ComunicacaoGrupoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED; }

        public override string getListName()
        {
            return "Listar Comunicacao Grupo";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewComunicacaoGrupo l = new ListViewComunicacaoGrupo();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int comunicacaoId, int grupoAssociadoId)
        {
            return _Edit(new ComunicacaoGrupoViewModel() { comunicacaoId = comunicacaoId, grupoAssociadoId = grupoAssociadoId });
        }
        #endregion
	}
}