using App_Dominio.Contratos;
using App_Dominio.Controllers;
using App_Dominio.Entidades;
using App_Dominio.Enumeracoes;
using App_Dominio.Negocio;
using App_Dominio.Repositories;
using App_Dominio.Security;
using Sindemed.Models.Entidades;
using Sindemed.Models.Persistence;
using Sindemed.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Sindemed.Controllers
{
    public class EsqueciMinhaSenhaController : ProcessController<UsuarioRepository, EsqueciMinhaSenhaModel>
    {
        public override int _sistema_id() { return (int)Sindemed.Models.Enumeracoes.Sistema.SINDMED; }
        public override string getListName()
        {
            return "Esqueci minha senha";
        }

        #region List
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Forgot
        [AllowAnonymous]
        public JsonResult CodigoSeguranca(string login)
        {
            IDictionary<int, string> result = new Dictionary<int, string>();

            try
            {
                EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
                UsuarioRepository value = security.getUsuarioByLogin(login, int.Parse(System.Configuration.ConfigurationManager.AppSettings["empresaId"]));

                if (value == null)
                    throw new Exception("Login " + login + " inválido ou inexistente");
                else if (value.situacao != "A")
                    throw new Exception("Esta conta " + login + " possui pendências. Favor entrar em contato com a secretaria para providenciar a atualização.");

                return SaveJSon(value, new CodigoSegurancaModel(), null);
            }
            catch (App_DominioException ex)
            {
                result.Add(4, MensagemPadrao.Message(4, ex.Message, "Valor incorreto").ToString());

                return new JsonResult()
                {
                    Data = result.ToArray(),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                App_DominioException.saveError(ex, GetType().FullName);

                result.Add(17, MensagemPadrao.Message(17).ToString());

                return new JsonResult()
                {
                    Data = result.ToArray(),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }


        }

        [AllowAnonymous]
        public ActionResult Forgot()
        {
            GetCreate();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Forgot(EsqueciMinhaSenhaRepository value, FormCollection collection)
        {
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            UsuarioRepository u = security.getUsuarioByLogin(value.login, int.Parse(System.Configuration.ConfigurationManager.AppSettings["empresaId"]));
            
            if (u == null)
            {
                Error("Login inváido ou inexistente.");
                return View(value);
            }
            else if (u.situacao != "A")
            {
                Error("Esta conta possui pendências. Favor entrar em contato com a secretaria para providenciar a atualização.");
                return View(value);
            }
            
            u.keyword = value.keyword;

            UsuarioRepository ret = SetCreate(u, getModel(), collection);

            if (ret.mensagem.Code == 0)
                return RedirectToAction("Login", "Account");
            else
                return View(value);
        }

        #endregion



    }
}