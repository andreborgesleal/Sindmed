using App_Dominio.Controllers;
using App_Dominio.Security;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System.Web.Mvc;

namespace DWM.Controllers
{
    public class FolderController : RootController<DocFolderViewModel, DocFolderModel>
    {
        public override int _sistema_id() { return (int)DWM.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Pastas";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewDocFolder l = new ListViewDocFolder();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int docFolderId)
        {
            return _Edit(new DocFolderViewModel() { docFolderId = docFolderId });
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int docFolderId)
        {
            return Edit(docFolderId);
        }
        #endregion
    }
}