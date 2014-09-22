using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using DWM.Models.Repositories;
using DWM.Models.Entidades;
using App_Dominio.Enumeracoes;
using App_Dominio.Security;
using App_Dominio.Repositories;
using System.Data.Entity.Infrastructure;

namespace DWM.Models.Persistence
{
    public class ChamadoModel : ProcessContext<Chamado, ChamadoViewModel, ApplicationContext>
    {
        public  ChamadoModel() : base()
        {
        }

        public ChamadoModel(ApplicationContext _db)
        {
            this.db = _db;
        }

        #region Métodos da classe CrudContext
        public override Chamado ExecProcess(ChamadoViewModel value, Crud operation)
        {
            Chamado chamado = MapToEntity(value);
            this.db.Set<Chamado>().Add(chamado);
            return chamado;
        }

        public override Validate AfterInsert(ChamadoViewModel value)
        {
            EmpresaSecurity<SecurityContext> empresaSecurity = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = empresaSecurity.getSessaoCorrente();
            int _fuso_horario = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.FUSO_HORARIO).valor);

            #region Alerta 1
            AlertaRepository alerta = new AlertaRepository()
            {
                empresaId = sessaoCorrente.empresaId,
                usuarioId = (from al in db.AreaAtendimentos where al.areaAtendimentoId == value.areaAtendimentoId select al.usuario1Id).First(),
                sistemaId = sessaoCorrente.sistemaId,
                dt_emissao = DateTime.Now.AddHours(_fuso_horario),
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
                    empresaId = sessaoCorrente.empresaId,
                    usuarioId = usuario2Id.Value,
                    sistemaId = sessaoCorrente.sistemaId,
                    dt_emissao = DateTime.Now.AddHours(_fuso_horario),
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

        public override Chamado MapToEntity(ChamadoViewModel value)
        {
            Chamado chamado = new Chamado()
            {
                chamadoId = value.chamadoId,
                associadoId = value.associadoId,
                areaAtendimentoId = value.areaAtendimentoId,
                chamadoMotivoId = value.chamadoMotivoId,
                chamadoStatusId = value.chamadoStatusId,
                dt_chamado = value.dt_chamado,
                assunto = value.assunto,
                situacao = value.situacao,
                usuarioId = value.usuarioId,
                mensagemOriginal = value.mensagemOriginal
            };

            return chamado;
        }

        public override ChamadoViewModel MapToRepository(Chamado entity)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["Avatar"].Replace("~", "..") + "/";

            using (SecurityContext seg = new SecurityContext())
                return new ChamadoViewModel()
                {
                    chamadoId = entity.chamadoId,
                    associadoId = entity.associadoId,
                    nome_associado = db.Associados.Find(entity.associadoId).nome,
                    avatar = path + db.Associados.Find(entity.associadoId).avatar,
                    areaAtendimentoId = entity.areaAtendimentoId,
                    descricao_areaAtendimento = db.AreaAtendimentos.Find(entity.areaAtendimentoId).descricao,
                    chamadoMotivoId = entity.chamadoMotivoId,
                    descricao_motivo = db.ChamadoMotivos.Find(entity.chamadoMotivoId).descricao,
                    chamadoStatusId = entity.chamadoStatusId,
                    descricao_status = db.ChamadoStatuss.Find(entity.chamadoStatusId).descricao,
                    dt_chamado = entity.dt_chamado,
                    assunto = entity.assunto,
                    situacao = entity.situacao,
                    usuarioId = entity.usuarioId,
                    nome_usuario = entity.usuarioId.HasValue ? seg.Usuarios.Find(entity.usuarioId).nome : null,
                    mensagemOriginal = entity.mensagemOriginal,
                    mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
                };
        }

        public override Chamado Find(ChamadoViewModel key)
        {
            return db.Chamados.Find(key.chamadoId);
        }

        public override Validate Validate(ChamadoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.associadoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "ID do Associado").ToString();
                value.mensagem.MessageBase = "Usuário precisa estar vinculado ao cadastro de associados para solicitar um chamado.";
            }

            if (value.chamadoMotivoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Motivo").ToString();
                value.mensagem.MessageBase = "Motivo do chamado deve ser informado.";
            }

            if (value.chamadoStatusId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Status").ToString();
                value.mensagem.MessageBase = "Status do chamado deve ser informado.";
            }

            return value.mensagem;
        }
        #endregion

        #region Métodos customizados
        public ChamadoViewModel Create()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["Avatar"].Replace("~", "..") + "/";
            EmpresaSecurity<SecurityContext> empresaSecurity = new EmpresaSecurity<SecurityContext>();
            int usuarioId = empresaSecurity.getSessaoCorrente().usuarioId;

            using (db = new ApplicationContext())
            {
                int _fuso_horario = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.FUSO_HORARIO).valor);
                return new ChamadoViewModel()
                {
                    chamadoStatusId = 1,
                    chamadoMotivoId = 1,
                    situacao = "A",
                    dt_chamado = DateTime.Now.AddHours(_fuso_horario),
                    associadoId = (from Ass in db.Associados where Ass.usuarioId == usuarioId select Ass.associadoId).FirstOrDefault(),
                    nome_associado = (from Ass in db.Associados where Ass.usuarioId == usuarioId select Ass.nome).FirstOrDefault(),
                    avatar = path + (from Ass in db.Associados where Ass.usuarioId == usuarioId select Ass.avatar).FirstOrDefault()
                };
            }
        }
        #endregion
    }

    public class ListViewChamadoAssociado : ListViewRepository<ChamadoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<ChamadoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();
            int? _chamadoId = null;
            _chamadoId = param != null && param.Count() > 0 && param[0] != null &&  param[0] != "" ? int.Parse(param[0].ToString()) : _chamadoId;
            return (from cham in db.Chamados
                    join are in db.AreaAtendimentos on cham.AreaAtendimento equals are
                    join ass in db.Associados on cham.Associado equals ass
                    join chm in db.ChamadoMotivos on cham.chamadoMotivoId equals chm.chamadoMotivoId
                    join chs in db.ChamadoStatuss on cham.chamadoStatusId equals chs.chamadoStatusId
                    where ass.usuarioId == sessaoCorrente.usuarioId &&
                          (_chamadoId == null || cham.chamadoId == _chamadoId)
                    orderby cham.dt_chamado descending
                    select new ChamadoViewModel
                    {
                        chamadoId = cham.chamadoId,
                        dt_chamado = cham.dt_chamado,
                        assunto = cham.assunto,
                        areaAtendimentoId = cham.areaAtendimentoId,
                        descricao_areaAtendimento = are.descricao,
                        associadoId = cham.associadoId,
                        nome_associado = ass.nome,
                        chamadoStatusId = cham.chamadoStatusId,
                        descricao_status = chs.descricao,
                        chamadoMotivoId = cham.chamadoMotivoId,
                        descricao_motivo = chm.descricao,
                        situacao = cham.situacao,                        
                        PageSize = pageSize,
                        TotalCount = (from cham1 in db.Chamados
                                      join are1 in db.AreaAtendimentos on cham1.AreaAtendimento equals are1
                                      join ass1 in db.Associados on cham1.Associado equals ass1
                                      where ass1.usuarioId == sessaoCorrente.usuarioId &&
                                            (_chamadoId == null || cham1.chamadoId == _chamadoId)
                                      select cham1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new ChamadoModel().getObject((ChamadoViewModel)id);
        }
        #endregion
    }

    public class ListViewChamadoAdministracao : ListViewRepository<ChamadoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<ChamadoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            #region Parâmetros
            int? _chamadoId = null;
            string _crm = null;
            int? _areaAtendimentoId = null;
            int? _chamadoMotivoId = null;
            int? _chamadoStatusId = null;
            string _situacao = "";

            _chamadoId = param[0] != null ? (int)param[0] : _chamadoId;
            if (param[1] != null && param [1] != "")
                _crm = param[1].ToString();
            
            DateTime _data1 = (DateTime)param[2];
            DateTime _data2 = (DateTime)param[3];

            _areaAtendimentoId = param[4] != null ? (int)param[4] : _areaAtendimentoId;
            _situacao = param[5] != null ? (string)param[5] : _situacao; //""-Todos, "A"-Chamados abertos ou "F"-Chamados fechados
            _chamadoMotivoId = param[6] != null ? (int)param[6] : _chamadoMotivoId;
            _chamadoStatusId = param[7] != null ? (int)param[7] : _chamadoStatusId;
            #endregion

            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();

            IEnumerable<ChamadoViewModel> q = null;

            if (_chamadoId != null)
                q = (from cham in db.Chamados
                     join are in db.AreaAtendimentos on cham.AreaAtendimento equals are
                     join ass in db.Associados on cham.Associado equals ass
                     join chm in db.ChamadoMotivos on cham.chamadoMotivoId equals chm.chamadoMotivoId
                     join chs in db.ChamadoStatuss on cham.chamadoStatusId equals chs.chamadoStatusId
                     where cham.chamadoId == _chamadoId &&
                            ((cham.usuarioId == sessaoCorrente.usuarioId) || (cham.usuarioId == null && (are.usuario1Id == sessaoCorrente.usuarioId || are.usuario2Id == sessaoCorrente.usuarioId)))
                     orderby cham.dt_chamado descending
                     select new ChamadoViewModel
                     {
                         chamadoId = cham.chamadoId,
                         dt_chamado = cham.dt_chamado,
                         assunto = cham.assunto,
                         areaAtendimentoId = cham.areaAtendimentoId,
                         descricao_areaAtendimento = are.descricao,
                         chamadoStatusId = cham.chamadoStatusId,
                         descricao_status = chs.descricao,
                         chamadoMotivoId = cham.chamadoMotivoId,
                         descricao_motivo = chm.descricao,
                         associadoId = cham.associadoId,
                         nome_associado = ass.nome,
                         situacao = cham.situacao,
                         PageSize = pageSize,
                         TotalCount = 1
                     }).ToList();
            else
                q = (from cham in db.Chamados
                     join are in db.AreaAtendimentos on cham.AreaAtendimento equals are
                     join ass in db.Associados on cham.Associado equals ass
                     join med in db.Medicos on ass.associadoId equals med.associadoId 
                     join chm in db.ChamadoMotivos on cham.chamadoMotivoId equals chm.chamadoMotivoId
                     join chs in db.ChamadoStatuss on cham.chamadoStatusId equals chs.chamadoStatusId
                     where ((cham.usuarioId == sessaoCorrente.usuarioId) || (cham.usuarioId == null && (are.usuario1Id == sessaoCorrente.usuarioId || are.usuario2Id == sessaoCorrente.usuarioId))) &&
                            (_crm == null || med.CRM == _crm) &&
                            (cham.dt_chamado >= _data1 && cham.dt_chamado <= _data2) &&
                            (_areaAtendimentoId == null || cham.areaAtendimentoId == _areaAtendimentoId) &&
                            (_chamadoMotivoId == null || chm.chamadoMotivoId == _chamadoMotivoId) &&
                            (_chamadoStatusId == null || chs.chamadoStatusId == _chamadoStatusId) &&
                            (_situacao == "" || cham.situacao == _situacao)
                     orderby cham.dt_chamado descending
                     select new ChamadoViewModel
                     {
                         chamadoId = cham.chamadoId,
                         dt_chamado = cham.dt_chamado,
                         assunto = cham.assunto,
                         areaAtendimentoId = cham.areaAtendimentoId,
                         descricao_areaAtendimento = are.descricao,
                         chamadoStatusId = cham.chamadoStatusId,
                         descricao_status = chs.descricao,
                         chamadoMotivoId = cham.chamadoMotivoId,
                         descricao_motivo = chm.descricao,
                         associadoId = cham.associadoId,
                         nome_associado = ass.nome,
                         situacao = cham.situacao,
                         PageSize = pageSize,
                         TotalCount = (from cham1 in db.Chamados
                                       join are1 in db.AreaAtendimentos on cham1.AreaAtendimento equals are1
                                       join ass1 in db.Associados on cham1.Associado equals ass1
                                       join med1 in db.Medicos on ass1.associadoId equals med1.associadoId
                                       join chm1 in db.ChamadoMotivos on cham1.chamadoMotivoId equals chm1.chamadoMotivoId
                                       join chs1 in db.ChamadoStatuss on cham1.chamadoStatusId equals chs1.chamadoStatusId
                                       where ((cham1.usuarioId == sessaoCorrente.usuarioId) || (cham1.usuarioId == null && (are1.usuario1Id == sessaoCorrente.usuarioId || are1.usuario2Id == sessaoCorrente.usuarioId))) &&
                                              (_crm == null || med.CRM == _crm) &&
                                              (cham1.dt_chamado >= _data1 && cham1.dt_chamado <= _data2) &&
                                              (_areaAtendimentoId == null || cham1.areaAtendimentoId == _areaAtendimentoId) &&
                                              (_chamadoMotivoId == null || chm1.chamadoMotivoId == _chamadoMotivoId) &&
                                              (_chamadoStatusId == null || chs1.chamadoStatusId == _chamadoStatusId) &&
                                              (_situacao == "" || cham1.situacao == _situacao)
                                       select cham1).Count()
                     }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();

            return q;
        }

        public override Repository getRepository(Object id)
        {
            return new ChamadoModel().getObject((ChamadoViewModel)id);
        }
        public override string action()
        {
            return "ListChamados";
        }
        #endregion
    }

}
