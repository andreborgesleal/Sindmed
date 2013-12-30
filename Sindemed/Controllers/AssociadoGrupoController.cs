using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Enumeracoes;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class AssociadoGrupoController : RootController<AssociadoGrupoViewModel, AssociadoGrupoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED; }
    
        public override string getListName()
        {
            return "Listar Associado Grupo";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewAssociadoGrupo l = new ListViewAssociadoGrupo();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int grupoAssociadoId, int associadoId)
        {
            return _Edit(new AssociadoGrupoViewModel() { grupoAssociadoId = grupoAssociadoId, associadoId = associadoId });
        }
        #endregion

    }
}