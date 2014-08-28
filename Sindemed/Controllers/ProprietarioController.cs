using App_Dominio.Controllers;
using App_Dominio.Security;
using App_Dominio.Contratos;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System.Web.Mvc;
using System;

namespace DWM.Controllers
{
    public class ProprietarioController : RootController<ProprietarioViewModel, ProprietarioModel>
    {
        public override int _sistema_id() { return (int)DWM.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Proprietários";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewProprietario l = new ListViewProprietario();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult ListProprietarioModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupProprietarioModel l = new LookupProprietarioModel();
            return this.ListModal(index, pageSize, l, "Proprietários", descricao);
        }
        [AuthorizeFilter]
        public ActionResult _ListProprietarioModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupProprietarioFiltroModel l = new LookupProprietarioFiltroModel();
                return this.ListModal(index, pageSize, l, "Proprietários", descricao);
            }
            else
                return View();
        }
        #endregion

        #region CreateError
        public override void OnCreateError(ref ProprietarioViewModel value, ICrudContext<ProprietarioViewModel> model, FormCollection collection)
        {
            if (collection["apto"] != null && collection["apto"] != "")
            {
                value.torreId = collection["apto"].Substring(0, 2);
                value.unidadeId = int.Parse(collection["apto"].Substring(2));
            }
            if (collection["dt_inicio"] != "")
                value.dt_inicio = DateTime.Parse(collection["dt_inicio"]);
            if (collection["dt_fim"] != "")
                value.dt_fim = DateTime.Parse(collection["dt_fim"]);
            value.nome = collection ["nome"];
            value.ind_tipo_pessoa = collection["ind_tipo_pessoa"];
            value.cpf_cnpj = collection["cpf_cnpj"];
            value.email = collection["email"];
            value.ind_est_civil = collection["ind_est_civil"];
            value.tel_contato1 = collection["tel_contato1"];
            value.tel_contato2 = collection["tel_contato2"];
            value.nome_conjuge = collection["nome_conjuge"];
            value.cpf_cnpj_conjuge = collection["cpf_cnpj_conjuge"];
            value.endereco = collection["endreco"];
            value.complemento = collection["complemento"];
            value.cidadeId = int.Parse(collection["cidadeId"]);
            value.cep = collection["cep"];
            value.uf = collection["uf"];
        }
        #endregion

        #region BeforeCreate
        public override void BeforeCreate(ref ProprietarioViewModel value, ICrudContext<ProprietarioViewModel> model, FormCollection collection)
        {
            if (collection["dt_inicio"] != "")
                value.dt_inicio = DateTime.Parse(collection["dt_inicio"].Substring(6, 4) + "-" + collection["dt_inicio"].Substring(3, 2) + "-" + collection["dt_inicio"].Substring(0, 2));

            if (collection["dt_fim"] != "")
                value.dt_fim = DateTime.Parse(collection["dt_fim"].Substring(6, 4) + "-" + collection["dt_fim"].Substring(3, 2) + "-" + collection["dt_fim"].Substring(0, 2));

            value.torreId = collection["apto"].Substring(0, 2);
            value.unidadeId = int.Parse(collection["apto"].Substring(2));
        }
        #endregion

        #region edit
        public override void BeforeEdit(ref ProprietarioViewModel value, ICrudContext<ProprietarioViewModel> model, FormCollection collection)
        {
            BeforeCreate(ref value, model, collection);
        }

        [AuthorizeFilter]
        public ActionResult Edit(int proprietarioId)
        {
            return _Edit(new ProprietarioViewModel() { proprietarioId = proprietarioId });
        }

        #region Edit Error
        public override void OnEditError(ref ProprietarioViewModel value, ICrudContext<ProprietarioViewModel> model, FormCollection collection)
        {
            OnCreateError(ref value, model, collection);
        }
        #endregion


        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int proprietarioId)
        {
            return Edit(proprietarioId);
        }

        #region Delete Error
        public override void OnDeleteError(ref ProprietarioViewModel value, ICrudContext<ProprietarioViewModel> model, FormCollection collection)
        {
            OnEditError(ref value, model, collection);
        }
        #endregion

        #endregion
    
    
    }
}