﻿using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Enumeracoes;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class EspecialidadeController : RootController<EspecialidadeMedicaViewModel, EspecialidadeMedicaModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED ; }
        public override string getListName()
        {
            return "Listar Especialidades";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewEspecialidadeMedica l = new ListViewEspecialidadeMedica();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        [AuthorizeFilter]
        public ActionResult ListEspecialidadeMedica1Modal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupEspecialidadeMedica1Model l = new LookupEspecialidadeMedica1Model();
            return this.ListModal(index, pageSize, l, "Especialidades Médicas", descricao);
        }
        [AuthorizeFilter]
        public ActionResult ListEspecialidadeMedica2Modal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupEspecialidadeMedica2Model l = new LookupEspecialidadeMedica2Model();
            return this.ListModal(index, pageSize, l, "Especialidades Médicas", descricao);
        }
        [AuthorizeFilter]
        public ActionResult ListEspecialidadeMedica3Modal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupEspecialidadeMedica3Model l = new LookupEspecialidadeMedica3Model();
            return this.ListModal(index, pageSize, l, "Especialidades Médicas", descricao);
        }
        [AuthorizeFilter]
        public ActionResult _ListEspecialidadeMedicaModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupEspecialidadeMedicaFiltroModel l = new LookupEspecialidadeMedicaFiltroModel();
            return this.ListModal(index, pageSize, l, "Especialidades Médicas", descricao);
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int especialidadeId)
        {
            return _Edit(new EspecialidadeMedicaViewModel() { especialidadeId = especialidadeId });
        }
        #endregion


        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int especialidadeId)
        {
            return Edit(especialidadeId);
        }
        #endregion

	}
}