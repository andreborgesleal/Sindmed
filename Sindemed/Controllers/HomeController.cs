using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Dominio.Controllers;
using App_Dominio.Security;
using App_Dominio.Negocio;
using Sindemed.Models;
using Sindemed.Models.Enumeracoes;
using Sindemed.Models.Repositories;
using Sindemed.Models.Persistence;

namespace Sindemed.Controllers
{
    public class HomeController : SuperController
    {
        #region Inheritance
        public override int _sistema_id() { return (int)Sistema.SINDMED ; }

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
            var q = new ListViewComunicacao().ListRepository(null);
            return View(q);
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

        #region Formulário Modal

        #region Formulário Modal Genérico
        public ActionResult LOVModal(IPagedList pagedList)
        {
            return View(pagedList);
        }
        #endregion

        #region Formulário Modal Usuario
        public ActionResult LovUsuarioModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupUsuarioModel(), "Usuários", null, Sistema.SINDMED);
        }

        public ActionResult LovUsuario2Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupUsuario2Model(), "Usuários", null, Sistema.SINDMED);
        }
        #endregion

        #endregion

    }
}