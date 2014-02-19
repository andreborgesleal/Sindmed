using App_Dominio.Component;
using App_Dominio.Controllers;
using App_Dominio.Security;
using Sindemed.Models.Enumeracoes;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using App_Dominio.Enumeracoes;
using App_Dominio.Contratos;

namespace Sindemed.Controllers
{
    public class AssociadoDocumentoController : ProcessController<AssociadoDocumentoViewModel, AssociadoDocumentoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED; }

        public override string getListName()
        {
            return "Listar Documentos";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest && Request["associadoId"] != null)
                return ListAssociadoDocumento(index, PageSize, int.Parse(Request["associadoId"]), descricao);
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult ListAssociadoDocumento(int? index, int? pageSize = 50, int? associadoId = null, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewAssociadoDocumento l = new ListViewAssociadoDocumento();
                return _List(index, pageSize, "Browse", l, associadoId, descricao);
            }
            else
                return View();
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int associadoId, string fileId)
        {
            return _Edit(new AssociadoDocumentoViewModel() { associadoId = associadoId, fileId = fileId });
        }

        [ValidateInput(false)]
        [AuthorizeFilter]
        [HttpPost]
        public override ActionResult Edit(AssociadoDocumentoViewModel value, FormCollection collection)
        {
            if (ViewBag.ValidateRequest)
            {
                AssociadoDocumentoViewModel ret = SetEdit(value, getModel(), collection);

                if (ret.mensagem.Code == 0)
                    return RedirectToAction("Browse", new { associadoId = ret.associadoId });
                else
                    return View(ret);
            }
            else
                return null;
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int associadoId, string fileId)
        {
            return Edit(associadoId, fileId);
        }

        [ValidateInput(false)]
        [AuthorizeFilter]
        [HttpPost]
        public override ActionResult Delete(AssociadoDocumentoViewModel value, FormCollection collection)
        {
            if (ViewBag.ValidateRequest)
            {
                AssociadoDocumentoViewModel ret = SetDelete(value, getModel(), collection);

                if (ret.mensagem.Code == 0)
                    return RedirectToAction("Browse", new { associadoId = ret.associadoId });
                else
                    return View(ret);
            }
            else
                return null;
        }
        #endregion

        #region Upload
        [AuthorizeFilter]
        public ActionResult Upload()
        {
            if (ViewBag.ValidateRequest)
            {
                GetCreate();
                return View(getModel().CreateRepository(Request));
            }
            else
                return null;
        }

        [HttpPost]
        [AuthorizeFilter]
        public ActionResult Upload(AssociadoDocumentoViewModel value, FormCollection collection)
        {
            if (ViewBag.ValidateRequest)
            {
                if (this.Request.Files.Count > 0 && this.Request.Files[0].FileName.Trim() != "")
                {
                    value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString() };

                    #region verifica o tamanho do arquivo
                    if (this.Request.Files[0].ContentLength > int.Parse(System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"])) // 1 mb
                    {
                        value.mensagem.Code = 51;
                        value.mensagem.Field = "arquivo";
                        value.mensagem.Message = MensagemPadrao.Message(51, System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"]).ToString();
                        value.mensagem.MessageBase = "O tamanho do arquivo deve ser maior que " + System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"] + " kb";
                        Error(value.mensagem.Message);
                        return View(value);
                    }
                    #endregion

                    #region Verifica o formato do arquivo
                    System.IO.FileInfo f = new FileInfo(Request.Files[0].FileName);

                    if (!(".png|.jpg|.bmp|.gif|.txt|.pdf|.doc|.docx|.xls|.xlsx|.ppt|.pptx|.htm|.html").Contains(f.Extension.ToLower()))
                    {
                        value.mensagem.Code = 52;
                        value.mensagem.Field = "arquivo";
                        value.mensagem.Message = MensagemPadrao.Message(52, ".png|.jpg|.bmp|.gif|.txt|.pdf|.doc|.docx|.xls|.xlsx|.ppt|.pptx|.htm|.html").ToString();
                        value.mensagem.MessageBase = "Formato de arquivo inválido";
                        Error(value.mensagem.Message);
                        return View(value);
                    }
                    #endregion

                    #region Enviar o arquivo
                    value.fileId = value.fileId.Replace(".htm", f.Extension);
                    var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Users_Data"])), value.fileId);
                    this.Request.Files[0].SaveAs(filePath);
                    #endregion

                    #region Salvar AssociadoDocumento
                    AssociadoDocumentoViewModel ret = SetCreate(value, getModel(), collection);

                    if (ret.mensagem.Code == 0)
                        return RedirectToAction("Upload");
                    else
                        return View(ret);
                    #endregion
                }
                else
                {
                    Attention("Nome do arquivo deve ser informado para efetuar o upload");
                    return View(value);
                }
            }
            else
                return null;
        }
        #endregion

        public override void BeforeCreate(ref AssociadoDocumentoViewModel value, ICrudContext<AssociadoDocumentoViewModel> model, FormCollection collection)
        {
            if (this.Request.Files.Count > 0 && this.Request.Files[0].FileName.Trim() != "")
            {
                value.nomeArquivoOriginal = Request.Files[0].FileName.Trim();
            }
            else
            {
                value.nomeArquivoOriginal = value.nomeArquivoOriginal + ".htm";
            }
        }

    }
}