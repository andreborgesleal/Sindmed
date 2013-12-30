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

        #region Formulário Modal Cidade
        public ActionResult LovCidadeModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupCidadeModel(), "Cidades", null, Sistema.SINDMED);
        }

        public ActionResult LovCidadeComModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupCidadeComModel(), "Cidades", null, Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal NaoLocalizadoCorreio
        public ActionResult LovNaoLocalizadoCorreioModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupNaoLocalizadoCorreioModel(), "Motivos Correios", null, Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Área de Atuação
        public ActionResult LovAreaAtuacao1Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupAreaAtuacao1Model(), "Área de Atuação", null, Sistema.SINDMED);
        }

        public ActionResult LovAreaAtuacao2Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupAreaAtuacao2Model(), "Área de Atuação", null, Sistema.SINDMED);
        }

        public ActionResult LovAreaAtuacao3Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupAreaAtuacao3Model(), "Área de Atuação", null, Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Especialidade Médica
        public ActionResult LovEspecialidadeMedica1Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupEspecialidadeMedica1Model(), "Especialidade Médica", null, Sistema.SINDMED);
        }

        public ActionResult LovEspecialidadeMedica2Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupEspecialidadeMedica2Model(), "Especialidade Médica", null, Sistema.SINDMED);
        }

        public ActionResult LovEspecialidadeMedica3Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupEspecialidadeMedica3Model(), "Especialidade Médica", null, Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Associado
        public ActionResult LovMedicoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupMedicoModel(), "Associado", null, Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Grupo Associado
        public ActionResult LovGrupoAssociadoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupGrupoAssociadoModel(), "Grupo Associado", null, Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Comunicacao
        public ActionResult LovComunicacaoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupComunicacaoModel(), "Comunicacao", null, Sistema.SINDMED);
        }
        #endregion

        #endregion

    }
}