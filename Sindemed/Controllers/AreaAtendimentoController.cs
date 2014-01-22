using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Enumeracoes;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class AreaAtendimentoController : RootController<AreaAtendimentoViewModel, AreaAtendimentoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED ; }
        public override string getListName()
        {
            return "Listar Áreas de Atendimento";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewAreaAtendimento l = new ListViewAreaAtendimento();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult ListAreaAtendimentoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupAreaAtendimentoModel l = new LookupAreaAtendimentoModel();
            return this.ListModal(index, pageSize, l, "Áreas de Atendimento", descricao);
        }
        [AuthorizeFilter]
        public ActionResult _ListAreaAtendimentoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupAreaAtendimentoFiltroModel l = new LookupAreaAtendimentoFiltroModel();
                return this.ListModal(index, pageSize, l, "Áreas de Atendimento", descricao);
            }
            else
                return View();
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int areaAtendimentoId)
        {
            return _Edit(new AreaAtendimentoViewModel() { areaAtendimentoId = areaAtendimentoId });
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int areaAtendimentoId)
        {
            return Edit(areaAtendimentoId);
        }
        #endregion
	}
}