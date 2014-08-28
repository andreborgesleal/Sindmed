using App_Dominio.Controllers;
using App_Dominio.Security;
using DWM.Models.Enumeracoes;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System.Web.Mvc;

namespace DWM.Controllers
{
    public class NaoLocalizadoCorreioController : RootController<NaoLocalizadoCorreioViewModel, NaoLocalizadoCorreioModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED ; }
        public override string getListName()
        {
            return "Correios";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewNaoLocalizadoCorreio l = new ListViewNaoLocalizadoCorreio();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult ListNaoLocalizadoCorreioModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupNaoLocalizadoCorreioModel l = new LookupNaoLocalizadoCorreioModel();
            return this.ListModal(index, pageSize, l, "Correios", descricao);
        }
        [AuthorizeFilter]
        public ActionResult _ListNaoLocalizadoCorreioModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupNaoLocalizadoCorreioFiltroModel l = new LookupNaoLocalizadoCorreioFiltroModel();
                return this.ListModal(index, pageSize, l, "Correios", descricao);
            }
            else
                return View();
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int correioId)
        {
            return _Edit(new NaoLocalizadoCorreioViewModel() { correioId = correioId });
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int correioId)
        {
            return Edit(correioId);
        }
        #endregion
    }
}