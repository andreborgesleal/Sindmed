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
            string Text = "<p>Enviar e-mail ao associado</p>";
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

    public class ListViewEnviarEmail : ListViewRepository<MedicoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<MedicoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            int? grupoAssociadoId = null;
            if (param[0] != null && param[0].ToString() != "")
                grupoAssociadoId = int.Parse(param[0].ToString());

            int? especialidadeId = null;
            if (param[1] != null && param[1].ToString() != "")
                especialidadeId = int.Parse(param[1].ToString());

            int? cidadeId = null;
            if (param[2] != null && param[2].ToString() != "")
                cidadeId = int.Parse(param[2].ToString());

            string aniversariante = param[3].ToString();
            int mes_atual = DateTime.Today.Month;

            int crm_inicial = 1;

            if (param[4] != null && param[4].ToString() != "")
                crm_inicial = int.Parse(param[4].ToString());

            int crm_final = 99999;
            if (param[5] != null && param[5].ToString() != "")
                crm_final = int.Parse(param[5].ToString());

            var q = (from a in db.Associados
                     join m in db.Medicos on a.associadoId equals m.associadoId
                     where (grupoAssociadoId == null || (from g in db.AssociadoGrupos
                                                         where g.grupoAssociadoId == grupoAssociadoId &&
                                                               g.associadoId == a.associadoId
                                                         select g).Count() > 0) &&
                           (especialidadeId == null || m.especialidade1Id == especialidadeId || m.especialidade2Id == especialidadeId || m.especialidade3Id == especialidadeId) &&
                           (cidadeId == null || a.cidadeId == cidadeId || a.cidadeComId == cidadeId) &&
                           (aniversariante == "N" || (a.dt_nascimento != null && a.dt_nascimento.Value.Month == mes_atual)) &&
                           a.situacao == "A" && ((a.email1 != null && a.email1 != "") || (a.email2 != null && a.email2 != "") || (a.email3 != null && a.email3 != ""))
                     select new MedicoViewModel()
                     {
                         associadoId = a.associadoId,
                         nome = a.nome,
                         email1 = a.email1,
                         email2 = a.email2,
                         email3 = a.email3,
                         dt_nascimento = a.dt_nascimento,
                         usuarioId = a.usuarioId,
                         CRM = m.CRM
                     }).ToList();

            q = (from xpto in q
                 where Convert.ToInt16(xpto.CRM) >= crm_inicial && Convert.ToInt16(xpto.CRM) <= crm_final
                 select new MedicoViewModel()
                 {
                     associadoId = xpto.associadoId,
                     nome = xpto.nome,
                     email1 = xpto.email1,
                     email2 = xpto.email2,
                     email3 = xpto.email3,
                     dt_nascimento = xpto.dt_nascimento,
                     usuarioId = xpto.usuarioId,
                     CRM = xpto.CRM,
                     PageSize = pageSize,
                     TotalCount = (from xpto1 in q
                                   where Convert.ToInt16(xpto1.CRM) >= crm_inicial && Convert.ToInt16(xpto1.CRM) <= crm_final
                                   select xpto1.associadoId).Count()
                 }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();

            return q.ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new MedicoModel().getObject((MedicoViewModel)id);
        }

        public override string action()
        {
            return "ListParam";
        }
        #endregion
    }

}