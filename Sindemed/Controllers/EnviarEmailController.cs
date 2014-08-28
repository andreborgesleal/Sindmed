using App_Dominio.Contratos;
using App_Dominio.Controllers;
using App_Dominio.Entidades;
using App_Dominio.Security;
using DWM.Models.Entidades;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DWM.Controllers
{
    public class EnviarEmailController : ExecController<EnviarEmailViewModel, EnviarEmailModel>
    {
        public override string getListName()
        {
            return "Listagem de E-mails";
        }

        public override bool mustListOnLoad()
        {
            return false;
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
                return ListParam(index, PageSize);
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult ListParam(int? index, int? pageSize = 50, string grupoAssociadoId = null, 
            string especialidadeId = null, string cidadeId = null, string aniversariante = null, string crm_inicial = null, 
            string crm_final = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewEnviarEmail list = new ListViewEnviarEmail();
                return _List(index, pageSize, "Browse", list, grupoAssociadoId, especialidadeId, cidadeId, aniversariante, crm_inicial, crm_final );
            }
            else
                return View();
        }
        #endregion

        [ValidateInput(false)]
        [HttpPost]
        [AuthorizeFilter]
        public virtual ActionResult Browse(EnviarEmailViewModel value, FormCollection collection)
        {
            if (ViewBag.ValidateRequest)
            {
                EnviarEmailViewModel ret = SetCreate(value, getModel(), collection);

                if (ret.mensagem.Code == 0)
                    return RedirectToAction("Browse");
                else
                    return View(ret);
            }
            else
                return null;
        }

        public override void BeforeCreate(ref EnviarEmailViewModel value, IExecContext<EnviarEmailViewModel> model, FormCollection collection)
        {
            if (value.Destinatarios == null)
            {
                ListViewEnviarEmail list = new ListViewEnviarEmail();
                value.Destinatarios = (IEnumerable<AssociadoViewModel>)list.ListRepository(0, 999999, collection["grupoAssociadoId"],
                                                                                           collection["especialidadeId"], collection["cidadeId"], collection["aniversariante"],
                                                                                           collection["crm_inicial"], collection["crm_final"]);
            }
        }
	}
}