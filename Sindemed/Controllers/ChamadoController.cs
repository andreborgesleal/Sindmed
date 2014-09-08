using App_Dominio.Contratos;
using App_Dominio.Controllers;
using App_Dominio.Entidades;
using App_Dominio.Security;
using DWM.Models.Entidades;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System;
using System.Web.Mvc;

namespace DWM.Controllers
{
    public class ChamadoController : ProcessController<ChamadoViewModel, ChamadoModel>
    {

        public override int _sistema_id() { return (int)DWM.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Chamados do Associado";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewChamadoAssociado l = new ListViewChamadoAssociado();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }
        #endregion

        public override void BeforeCreate(ref ChamadoViewModel value, ICrudContext<ChamadoViewModel> model, FormCollection collection)
        {
            ChamadoViewModel repository = new ChamadoModel().Create();

            value.chamadoStatusId = repository.chamadoStatusId;
            value.situacao = repository.situacao;
            value.associadoId = repository.associadoId;
            value.dt_chamado = repository.dt_chamado;
            value.nome_associado = repository.nome_associado;
        }
    }
}