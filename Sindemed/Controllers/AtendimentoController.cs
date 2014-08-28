using App_Dominio.Contratos;
using App_Dominio.Controllers;
using App_Dominio.Security;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System;
using System.Web.Mvc;
using App_Dominio.Enumeracoes;
using System.IO;

namespace DWM.Controllers
{
    public class AtendimentoController : ProcessController<AtendimentoViewModel, AtendimentoModel>
    {
        public override int _sistema_id() { return (int)DWM.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Atendimentos";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
                return ListChamados(index, PageSize);
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult ListChamados(int? index, int? pageSize = 50, int? chamadoId = null, string apto = null,
                                        string data1 = null, string data2 = null, int? areaAtendimento = null, string situacao = null,
                                        int? chamadoMotivoId = null, int? chamadoStatusId = null)
        {
            if (ViewBag.ValidateRequest)
            {
                DateTime _data1 = DateTime.Parse(DateTime.Today.ToString("yyyy-MM") + "-01");
                DateTime _data2 = DateTime.Parse(DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"));

                if (data1 != null && data1 != "")
                    _data1 = DateTime.Parse(data1.Substring(6, 4) + "-" + data1.Substring(3, 2) + "-" + data1.Substring(0, 2));

                if (data2 != null && data2 != "")
                    _data2 = DateTime.Parse(data2.Substring(6, 4) + "-" + data2.Substring(3, 2) + "-" + data2.Substring(0, 2)).AddDays(1);

                ListViewChamadoAdministracao l = new ListViewChamadoAdministracao();
                return _List(index, pageSize, "Browse", l, chamadoId, apto, _data1, _data2, areaAtendimento, situacao, chamadoMotivoId, chamadoStatusId);
            }
            else
                return View();
        }
        #endregion

        #region Create
        [AuthorizeFilter]
        [HttpGet]
        public ActionResult Create(AtendimentoViewModel value)
        {
            if (ViewBag.ValidateRequest)
            {
                GetCreate();
                value = getModel().Create(value);
                if (value.mensagem.Code != 0)
                {
                    Error(value.mensagem.Message);
                    Response.Redirect("/Home/_Error");
                    return null;
                }
                else
                {
                    if (value.chamado.situacao != "F")
                        return View(value);
                    else
                        return RedirectToAction("Detail", new AtendimentoViewModel() { chamadoId = value.chamadoId, fluxo = value.fluxo });
                }
            }
            else
                return null;
        }

        [ValidateInput(false)]
        [HttpPost]
        [AuthorizeFilter]
        public override ActionResult Create(AtendimentoViewModel value, FormCollection collection)
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
                    value.anexo.fileId = value.anexo.fileId.Replace(".htm", f.Extension);
                    var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Users_Data"])), value.anexo.fileId);
                    this.Request.Files[0].SaveAs(filePath);
                    #endregion
                }

                AtendimentoViewModel ret = SetCreate(value, getModel(), collection);

                if (ret.mensagem.Code == 0 && collection["fluxo"] == "2")
                    return RedirectToAction("Browse");
                else if (ret.mensagem.Code == 0)
                    return RedirectToAction("Browse", "Chamado");
                else
                    return View(ret);
            }
            else
                return null;
        }

        public override void BeforeCreate(ref AtendimentoViewModel value, ICrudContext<AtendimentoViewModel> model, FormCollection collection)
        {
            if (value.fluxo == "2")
            {
                value.chamado = new ChamadoViewModel()
                {
                    chamadoId = value.chamadoId,
                    situacao = collection ["situacao"].ToString().Contains("true") ? "F" : "A",
                    chamadoStatusId = int.Parse(collection["chamado.chamadoStatusId"])
                };
            }
            if (this.Request.Files.Count > 0 && this.Request.Files[0].FileName.Trim() != "")
                value.anexo.nomeArquivoOriginal = Request.Files[0].FileName.Trim();
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
            if (ViewBag.ValidateRequest)
                return Create(value);
            else
                return null;
        }

        [ValidateInput(false)]
        [HttpPost]
        [AuthorizeFilter]
        public ActionResult Fechar(AtendimentoViewModel value, FormCollection collection)
        {
            if (ViewBag.ValidateRequest)
            {
                AtendimentoViewModel ret = SetCreate(value, new FechamentoChamadoModel(), collection);

                if (ret.mensagem.Code == 0 && collection["fluxo"] == "2")
                    return RedirectToAction("Browse");
                else if (ret.mensagem.Code == 0)
                    return RedirectToAction("Browse", "Chamado");
                else
                    return View(ret);
            }
            else
                return null;
        }
        #endregion

        #region Detalhar
        [AuthorizeFilter]
        public virtual ActionResult Detail(AtendimentoViewModel value = null)
        {
            if (ViewBag.ValidateRequest)
            {
                GetCreate();
                value = getModel().Create(value);
                if (value.mensagem.Code != 0)
                {
                    Error(value.mensagem.Message);
                    Response.Redirect("/Home/_Error");
                    return null;
                }
                else
                    return View(value);
            }
            else
                return null;
        }
        #endregion

    }
}