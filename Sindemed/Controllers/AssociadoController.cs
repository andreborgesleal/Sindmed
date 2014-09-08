using App_Dominio.Controllers;
using App_Dominio.Security;
using App_Dominio.Contratos;
using DWM.Models.Persistence;
using DWM.Models.Repositories;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using App_Dominio.Entidades;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web;
using App_Dominio.Component;

namespace Sindemed.Controllers
{
    public class AssociadoController : RootController<MedicoViewModel, MedicoModel>
    {
        private int _avatarWidth = 100; // ToDo - Change the size of the stored avatar image
        private int _avatarHeight = 100; // ToDo - Change the size of the stored avatar image

        public override int _sistema_id() { return (int)DWM.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Listagem de Associados";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewMedico l = new ListViewMedico();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult ListMedicoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupMedicoModel l = new LookupMedicoModel();
            return this.ListModal(index, pageSize, l, "Médicos", descricao);
        }
        [AuthorizeFilter]
        public ActionResult _ListMedicoModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupMedicoFiltroModel l = new LookupMedicoFiltroModel();
                return this.ListModal(index, pageSize, l, "Médicos", descricao);
            }
            else
                return View();
        }
        #endregion

        #region CreateError
        public override void OnCreateError(ref MedicoViewModel value, ICrudContext<MedicoViewModel> model, FormCollection collection)
        {
            value.nome_correio = collection["nome_correio1"] ?? "";
            value.nome_cidade = collection["nome_cidade1"] ?? "";
            value.nome_cidadeCom = collection["nome_cidadeCom1"] ?? "";
            value.descricao_areaAtuacao1 = collection["descricao_areaAtuacao11"] ?? "";
            value.descricao_areaAtuacao2 = collection["descricao_areaAtuacao21"] ?? "";
            value.descricao_areaAtuacao3 = collection["descricao_areaAtuacao31"] ?? "";
            value.nome_especialidade1 = collection["descricao_especialidade11"] ?? "";
            value.nome_especialidade2 = collection["descricao_especialidade21"] ?? "";
            value.nome_especialidade3 = collection["descricao_especialidade31"] ?? "";
        }
        #endregion

        #region BeforeCreate
        public override void BeforeCreate(ref MedicoViewModel value, ICrudContext<MedicoViewModel> model, FormCollection collection)
        {
            if (collection["dt_nascimento"] != "")
                value.dt_nascimento = DateTime.Parse(collection["dt_nascimento"].Substring(6, 4) + "-" + collection["dt_nascimento"].Substring(3, 2) + "-" + collection["dt_nascimento"].Substring(0, 2));

            if (collection["dt_admin_sindicato"] != "")
                value.dt_admin_sindicato = DateTime.Parse(collection["dt_admin_sindicato"].Substring(6, 4) + "-" + collection["dt_admin_sindicato"].Substring(3, 2) + "-" + collection["dt_admin_sindicato"].Substring(0, 2));
        }
        #endregion

        #region edit
        public override void BeforeEdit(ref MedicoViewModel value, ICrudContext<MedicoViewModel> model, FormCollection collection)
        {
            BeforeCreate(ref value, model, collection);
        }

        [AuthorizeFilter]
        public ActionResult Edit(int associadoId)
        {
            return _Edit(new MedicoViewModel() { associadoId = associadoId });
        }

        #region Edit Error
        public override void OnEditError(ref MedicoViewModel value, ICrudContext<MedicoViewModel> model, FormCollection collection)
        {
            value.nome_usuario = collection["nome_usuario1"] ?? "";
            OnCreateError(ref value, model, collection);

        }
        #endregion


        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int associadoId)
        {
            return Edit(associadoId);
        }

        #region Delete Error
        public override void OnDeleteError(ref MedicoViewModel value, ICrudContext<MedicoViewModel> model, FormCollection collection)
        {
            OnEditError(ref value, model, collection);

        }
        #endregion

        #endregion

        #region Avatar
        [HttpGet]
        [AuthorizeFilter]
        public ActionResult Avatar(int? associadoId)
        {
            if (associadoId == null || associadoId == 0)
            {
                App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext> security = new App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext>();
                Sessao sessaoCorrente = security.getSessaoCorrente();
                if (sessaoCorrente.value1 != "0")
                    associadoId = int.Parse(sessaoCorrente.value1);
            }

            return _Edit(new MedicoViewModel() { associadoId = associadoId.Value }, "Avatar");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Avatar(IEnumerable<HttpPostedFileBase> files)
        {
            string errorMessage = "";

            if (files != null && files.Count() > 0)
            {
                // Get one only
                var file = files.FirstOrDefault();
                // Check if the file is an image
                if (file != null && IsImage(file))
                {
                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        var webPath = SaveTemporaryFile(file);
                        return Json(new { success = true, fileName = webPath.Replace("/", "\\") }); // success
                    }
                    errorMessage = "Arquivo não pode ser de tamnho nulo."; //failure
                }
                errorMessage = "Formato de arquivo inválido."; //failure
            }
            errorMessage = "Nenhum arquivo foi enviado."; //failure

            return Json(new { success = false, errorMessage = errorMessage });
        }

        [HttpPost]
        public ActionResult Save(string t, string l, string h, string w, string fileName, string key)
        {
            try
            {
                // Get file from temporary folder
                var fn = Path.Combine(Server.MapPath("~/Temp"), Path.GetFileName(fileName));

                // Calculate dimesnions
                int top = Convert.ToInt32(t.Replace("-", "").Replace("px", ""));
                int left = Convert.ToInt32(l.Replace("-", "").Replace("px", ""));
                int height = Convert.ToInt32(h.Replace("-", "").Replace("px", ""));
                int width = Convert.ToInt32(w.Replace("-", "").Replace("px", ""));

                // Get image and resize it, ...
                var img = new WebImage(fn);
                img.Resize(width, height);
                // ... crop the part the user selected, ...
                img.Crop(top, left, img.Height - top - _avatarHeight, img.Width - left - _avatarWidth);
                // ... delete the temporary file,...
                System.IO.File.Delete(fn);
                // ... and save the new one.

                string newName = String.Format("{0}" + new FileInfo(fn).Extension, Guid.NewGuid().ToString());
                MedicoViewModel value = (MedicoViewModel)getModel().getObject(new MedicoViewModel() { associadoId = int.Parse(key) });
                if (value.avatar != null)
                    newName = value.avatar;

                string newFileName = System.Configuration.ConfigurationManager.AppSettings["Avatar"] + "/" + newName; // Path.GetFileName(fn);
                string newFileLocation = HttpContext.Server.MapPath(newFileName);
                if (Directory.Exists(Path.GetDirectoryName(newFileLocation)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newFileLocation));
                }

                img.FileName = newName;
                img.Save(newFileLocation);

                #region Salvar foto na cadastro do associado
                value.avatar = newName;

                MedicoViewModel ret = SetEdit(value, getModel(), null, "Save");

                if (ret.mensagem.Code == 0)
                {
                    BreadCrumb b = (BreadCrumb)ViewBag.BreadCrumb;
                    if (b.items.Count > 1)
                    {
                        string[] split = b.items[b.items.Count - 2].queryString.Split('&');
                        //string _index = split[0].Replace("?index=", "");
                        System.Web.Routing.RouteValueDictionary routValues = new System.Web.Routing.RouteValueDictionary();

                        for (int z = 0; z <= split.Count() - 1; z++)
                        {
                            string[] p = split[z].Replace("?", "").Split('=');
                            if (p.Count() == 2)
                                routValues.Add(p[0], p[1]);
                        }
                        return Json(new { success = true, avatarFileLocation = newFileLocation });
                    }
                    else
                        return RedirectToAction("Principal", "Home");
                }
                else
                    return Json(new { success = false, avatarFileLocation = newFileName });

                #endregion
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "Não foi possível fazer o upload do arquivo.\nERRORINFO: " + ex.Message });
            }
        }

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" }; // add more if you like...

            // linq from Henrik Stenbæk
            return extensions.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        private string SaveTemporaryFile(HttpPostedFileBase file)
        {
            // Define destination
            var folderName = "/Temp";
            var serverPath = HttpContext.Server.MapPath(folderName);
            if (Directory.Exists(serverPath) == false)
            {
                Directory.CreateDirectory(serverPath);
            }

            // Generate unique file name
            var fileName = Path.GetFileName(file.FileName);
            fileName = SaveTemporaryAvatarFileImage(file, serverPath, fileName);

            // Clean up old files after every save
            CleanUpTempFolder(1);

            return Path.Combine(folderName, fileName);
        }

        private string SaveTemporaryAvatarFileImage(HttpPostedFileBase file, string serverPath, string fileName)
        {
            var img = new WebImage(file.InputStream);
            double ratio = (double)img.Height / (double)img.Width;

            string fullFileName = Path.Combine(serverPath, fileName);

            img.Resize(400, (int)(400 * ratio)); // ToDo - Change the value of the width of the image oin the screen

            if (System.IO.File.Exists(fullFileName))
                System.IO.File.Delete(fullFileName);

            img.Save(fullFileName);

            return Path.GetFileName(img.FileName);
        }

        private void CleanUpTempFolder(int hoursOld)
        {
            try
            {
                DateTime fileCreationTime;
                DateTime currentUtcNow = DateTime.UtcNow;

                var serverPath = HttpContext.Server.MapPath("/Temp");
                if (Directory.Exists(serverPath))
                {
                    string[] fileEntries = Directory.GetFiles(serverPath);
                    foreach (var fileEntry in fileEntries)
                    {
                        fileCreationTime = System.IO.File.GetCreationTimeUtc(fileEntry);
                        var res = currentUtcNow - fileCreationTime;
                        if (res.TotalHours > hoursOld)
                        {
                            System.IO.File.Delete(fileEntry);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion
    }
}
