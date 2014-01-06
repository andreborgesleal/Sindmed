using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using Sindemed.Models.Repositories;
using Sindemed.Models.Entidades;
using App_Dominio.Enumeracoes;
using App_Dominio.Security;
using App_Dominio.Repositories;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace Sindemed.Models.Persistence
{
    public class AtendimentoModel : ProcessContext<Atendimento, AtendimentoViewModel, ApplicationContext>
    {
        protected virtual void AtualizarChamado(Chamado chamado, EmpresaSecurity<SecurityContext> empresaSecurity)
        {
            if (!chamado.usuarioId.HasValue)
            {
                chamado.usuarioId = empresaSecurity.getUsuario().usuarioId;
                db.Entry(chamado).State = EntityState.Modified;
            }
        }

        protected virtual string getLinkTextAlerta()
        {
            return "<span class=\"label label-primary\">Resposta</span>";
        }

        #region Métodos da classe CrudContext
        public override Atendimento ExecProcess(AtendimentoViewModel value)
        {
            if (value.fluxo == "2") // administração/associado
            {
                EmpresaSecurity<SecurityContext> empresaSecurity = new EmpresaSecurity<SecurityContext>();

                Chamado chamado = this.db.Chamados.Find(value.chamadoId);

                AtualizarChamado(chamado, empresaSecurity);
            }

            Atendimento atendimento = MapToEntity(value);
            this.db.Set<Atendimento>().Add(atendimento);
            return atendimento;
        }

        public override Validate AfterInsert(AtendimentoViewModel value)
        {
            EmpresaSecurity<SecurityContext> empresaSecurity = new EmpresaSecurity<SecurityContext>();
            int? _usuarioId = null;

            #region Alerta 1
            if (value.fluxo == "1") // 1-associado/sindmed (o alerta deve ser criado p/ o funcionário)
            {
                if (value.chamado.usuarioId == null)
                    _usuarioId = (from aa in db.AreaAtendimentos where aa.areaAtendimentoId == value.chamado.areaAtendimentoId select aa.usuario1Id).First();
                else
                    _usuarioId = value.chamado.usuarioId;
            }                
            else
                _usuarioId = (from ass in db.Associados where ass.associadoId == value.chamado.associadoId select ass.usuarioId).First();
            
            AlertaRepository alerta = new AlertaRepository()
            {
                usuarioId = _usuarioId.Value,
                dt_emissao = DateTime.Now,
                linkText = getLinkTextAlerta(),
                url = "../Atendimento/Create?chamadoId=" + value.chamadoId.ToString() + "&fluxo=" + (value.fluxo == "2" ? "1" : "2"),
                mensagemAlerta = "<b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "h</b><p>" + value.chamado.assunto + "</p>"
            };

            AlertaRepository r = empresaSecurity.InsertAlerta(alerta);
            if (r.mensagem.Code > 0)
                throw new DbUpdateException(r.mensagem.Message);
            #endregion

            #region Alerta 2
            if (value.fluxo == "1" && value.chamado.usuarioId == null) // 1-associado/sindmed (o alerta deve ser criado p/ o funcionário)
            {
                _usuarioId = (from aa in db.AreaAtendimentos where aa.areaAtendimentoId == value.chamado.areaAtendimentoId select aa.usuario2Id).FirstOrDefault();

                if (_usuarioId.HasValue)
                {
                    AlertaRepository alerta2 = new AlertaRepository()
                    {
                        usuarioId = _usuarioId.Value,
                        dt_emissao = DateTime.Now,
                        linkText = "<span class=\"label label-primary\">Resposta</span>",
                        url = "../Atendimento/Create?chamadoId=" + value.chamadoId.ToString() + "&fluxo=2",
                        mensagemAlerta = "<b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "h</b><p>" + value.chamado.assunto + "</p>"
                    };

                    AlertaRepository r2 = empresaSecurity.InsertAlerta(alerta2);
                    if (r2.mensagem.Code > 0)
                        throw new DbUpdateException(r2.mensagem.Message);
                }
            }
            #endregion

            return new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };
        }

        public override Atendimento MapToEntity(AtendimentoViewModel value)
        {
            Atendimento atendimento = new Atendimento()
            {
                chamadoId = value.chamadoId,
                dt_atendimento = DateTime.Now,
                fluxo = value.fluxo,
                mensagem = value.mensagemResposta,
            };

            return atendimento;
        }

        public override AtendimentoViewModel MapToRepository(Atendimento entity)
        {
            Chamado chamado = db.Chamados.Find(entity.chamadoId);

            return new AtendimentoViewModel()
            {
                chamadoId = entity.chamadoId,
                dt_atendimento = entity.dt_atendimento,
                fluxo = entity.fluxo,
                mensagemResposta = entity.mensagem,
                chamado = new ChamadoModel(db).MapToRepository(chamado),
                atendimentos = (from ate in db.Atendimentos
                                where ate.chamadoId == entity.chamadoId
                                select new AtendimentoViewModel()
                                {
                                    chamadoId = entity.chamadoId,
                                    dt_atendimento = ate.dt_atendimento,
                                    fluxo = ate.fluxo,
                                    mensagemResposta = ate.mensagem,
                                    mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
                                }).ToList(),
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override Atendimento Find(AtendimentoViewModel key)
        {
            return db.Atendimentos.Find(key.chamadoId);
        }

        public override Validate Validate(AtendimentoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };
            
            if (value.mensagemResposta == null ||value.mensagemResposta.Trim() == "")
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Mensagem de Resposta").ToString();
                value.mensagem.MessageBase = "Campo Mensagem de resposta deve ser informada.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }
            return value.mensagem;
        }
        #endregion

        #region Métodos customizados
        public AtendimentoViewModel Create(AtendimentoViewModel value)
        {
            Atendimento entity = new Atendimento()
            {
                chamadoId = value.chamadoId,
                fluxo = value.fluxo,
                dt_atendimento = DateTime.Now
            };

            using (db = getContextInstance())
                return MapToRepository(entity);
        }
        #endregion
    }

    public class FechamentoChamadoModel : AtendimentoModel
    {
        protected override void AtualizarChamado(Chamado chamado, EmpresaSecurity<SecurityContext> empresaSecurity)
        {
            chamado.situacao = "F";

            if (!chamado.usuarioId.HasValue)
                chamado.usuarioId = empresaSecurity.getUsuario().usuarioId;

            db.Entry(chamado).State = EntityState.Modified;
        }

        protected override string getLinkTextAlerta()
        {
            return "<span class=\"label label-success\">Fechado</span>";
        }

    }

    public class DetalhamentoChamadoModel : AtendimentoModel
    {
        protected override void AtualizarChamado(Chamado chamado, EmpresaSecurity<SecurityContext> empresaSecurity)
        {
            if (!chamado.usuarioId.HasValue)
                chamado.usuarioId = empresaSecurity.getUsuario().usuarioId;

            db.Entry(chamado).State = EntityState.Modified;
        }

        protected override string getLinkTextAlerta()
        {
            return "<span class=\"label label-success\">Fechado</span>";
        }

    }
}