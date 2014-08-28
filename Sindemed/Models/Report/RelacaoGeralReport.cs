using DWM.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App_Dominio.Component;
using App_Dominio.Entidades;
using DWM.Models.Entidades;
using System.Data.Entity.SqlServer;

namespace DWM.Models.Report
{
    public class RelacaoGeralReport : ReportRepository<RelacaoGeralViewModel, ApplicationContext>
    {
        #region Métodos da classe ReportRepository
        public override IEnumerable<RelacaoGeralViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string ind_proprietario = param[0].ToString();
            string torreId = null;
            if (param[1].ToString() != "")
                torreId = param[1].ToString();
            int? unidadeId = null;
            if (param[2].ToString() != "")
                unidadeId = int.Parse(param[2].ToString());
            int? grupoAssociadoId = null;
            if (param[3].ToString() != "")
                grupoAssociadoId = int.Parse(param[3].ToString());
            string ind_email = null;
            if (param[4].ToString() != "")
                ind_email = param[4].ToString();

            var q = (from a in db.Associados
                     join c in db.Cidades on a.cidadeComId equals c.cidadeId
                     where (ind_proprietario == "" || a.ind_proprietario == ind_proprietario) &&
                           (torreId == null || a.torreId == torreId) &&
                           (unidadeId == null || a.unidadeId == unidadeId) &&
                           (grupoAssociadoId == null || (from agr in db.AssociadoGrupos
                                                         where agr.grupoAssociadoId == grupoAssociadoId &&
                                                               agr.associadoId == a.associadoId
                                                         select agr.associadoId).Count() > 0) &&
                           (ind_email == null || ind_email == "S" && a.email1 != "" || ind_email == "N" && a.email1 == "")
                     orderby a.nome
                     select new RelacaoGeralViewModel()
                     {
                         associadoId = a.associadoId,
                         nome = a.nome,
                         cpf = a.cpf_cnpj,
                         email = a.email1,
                         dt_nascimento = a.dt_nascimento,
                         sexo = a.sexo,
                         endereco = a.enderecoCom,
                         complementoEnd = a.complementoEndCom,
                         cep = a.cepCom,
                         uf = a.ufCom,
                         bairro = a.bairroCom,
                         cidade = c.nome,
                         telParticular1 = a.telParticular1,
                         telParticular2 = a.telParticular2,
                         telCom1 = a.telParticular3,
                         ind_proprietario = a.ind_proprietario,
                         PageSize = pageSize,
                         TotalCount = (from a1 in db.Associados
                                       join c1 in db.Cidades on a1.cidadeComId equals c1.cidadeId
                                       where (ind_proprietario == "" || a1.ind_proprietario == ind_proprietario) &&
                                             (torreId == null || a1.torreId == torreId) &&
                                             (unidadeId == null || a1.unidadeId == unidadeId) &&
                                             (grupoAssociadoId == null || (from agr1 in db.AssociadoGrupos
                                                                           where agr1.grupoAssociadoId == grupoAssociadoId &&
                                                                               agr1.associadoId == a1.associadoId
                                                                           select agr1.associadoId).Count() > 0) &&
                                             (ind_email == null || ind_email == "S" && a1.email1 != "" || ind_email == "N" && a1.email1 == "")
                                       orderby a1.nome
                                       select a1).Count()
                     }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();

            return q.ToList();
        }

        public override Repository getRepository(Object id)
        {
            throw new NotImplementedException();
        }

        public override string action()
        {
            return "ListParam";
        }

        public override string DivId()
        {
            return "div-list-static";
        }

        public override IEnumerable<RelacaoGeralViewModel> BindReport(params object[] param)
        {
            string ind_proprietario = param[0].ToString();
            string torreId = null;
            if (param[1].ToString() != "")
                torreId = param[1].ToString();
            int? unidadeId = null ;
            if (param[2].ToString() != "")
                unidadeId = int.Parse(param[2].ToString());
            int? grupoAssociadoId = null;
            if (param[3].ToString() != "")
                grupoAssociadoId = int.Parse(param[3].ToString());
            string ind_email = null;
            if (param[4].ToString() != "")
                ind_email = param[4].ToString();

            var q = (from a in db.Associados
                     join c in db.Cidades on a.cidadeComId equals c.cidadeId
                     where (ind_proprietario == "" || a.ind_proprietario == ind_proprietario) &&
                           (torreId == null || a.torreId == torreId) &&
                           (unidadeId == null || a.unidadeId == unidadeId) &&
                           (grupoAssociadoId == null || (from agr in db.AssociadoGrupos
                                                         where agr.grupoAssociadoId == grupoAssociadoId &&
                                                               agr.associadoId == a.associadoId
                                                         select agr.associadoId).Count() > 0) &&
                           (ind_email == null || ind_email == "S" && a.email1 != "" || ind_email == "N" && a.email1 == "")
                     orderby a.nome
                     select new RelacaoGeralViewModel()
                     {
                         associadoId = a.associadoId,
                         nome = a.nome,
                         cpf = a.cpf_cnpj,
                         email = a.email1,
                         dt_nascimento = a.dt_nascimento,
                         sexo = a.sexo,
                         endereco = a.enderecoCom,
                         complementoEnd = a.complementoEndCom,
                         cep = a.cepCom,
                         uf = a.ufCom,
                         bairro = a.bairroCom,
                         cidade = c.nome,
                         telParticular1 = a.telParticular1,
                         telParticular2 = a.telParticular2,
                         telCom1 = a.telParticular3,
                         ind_proprietario = a.ind_proprietario
                     }).ToList();

            return q.ToList();
        }
        #endregion
    }
}