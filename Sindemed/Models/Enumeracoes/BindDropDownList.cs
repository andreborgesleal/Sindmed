using App_Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Dominio.Entidades;
using System.Data.Objects.SqlClient;
using Sindemed.Models.Entidades;
using App_Dominio.Security;

namespace Sindemed.Models.Enumeracoes
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
    }
}