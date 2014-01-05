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

namespace Sindemed.Models.Persistence
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
        public override Chamado ExecProcess(ChamadoViewModel value)
        {
            Chamado chamado = MapToEntity(value);
            this.db.Set<Chamado>().Add(chamado);
            return chamado;
        }

        public override Validate AfterInsert(ChamadoViewModel value)
        {
            EmpresaSecurity<SecurityContext> empresaSecurity = new EmpresaSecurity<SecurityContext>();
            #region Alerta 1
            AlertaRepository alerta = new AlertaRepository()
            {
                usuarioId = (from al in db.AreaAtendimentos where al.areaAtendimentoId == value.areaAtendimentoId select al.usuario1Id).First(),
                dt_emissao = DateTime.Now,
                linkText = "<span class=\"label label-warning\">Atendimento</span>",
                url = "../Atendimento/Create?chamadoId=" + value.chamadoId.ToString() + "&fluxo=2",
                mensagemAlerta = "<b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "h</b><p>" + value.assunto + "</p>"
            };

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
                    dt_emissao = DateTime.Now,
                    linkText = "<span class=\"label label-warning\">Atendimento</span>",
                    url = "../Atendimento/Create?chamadoId=" + value.chamadoId.ToString() + "&fluxo=2",
                    mensagemAlerta = "<b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "h</b><p>" + value.assunto + "</p>"
                };

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
            using (SecurityContext seg = new SecurityContext())
                return new ChamadoViewModel()
                {
                    chamadoId = entity.chamadoId,
                    associadoId = entity.associadoId,
                    nome_associado = db.Associados.Find(entity.associadoId).nome,
                    areaAtendimentoId = entity.areaAtendimentoId,
                    descricao_areaAtendimento = db.AreaAtendimentos.Find(entity.areaAtendimentoId).descricao,
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

            return value.mensagem;
        }

        public override ChamadoViewModel CreateRepository(ChamadoViewModel value = null)
        {
            EmpresaSecurity<SecurityContext> empresaSecurity = new EmpresaSecurity<SecurityContext>();
            int usuarioId = empresaSecurity.getSessaoCorrente().usuarioId;

            using (db = new ApplicationContext())
            {
                return new ChamadoViewModel()
                {
                    situacao = "A",
                    dt_chamado = DateTime.Now,
                    associadoId = (from Ass in db.Associados where Ass.usuarioId == usuarioId select Ass.associadoId).FirstOrDefault(),
                    nome_associado = (from Ass in db.Associados where Ass.usuarioId == usuarioId select Ass.nome).FirstOrDefault()
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
            int? _associadoId = null;
            int? _areaAtendimentoId = null;
            string _situacao = "";

            _chamadoId = param[0] != null ? (int)param[0] : _chamadoId;
            _associadoId = param[1] != null ? (int)param[1] : _associadoId;
            
            DateTime _data1 = (DateTime)param[2];
            DateTime _data2 = (DateTime)param[3];

            _areaAtendimentoId = param[4] != null ? (int)param[4] : _areaAtendimentoId;
            _situacao = param[5] != null ? (string)param[5] : _situacao; //""-Todos, "A"-Chamados abertos ou "F"-Chamados fechados
            #endregion

            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();

            IEnumerable<ChamadoViewModel> q = null;

            if (_chamadoId != null)
                q = (from cham in db.Chamados
                     join are in db.AreaAtendimentos on cham.AreaAtendimento equals are
                     join ass in db.Associados on cham.Associado equals ass
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
                     where ((cham.usuarioId == sessaoCorrente.usuarioId) || (cham.usuarioId == null && (are.usuario1Id == sessaoCorrente.usuarioId || are.usuario2Id == sessaoCorrente.usuarioId))) &&
                            (_associadoId == null || cham.associadoId == _associadoId) &&
                            (cham.dt_chamado >= _data1 && cham.dt_chamado <= _data2) &&
                            (_areaAtendimentoId == null || cham.areaAtendimentoId == _areaAtendimentoId) &&
                            (_situacao == "" || cham.situacao == _situacao)
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
                         situacao = cham.situacao,
                         PageSize = pageSize,
                         TotalCount = (from cham1 in db.Chamados
                                       join are1 in db.AreaAtendimentos on cham1.AreaAtendimento equals are1
                                       join ass1 in db.Associados on cham1.Associado equals ass1
                                       where ((cham1.usuarioId == sessaoCorrente.usuarioId) || (cham1.usuarioId == null && (are1.usuario1Id == sessaoCorrente.usuarioId || are1.usuario2Id == sessaoCorrente.usuarioId))) &&
                                              (_associadoId == null || cham1.associadoId == _associadoId) &&
                                              (cham1.dt_chamado >= _data1 && cham1.dt_chamado <= _data2) &&
                                              (_areaAtendimentoId == null || cham1.areaAtendimentoId == _areaAtendimentoId) &&
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
