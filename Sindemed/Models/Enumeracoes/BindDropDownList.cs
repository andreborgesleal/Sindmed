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
            string selectedValue = param[0].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IEnumerable<SelectListItem> q = (from a in db.AreaAtendimentos.AsEnumerable()
                                                  orderby a.descricao
                                                  select new SelectListItem()
                                                  {
                                                      //Value = SqlFunctions.StringConvert((decimal?)a.areaAtendimentoId),
                                                      Value = a.areaAtendimentoId.ToString(),
                                                      Text = a.descricao,
                                                      Selected = (selectedValue != "" ? a.descricao.Equals(selectedValue) : false)
                                                  }).ToList();

                q.ToList().Add(new SelectListItem() { Value = "-1", Text = "Selecione...", Selected = selectedValue == "-1" });

                return q;
            }

            
        }
    }
}