using DWM.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App_Dominio.Component;
using App_Dominio.Entidades;
using DWM.Models.Entidades;
using System.Data.Entity.SqlServer;
using System.Data.Linq.SqlClient;

namespace DWM.Models.Report
{
    public class AtendimentoPendenteReport : ReportRepository<AtendimentoPendenteViewModel, ApplicationContext>
    {
        #region Métodos da classe ReportRepository
        public override IEnumerable<AtendimentoPendenteViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            int? areaAtendimentoId = null;
            if (param[0].ToString() != "")
                areaAtendimentoId = int.Parse(param[0].ToString());

            using (seguranca_db = new SecurityContext())
            {
                var q = from zapata in
                            ((from cha in db.Chamados.AsEnumerable()
                              join ate in db.Atendimentos on cha.chamadoId equals ate.chamadoId into ATE
                              from ate in ATE.DefaultIfEmpty()
                              join a in db.Associados on cha.associadoId equals a.associadoId
                              join are in db.AreaAtendimentos on cha.areaAtendimentoId equals are.areaAtendimentoId
                              where (areaAtendimentoId == null || cha.areaAtendimentoId == areaAtendimentoId) &&
                                    cha.situacao == "A" &&
                                    ate == null
                              select new AtendimentoPendenteViewModel()
                              {
                                  associadoId = a.associadoId,
                                  nome = a.nome,
                                  cpf = a.cpf,
                                  email = a.email1,
                                  chamadoId = cha.chamadoId,
                                  dt_chamado = cha.dt_chamado,
                                  dt_chamado2 = cha.dt_chamado.ToString("dd/Mm/yyyy HH:mm"),
                                  atraso = SqlMethods.DateDiffDay(cha.dt_chamado, DateTime.Today),
                                  areaAtendimentoId = cha.areaAtendimentoId,
                                  descricao_areaAtendimento = are.descricao,
                                  assunto = cha.assunto,
                                  usuarioId = cha.usuarioId,
                                  PageSize = pageSize,
                                  TotalCount = (from cha1 in db.Chamados
                                                join a1 in db.Associados on cha1.associadoId equals a1.associadoId
                                                join are1 in db.AreaAtendimentos on cha1.areaAtendimentoId equals are1.areaAtendimentoId
                                                where (cha1.areaAtendimentoId == null || cha1.areaAtendimentoId == areaAtendimentoId) &&
                                                      cha1.situacao == "A"
                                                orderby cha1.dt_chamado, a1.nome
                                                select cha1.associadoId).Count()

                              }))
                        select new AtendimentoPendenteViewModel()
                        {
                            associadoId = zapata.associadoId,
                            nome = zapata.nome,
                            cpf = zapata.cpf,
                            crm = zapata.crm,
                            email = zapata.email,
                            chamadoId = zapata.chamadoId,
                            dt_chamado = zapata.dt_chamado,
                            dt_chamado2 = zapata.dt_chamado2,
                            atraso = zapata.atraso,
                            areaAtendimentoId = zapata.areaAtendimentoId,
                            descricao_areaAtendimento = zapata.descricao_areaAtendimento,
                            assunto = zapata.assunto,
                            usuarioId = zapata.usuarioId,
                            usuario_nome = (zapata.usuarioId.HasValue ? (from usu in seguranca_db.Usuarios
                                                                         where usu.usuarioId == zapata.usuarioId
                                                                         select usu.nome).FirstOrDefault() : null),
                            PageSize = pageSize,
                            TotalCount = zapata.TotalCount
                        };

                var z = from zapata in
                            ((from cha in db.Chamados.AsEnumerable()
                              join ate in db.Atendimentos on cha.chamadoId equals ate.chamadoId
                              join a in db.Associados on cha.associadoId equals a.associadoId
                              join are in db.AreaAtendimentos on cha.areaAtendimentoId equals are.areaAtendimentoId
                              where (areaAtendimentoId == null || cha.areaAtendimentoId == areaAtendimentoId) &&
                                    ate.dt_atendimento == (from ate1 in db.Atendimentos
                                                           where ate1.chamadoId == cha.chamadoId
                                                           select ate1.dt_atendimento).Max()
                                    && cha.situacao == "A"
                              select new AtendimentoPendenteViewModel()
                              {
                                  associadoId = a.associadoId,
                                  nome = a.nome,
                                  cpf = a.cpf,
                                  email = a.email1,
                                  chamadoId = cha.chamadoId,
                                  dt_chamado = cha.dt_chamado,
                                  dt_chamado2 = cha.dt_chamado.ToString("dd/Mm/yyyy HH:mm"),
                                  dt_atendimento = ate.dt_atendimento,
                                  atraso = SqlMethods.DateDiffDay(cha.dt_chamado, DateTime.Today),
                                  areaAtendimentoId = cha.areaAtendimentoId,
                                  descricao_areaAtendimento = are.descricao,
                                  assunto = cha.assunto,
                                  usuarioId = cha.usuarioId,
                                  PageSize = pageSize,
                                  TotalCount = (from cha1 in db.Chamados
                                                join a1 in db.Associados on cha1.associadoId equals a1.associadoId
                                                join are1 in db.AreaAtendimentos on cha1.areaAtendimentoId equals are1.areaAtendimentoId
                                                where (cha1.areaAtendimentoId == null || cha1.areaAtendimentoId == areaAtendimentoId) &&
                                                      cha1.situacao == "A"
                                                orderby cha1.dt_chamado, a1.nome
                                                select cha1.associadoId).Count()

                              }))
                        select new AtendimentoPendenteViewModel()
                        {
                            associadoId = zapata.associadoId,
                            nome = zapata.nome,
                            cpf = zapata.cpf,
                            crm = zapata.crm,
                            email = zapata.email,
                            chamadoId = zapata.chamadoId,
                            dt_chamado = zapata.dt_chamado,
                            dt_chamado2 = zapata.dt_chamado2,
                            dt_atendimento = zapata.dt_atendimento,
                            atraso = zapata.atraso,
                            areaAtendimentoId = zapata.areaAtendimentoId,
                            descricao_areaAtendimento = zapata.descricao_areaAtendimento,
                            assunto = zapata.assunto,
                            usuarioId = zapata.usuarioId,
                            usuario_nome = (zapata.usuarioId.HasValue ? (from usu in seguranca_db.Usuarios
                                                                         where usu.usuarioId == zapata.usuarioId
                                                                         select usu.nome).FirstOrDefault() : null),
                            PageSize = pageSize,
                            TotalCount = zapata.TotalCount
                        };

                return q.Union(z).OrderBy(m => m.chamadoId).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
            }
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

        public override IEnumerable<AtendimentoPendenteViewModel> BindReport(params object[] param)
        {
            int? areaAtendimentoId = null;
            if (param[0].ToString() != "")
                areaAtendimentoId = int.Parse(param[0].ToString());

            using (seguranca_db = new SecurityContext())
            {
                var q = from zapata in
                            ((from cha in db.Chamados.AsEnumerable()
                              join ate in db.Atendimentos on cha.chamadoId equals ate.chamadoId into ATE
                              from ate in ATE.DefaultIfEmpty()
                              join a in db.Associados on cha.associadoId equals a.associadoId
                              join are in db.AreaAtendimentos on cha.areaAtendimentoId equals are.areaAtendimentoId
                              where (areaAtendimentoId == null || cha.areaAtendimentoId == areaAtendimentoId) &&
                                    cha.situacao == "A" &&
                                    ate == null
                              select new AtendimentoPendenteViewModel()
                              {
                                  associadoId = a.associadoId,
                                  nome = a.nome,
                                  cpf = a.cpf,
                                  email = a.email1,
                                  chamadoId = cha.chamadoId,
                                  dt_chamado = cha.dt_chamado,
                                  dt_chamado2 = cha.dt_chamado.ToString("dd/Mm/yyyy HH:mm"),
                                  atraso = SqlMethods.DateDiffDay(cha.dt_chamado, DateTime.Today),
                                  areaAtendimentoId = cha.areaAtendimentoId,
                                  descricao_areaAtendimento = are.descricao,
                                  assunto = cha.assunto,
                                  usuarioId = cha.usuarioId,
                              }))
                        select new AtendimentoPendenteViewModel()
                        {
                            associadoId = zapata.associadoId,
                            nome = zapata.nome,
                            cpf = zapata.cpf,
                            crm = zapata.crm,
                            email = zapata.email,
                            chamadoId = zapata.chamadoId,
                            dt_chamado = zapata.dt_chamado,
                            dt_chamado2 = zapata.dt_chamado2,
                            atraso = zapata.atraso,
                            areaAtendimentoId = zapata.areaAtendimentoId,
                            descricao_areaAtendimento = zapata.descricao_areaAtendimento,
                            assunto = zapata.assunto,
                            usuarioId = zapata.usuarioId,
                            usuario_nome = (zapata.usuarioId.HasValue ? (from usu in seguranca_db.Usuarios
                                                                         where usu.usuarioId == zapata.usuarioId
                                                                         select usu.nome).FirstOrDefault() : null)
                        };

                var z = from zapata in
                            ((from cha in db.Chamados.AsEnumerable()
                              join ate in db.Atendimentos on cha.chamadoId equals ate.chamadoId
                              join a in db.Associados on cha.associadoId equals a.associadoId
                              join m in db.Associados on a.associadoId equals m.associadoId
                              join are in db.AreaAtendimentos on cha.areaAtendimentoId equals are.areaAtendimentoId
                              where (areaAtendimentoId == null || cha.areaAtendimentoId == areaAtendimentoId) &&
                                    ate.dt_atendimento == (from ate1 in db.Atendimentos
                                                           where ate1.chamadoId == cha.chamadoId
                                                           select ate1.dt_atendimento).Max()
                                    && cha.situacao == "A"
                              select new AtendimentoPendenteViewModel()
                              {
                                  associadoId = a.associadoId,
                                  nome = a.nome,
                                  cpf = a.cpf,
                                  email = a.email1,
                                  chamadoId = cha.chamadoId,
                                  dt_chamado = cha.dt_chamado,
                                  dt_chamado2 = cha.dt_chamado.ToString("dd/Mm/yyyy HH:mm"),
                                  dt_atendimento = ate.dt_atendimento,
                                  atraso = SqlMethods.DateDiffDay(cha.dt_chamado, DateTime.Today),
                                  areaAtendimentoId = cha.areaAtendimentoId,
                                  descricao_areaAtendimento = are.descricao,
                                  assunto = cha.assunto,
                                  usuarioId = cha.usuarioId
                              }))
                        select new AtendimentoPendenteViewModel()
                        {
                            associadoId = zapata.associadoId,
                            nome = zapata.nome,
                            cpf = zapata.cpf,
                            crm = zapata.crm,
                            email = zapata.email,
                            chamadoId = zapata.chamadoId,
                            dt_chamado = zapata.dt_chamado,
                            dt_chamado2 = zapata.dt_chamado2,
                            dt_atendimento = zapata.dt_atendimento,
                            atraso = zapata.atraso,
                            areaAtendimentoId = zapata.areaAtendimentoId,
                            descricao_areaAtendimento = zapata.descricao_areaAtendimento,
                            assunto = zapata.assunto,
                            usuarioId = zapata.usuarioId,
                            usuario_nome = (zapata.usuarioId.HasValue ? (from usu in seguranca_db.Usuarios
                                                                         where usu.usuarioId == zapata.usuarioId
                                                                         select usu.nome).FirstOrDefault() : null)
                        };

                return q.Union(z).OrderBy(m => m.chamadoId).ToList();

            }
        }
        #endregion
    }
}