using App_Dominio.Controllers;
using App_Dominio.Security;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System.Web.Mvc;
using App_Dominio.Contratos;
using App_Dominio.Enumeracoes;
using System.IO;
using System;
using App_Dominio.Models;

namespace DWM.Controllers
{
    public class DocumentosController : RootController<DocInternoViewModel, DocInternoModel>
    {
        public override int _sistema_id() { return (int)DWM.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Documentos";
        }

        public override bool mustListOnLoad()
        {
            return false;
        }

        #region List
        [HttpGet]
        [AuthorizeFilter]
        public ActionResult Browse(int? index, int? pageSize = 50, string descricao = null, string docFolderId = null, string dt_inicio = null)
        {
            if (ViewBag.ValidateRequest)
            {
                BindBreadCrumb(getListName(), true);

                TempData.Remove("Controller");
                TempData.Add("Controller", this.ControllerContext.RouteData.Values["controller"].ToString());

                return ListParam(index, this.PageSize, descricao, dt_inicio ?? "3", docFolderId);
            }
            else
                return null;
        }

        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
                return ListParam(index, PageSize);
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult ListParam(int? index, int? pageSize = 50, string descricao = null, string dt_inicio = null, string docFolderId = null)
        {
            if (ViewBag.ValidateRequest)
            {
                #region Sessão Corrente
                App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext> security = new App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext>();
                App_Dominio.Entidades.Sessao sessaoCorrente = security.getSessaoCorrente();

                TempData.Remove("SessaoCorrente");
                TempData.Add("SessaoCorrente", sessaoCorrente);
                #endregion
                
                string _dt_inicio = null;
                DateTime? d = null;
                if (dt_inicio != null && dt_inicio != "" )
                {
                    if (int.Parse(dt_inicio) < 30)
                        d = DateTime.Today.AddMonths(-int.Parse(dt_inicio));
                    else
                        d = DateTime.Today.AddDays(-int.Parse(dt_inicio));
                    _dt_inicio = d.Value.ToString("yyyy-MM-dd");
                }

                ListViewDocInterno list = new ListViewDocInterno();
                return _List(index, pageSize, "Browse", list, descricao, _dt_inicio, docFolderId);
            }
            else
                return View();
        }
        #endregion

        #region BeforeCreate
        public override void BeforeCreate(ref DocInternoViewModel value, ICrudContext<DocInternoViewModel> model, FormCollection collection)
        {
            #region upload dos arquivo
            if (this.Request.Files.Count > 0 && this.Request.Files[0].FileName.Trim() != "") // arquivo PDF
            {
                value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString() };

                #region verifica o tamanho do arquivo
                if (this.Request.Files[0].ContentLength > int.Parse(System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"])) // 1 mb
                {
                    value.mensagem.Code = 51;
                    value.mensagem.Field = "arquivo";
                    value.mensagem.Message = MensagemPadrao.Message(51, System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"]).ToString();
                    value.mensagem.MessageBase = "O tamanho do arquivo deve ser menor que " + Convert.ToString((int.Parse(System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"]) / 1024) / 1000) + " mb";
                    Error(value.mensagem.Message);
                    return;
                }
                #endregion

                #region Verifica o formato do arquivo
                System.IO.FileInfo f = new FileInfo(Request.Files[0].FileName);

                if (!(".png|.jpg|.bmp|.gif|.jpeg|.pdf|.docx|.doc|.zip|.rar").Contains(f.Extension.ToLower()))
                {
                    value.mensagem.Code = 52;
                    value.mensagem.Field = "arquivo";
                    value.mensagem.Message = MensagemPadrao.Message(52, ".png|.jpg|.bmp|.gif|.jpeg|.pdf|.docx|.doc|.zip|.rar").ToString();
                    value.mensagem.MessageBase = "Formato de arquivo inválido";
                    Error(value.mensagem.Message);
                    return;
                }
                #endregion

                #region Enviar o arquivo
                value.arquivo = String.Format("{0}" + f.Extension, Guid.NewGuid().ToString());
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Admin_Data"])), value.arquivo);
                this.Request.Files[0].SaveAs(filePath);
                #endregion
            }
            #endregion
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int docId)
        {
            return _Edit(new DocInternoViewModel() { docId = docId });
        }

        public override void BeforeEdit(ref DocInternoViewModel value, ICrudContext<DocInternoViewModel> model, FormCollection collection)
        {
            var arquivo = value.arquivo; // arquivo original

            BeforeCreate(ref value, model, collection);

            #region Excluir arquivo
            if (this.Request.Files.Count > 0 && this.Request.Files[0].FileName.Trim() != "" && value.mensagem.Code == 0)
            {
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Admin_Data"])), arquivo);
                System.IO.FileInfo f = new FileInfo(filePath);
                if (f.Exists)
                    f.Delete();
            }
            #endregion
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int docId)
        {
            return Edit(docId);
        }

        public override void BeforeDelete(ref DocInternoViewModel value, ICrudContext<DocInternoViewModel> model, FormCollection collection)
        {
            #region Excluir arquivo
            var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Admin_Data"])), value.arquivo);
            System.IO.FileInfo f = new FileInfo(filePath);
            if (f.Exists)
                f.Delete();
            #endregion
        }
        #endregion
    }
}