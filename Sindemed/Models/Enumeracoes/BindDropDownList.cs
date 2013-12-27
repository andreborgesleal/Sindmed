using App_Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Dominio.Entidades;
using System.Data.Objects.SqlClient;
using Sindemed.Models.Entidades;

namespace Sindemed.Models.Enumeracoes
{
    public class BindDropDownList
    {
        public IEnumerable<SelectListItem> AreaAtendimento(params object[] param)
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

                q = q.Union(from a in db.AreaAtendimentos.AsEnumerable()
                     orderby a.descricao
                     select new SelectListItem()
                     {
                         Value = a.areaAtendimentoId.ToString(),
                         Text = a.descricao,
                         Selected = (selectedValue != "" ? a.descricao.Equals(selectedValue) : false)
                     }).ToList();

                return q;
            }

            
        }
    }
}