using App_Dominio.Controllers;
using App_Dominio.Security;
using DWM.Models.Enumeracoes;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System.Web.Mvc;

namespace DWM.Controllers
{
    public class ComunicacaoGrupoController : RootController<ComunicacaoGrupoViewModel, ComunicacaoGrupoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED; }

        public override string getListName()
        {
            return "Listar Comunicacao Grupo";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewComunicacaoGrupo l = new ListViewComunicacaoGrupo();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int comunicacaoId, int grupoAssociadoId)
        {
            return _Edit(new ComunicacaoGrupoViewModel() { comunicacaoId = comunicacaoId, grupoAssociadoId = grupoAssociadoId });
        }
        #endregion
	}
}