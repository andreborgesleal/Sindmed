using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Enumeracoes;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class ComunicadoGeralController : RootController<ComunicacaoViewModel, ComunicacaoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Comunicados";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewComunicacao l = new ListViewComunicacao();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult ListComunicacaoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupComunicacaoModel l = new LookupComunicacaoModel();
            return this.ListModal(index, pageSize, l, "Comunicacaos", descricao);
        }

        public ActionResult _ListComunicacaoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupComunicacaoFiltroModel l = new LookupComunicacaoFiltroModel();
                return this.ListModal(index, pageSize, l, "Comunicacaos", descricao);

            }
            else
                return View();
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int comunicacaoId)
        {
            return _Edit(new ComunicacaoViewModel() { comunicacaoId = comunicacaoId });
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int comunicacaoId)
        {
            return Edit(comunicacaoId);
        }
        #endregion
    }
}