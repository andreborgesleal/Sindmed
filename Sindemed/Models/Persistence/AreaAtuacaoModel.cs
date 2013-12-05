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
    public class AreaAtuacaoModel : CrudContext<AreaAtuacao, AreaAtuacaoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override AreaAtuacao MapToEntity(AreaAtuacaoViewModel value)
        {
            return new AreaAtuacao()
            {
                areaAtuacaoId = value.areaAtuacaoId,
                descricao = value.descricao,
                codigo = value.codigo
            };
        }

        public override AreaAtuacaoViewModel MapToRepository(AreaAtuacao entity)
        {
            return new AreaAtuacaoViewModel()
            {
                areaAtuacaoId = entity.areaAtuacaoId,
                descricao = entity.descricao,
                codigo = entity.codigo,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override AreaAtuacao Find(AreaAtuacaoViewModel key)
        {
            return db.AreaAtuacaos.Find(key.areaAtuacaoId);
        }

        public override Validate Validate(AreaAtuacaoViewModel value, Crud operation)
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

        public override AreaAtuacaoViewModel CreateRepository()
        {
            return new AreaAtuacaoViewModel();
        }
        #endregion
    }

    public class ListViewAreaAtuacao : ListViewRepository<AreaAtuacaoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<AreaAtuacaoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from are in db.AreaAtuacaos
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || are.descricao.StartsWith(_descricao.Trim()))
                    orderby are.descricao
                    select new AreaAtuacaoViewModel
                    {
                        areaAtuacaoId = are.areaAtuacaoId,
                        descricao = are.descricao,
                        codigo = are.codigo,
                        PageSize = pageSize,
                        TotalCount = (from are1 in db.AreaAtuacaos
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || are1.descricao.StartsWith(_descricao.Trim()))
                                      select are1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new AreaAtuacaoModel().getObject((AreaAtuacaoViewModel)id);
        }
        #endregion
    }

    public class LookupAreaAtuacaoModel : ListViewAreaAtuacao
    {
        public override string action()
        {
            return "../AreaAtuacao/ListAreaAtuacaoModal";
        }
    }

    public class LookupAreaAtuacaoFiltroModel : ListViewAreaAtuacao
    {
        public override string action()
        {
            return "../AreaAtuacao/_ListAreaAtuacaoModal";
        }
    }

}