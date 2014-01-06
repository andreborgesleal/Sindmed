using App_Dominio.Contratos;
using App_Dominio.Controllers;
using App_Dominio.Entidades;
using App_Dominio.Security;
using Sindemed.Models.Entidades;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class ChamadoController : ProcessController<ChamadoViewModel, ChamadoModel>
    {

        public override int _sistema_id() { return (int)Sindemed.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Chamados do Associado";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewChamadoAssociado l = new ListViewChamadoAssociado();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        #endregion

        public override void BeforeCreate(ref ChamadoViewModel value, ICrudContext<ChamadoViewModel> model, FormCollection collection)
        {
            ChamadoViewModel repository = new ChamadoModel().Create();

            value.situacao = repository.situacao;
            value.associadoId = repository.associadoId;
            value.dt_chamado = repository.dt_chamado;
            value.nome_associado = repository.nome_associado;
        }
    }
}