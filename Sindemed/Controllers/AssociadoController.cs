using App_Dominio.Controllers;
using App_Dominio.Security;
using App_Dominio.Contratos;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Web.Mvc;
using System;

namespace Sindemed.Controllers
{
    public class AssociadoController : RootController<MedicoViewModel, MedicoModel>
    {
        public override int _sistema_id() { return (int)Sindemed.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Associados";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewMedico l = new ListViewMedico();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        [AuthorizeFilter]
        public ActionResult ListMedicoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupMedicoModel l = new LookupMedicoModel();
            return this.ListModal(index, pageSize, l, "Médicos", descricao);
        }
        [AuthorizeFilter]
        public ActionResult _ListMedicoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupMedicoFiltroModel l = new LookupMedicoFiltroModel();
            return this.ListModal(index, pageSize, l, "Médicos", descricao);
        }
        #endregion

        #region CreateError
        public override void OnCreateError(ref MedicoViewModel value, ICrudContext<MedicoViewModel> model, FormCollection collection)
        {
            value.nome_correio = collection["nome_correio1"] ?? "";
            value.nome_cidade = collection["nome_cidade1"] ?? "";
            value.nome_cidadeCom = collection["nome_cidadeCom1"] ?? "";
            value.descricao_areaAtuacao1 = collection["descricao_areaAtuacao11"] ?? "";
            value.descricao_areaAtuacao2 = collection["descricao_areaAtuacao21"] ?? "";
            value.descricao_areaAtuacao3 = collection["descricao_areaAtuacao31"] ?? "";
            value.nome_especialidade1 = collection["descricao_especialidade11"] ?? "";
            value.nome_especialidade2 = collection["descricao_especialidade21"] ?? "";
            value.nome_especialidade3 = collection["descricao_especialidade31"] ?? "";
        }
        #endregion

        #region BeforeCreate
        public override void BeforeCreate(ref MedicoViewModel value, ICrudContext<MedicoViewModel> model, FormCollection collection)
        {
            if (collection["dt_nascimento"] != "")
                value.dt_nascimento = DateTime.Parse(collection["dt_nascimento"].Substring(6, 4) + "-" + collection["dt_nascimento"].Substring(3, 2) + "-" + collection["dt_nascimento"].Substring(0, 2));

            if (collection["dt_admin_sindicato"] != "")
                value.dt_admin_sindicato = DateTime.Parse(collection["dt_admin_sindicato"].Substring(6, 4) + "-" + collection["dt_admin_sindicato"].Substring(3, 2) + "-" + collection["dt_admin_sindicato"].Substring(0, 2));
        }
        #endregion

        #region edit
        public override void BeforeEdit(ref MedicoViewModel value, ICrudContext<MedicoViewModel> model, FormCollection collection)
        {
            BeforeCreate(ref value, model, collection);
        }

        [AuthorizeFilter]
        public ActionResult Edit(int associadoId)
        {
            return _Edit(new MedicoViewModel() { associadoId = associadoId });
        }

        #region Edit Error
        public override void OnEditError(ref MedicoViewModel value, ICrudContext<MedicoViewModel> model, FormCollection collection)
        {
            value.nome_usuario = collection["nome_usuario1"] ?? "";
            OnCreateError(ref value, model, collection);

        }
        #endregion


        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int associadoId)
        {
            return Edit(associadoId);
        }

        #region Delete Error
        public override void OnDeleteError(ref MedicoViewModel value, ICrudContext<MedicoViewModel> model, FormCollection collection)
        {
            OnEditError(ref value, model, collection);

        }
        #endregion

        #endregion

	}
}