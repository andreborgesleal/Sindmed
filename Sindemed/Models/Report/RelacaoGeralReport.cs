using Sindemed.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App_Dominio.Component;
using App_Dominio.Entidades;
using Sindemed.Models.Entidades;
using System.Data.Entity.SqlServer;

namespace Sindemed.Models.Report
{
    public class RelacaoGeralReport : ReportRepository<RelacaoGeralViewModel, ApplicationContext>
    {
        #region Métodos da classe ReportRepository
        public override IEnumerable<RelacaoGeralViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string ind_sindicalizado = param[0].ToString();
            int crm_inicial = int.Parse(param[1].ToString());
            int crm_final = int.Parse(param[2].ToString());
            int? especialidadeId = null ;
            if (param[3].ToString() != "")
                especialidadeId = int.Parse(param[3].ToString());
            int? grupoAssociadoId = null;
            if (param[4].ToString() != "")
                grupoAssociadoId = int.Parse(param[4].ToString());
            string ind_email = null;
            if (param[5].ToString() != "")
                ind_email = param[5].ToString();

            var q = (from a in db.Associados
                     join m in db.Medicos on a.associadoId equals m.associadoId
                     join e in db.EspecialidadeMedicas on m.especialidade1Id equals e.especialidadeId
                     join c in db.Cidades on a.cidadeId equals c.cidadeId
                     let _crm = int.Parse(m.CRM)
                     where (ind_sindicalizado == "" || a.isSindicalizado == ind_sindicalizado) &&
                           _crm >= crm_inicial && _crm <= crm_final && 
                           (especialidadeId == null || m.especialidade1Id == especialidadeId) &&
                           (grupoAssociadoId == null || (from agr in db.AssociadoGrupos 
                                                         where agr.grupoAssociadoId == grupoAssociadoId && 
                                                               agr.associadoId == a.associadoId
                                                         select agr.associadoId).Count() > 0 ) &&
                           (ind_email == null || ind_email == "C" && a.email1 != "" || ind_email == "S" && a.email1 == "")
                     select new RelacaoGeralViewModel()
                     {
                         associadoId = a.associadoId,
                         nome = a.nome,
                         cpf = a.cpf,
                         crm = m.CRM,
                         email = a.email1,
                         nome_especialidade = e.descricao,
                         dt_nascimento = a.dt_nascimento,
                         sexo = a.sexo,
                         endereco = a.endereco,
                         complementoEnd = a.complementoEnd,
                         cep = a.cep,
                         uf = a.uf,
                         bairro = a.bairro,
                         cidade = c.nome,
                         telParticular1 = a.telParticular1,
                         telParticular2 = a.telParticular2,
                         telCom1 = a.telCom1,
                         isSindicalizado = a.isSindicalizado,
                         PageSize = pageSize,
                         TotalCount = (from a1 in db.Associados
                                       join m1 in db.Medicos on a1.associadoId equals m1.associadoId
                                       join e1 in db.EspecialidadeMedicas on m1.especialidade1Id equals e1.especialidadeId
                                       join c1 in db.Cidades on a1.cidadeId equals c1.cidadeId
                                       let _crm1 = int.Parse(m1.CRM)
                                       where (ind_sindicalizado == "" || a1.isSindicalizado == ind_sindicalizado) &&
                                             _crm1 >= crm_inicial && _crm1 <= crm_final && 
                                             (especialidadeId == null || m1.especialidade1Id == especialidadeId) &&
                                             (grupoAssociadoId == null || (from agr1 in db.AssociadoGrupos 
                                                                           where agr1.grupoAssociadoId == grupoAssociadoId && 
                                                                                 agr1.associadoId == a1.associadoId
                                                                           select agr1.associadoId).Count() > 0 ) &&
                                             (ind_email == null || ind_email == "C" && a1.email1 != "" || ind_email == "S" && a1.email1 == "")
                                       select a1.associadoId).Count()
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
            string ind_sindicalizado = param[0].ToString();
            int crm_inicial = int.Parse(param[1].ToString());
            int crm_final = int.Parse(param[2].ToString());
            int? especialidadeId = null;
            if (param[3].ToString() != "")
                especialidadeId = int.Parse(param[3].ToString());
            int? grupoAssociadoId = null;
            if (param[4].ToString() != "")
                grupoAssociadoId = int.Parse(param[4].ToString());
            string ind_email = null;
            if (param[5].ToString() != "")
                ind_email = param[5].ToString();

            var q = (from a in db.Associados
                     join m in db.Medicos on a.associadoId equals m.associadoId
                     join e in db.EspecialidadeMedicas on m.especialidade1Id equals e.especialidadeId
                     join c in db.Cidades on a.cidadeId equals c.cidadeId
                     let _crm = int.Parse(m.CRM)
                     where (ind_sindicalizado == "" || a.isSindicalizado == ind_sindicalizado) &&
                           _crm >= crm_inicial && _crm <= crm_final &&
                           (especialidadeId == null || m.especialidade1Id == especialidadeId) &&
                           (grupoAssociadoId == null || (from agr in db.AssociadoGrupos
                                                         where agr.grupoAssociadoId == grupoAssociadoId &&
                                                               agr.associadoId == a.associadoId
                                                         select agr.associadoId).Count() > 0) &&
                           (ind_email == null || ind_email == "C" && a.email1 != "" || ind_email == "S" && a.email1 == "")
                     select new RelacaoGeralViewModel()
                     {
                         associadoId = a.associadoId,
                         nome = a.nome,
                         cpf = a.cpf,
                         email = a.email1,
                         dt_nascimento = a.dt_nascimento,
                         sexo = a.sexo,
                         endereco = a.endereco,
                         complementoEnd = a.complementoEnd,
                         cep = a.cep,
                         uf = a.uf,
                         bairro = a.bairro,
                         cidade = c.nome,
                         telParticular1 = a.telParticular1,
                         telParticular2 = a.telParticular2,
                         telCom1 = a.telCom1,
                         isSindicalizado = a.isSindicalizado
                     });

            return q.ToList();
        }
        #endregion
    }
}