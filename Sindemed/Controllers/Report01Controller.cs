using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Report;
using Sindemed.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using App_Dominio.Entidades;
using App_Dominio.Contratos;

namespace Sindemed.Controllers
{
    public class Report01Controller : ReportController<RelacaoGeralViewModel>
    {
        public override bool mustListOnLoad()
        {
            return false;
        }

        public override int _sistema_id() { return (int)Sindemed.Models.Enumeracoes.Sistema.SINDMED; }

        public override string getListName()
        {
            return "Relação geral de associados";
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
        public ActionResult ListParam(int? index, int? pageSize = 50, string ind_sindicalizado = "", string crm_inicial = "", string crm_final = "", string especialidadeId = "", string grupoAssociadoId = "", string ind_email = "", string correioId = "")
        {
            if (ViewBag.ValidateRequest)
            {
                RelacaoGeralReport rep = new RelacaoGeralReport();
                return _List(index, pageSize, "Browse", rep, ind_sindicalizado, crm_inicial, crm_final, especialidadeId, grupoAssociadoId, ind_email, correioId);
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
                    ind_sindicalizado = collection ["ind_sindicalizado"] ,
                    crm_inicial = collection ["crm_inicial"],
                    crm_final = collection["crm_final"],
                    especialidadeId = collection ["especialidadeId"],
                    descricao_especialidade = collection ["descricao_especialidade"],
                    grupoAssociadoId = collection ["grupoAssociadoId"],
                    descricao_grupo =  collection["descricao_grupo"],
                    ind_email = collection["ind_email"],
                    correioId = collection["correioId"],
                    descricao_correio = collection["descricao_correio"]
                });
            else
                return View();            
        }

        [AuthorizeFilter]
        public ActionResult Print(string ind_sindicalizado = "", string crm_inicial = "", string crm_final = "", 
                                    string especialidadeId = "", string descricao_especialidade = "", 
                                    string grupoAssociadoId = "", string descricao_grupo = "", string ind_email = "",
                                    string correioId = "", string descricao_correio = "")
        {
            if (ViewBag.ValidateRequest)
            {
                IDictionary<string, string> header = new Dictionary<string, string>()
                    {
                       { "empresa", new EmpresaSecurity<App_DominioContext>().getEmpresa().nome },
                       { "sindicalizado", ind_sindicalizado == "" ? "Sindicalizados e Não Sindicalizados" : ind_sindicalizado == "S" ? "Sindicalizados" : "Não Sindicalizados" },
                       { "especialidade", "Especialidade: " + descricao_especialidade },
                       { "correio", "Localização Correio: " + descricao_correio },
                       { "grupo", "Grupo: " + descricao_grupo}
                    };

                ViewBag.Header = header;
                RelacaoGeralReport model = new RelacaoGeralReport();
                IEnumerable<RelacaoGeralViewModel> r = model.ListReportRepository(ind_sindicalizado, crm_inicial, crm_final, especialidadeId, grupoAssociadoId, ind_email, correioId);
                return View(r);
            }
            else
                return View();            
        }

        //[AuthorizeFilter]
        public FileResult PDF(string export, string ind_sindicalizado = "", string crm_inicial = "", string crm_final = "", string especialidadeId = "", string descricao_especialidade = "", string grupoAssociadoId = "", string descricao_grupo = "", string ind_email = "")
        {
            RelacaoGeralReport rep = new RelacaoGeralReport();
            ReportParameter[] p = new ReportParameter[4];
            // o parâmetro p[0] fica reservado para ser preenchido automaticamente com o nome da empresa
            p[1] = new ReportParameter("sindicalizado", ind_sindicalizado == "" ? "Sindicalizados e Não Sindicalizados" : ind_sindicalizado == "S" ? "Sindicalizados" : "Não Sindicalizados" , false);
            p[2] = new ReportParameter("especialidade", "Especialidade: " + descricao_especialidade, false);
            p[3] = new ReportParameter("grupo", "Grupo: " + descricao_grupo, false);

            return _PDF(export, "RelacaoGeralAssociados", rep, p, null, null, ind_sindicalizado, crm_inicial, crm_final, 
                        especialidadeId, grupoAssociadoId, ind_email);
        }
        #endregion
	}
}