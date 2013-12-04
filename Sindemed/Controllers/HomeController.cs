using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Dominio.Controllers;
using App_Dominio.Security;

namespace StockLite.Controllers
{
    public class HomeController : SuperController
    {
        #region Inheritance
        public override int _sistema_id() { return 2; }

        public override string getListName()
        {
            return "Página Inicial";
        }

        public override ActionResult List(int? index, int? PageSize, string descricao = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        [AuthorizeFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sistema de Controle de Associados.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Arquiteto de Software.";

            return View();
        }
    }
}