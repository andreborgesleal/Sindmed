using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using App_Dominio.Security;
using App_Dominio.Contratos;
using App_Dominio.Controllers;
using System.Data.Entity.Validation;
using App_Dominio.Entidades;
using DWM.Models;
using DWM.Models.Persistence;
using App_Dominio.Enumeracoes;
using DWM.Models.Repositories;
using App_Dominio.Repositories;
using DWM.Models.Entidades;

namespace DWM.Controllers
{
    [Authorize]
    public class AccountController : SuperController
    {
        #region Inheritance
        public override int _sistema_id() { return (int)DWM.Models.Enumeracoes.Sistema.SINDMED ; }

        public override string getListName()
        {
            return "Login";
        }

        public override ActionResult List(int? index, int? pageSize = 40, string descricao = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return RedirectToAction("Default", "Home");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                EmpresaSecurity<App_DominioContext> security = new EmpresaSecurity<App_DominioContext>();
                try
                {
                    AssociadoModel associadoModel = new AssociadoModel();
                    AssociadoViewModel associadoViewModel = associadoModel.getAssociadoByLogin(model.UserName, security);

                    if (associadoViewModel != null && associadoViewModel.associadoId < 0)
                        throw new DbEntityValidationException();

                    #region Autorizar
                    Validate result = security.Autorizar(model.UserName, model.Password, _sistema_id(), associadoViewModel != null ? associadoViewModel.associadoId.ToString() : "0", associadoViewModel != null && associadoViewModel.avatar != null ? associadoViewModel.avatar : null);
                    if (result.Code > 0)
                        throw new ArgumentException(result.Message);
                    #endregion

                    string sessaoId = result.Field;

                    return RedirectToAction("index", "Home");
                }
                catch (ArgumentException ex)
                {
                    Error(ex.Message);
                }
                catch(App_DominioException ex)
                {
                    Error("Erro na autorização de acesso. Favor entre em contato com o administrador do sistema");
                }
                catch (DbEntityValidationException ex)
                {
                    Error("Não foi possível autorizar o seu acesso. Favor entre em contato com o administrador do sistema");
                }
                catch(Exception ex)
                {
                    Error("Erro na autorização de acesso. Favor entre em contato com o administrador do sistema");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel value, FormCollection collection)
        {
            if (ModelState.IsValid)
                try
                {
                    value.uri = this.ControllerContext.Controller.GetType().Name.Replace("Controller", "") + "/" + this.ControllerContext.RouteData.Values["action"].ToString();

                    #region BeforeCreate
                    if (collection["dt_nascimento"] != "" && collection["dt_nascimento"].Length == 10)
                        value.dt_nascimento = DateTime.Parse(collection["dt_nascimento"].Substring(6, 4) + "-" + collection["dt_nascimento"].Substring(3, 2) + "-" + collection["dt_nascimento"].Substring(0, 2));

                    value.dt_inicio = DateTime.Today;

                    #region Dependentes
                    IList<DependenteViewModel> Dependentes = new List<DependenteViewModel>();

                    for (int i = 1; i <= 8; i++)
                    {
                        if (collection["nome_dependente" + i.ToString()] != "")
                        {
                            DependenteViewModel d = new DependenteViewModel()
                            {
                                associadoId = value.associadoId,
                                dependenteId = i,
                                nome = collection["nome_dependente" + i.ToString()],
                                tx_relacao_associado = collection["tx_relacao_associado" + i.ToString()],
                                email = collection["e_mail" + i.ToString()],
                                sexo = collection["sexo" + i.ToString()]
                            };

                            Dependentes.Add(d);
                        }
                    }

                    value.Dependentes = Dependentes;
                    #endregion

                    #region Veículos

                    IList<VeiculoViewModel> Veiculos = new List<VeiculoViewModel>();

                    for (int i = 1; i <= 6; i++)
                    {
                        if (collection["placa" + i.ToString()] != "")
                        {
                            VeiculoViewModel v = new VeiculoViewModel()
                            {
                                associadoId = value.associadoId,
                                placa = collection["placa" + i.ToString()],
                                cor = collection["cor" + i.ToString()],
                                marca = collection["marca" + i.ToString()],
                                descricao = collection["descricao" + i.ToString()],
                                condutor = collection["condutor" + i.ToString()]
                            };

                            Veiculos.Add(v);
                        }
                    }

                    value.Veiculos = Veiculos;
                    #endregion

                    #region Funcionarios
                    IList<FuncionarioViewModel> Funcionarios = new List<FuncionarioViewModel>();

                    for (int i = 1; i <= 6; i++)
                    {
                        if (collection["nome_func" + i.ToString()] != "")
                        {
                            FuncionarioViewModel f = new FuncionarioViewModel()
                            {
                                associadoId = value.associadoId,
                                funcionarioId = i,
                                nome = collection["nome_func" + i.ToString()],
                                funcao = collection["funcao" + i.ToString()],
                                sexo = collection["sexo_func" + i.ToString()],
                                rg = collection["rg" + i.ToString()],
                                dia_semana = (collection["dom" + i.ToString()] == "true,false" ? "S" : "N") +
                                             (collection["seg" + i.ToString()] == "true,false" ? "S" : "N") +
                                             (collection["ter" + i.ToString()] == "true,false" ? "S" : "N") +
                                             (collection["qua" + i.ToString()] == "true,false" ? "S" : "N") +
                                             (collection["qui" + i.ToString()] == "true,false" ? "S" : "N") +
                                             (collection["sex" + i.ToString()] == "true,false" ? "S" : "N") +
                                             (collection["sab" + i.ToString()] == "true,false" ? "S" : "N"),
                                horario_ini = collection["horario_ini" + i.ToString()],
                                horario_fim = collection["horario_fim" + i.ToString()]
                            };

                            Funcionarios.Add(f);
                        }
                    }

                    value.Funcionarios = Funcionarios;
                    #endregion

                    value.torreId = collection["apto"].Substring(0, 2);
                    value.unidadeId = int.Parse(collection["apto"].Substring(2));
                    #endregion

                    AccountModel model = new AccountModel();

                    value = model.SaveAll(value, Crud.INCLUIR);
                    if (value.mensagem.Code > 0)
                        throw new App_DominioException(value.mensagem);

                    Success("Registro incluído com sucesso");
                    return RedirectToAction("Login", "Account");
                }
                catch (App_DominioException ex)
                {
                    ModelState.AddModelError(ex.Result.Field, ex.Result.Message); // mensagem amigável ao usuário
                    if (ex.Result.MessageType == MsgType.ERROR)
                        Error(ex.Result.MessageBase); // Mensagem em inglês com a descrição detalhada do erro e fica no topo da tela
                    else
                        Attention(ex.Result.MessageBase); // Mensagem em inglês com a descrição detalhada do erro e fica no topo da tela
                }
                catch (Exception ex)
                {
                    App_DominioException.saveError(ex, GetType().FullName);
                    ModelState.AddModelError("", MensagemPadrao.Message(17).ToString()); // mensagem amigável ao usuário
                    Error(ex.Message); // Mensagem em inglês com a descrição detalhada do erro e fica no topo da tela
                }
            else
            {
                value.mensagem = new Validate()
                {
                    Code = 999,
                    Message = MensagemPadrao.Message(999).ToString(),
                    MessageBase = ModelState.Values.Where(erro => erro.Errors.Count > 0).First().Errors[0].ErrorMessage
                };
                ModelState.AddModelError("", value.mensagem.Message); // mensagem amigável ao usuário
                Attention(value.mensagem.MessageBase);
            }

            return View(value);
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            System.Web.HttpContext web = System.Web.HttpContext.Current;
            new EmpresaSecurity<App_DominioContext>().EncerrarSessao(web.Session.SessionID);

            return RedirectToAction("Login", "Account");
        }


    }
}