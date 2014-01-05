﻿using System;
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
using App_Dominio.Entidades;

namespace Sindemed.Controllers
{
    public class HomeController : SuperController
    {
        #region Inheritance
        public override int _sistema_id() { return (int)Sindemed.Models.Enumeracoes.Sistema.SINDMED ; }

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
            var _com = new ListViewComunicacao();
            IEnumerable<ComunicacaoViewModel> _com1 = (IEnumerable<ComunicacaoViewModel>)_com.ListRepository(null);

            var _comGrupo = new ListViewComunicacaoGrupoEspecifico();
            IEnumerable<ComunicacaoViewModel> _comGrupo1 = (IEnumerable<ComunicacaoViewModel>)_comGrupo.ListRepository(null);

            ComunicacaoGlobalViewModel q = new ComunicacaoGlobalViewModel()
            {
                comGrupo = _comGrupo1,
                com = _com1
            };

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

        #region Alerta - segurança
        public ActionResult ReadAlert(int? alertaId)
        {
            try
            {
                EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
                if (alertaId.HasValue && alertaId > 0)
                    security.ReadAlert(alertaId.Value);
            }
            catch
            {
                return null;
            }

            return null;
        }
        #endregion

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
            return this.ListModal(index, pageSize, new LookupUsuarioModel(), "Usuários", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }

        public ActionResult LovUsuario2Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupUsuario2Model(), "Usuários", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Usuario Médico
        public ActionResult LovUsuarioMedicoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupUsuarioMedicoModel(), "Usuários", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Cidade
        public ActionResult LovCidadeModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupCidadeModel(), "Cidades", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }

        public ActionResult LovCidadeComModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupCidadeComModel(), "Cidades", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal NaoLocalizadoCorreio
        public ActionResult LovNaoLocalizadoCorreioModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupNaoLocalizadoCorreioModel(), "Motivos Correios", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Área de Atuação
        public ActionResult LovAreaAtuacao1Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupAreaAtuacao1Model(), "Área de Atuação", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }

        public ActionResult LovAreaAtuacao2Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupAreaAtuacao2Model(), "Área de Atuação", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }

        public ActionResult LovAreaAtuacao3Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupAreaAtuacao3Model(), "Área de Atuação", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Especialidade Médica
        public ActionResult LovEspecialidadeMedica1Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupEspecialidadeMedica1Model(), "Especialidade Médica", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }

        public ActionResult LovEspecialidadeMedica2Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupEspecialidadeMedica2Model(), "Especialidade Médica", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }

        public ActionResult LovEspecialidadeMedica3Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupEspecialidadeMedica3Model(), "Especialidade Médica", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Associado
        public ActionResult LovMedicoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupMedicoModel(), "Associado", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Grupo Associado
        public ActionResult LovGrupoAssociadoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupGrupoAssociadoModel(), "Grupo Associado", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }
        #endregion

        #region Formulário Modal Comunicacao
        public ActionResult LovComunicacaoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupComunicacaoModel(), "Comunicacao", null, Sindemed.Models.Enumeracoes.Sistema.SINDMED);
        }
        #endregion

        #endregion

    }
}