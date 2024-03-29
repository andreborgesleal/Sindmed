﻿using System;
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

namespace DWM.Models.Persistence
{
    public class AtendimentoModel : ProcessContext<Atendimento, AtendimentoViewModel, ApplicationContext>
    {
        protected virtual void AtualizarChamado(Chamado chamado, EmpresaSecurity<SecurityContext> empresaSecurity)
        {
            if (!chamado.usuarioId.HasValue)
                chamado.usuarioId = empresaSecurity.getUsuario().usuarioId;
                
            db.Entry(chamado).State = EntityState.Modified;
        }

        protected virtual string getLinkTextAlerta()
        {
            return "<span class=\"label label-primary\">Resposta</span>";
        }

        #region Métodos da classe CrudContext
        public override Atendimento ExecProcess(AtendimentoViewModel value, Crud operation)
        {
            if (value.fluxo == "2") // administração/associado
            {
                EmpresaSecurity<SecurityContext> empresaSecurity = new EmpresaSecurity<SecurityContext>();

                Chamado chamado = this.db.Chamados.Find(value.chamadoId);

                if (value.chamado != null)
                {
                    if (value.chamado.situacao == "F")
                    {
                        chamado.situacao = "F";
                        chamado.chamadoStatusId = 8;
                    }
                    else
                        chamado.chamadoStatusId = value.chamado.chamadoStatusId;

                    AtualizarChamado(chamado, empresaSecurity);
                }
            }

            Atendimento atendimento = MapToEntity(value);
            this.db.Set<Atendimento>().Add(atendimento);

            return atendimento;
        }

        public override Validate AfterInsert(AtendimentoViewModel value)
        {
            int _fuso_horario = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.FUSO_HORARIO).valor);
            EmpresaSecurity<SecurityContext> empresaSecurity = new EmpresaSecurity<SecurityContext>();
            int? _usuarioId = null;

            sessaoCorrente = empresaSecurity.getSessaoCorrente();

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
                empresaId = sessaoCorrente.empresaId,
                usuarioId = _usuarioId.Value,
                sistemaId = sessaoCorrente.sistemaId,
                dt_emissao = DateTime.Now.AddHours(_fuso_horario),
                linkText = getLinkTextAlerta(),
                url = "../Atendimento/Create?chamadoId=" + value.chamadoId.ToString() + "&fluxo=" + (value.fluxo == "2" ? "1" : "2"),
                mensagemAlerta = "<b>" + DateTime.Now.AddHours(_fuso_horario).ToString("dd/MM/yyyy HH:mm") + "h</b><p>" + value.chamado.assunto + "</p>"
            };

            alerta.uri = value.uri;

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
                        empresaId = sessaoCorrente.empresaId,
                        usuarioId = _usuarioId.Value,
                        sistemaId = sessaoCorrente.sistemaId,
                        dt_emissao = DateTime.Now.AddHours(_fuso_horario),
                        linkText = "<span class=\"label label-primary\">Resposta</span>",
                        url = "../Atendimento/Create?chamadoId=" + value.chamadoId.ToString() + "&fluxo=2",
                        mensagemAlerta = "<b>" + DateTime.Now.AddHours(_fuso_horario).ToString("dd/MM/yyyy HH:mm") + "h</b><p>" + value.chamado.assunto + "</p>"
                    };

                    alerta2.uri = value.uri;

                    AlertaRepository r2 = empresaSecurity.InsertAlerta(alerta2);
                    if (r2.mensagem.Code > 0)
                        throw new DbUpdateException(r2.mensagem.Message);
                }
            }
            #endregion

            #region Enviar e-mail ao associado
            if (db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.HABILITA_EMAIL).valor == "S" && value.fluxo == "2")
            {
                int _sistemaId = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.SISTEMA).valor);

                SendEmail sendMail = new SendEmail();

                Empresa empresa = seguranca_db.Empresas.Find(sessaoCorrente.empresaId);
                Sistema sistema = seguranca_db.Sistemas.Find(_sistemaId);
                Associado associado = db.Associados.Find(value.chamado.associadoId);

                MailAddress sender = new MailAddress(empresa.nome + " <" + empresa.email + ">");
                List<string> recipients = new List<string>();

                recipients.Add(associado.nome + "<" + associado.email1 + ">");

                string Subject = "Re: Chamado " + value.chamado.chamadoId.ToString() + "-" + value.chamado.assunto + " - " + empresa.nome;
                string Text = "<p>Atendimento de chamado</p>";
                string Html = "<p><span style=\"font-family: Verdana; font-size: larger; color: #656464\">" + sistema.descricao + "</span></p>" +
                              "<p><span style=\"font-family: Verdana; font-size: xx-large; color: #0094ff\">" + associado.nome.ToUpper() + "</span></p>" +
                              "<p></p>" +
                              "<p><span style=\"font-family: Verdana; font-size: small; color: #000\">Essa é uma mensagem de notificação de atendimento de chamado. Sua solicitação foi respondida. Acesse o sistema para ler o retorno da administração.</span></p>";

                Html += "<p></p>" +
                        "<p>&nbsp;</p>" +
                        "<p>&nbsp;</p>" +
                        "<p><span style=\"font-family: Verdana; font-size: small; color: #000\">Obrigado,</span></p>" +
                        "<p><span style=\"font-family: Verdana; font-size: small; color: #000\">Administração " + empresa.nome + "</span></p>";

                Validate result = sendMail.Send(sender, recipients, Html, Subject, Text);
                if (result.Code > 0)
                {
                    result.MessageBase = "Resposta de chamado cadastrada com sucesso, mas por falhas de comunicação não foi possível enviar o e-mail de notificação ao usuário. ";
                    throw new App_DominioException(result);
                }

            }
            #endregion

            return new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };
        }

        public override Atendimento MapToEntity(AtendimentoViewModel value)
        {
            int _fuso_horario = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.FUSO_HORARIO).valor);

            Atendimento atendimento = new Atendimento()
            {
                chamadoId = value.chamadoId,
                dt_atendimento = DateTime.Now.AddHours(_fuso_horario),
                fluxo = value.fluxo,
                mensagem = value.mensagemResposta
            };

            if (value.anexo.nomeArquivoOriginal != null && value.anexo.nomeArquivoOriginal != "")
            {
                atendimento.fileId = value.anexo.fileId;

                atendimento.AssociadoDocumento = new AssociadoDocumento()
                {
                    fileId = value.anexo.fileId,
                    associadoId = this.db.Chamados.Find(value.chamadoId).associadoId,
                    tipoDocumentoId = value.anexo.tipoDocumentoId.Value,
                    palavra_chave = value.anexo.palavra_chave,
                    nomeArquivoOriginal = value.anexo.nomeArquivoOriginal,
                    dt_arquivo = DateTime.Now.AddHours(_fuso_horario)
                };
            }

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
                anexo = new AssociadoDocumentoModel(db).CreateRepository(null),
                atendimentos = (from ate in db.Atendimentos 
                                join asd in db.AssociadoDocumentos on ate.fileId equals asd.fileId into ASD
                                from asd in ASD.DefaultIfEmpty()
                                where ate.chamadoId == entity.chamadoId
                                select new AtendimentoViewModel()
                                {
                                    chamadoId = entity.chamadoId,
                                    dt_atendimento = ate.dt_atendimento,
                                    fluxo = ate.fluxo,
                                    anexo = asd != null ? new AssociadoDocumentoViewModel() { fileId = asd.fileId, dt_arquivo = asd.dt_arquivo, nomeArquivoOriginal = asd.nomeArquivoOriginal } : null,
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

            if (value.anexo.nomeArquivoOriginal != "" && value.anexo.tipoDocumentoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Tipo do anexo").ToString();
                value.mensagem.MessageBase = "Campo Tipo do Anexo deve ser informado.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }
            return value.mensagem;
        }
        #endregion

        #region Métodos customizados
        public AtendimentoViewModel Create(AtendimentoViewModel value)
        {
            using (db = getContextInstance())
            {
                int _fuso_horario = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.FUSO_HORARIO).valor);

                Atendimento entity = new Atendimento()
                {
                    chamadoId = value.chamadoId,
                    fluxo = value.fluxo,
                    dt_atendimento = DateTime.Now.AddHours(_fuso_horario)
                };


                #region verifica se o chamado existe
                if (db.Chamados.Find(value.chamadoId) == null)
                {
                    value.mensagem = new Validate() { Code = 20, Message = MensagemPadrao.Message(20).ToString() };
                    return value;
                }
                #endregion

                value = MapToRepository(entity);
                
                #region Valida se o usuário corrente é o usuário do chamado
                EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
                Usuario usuario = security.getUsuario();
                Associado ass = db.Associados.Find(value.chamado.associadoId);
                if (ass.usuarioId == null || usuario.usuarioId != ass.usuarioId)
                    if (value.chamado.usuarioId != null && value.chamado.usuarioId != usuario.usuarioId)
                        value.mensagem = new Validate() { Code = -1, Message = "Acesso negado" };
                    else if (value.chamado.usuarioId == null)
                    {
                        AreaAtendimento ar = db.AreaAtendimentos.Find(value.chamado.areaAtendimentoId);
                        if (ar.usuario1Id != 0 && ar.usuario1Id != usuario.usuarioId && ar.usuario2Id != usuario.usuarioId)
                            value.mensagem = new Validate() { Code = -1, Message = "Acesso negado" };
                    }

                #endregion
            }
            return value;
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