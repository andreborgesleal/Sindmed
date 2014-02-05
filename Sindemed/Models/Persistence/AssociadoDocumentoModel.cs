using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using Sindemed.Models.Repositories;
using Sindemed.Models.Entidades;
using App_Dominio.Enumeracoes;
using System.Data.Entity.SqlServer;
using App_Dominio.Models;

namespace Sindemed.Models.Persistence
{
    public class AssociadoDocumentoModel : ProcessContext<AssociadoDocumento, AssociadoDocumentoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override AssociadoDocumento ExecProcess(AssociadoDocumentoViewModel value)
        {
            AssociadoDocumento entity = MapToEntity(value);
            this.db.Set<AssociadoDocumento>().Add(entity);
            return entity;
        }

        public override Validate AfterInsert(AssociadoDocumentoViewModel value)
        {
            EmpresaSecurity<SecurityContext> empresaSecurity = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = empresaSecurity.getSessaoCorrente();

            #region Alerta 1
            AlertaRepository alerta = new AlertaRepository()
            {
                usuarioId = (from al in db.AreaAtendimentos where al.areaAtendimentoId == value.areaAtendimentoId select al.usuario1Id).First(),
                sistemaId = sessaoCorrente.sistemaId,
                dt_emissao = DateTime.Now,
                linkText = "<span class=\"label label-warning\">Atendimento</span>",
                url = "../Atendimento/Create?chamadoId=" + value.chamadoId.ToString() + "&fluxo=2",
                mensagemAlerta = "<b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "h</b><p>" + value.assunto + "</p>"
            };

            alerta.uri = value.uri;

            AlertaRepository r = empresaSecurity.InsertAlerta(alerta);
            if (r.mensagem.Code > 0)
                throw new DbUpdateException(r.mensagem.Message);
            #endregion

            #region Alerta 2
            int? usuario2Id = (from al in db.AreaAtendimentos where al.areaAtendimentoId == value.areaAtendimentoId select al.usuario2Id).FirstOrDefault();

            if (usuario2Id.HasValue)
            {
                AlertaRepository alerta2 = new AlertaRepository()
                {
                    usuarioId = usuario2Id.Value,
                    sistemaId = sessaoCorrente.sistemaId,
                    dt_emissao = DateTime.Now,
                    linkText = "<span class=\"label label-warning\">Atendimento</span>",
                    url = "../Atendimento/Create?chamadoId=" + value.chamadoId.ToString() + "&fluxo=2",
                    mensagemAlerta = "<b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "h</b><p>" + value.assunto + "</p>"
                };

                alerta2.uri = value.uri;

                AlertaRepository r2 = empresaSecurity.InsertAlerta(alerta2);
                if (r2.mensagem.Code > 0)
                    throw new DbUpdateException(r2.mensagem.Message);
            }
            #endregion

            return new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };
        }   


        public override AssociadoDocumento MapToEntity(AssociadoDocumentoViewModel value)
        {
            return new AssociadoDocumento()
            {
                associadoId = value.associadoId,
                fileId = value.fileId,
                nomeArquivoOriginal = value.nomeArquivoOriginal,
                dt_arquivo = value.dt_arquivo
            };
        }

        public override AssociadoDocumentoViewModel MapToRepository(AssociadoDocumento entity)
        {
            return new AssociadoDocumentoViewModel()
            {
                associadoId = entity.associadoId,
                fileId = entity.fileId,
                nome = (from ass in db.Associados where ass.associadoId == entity.associadoId select ass).Select(info => info.nome).FirstOrDefault() ?? "",
                nomeArquivoOriginal = entity.nomeArquivoOriginal,
                dt_arquivo = entity.dt_arquivo,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override AssociadoDocumento Find(AssociadoDocumentoViewModel key)
        {
            return db.AssociadoDocumentos.Find(key.associadoId, key.fileId);
        }

        public override Validate Validate(AssociadoDocumentoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.associadoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Associado").ToString();
                value.mensagem.MessageBase = "Campo Associado deve ser informado.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (value.fileId == "")
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Especialidade").ToString();
                value.mensagem.MessageBase = "Identificador interno do arquivo deve ser informado.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }


            return value.mensagem;
        }

        #endregion
    }


    public class ListViewAssociadoDocumento : ListViewRepository<AssociadoDocumentoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<AssociadoDocumentoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            int _associadoId = (int)param[0];
            string _descricao = param != null && param.Count() > 1 && param[1] != null ? param[1].ToString() : null;

            return (from ad in db.AssociadoDocumentos
                    join ass in db.Associados on ad.associadoId equals ass.associadoId
                    where ad.associadoId == _associadoId &&
                          (_descricao == null || String.IsNullOrEmpty(_descricao) || ad.nomeArquivoOriginal.StartsWith(_descricao.Trim()))
                    orderby ad.nomeArquivoOriginal
                    select new AssociadoDocumentoViewModel
                    {
                        associadoId = ad.associadoId,
                        fileId = ad.fileId,
                        nomeArquivoOriginal = ad.nomeArquivoOriginal,
                        dt_arquivo = ad.dt_arquivo,
                        nome = ass.nome,
                        PageSize = pageSize,
                        TotalCount = (from ad1 in db.AssociadoDocumentos
                                      join ass1 in db.Associados on ad1.associadoId equals ass1.associadoId
                                      where ad1.associadoId == _associadoId &&
                                            (_descricao == null || String.IsNullOrEmpty(_descricao) || ad1.nomeArquivoOriginal.StartsWith(_descricao.Trim()))
                                      orderby ad.nomeArquivoOriginal
                                      select ad1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new AssociadoDocumentoModel().getObject((AssociadoDocumentoViewModel)id);
        }
        #endregion
    }
}