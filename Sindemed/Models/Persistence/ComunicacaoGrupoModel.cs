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
                cabecalho = db.Comunicacaos.Find(entity.comunicacaoId).cabecalho,
                grupoAssociadoId = entity.grupoAssociadoId,
                descricao = db.GrupoAssociados.Find(entity.grupoAssociadoId).descricao,
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

    

}