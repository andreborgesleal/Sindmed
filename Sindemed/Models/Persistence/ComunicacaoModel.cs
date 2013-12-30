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
    public class ComunicacaoModel : CrudContext<Comunicacao, ComunicacaoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override Comunicacao MapToEntity(ComunicacaoViewModel value)
        {
            return new Comunicacao()
            {
                comunicacaoId = value.comunicacaoId,
                dt_comunicacao = value.dt_comunicacao,
                cabecalho = value.cabecalho,
                resumo = value.resumo,
                mensagemDetalhada = value.mensagemDetalhada
            };
        }

        public override ComunicacaoViewModel MapToRepository(Comunicacao entity)
        {
            return new ComunicacaoViewModel()
            {
                comunicacaoId = entity.comunicacaoId,
                dt_comunicacao = entity.dt_comunicacao,
                cabecalho = entity.cabecalho,
                resumo = entity.resumo,
                mensagemDetalhada = entity.mensagemDetalhada,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override Comunicacao Find(ComunicacaoViewModel key)
        {
            return db.Comunicacaos.Find(key.comunicacaoId);
        }

        public override Validate Validate(ComunicacaoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            return value.mensagem;
        }

        public override ComunicacaoViewModel CreateRepository()
        {
            return new ComunicacaoViewModel() { dt_comunicacao = DateTime.Today };
        }
        #endregion
    }

    public class ListViewComunicacao : ListViewRepository<ComunicacaoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<ComunicacaoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from com in db.Comunicacaos
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || com.cabecalho.StartsWith(_descricao.Trim()) || com.resumo.StartsWith(_descricao.Trim()) || com.mensagemDetalhada.StartsWith(_descricao.Trim()))
                    orderby com.dt_comunicacao descending
                    select new ComunicacaoViewModel
                    {
                        comunicacaoId = com.comunicacaoId,
                        dt_comunicacao = com.dt_comunicacao,
                        cabecalho = com.cabecalho,
                        resumo = com.resumo,
                        mensagemDetalhada = com.mensagemDetalhada,
                        PageSize = pageSize,
                        TotalCount = (from com1 in db.Comunicacaos
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || com1.cabecalho.StartsWith(_descricao.Trim()) || com1.resumo.StartsWith(_descricao.Trim()) || com1.mensagemDetalhada.StartsWith(_descricao.Trim()))
                                      select com1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new ComunicacaoModel().getObject((ComunicacaoViewModel)id);
        }
        #endregion
    }

    public class LookupComunicacaoModel : ListViewComunicacao
    {
        public override string action()
        {
            return "../ComunicadoGeral/ListComunicacaoModal";
        }
    }

    public class LookupComunicacaoFiltroModel : ListViewComunicacao
    {
        public override string action()
        {
            return "../ComunicadoGeral/_ListComunicacaoModal";
        }
    }

}