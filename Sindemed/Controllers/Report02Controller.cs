using App_Dominio.Controllers;
using App_Dominio.Security;
using DWM.Models.Report;
using DWM.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using App_Dominio.Entidades;
using App_Dominio.Contratos;

namespace DWM.Controllers
{
    public class Report02Controller : ReportController<AtendimentoPendenteViewModel>
    {
        public override bool mustListOnLoad()
        {
            return false;
        }

        public override int _sistema_id() { return (int)DWM.Models.Enumeracoes.Sistema.SINDMED; }

        public override string getListName()
        {
            return "Relação de atendimentos pendentes";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? PageSize, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
                return ListParam(index, PageSize);
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult ListParam(int? index, int? pageSize = 50, string areaAtendimentoId = "")
        {
            if (ViewBag.ValidateRequest)
            {
                AtendimentoPendenteReport rep = new AtendimentoPendenteReport();
                return _List(index, pageSize, "Browse", rep, areaAtendimentoId);
            }
            else
                return View();
        }

        [AuthorizeFilter]
        [HttpPost]
        public ActionResult Browse(FormCollection collection)
        {
            if (ViewBag.ValidateRequest)
                return RedirectToAction("Print", new
                {
                    areaAtendimentoId = collection["areaAtendimentoId"],
                    descricao_areaAtendimento = collection["descricao_areaAtendimento"]
                });
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult Print(string areaAtendimentoId = "", string descricao_areaAtendimento = "")
        {
            if (ViewBag.ValidateRequest)
            {
                IDictionary<string, string> header = new Dictionary<string, string>()
                    {
                       { "empresa", new EmpresaSecurity<App_DominioContext>().getEmpresa().nome },
                       { "areaAtendimento", "Área de Atendimento: " + descricao_areaAtendimento }
                    };

                ViewBag.Header = header;
                AtendimentoPendenteReport model = new AtendimentoPendenteReport();
                IEnumerable<AtendimentoPendenteViewModel> r = model.ListReportRepository(areaAtendimentoId);
                return View(r);
            }
            else
                return View();
        }

        //[AuthorizeFilter]
        //public FileResult PDF(string export, string areaAtendimentoId = "", string descricao_areaAtendimento = "")
        //{
        //    AtendimentoPendenteReport rep= new AtendimentoPendenteReport();
        //    ReportParameter[] p = new ReportParameter[4];
        //    // o parâmetro p[0] fica reservado para ser preenchido automaticamente com o nome da empresa
        //    p[2] = new ReportParameter("areaAtendimento", "Área de Atendimento: " + descricao_areaAtendimento, false);

        //    return _PDF(export, "AtendimentoPendente", rep, p, null, null, areaAtendimentoId);
        //}
        #endregion

	}
}