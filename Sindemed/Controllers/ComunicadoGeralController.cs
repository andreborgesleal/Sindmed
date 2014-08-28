using App_Dominio.Controllers;
using App_Dominio.Security;
using DWM.Models.Enumeracoes;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System.Web.Mvc;
using App_Dominio.Contratos;
using App_Dominio.Enumeracoes;
using System.IO;
using System;

namespace DWM.Controllers
{
    public class ComunicadoGeralController : RootController<ComunicacaoViewModel, ComunicacaoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Comunicados";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewComunicacao l = new ListViewComunicacao();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult ListComunicacaoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupComunicacaoModel l = new LookupComunicacaoModel();
            return this.ListModal(index, pageSize, l, "Comunicacaos", descricao);
        }

        public ActionResult _ListComunicacaoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupComunicacaoFiltroModel l = new LookupComunicacaoFiltroModel();
                return this.ListModal(index, pageSize, l, "Comunicacaos", descricao);

            }
            else
                return View();
        }
        #endregion

        #region BeforeCreate
        public override void BeforeCreate(ref ComunicacaoViewModel value, ICrudContext<ComunicacaoViewModel> model, FormCollection collection)
        {
            #region upload dos arquivos de imagem 300x200
            if (this.Request.Files.Count > 0 && this.Request.Files[0].FileName.Trim() != "") // arquivo imagem 300x200
            {
                value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString() };

                #region verifica o tamanho do arquivo
                if (this.Request.Files[0].ContentLength > int.Parse(System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"])) // 1 mb
                {
                    value.mensagem.Code = 51;
                    value.mensagem.Field = "arq_imagem_300x200";
                    value.mensagem.Message = MensagemPadrao.Message(51, System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"]).ToString();
                    value.mensagem.MessageBase = "O tamanho do arquivo da imagem 300x200 deve ser menor que " + Convert.ToString((int.Parse(System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"]) / 1024) / 1000) + " mb";
                    Error(value.mensagem.Message);
                    return;
                }
                #endregion

                #region Verifica o formato do arquivo
                System.IO.FileInfo f = new FileInfo(Request.Files[0].FileName);

                if (!(".png|.jpg|.bmp|.gif|.jpeg").Contains(f.Extension.ToLower()))
                {
                    value.mensagem.Code = 52;
                    value.mensagem.Field = "arq_imagem_300x200";
                    value.mensagem.Message = MensagemPadrao.Message(52, ".png|.jpg|.bmp|.gif|.jpeg").ToString();
                    value.mensagem.MessageBase = "Formato de arquivo da imagem 300x200 inválido";
                    Error(value.mensagem.Message);
                    return;
                }
                #endregion

                #region Enviar o arquivo
                value.arq_imagem_300x200 = String.Format("{0}" + f.Extension, Guid.NewGuid().ToString());
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Comunicados"])), value.arq_imagem_300x200);
                this.Request.Files[0].SaveAs(filePath);
                #endregion
            }
            #endregion

            #region upload dos arquivos de imagem 100x100
            if (this.Request.Files.Count > 0 && this.Request.Files[1].FileName.Trim() != "") // arquivo imagem 100x100
            {
                value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString() };

                #region verifica o tamanho do arquivo
                if (this.Request.Files[1].ContentLength > int.Parse(System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"])) // 1 mb
                {
                    value.mensagem.Code = 51;
                    value.mensagem.Field = "arq_imagem_100x100";
                    value.mensagem.Message = MensagemPadrao.Message(51, System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"]).ToString();
                    value.mensagem.MessageBase = "O tamanho do arquivo da imagem 100x100 deve ser menor que " + Convert.ToString((int.Parse(System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"]) / 1024) / 1000) + " mb";
                    Error(value.mensagem.Message);
                    return;
                }
                #endregion

                #region Verifica o formato do arquivo
                System.IO.FileInfo f = new FileInfo(Request.Files[1].FileName);

                if (!(".png|.jpg|.bmp|.gif|.jpeg").Contains(f.Extension.ToLower()))
                {
                    value.mensagem.Code = 52;
                    value.mensagem.Field = "arq_imagem_100x100";
                    value.mensagem.Message = MensagemPadrao.Message(52, ".png|.jpg|.bmp|.gif|.jpeg").ToString();
                    value.mensagem.MessageBase = "Formato de arquivo d0 imagem 100x100 inválido";
                    Error(value.mensagem.Message);
                    return;
                }
                #endregion

                #region Enviar o arquivo
                value.arq_imagem_100x100 = String.Format("{0}" + f.Extension, Guid.NewGuid().ToString());
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Comunicados"])), value.arq_imagem_100x100);
                this.Request.Files[1].SaveAs(filePath);
                #endregion
            }
            #endregion

            #region upload dos arquivos de imagem 400x300
            if (this.Request.Files.Count > 0 && this.Request.Files[2].FileName.Trim() != "") // arquivo imagem 400x300
            {
                value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString() };

                #region verifica o tamanho do arquivo
                if (this.Request.Files[2].ContentLength > int.Parse(System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"])) // 1 mb
                {
                    value.mensagem.Code = 51;
                    value.mensagem.Field = "arq_imagem_400x300";
                    value.mensagem.Message = MensagemPadrao.Message(51, System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"]).ToString();
                    value.mensagem.MessageBase = "O tamanho do arquivo da imagem 400x300 deve ser menor que " + Convert.ToString((int.Parse(System.Configuration.ConfigurationManager.AppSettings["tam_arquivo"]) / 1024)/1000) + " mb";
                    Error(value.mensagem.Message);
                    return;
                }
                #endregion

                #region Verifica o formato do arquivo
                System.IO.FileInfo f = new FileInfo(Request.Files[2].FileName);

                if (!(".png|.jpg|.bmp|.gif|.jpeg").Contains(f.Extension.ToLower()))
                {
                    value.mensagem.Code = 52;
                    value.mensagem.Field = "arq_imagem_400x300";
                    value.mensagem.Message = MensagemPadrao.Message(52, ".png|.jpg|.bmp|.gif|.jpeg").ToString();
                    value.mensagem.MessageBase = "Formato de arquivo d0 imagem 400x300 inválido";
                    Error(value.mensagem.Message);
                    return;
                }
                #endregion

                #region Enviar o arquivo
                value.arq_imagem_400x300 = String.Format("{0}" + f.Extension, Guid.NewGuid().ToString());
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Comunicados"])), value.arq_imagem_400x300);
                this.Request.Files[2].SaveAs(filePath);
                #endregion
            }
            #endregion
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int comunicacaoId)
        {
            return _Edit(new ComunicacaoViewModel() { comunicacaoId = comunicacaoId });
        }

        public override void BeforeEdit(ref ComunicacaoViewModel value, ICrudContext<ComunicacaoViewModel> model, FormCollection collection) 
        {
            value.dt_comunicacao = DateTime.Now;

            #region Excluir arquivos
            if (value.arq_imagem_300x200 != "default_300x200.png" && this.Request.Files[0].FileName.Trim() != "")
            {
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Comunicados"])), value.arq_imagem_300x200);
                System.IO.FileInfo f = new FileInfo(filePath);
                if (f.Exists)
                    f.Delete();
            }

            if (value.arq_imagem_100x100 != "default_100x100.jpg" && this.Request.Files[1].FileName.Trim() != "")
            {
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Comunicados"])), value.arq_imagem_100x100);
                System.IO.FileInfo f = new FileInfo(filePath);
                if (f.Exists)
                    f.Delete();
            }

            if (value.arq_imagem_400x300 != "default_400x300.jpg" && this.Request.Files[2].FileName.Trim() != "")
            {
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Comunicados"])), value.arq_imagem_400x300);
                System.IO.FileInfo f = new FileInfo(filePath);
                if (f.Exists)
                    f.Delete();
            }
            #endregion
            
            BeforeCreate(ref value, model, collection);
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int comunicacaoId)
        {
            return Edit(comunicacaoId);
        }

        public override void BeforeDelete(ref ComunicacaoViewModel value, ICrudContext<ComunicacaoViewModel> model, FormCollection collection)
        {
            #region Excluir arquivos
            if (value.arq_imagem_300x200 != "default_300x200.png")
            {
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Comunicados"])), value.arq_imagem_300x200);
                System.IO.FileInfo f = new FileInfo(filePath);
                if (f.Exists)
                    f.Delete();
            }

            if (value.arq_imagem_100x100 != "default_100x100.jpg")
            {
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Comunicados"])), value.arq_imagem_100x100);
                System.IO.FileInfo f = new FileInfo(filePath);
                if (f.Exists)
                    f.Delete();
            }

            if (value.arq_imagem_400x300 != "default_400x300.jpg")
            {
                var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Comunicados"])), value.arq_imagem_400x300);
                System.IO.FileInfo f = new FileInfo(filePath);
                if (f.Exists)
                    f.Delete();
            }
            #endregion
        }
        #endregion

        public JsonResult DelFile(int comunicacaoId, string arq, string arqType)
        {
            ComunicacaoModel model = new ComunicacaoModel();
            var item = new SelectListItem();

            var filePath = Path.Combine(Server.MapPath(Url.Content(System.Configuration.ConfigurationManager.AppSettings["Comunicados"])), arq);
            System.IO.FileInfo f = new FileInfo(filePath);
            f.Delete();

            ComunicacaoViewModel value = new ComunicacaoViewModel() { comunicacaoId = comunicacaoId };

            value = (ComunicacaoViewModel)model.getObject(value);

            if (arqType == "300x200")
            {
                value.arq_imagem_300x200 = "default_300x200.png";
                var filePath300x200 = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["Comunicados"].Replace("~", ".."), "default_300x200.png");
                item.Text = filePath300x200;
                item.Value = filePath300x200;
            }
            else if (arqType == "100x100")
            {
                value.arq_imagem_100x100 = "default_100x100.jpg";
                var filePath100x100 = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["Comunicados"].Replace("~", ".."), "default_100x100.jpg");
                item.Text = filePath100x100;
                item.Value = filePath100x100;
            }
            else
            {
                value.arq_imagem_400x300 = "default_400x300.jpg";
                var filePath400x300 = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["Comunicados"].Replace("~", ".."), "default_400x300.jpg");
                item.Text = filePath400x300;
                item.Value = filePath400x300;
            }

            value = model.Update(value);

            var results = item;

            return new JsonResult()
            {
                Data = results,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };


        }

    }
}