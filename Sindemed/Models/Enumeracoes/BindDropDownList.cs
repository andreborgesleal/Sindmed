using App_Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Dominio.Entidades;
//using System.Data.Objects.SqlClient;
using DWM.Models.Entidades;
using App_Dominio.Security;
using App_Dominio.Models;

namespace DWM.Models.Enumeracoes
{
    public class BindDropDownList
    {
        public IEnumerable<SelectListItem> AreaAtendimento(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            // params[3] -> indica se deve pesquisar somente as areas de atendimento do funcionário (usuário) corrente
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();


            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();
                
                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                if (param.Count() == 2)
                {
                    q = q.Union(from a in db.AreaAtendimentos.AsEnumerable()
                                orderby a.descricao
                                select new SelectListItem()
                                {
                                    Value = a.areaAtendimentoId.ToString(),
                                    Text = a.descricao,
                                    Selected = (selectedValue != "" ? a.descricao.Equals(selectedValue) : false)
                                }).ToList();
                }
                else
                {
                    EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
                    Sessao sessaoCorrente = security.getSessaoCorrente();

                    q = q.Union(from a in db.AreaAtendimentos.AsEnumerable()
                                where a.usuario1Id == sessaoCorrente.usuarioId ||
                                        a.usuario2Id == sessaoCorrente.usuarioId
                                orderby a.descricao
                                select new SelectListItem()
                                {
                                    Value = a.areaAtendimentoId.ToString(),
                                    Text = a.descricao,
                                    Selected = (selectedValue != "" ? a.descricao.Equals(selectedValue) : false)
                                }).ToList();
                }

                return q;
            }

            
        }

        public IEnumerable<SelectListItem> Especialidade(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from e in db.EspecialidadeMedicas.AsEnumerable()
                            orderby e.descricao
                            select new SelectListItem()
                            {
                                Value = e.especialidadeId.ToString(),
                                Text = e.descricao,
                                Selected = (selectedValue != "" ? e.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }

        public IEnumerable<SelectListItem> GruposAssociados(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from g in db.GrupoAssociados.AsEnumerable()
                            orderby g.descricao
                            select new SelectListItem()
                            {
                                Value = g.grupoAssociadoId.ToString(),
                                Text = g.descricao,
                                Selected = (selectedValue != "" ? g.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }

        public IEnumerable<SelectListItem> AreaAtuacao(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from p in db.AreaAtuacaos.AsEnumerable()
                            orderby p.descricao
                            select new SelectListItem()
                            {
                                Value = p.areaAtuacaoId.ToString(),
                                Text = p.descricao,
                                Selected = (selectedValue != "" ? p.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }

        public IEnumerable<SelectListItem> Cidade(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from p in db.Cidades.AsEnumerable()
                            orderby p.nome
                            select new SelectListItem()
                            {
                                Value = p.cidadeId.ToString(),
                                Text = p.nome,
                                Selected = (selectedValue != "" ? p.nome.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }

        public IEnumerable<SelectListItem> TipoDocumento(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from obj in db.TipoDocumentos.AsEnumerable()
                            orderby obj.descricao
                            select new SelectListItem()
                            {
                                Value = obj.tipoDocumentoId.ToString(),
                                Text = obj.descricao,
                                Selected = (selectedValue != "" ? obj.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }
        }

        public IEnumerable<SelectListItem> ChamadoMotivo(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from obj in db.ChamadoMotivos.AsEnumerable()
                            orderby obj.descricao
                            select new SelectListItem()
                            {
                                Value = obj.chamadoMotivoId.ToString(),
                                Text = obj.descricao,
                                Selected = (selectedValue != "" ? obj.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }
        }

        public IEnumerable<SelectListItem> ChamadoStatus(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from obj in db.ChamadoStatuss.AsEnumerable()
                            orderby obj.descricao
                            select new SelectListItem()
                            {
                                Value = obj.chamadoStatusId.ToString(),
                                Text = obj.descricao,
                                Selected = (selectedValue != "" ? obj.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }
        }

        public IEnumerable<SelectListItem> Correio(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from obj in db.NaoLocalizadoCorreios.AsEnumerable()
                            orderby obj.descricao
                            select new SelectListItem()
                            {
                                Value = obj.correioId.ToString(),
                                Text = obj.descricao,
                                Selected = (selectedValue != "" ? obj.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }
        }

        public IEnumerable<SelectListItem> Folder(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from p in db.DocFolders.AsEnumerable()
                            orderby p.descricao
                            select new SelectListItem()
                            {
                                Value = p.docFolderId.ToString(),
                                Text = p.descricao,
                                Selected = (selectedValue != "" ? p.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }

        #region DropDownList Periodo
        /// <summary>
        /// Retorna o período para processamento de datas
        /// </summary>
        /// <param name="selectedValue">Item da lista que receberá o foco inicial</param>
        /// <param name="header">Informar o cabeçalho do dropdownlist. Exemplo: "Selecione...". Observação: Se não informado o dropdownlist não terá cabeçalho.</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> Periodo(string selectedValue = "", string header = "")
        {
            List<SelectListItem> drp = new List<SelectListItem>() { 
                new SelectListItem() { Value = "30", Text = "Últimos 30 dias" }, 
                new SelectListItem() { Value = "3", Text = "Últimos 3 meses" }, 
                new SelectListItem() { Value = "6", Text = "Últimos 6 meses" },
                new SelectListItem() { Value = "12", Text = "Últimos 12 meses" }, 
            };

            return Funcoes.SelectListEnum(drp, selectedValue, header);
        }
        #endregion
    }
}