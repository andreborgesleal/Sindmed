using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using Sindemed.Models.Repositories;
using Sindemed.Models.Entidades;
using App_Dominio.Enumeracoes;

namespace Sindemed.Models.Persistence
{
    public class GrupoAssociadoModel : CrudContext<GrupoAssociado, GrupoAssociadoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override GrupoAssociado MapToEntity(GrupoAssociadoViewModel value)
        {
            return new GrupoAssociado()
            {
                grupoAssociadoId = value.grupoAssociadoId,
                descricao = value.descricao,
                objetivo = value.objetivo
            };
        }

        public override GrupoAssociadoViewModel MapToRepository(GrupoAssociado entity)
        {
            return new GrupoAssociadoViewModel()
            {
                grupoAssociadoId = entity.grupoAssociadoId,
                descricao = entity.descricao,
                objetivo = entity.objetivo,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override GrupoAssociado Find(GrupoAssociadoViewModel key)
        {
            return db.GrupoAssociados.Find(key.grupoAssociadoId);
        }

        public override Validate Validate(GrupoAssociadoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.descricao.Trim().Length == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Descrição").ToString();
                value.mensagem.MessageBase = "Campo Descrição deve ser informado";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }

        public override GrupoAssociadoViewModel CreateRepository()
        {
            return new GrupoAssociadoViewModel();
        }
        #endregion
    }

    public class ListViewGrupoAssociado : ListViewRepository<GrupoAssociadoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<GrupoAssociadoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from gru in db.GrupoAssociados
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || gru.descricao.StartsWith(_descricao.Trim()))
                    orderby gru.descricao
                    select new GrupoAssociadoViewModel
                    {
                        grupoAssociadoId = gru.grupoAssociadoId,
                        descricao = gru.descricao,
                        objetivo = gru.objetivo,
                        PageSize = pageSize,
                        TotalCount = (from gru1 in db.GrupoAssociados
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || gru1.descricao.StartsWith(_descricao.Trim()))
                                      select gru1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new GrupoAssociadoModel().getObject((GrupoAssociadoViewModel)id);
        }
        #endregion
    }

    public class LookupGrupoAssociadoModel : ListViewGrupoAssociado
    {
        public override string action()
        {
            return "../GrupoMedico/ListGrupoAssociadoModal";
        }
    }

    public class LookupGrupoAssociadoFiltroModel : ListViewGrupoAssociado
    {
        public override string action()
        {
            return "../GrupoMedico/_ListGrupoAssociadoModal";
        }
    }

}