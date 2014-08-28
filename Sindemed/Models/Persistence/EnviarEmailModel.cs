using System;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using DWM.Models.Repositories;
using DWM.Models.Entidades;
using App_Dominio.Enumeracoes;
using App_Dominio.Security;
using App_Dominio.Repositories;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using App_Dominio.Component;
using System.Net.Mail;
using System.Collections.Generic;
//using System.Data.Objects.SqlClient;

namespace DWM.Models.Persistence
{
    public class EnviarEmailModel : ExecContext<EnviarEmailViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override EnviarEmailViewModel ExecProcess(EnviarEmailViewModel value, Crud operation)
        {
            Validate result = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };
            EmpresaSecurity<SecurityContext> empresaSecurity = new EmpresaSecurity<SecurityContext>();

            #region Enviar e-mail ao usuário
            Empresa empresa = seguranca_db.Empresas.Find(value.empresaId);
            SendEmail sendMail = new SendEmail();

            MailAddress sender = new MailAddress(empresa.nome + " <" + empresa.email + ">");
            List<string> recipients = new List<string>();
            List<string> carbon = new List<string>();

            recipients.Add(empresa.nome + " <" + empresa.email + ">");

            foreach (AssociadoViewModel a in value.Destinatarios)
            {
                if (a.email1 != null && a.email1.Trim() != "")
                    carbon.Add(a.nome + "<" + a.email1 + ">");
            }
            
            string Subject = value.assunto;
            string Text = "<p>Enviar e-mail ao condômino</p>";
            string Html = value.mensagemEmail;

            result = sendMail.Send(sender, recipients, Html, Subject, Text, carbon);
            if (result.Code > 0)
                throw new ArgumentException(result.MessageBase);
            #endregion

            #region Enviar alerta
            foreach (AssociadoViewModel a in value.Destinatarios.Where(info => info.usuarioId.HasValue))
            {
                AlertaRepository alerta = new AlertaRepository()
                {
                    usuarioId = a.usuarioId.Value,
                    sistemaId = sessaoCorrente.sistemaId,
                    dt_emissao = DateTime.Now,
                    linkText =  "<span class=\"label label-primary\">E-mail</span>",
                    url = "#",
                    mensagemAlerta = "<b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "h</b><p>" + value.assunto + "</p>"
                };

                alerta.uri = value.uri;

                AlertaRepository r = empresaSecurity.InsertAlerta(alerta);
                if (r.mensagem.Code > 0)
                    throw new DbUpdateException(r.mensagem.Message);
            }

            return value;
            #endregion
        }

        public override Validate Validate(EnviarEmailViewModel value, App_Dominio.Enumeracoes.Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.Destinatarios.Count() > 200)
            {
                value.mensagem.Code = 53;
                value.mensagem.Message = MensagemPadrao.Message(53, "200 e-mails").ToString();
                value.mensagem.MessageBase = "Por favor refine sua consulta para reduzir a quantidade de e-mails a serem enviados.";
            }

            return value.mensagem;
        }
        #endregion
    }

    public class ListViewEnviarEmail : ListViewRepository<AssociadoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<AssociadoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            int? grupoAssociadoId = null;
            if (param[0] != null && param[0].ToString() != "")
                grupoAssociadoId = int.Parse(param[0].ToString());

            int? areaAtuacaoId = null;
            if (param[1] != null && param[1].ToString() != "")
                areaAtuacaoId = int.Parse(param[1].ToString());

            string aniversariante = param[2].ToString();
            int mes_atual = DateTime.Today.Month;

            string torreId = null;
            if (param[3] != null && param[3].ToString() != "")
                torreId = param[3].ToString();

            int? unidadeId = null;
            if (param[4] != null && param[4].ToString() != "")
                unidadeId = int.Parse(param[4].ToString());

            var q = (from a in db.Associados
                     where (grupoAssociadoId == null || (from g in db.AssociadoGrupos
                                                         where g.grupoAssociadoId == grupoAssociadoId &&
                                                               g.associadoId == a.associadoId
                                                         select g).Count() > 0) &&
                           (areaAtuacaoId == null || a.areaAtuacaoId == areaAtuacaoId) &&
                           (aniversariante == "N" || (a.dt_nascimento != null && a.dt_nascimento.Value.Month == mes_atual)) &&
                           (torreId == null || a.torreId == torreId) &&
                           (unidadeId == null || a.unidadeId == unidadeId) &&
                           a.dt_fim == null && ((a.email1 != null && a.email1 != ""))
                     select new AssociadoViewModel()
                     {
                         associadoId = a.associadoId,
                         nome = a.nome,
                         email1 = a.email1,
                         torreId = a.torreId,
                         unidadeId = a.unidadeId,
                         dt_nascimento = a.dt_nascimento,
                         usuarioId = a.usuarioId,
                         areaAtuacaoId = a.areaAtuacaoId,
                         PageSize = pageSize,
                         TotalCount = (from a1 in db.Associados
                                       where (grupoAssociadoId == null || (from g1 in db.AssociadoGrupos
                                                                           where g1.grupoAssociadoId == grupoAssociadoId &&
                                                                                 g1.associadoId == a1.associadoId
                                                                           select g1).Count() > 0) &&
                                             (areaAtuacaoId == null || a1.areaAtuacaoId == areaAtuacaoId) &&
                                             (aniversariante == "N" || (a1.dt_nascimento != null && a1.dt_nascimento.Value.Month == mes_atual)) &&
                                             (torreId == null || a1.torreId == torreId) &&
                                             (unidadeId == null || a1.unidadeId == unidadeId) &&
                                             a1.dt_fim == null && ((a1.email1 != null && a1.email1 != ""))
                                       select a1).Count()
                     }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();

            return q;
        }

        public override Repository getRepository(Object id)
        {
            return new AssociadoModel().getObject((AssociadoViewModel)id);
        }

        public override string action()
        {
            return "ListParam";
        }
        #endregion
    }

}