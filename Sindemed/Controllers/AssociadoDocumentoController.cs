using App_Dominio.Component;
using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Enumeracoes;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class AssociadoDocumentoController : ProcessController<AssociadoDocumentoViewModel, AssociadoDocumentoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED; }

        public override string getListName()
        {
            return "Listar Documentos";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest && Request["associadoId"] != null)
                return ListAssociadoDocumento(index, PageSize, int.Parse(Request["associadoId"]), descricao);
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult ListAssociadoDocumento(int? index, int? pageSize = 50, int? associadoId = null, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewAssociadoDocumento l = new ListViewAssociadoDocumento();
                return _List(index, pageSize, "Browse", l, associadoId, descricao);
            }
            else
                return View();
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int associadoId, string fileId)
        {
            return _Edit(new AssociadoDocumentoViewModel() { associadoId = associadoId, fileId = fileId });
        }

        [ValidateInput(false)]
        [AuthorizeFilter]
        [HttpPost]
        public override ActionResult Edit(AssociadoDocumentoViewModel value, FormCollection collection)
        {
            if (ViewBag.ValidateRequest)
            {
                AssociadoDocumentoViewModel ret = SetEdit(value, getModel(), collection);

                if (ret.mensagem.Code == 0)
                    return RedirectToAction("Browse", new { associadoId = ret.associadoId });
                else
                    return View(ret);
            }
            else
                return null;
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int associadoId, string fileId)
        {
            return Edit(associadoId, fileId);
        }

        [ValidateInput(false)]
        [AuthorizeFilter]
        [HttpPost]
        public override ActionResult Delete(AssociadoDocumentoViewModel value, FormCollection collection)
        {
            if (ViewBag.ValidateRequest)
            {
                AssociadoDocumentoViewModel ret = SetDelete(value, getModel(), collection);

                if (ret.mensagem.Code == 0)
                    return RedirectToAction("Browse", new { associadoId = ret.associadoId });
                else
                    return View(ret);
            }
            else
                return null;
        }
        #endregion
    }
}