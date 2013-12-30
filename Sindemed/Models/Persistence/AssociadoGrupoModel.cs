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
    public class AssociadoGrupoModel : CrudContext<AssociadoGrupo, AssociadoGrupoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override AssociadoGrupo MapToEntity(  AssociadoGrupoViewModel value)
        {
            return new AssociadoGrupo()
            {
                associadoId = value.associadoId,
                grupoAssociadoId = value.grupoAssociadoId
            };
        }

        public override AssociadoGrupoViewModel MapToRepository(AssociadoGrupo entity)
        {
            return new AssociadoGrupoViewModel()
            {
                associadoId = entity.associadoId,
                grupoAssociadoId = entity.grupoAssociadoId,
                nome = (from ass in db.Associados where ass.associadoId == entity.associadoId select ass).Select(info => info.nome).FirstOrDefault() ?? "",
                descricao = (from ga in db.GrupoAssociados where ga.grupoAssociadoId == entity.grupoAssociadoId select ga).Select(info => info.descricao).FirstOrDefault() ?? "",
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override AssociadoGrupo Find(AssociadoGrupoViewModel key)
        {
            return db.AssociadoGrupos.Find(key.grupoAssociadoId, key.associadoId);
        }

        public override Validate Validate(AssociadoGrupoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.grupoAssociadoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Especialidade").ToString();
                value.mensagem.MessageBase = "Campo Especialidade Médica deve ser informada nos dados do profissional";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (value.associadoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Especialidade").ToString();
                value.mensagem.MessageBase = "Campo Especialidade Médica deve ser informada nos dados do profissional";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }


            return value.mensagem;
        }

        public override AssociadoGrupoViewModel CreateRepository()
        {
            return new AssociadoGrupoViewModel();
        }
        #endregion
    }

    public class ListViewAssociadoGrupo : ListViewRepository<AssociadoGrupoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<AssociadoGrupoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from ag in db.AssociadoGrupos join ass in db.Associados on ag.associadoId equals ass.associadoId
                    join g in db.GrupoAssociados on ag.grupoAssociadoId equals g.grupoAssociadoId
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || ass.nome.StartsWith(_descricao.Trim()) || g.descricao.StartsWith(_descricao.Trim()))
                    orderby g.descricao, ass.nome
                    select new AssociadoGrupoViewModel
                    {
                        grupoAssociadoId = g.grupoAssociadoId,
                        associadoId = ass.associadoId,
                        descricao = g.descricao,
                        nome = ass.nome,
                        PageSize = pageSize,
                        TotalCount = (from ag1 in db.AssociadoGrupos
                                      join ass1 in db.Associados on ag1.associadoId equals ass1.associadoId
                                      join g1 in db.GrupoAssociados on ag1.grupoAssociadoId equals g1.grupoAssociadoId
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || ass1.nome.StartsWith(_descricao.Trim()) || g1.descricao.StartsWith(_descricao.Trim()))
                                      select ag1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new CidadeModel().getObject((CidadeViewModel)id);
        }
        #endregion
    }
   

    public class LookupAssociadoGrupoModel : ListViewAssociadoGrupo
    {
        public override string action()
        {
            return "../AssociadoGrupo/ListAssociadoGrupoModal";
        }
    }

    public class LookupAssociadoGrupoFiltroModel : ListViewAssociadoGrupo
    {
        public override string action()
        {
            return "../AssociadoGrupo/_ListAssociadoGrupoModal";
        }
    }
}