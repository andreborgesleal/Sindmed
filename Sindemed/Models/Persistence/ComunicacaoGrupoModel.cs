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
using App_Dominio.Security;

namespace Sindemed.Models.Persistence
{
    public class ComunicacaoGrupoModel : CrudContext<ComunicacaoGrupo, ComunicacaoGrupoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override ComunicacaoGrupo MapToEntity(ComunicacaoGrupoViewModel value)
        {
            return new ComunicacaoGrupo()
            {
                comunicacaoId = value.comunicacaoId,
                grupoAssociadoId = value.grupoAssociadoId
            };
        }

        public override ComunicacaoGrupoViewModel MapToRepository(ComunicacaoGrupo entity)
        {
            return new ComunicacaoGrupoViewModel()
            {
                comunicacaoId = entity.comunicacaoId,
                grupoAssociadoId = entity.grupoAssociadoId,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override ComunicacaoGrupo Find(ComunicacaoGrupoViewModel key)
        {
            return db.ComunicacaoGrupos.Find(key.comunicacaoId, key.grupoAssociadoId);
        }

        public override Validate Validate(ComunicacaoGrupoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.grupoAssociadoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "grupoAssociadoId").ToString();
                value.mensagem.MessageBase = "Campo grupoAssociado deve ser informado nos dados do profissional";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (value.comunicacaoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "comunicacaoId").ToString();
                value.mensagem.MessageBase = "Campo comunicacao deve ser informado nos dados do profissional";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }


            return value.mensagem;
        }

        public override ComunicacaoGrupoViewModel CreateRepository()
        {
            return new ComunicacaoGrupoViewModel();
        }
        #endregion
    }

    public class ListViewComunicacaoGrupo : ListViewRepository<ComunicacaoGrupoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<ComunicacaoGrupoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from cg in db.ComunicacaoGrupos
                    join cm in db.Comunicacaos on cg.comunicacaoId equals cm.comunicacaoId
                    join g in db.GrupoAssociados on cg.grupoAssociadoId equals g.grupoAssociadoId
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || cm.cabecalho.StartsWith(_descricao.Trim()) || g.descricao.StartsWith(_descricao.Trim()))
                    orderby g.descricao, cm.cabecalho
                    select new ComunicacaoGrupoViewModel
                    {
                        grupoAssociadoId = g.grupoAssociadoId,
                        comunicacaoId = cm.comunicacaoId,
                        descricao = g.descricao,
                        cabecalho = cm.cabecalho,
                        PageSize = pageSize,
                        TotalCount = (from cg1 in db.ComunicacaoGrupos
                                      join cm1 in db.Comunicacaos on cg1.comunicacaoId equals cm1.comunicacaoId
                                      join g1 in db.GrupoAssociados on cg1.grupoAssociadoId equals g1.grupoAssociadoId
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || cm1.cabecalho.StartsWith(_descricao.Trim()) || g1.descricao.StartsWith(_descricao.Trim()))
                                      select cg1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new ComunicacaoGrupoModel().getObject((ComunicacaoGrupoViewModel)id);
        }
        #endregion
    }

    public class LookupComunicacaoGrupoModel : ListViewComunicacaoGrupo
    {
        public override string action()
        {
            return "../ComunicadoGeral/ListComunicacaoGrupoModal";
        }
    }

    public class LookupComunicacaoGrupoFiltroModel : ListViewComunicacaoGrupo
    {
        public override string action()
        {
            return "../ComunicadoGeral/_ListComunicacaoGrupoModal";
        }
    }

    public class ListViewComunicacaoGrupoEspecifico : ListViewRepository<ComunicacaoGrupoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<ComunicacaoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            int _usuarioId = security.getUsuario().usuarioId;

            return (from ass in db.Associados join asg in db.AssociadoGrupos on ass.associadoId equals asg.associadoId
                    join gas in db.GrupoAssociados on asg.grupoAssociadoId equals gas.grupoAssociadoId
                    join cog in db.ComunicacaoGrupos on gas.grupoAssociadoId equals cog.grupoAssociadoId
                    join com in db.Comunicacaos on cog.comunicacaoId equals com.comunicacaoId
                    where ass.usuarioId == _usuarioId
                    orderby com.dt_comunicacao descending
                    select new ComunicacaoViewModel
                    {
                        comunicacaoId = com.comunicacaoId,
                        dt_comunicacao = com.dt_comunicacao,
                        cabecalho = com.cabecalho,
                        resumo = com.resumo,
                        mensagemDetalhada = com.mensagemDetalhada,
                        PageSize = pageSize,
                        TotalCount = (from ass1 in db.Associados join asg1 in db.AssociadoGrupos on ass1.associadoId equals asg1.associadoId
                                      join gas1 in db.GrupoAssociados on asg1.grupoAssociadoId equals gas1.grupoAssociadoId
                                      join cog1 in db.ComunicacaoGrupos on gas1.grupoAssociadoId equals cog1.grupoAssociadoId
                                      join com1 in db.Comunicacaos on cog1.comunicacaoId equals com1.comunicacaoId
                                      where ass1.usuarioId == _usuarioId
                                      orderby com1.dt_comunicacao descending
                                      select com1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new ComunicacaoGrupoModel().getObject((ComunicacaoGrupoViewModel)id);
        }
        #endregion
    }

}