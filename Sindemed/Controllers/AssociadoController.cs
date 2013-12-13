using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class AssociadoController : RootController<MedicoViewModel, MedicoModel>
    {
        public override int _sistema_id() { return (int)Sindemed.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Associados";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewMedico l = new ListViewMedico();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        [AuthorizeFilter]
        public ActionResult ListMedicoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupMedicoModel l = new LookupMedicoModel();
            return this.ListModal(index, pageSize, l, "Médicos", descricao);
        }
        [AuthorizeFilter]
        public ActionResult _ListMedicoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupMedicoFiltroModel l = new LookupMedicoFiltroModel();
            return this.ListModal(index, pageSize, l, "Médicos", descricao);
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int associadoId)
        {
            return _Edit(new MedicoViewModel() { associadoId = associadoId });
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int associadoId)
        {
            return Edit(associadoId);
        }
        #endregion

	}
}