using App_Dominio.Contratos;
using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System;
using System.Web.Mvc;

namespace Sindemed.Controllers
{
    public class AtendimentoController : ProcessController<AtendimentoViewModel, AtendimentoModel>
    {
        public override int _sistema_id() { return (int)Sindemed.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Atendimentos";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            return ListChamados(index, PageSize);
        }

        //[AuthorizeFilter]
        public ActionResult ListChamados(int? index, int? pageSize = 50, int? chamadoId = null, int? associadoId = null, 
                                    string nome_associado1 = null, string nome_associado = null, string data1 = null, string data2 = null, 
                                    int? areaAtendimento = null, string situacao = null)
        {
            DateTime _data1 = DateTime.Parse("01/" + DateTime.Today.ToString("MM/yyyy"));
            DateTime _data2 = DateTime.Parse(DateTime.Today.AddDays(1).ToString("dd/MM/yyyy"));

            if (data1 != null && data1 != "")
                _data1 = DateTime.Parse(data1);

            if (data2 != null && data2 != "")
                _data2 = DateTime.Parse(data2).AddDays(1);
            
            ListViewChamadoAdministracao l = new ListViewChamadoAdministracao();
            return _List(index, pageSize, "Browse", l, chamadoId, associadoId, _data1, _data2, areaAtendimento, situacao);
        }
        #endregion

        #region Create
        [AuthorizeFilter]
        [HttpGet]
        public ActionResult Create(AtendimentoViewModel value)
        {
            //if (AccessDenied(System.Web.HttpContext.Current.Session.SessionID))
            //    return RedirectToAction("Index", "Home");

            GetCreate();

            value = getModel().Create(value);

            if (value.chamado.situacao != "F")
                return View(value);
            else
                return RedirectToAction("Detail", new AtendimentoViewModel() { chamadoId = value.chamadoId });
        }

        [ValidateInput(false)]
        [HttpPost]
        [AuthorizeFilter]
        public override ActionResult Create(AtendimentoViewModel value, FormCollection collection)
        {
            //if (AccessDenied(System.Web.HttpContext.Current.Session.SessionID))
            //    return RedirectToAction("Index", "Home");

            AtendimentoViewModel ret = SetCreate(value, getModel(), collection);

            if (ret.mensagem.Code == 0 && collection["fluxo"] == "2")
                return RedirectToAction("Browse");
            else if (ret.mensagem.Code == 0)
                return RedirectToAction("Browse", "Chamado");
            else
                return View(ret);
        }
       
        public override void OnCreateError(ref AtendimentoViewModel value, ICrudContext<AtendimentoViewModel> model, FormCollection collection)
        {
            AtendimentoModel atendimentoModel = new AtendimentoModel();
            value = atendimentoModel.Create(value);
            value.mensagemResposta = collection["mensagemResposta"];
        }
        #endregion

        #region Fechar

        [AuthorizeFilter]
        public ActionResult Fechar(AtendimentoViewModel value)
        {
            return Create(value);
        }

        [ValidateInput(false)]
        [HttpPost]
        [AuthorizeFilter]
        public ActionResult Fechar(AtendimentoViewModel value, FormCollection collection)
        {
            //if (AccessDenied(System.Web.HttpContext.Current.Session.SessionID))
            //    return RedirectToAction("Index", "Home");

            AtendimentoViewModel ret = SetCreate(value, new FechamentoChamadoModel(), collection);

            if (ret.mensagem.Code == 0 && collection["fluxo"] == "2")
                return RedirectToAction("Browse");
            else if (ret.mensagem.Code == 0)
                return RedirectToAction("Browse", "Chamado");
            else
                return View(ret);
        }
        #endregion

    }
}