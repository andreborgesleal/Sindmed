using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Dominio.Controllers;
using App_Dominio.Security;
using App_Dominio.Negocio;
using DWM.Models;
using DWM.Models.Enumeracoes;
using DWM.Models.Repositories;
using DWM.Models.Persistence;
using App_Dominio.Entidades;
using App_Dominio.Enumeracoes;

namespace DWM.Controllers
{
    public class HomeController : SuperController
    {
        #region Inheritance
        public override int _sistema_id() { return (int)DWM.Models.Enumeracoes.Sistema.SINDMED ; }

        public override string getListName()
        {
            return "Detalhar";
        }

        public override ActionResult List(int? index, int? PageSize, string descricao = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        public ActionResult Default()
        {
            return View();
        }

        [AuthorizeFilter]
        public ActionResult Index()
        {
            var _com = new ListViewComunicacao();
            IEnumerable<ComunicacaoViewModel> _com1 = (IEnumerable<ComunicacaoViewModel>)_com.ListRepository(null);

            var _comGrupo = new ListViewComunicacaoGrupoEspecifico();
            IEnumerable<ComunicacaoViewModel> _comGrupo1 = (IEnumerable<ComunicacaoViewModel>)_comGrupo.ListRepository(null);

            #region documentos da administração
            var _docInterno = new ListViewDocInterno();
            var _folder = new ListViewDocFolder();
            
            string _descricao = null;
            string _dt_inicio = DateTime.Today.AddMonths(-6).ToString("yyyy-MM-dd");;
            string _docFolderId = null;

            #region Últimos documentos
            IEnumerable<DocInternoViewModel> _docInterno1 = (IEnumerable<DocInternoViewModel>)_docInterno.ListRepository(0, 6, _descricao, _dt_inicio, _docFolderId);
            #endregion

            #region Documentos Favoritos
            _dt_inicio = null;
            DocFolderViewModel _folder1 = (DocFolderViewModel)_folder.getRepository("FAVORITOS");
            IEnumerable<DocInternoViewModel> _favoritos1 = (IEnumerable<DocInternoViewModel>)_docInterno.ListRepository(0, 10, _descricao, _dt_inicio, _folder1.docFolderId);
            #endregion

            #endregion

            #region Sessão Corrente
            App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext> security = new App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext>();
            App_Dominio.Entidades.Sessao sessaoCorrente = security.getSessaoCorrente();

            TempData.Remove("SessaoCorrente");
            TempData.Add("SessaoCorrente", sessaoCorrente);
            #endregion

            ComunicacaoGlobalViewModel q = new ComunicacaoGlobalViewModel()
            {
                comGrupo = _comGrupo1,
                com = _com1,
                docInterno = _docInterno1.Where(info => info.docFolderId != _folder1.docFolderId).ToList(),
                favoritos = _favoritos1
            };

            return View(q);
        }

        [AuthorizeFilter]
        public ActionResult detail(int? comunicacaoId)
        {
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            Sessao sessaoCorrente = security.getSessaoCorrente();

            TempData.Remove("SessaoCorrente");
            TempData.Add("SessaoCorrente", sessaoCorrente);

            if (ViewBag.ValidateRequest)
            {
                BindBreadCrumb(getListName(), true);
                ComentarioModel com = new ComentarioModel();
                ComentarioViewModel value = com.CreateRepository(Request);

                return View(value);
            }
            else
                return View();
        }

        public ActionResult _detail(int comunicacaoId, string descricao)
        {
            Sessao sessaoCorrente = null;

            if (TempData.Peek("SessaoCorrente") == null)
            {
                EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
                sessaoCorrente = security.getSessaoCorrente();
                TempData.Add("SessaoCorrente", sessaoCorrente);
            }
            else
                sessaoCorrente = (Sessao)TempData.Peek("SessaoCorrente");

            ComentarioModel model = new ComentarioModel();
            ComentarioViewModel value = new ComentarioViewModel()
            {
                comunicacaoId = comunicacaoId,
                associadoId = int.Parse(sessaoCorrente.value1),
                dt_comentario = DateTime.Now,
                descricao = descricao
            };

            try
            {
                value.uri = this.ControllerContext.Controller.GetType().Name.Replace("Controller", "") + "/" + this.ControllerContext.RouteData.Values["action"].ToString();

                value = model.Insert(value);
                if (value.mensagem.Code > 0)
                    throw new App_DominioException(value.mensagem);
            }
            catch (App_DominioException ex)
            {
                if (ex.Result.MessageType == MsgType.ERROR)
                    Error(ex.Result.MessageBase); // Mensagem em inglês com a descrição detalhada do erro e fica no topo da tela
                else
                    Attention(ex.Result.MessageBase); // Mensagem em inglês com a descrição detalhada do erro e fica no topo da tela
            }
            catch (Exception ex)
            {
                App_DominioException.saveError(ex, GetType().FullName);
                Error(ex.Message); // Mensagem em inglês com a descrição detalhada do erro e fica no topo da tela
            }
            value = model.Create(comunicacaoId);
            return View(value);
        }

        public ActionResult _delComment(int comentarioId, int comunicacaoId)
        {
            Sessao sessaoCorrente = null;

            if (TempData.Peek("SessaoCorrente") == null)
            {
                EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
                sessaoCorrente = security.getSessaoCorrente();
                TempData.Add("SessaoCorrente", sessaoCorrente);
            }
            else
                sessaoCorrente = (Sessao)TempData.Peek("SessaoCorrente");

            ComentarioModel model = new ComentarioModel();
            ComentarioViewModel key = new ComentarioViewModel()
            {
                comentarioId = comentarioId
            };

            try
            {
                ComentarioViewModel value = (ComentarioViewModel)model.getObject(key);
                value.uri = this.ControllerContext.Controller.GetType().Name.Replace("Controller", "") + "/" + this.ControllerContext.RouteData.Values["action"].ToString();

                value = model.Delete(value);
                if (value.mensagem.Code > 0)
                    throw new App_DominioException(value.mensagem);
            }
            catch (App_DominioException ex)
            {
                if (ex.Result.MessageType == MsgType.ERROR)
                    Error(ex.Result.MessageBase); // Mensagem em inglês com a descrição detalhada do erro e fica no topo da tela
                else
                    Attention(ex.Result.MessageBase); // Mensagem em inglês com a descrição detalhada do erro e fica no topo da tela
            }
            catch (Exception ex)
            {
                App_DominioException.saveError(ex, GetType().FullName);
                Error(ex.Message); // Mensagem em inglês com a descrição detalhada do erro e fica no topo da tela
            }
            key = model.Create(comunicacaoId);
            return View("_detail",key);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sistema de Cadastro de Associados";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Sindicato dos Médicos do Estado do Pará.";

            return View();
        }

        public ActionResult _Error()
        {
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
        [AuthorizeFilter]
        public ActionResult LovUsuarioModal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupUsuarioModel(), "Usuários", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult LovUsuario2Modal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupUsuario2Model(), "Usuários", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        #endregion

        #region Formulário Modal Usuario Médico
        [AuthorizeFilter]
        public ActionResult LovUsuarioMedicoModal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupUsuarioMedicoModel(), "Usuários", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        #endregion

        #region Formulário Modal Cidade
        [AuthorizeFilter]
        public ActionResult LovCidadeModal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupCidadeModel(), "Cidades", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult LovCidadeComModal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupCidadeComModel(), "Cidades", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        #endregion

        #region Formulário Modal Área de Atuação
        [AuthorizeFilter]
        public ActionResult LovAreaAtuacao1Modal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupAreaAtuacao1Model(), "Área de Atuação", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult LovAreaAtuacao2Modal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupAreaAtuacao2Model(), "Área de Atuação", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult LovAreaAtuacao3Modal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupAreaAtuacao3Model(), "Área de Atuação", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        #endregion

        #region Formulário Modal Especialidade Médica
        [AuthorizeFilter]
        public ActionResult LovEspecialidadeMedica1Modal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupEspecialidadeMedica1Model(), "Especialidade Médica", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult LovEspecialidadeMedica2Modal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupEspecialidadeMedica2Model(), "Especialidade Médica", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult LovEspecialidadeMedica3Modal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupEspecialidadeMedica3Model(), "Especialidade Médica", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        #endregion

        #region Formulário Modal Condômino
        [AuthorizeFilter]
        public ActionResult LovMedicoModal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupAssociadoModel(), "Condômino", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        #endregion

        #region Formulário Modal Grupo Condômino
        [AuthorizeFilter]        
        public ActionResult LovGrupoAssociadoModal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupGrupoAssociadoModel(), "Grupo Condômino", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        #endregion

        #region Formulário Modal Comunicacao
        [AuthorizeFilter]
        public ActionResult LovComunicacaoModal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupComunicacaoModel(), "Comunicacao", null, DWM.Models.Enumeracoes.Sistema.SINDMED);
            else
                return View();
        }
        #endregion

        #endregion

        #region Blogs
        public ActionResult Blog01()
        {
            return View();
        }
        public ActionResult Blog02()
        {
            return View();
        }
        #endregion

        #region dicas jurídicas
        public ActionResult DicaJuridica01()
        {
            return View();
        }

        public ActionResult DicaJuridica02()
        {
            return View();
        }
        #endregion

    }
}