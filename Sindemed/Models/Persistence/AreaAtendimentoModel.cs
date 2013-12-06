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
    public class AreaAtendimentoModel : CrudContext<AreaAtendimento, AreaAtendimentoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override AreaAtendimento MapToEntity(AreaAtendimentoViewModel value)
        {
            return new AreaAtendimento()
            {
                areaAtendimentoId = value.areaAtendimentoId,
                descricao = value.descricao,
                codigo = value.codigo,
                usuario1Id = value.usuario1Id,
                usuario2Id = value.usuario2Id
            };
        }

        public override AreaAtendimentoViewModel MapToRepository(AreaAtendimento entity)
        {
            return new AreaAtendimentoViewModel()
            {
                areaAtendimentoId = entity.areaAtendimentoId,
                descricao = entity.descricao,
                codigo = entity.codigo,
                usuario1Id = entity.usuario1Id,
                usuario2Id = entity.usuario2Id,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override AreaAtendimento Find(AreaAtendimentoViewModel key)
        {
            return db.AreaAtendimentos.Find(key.areaAtendimentoId);
        }

        public override Validate Validate(AreaAtendimentoViewModel value, Crud operation)
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
            else if (value.usuario1Id == null || value.usuario1Id == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Atendendte 1").ToString();
                value.mensagem.MessageBase = "Campo Atendente 1 deve ser informado";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }

        public override AreaAtendimentoViewModel CreateRepository()
        {
            return new AreaAtendimentoViewModel();
        }
        #endregion
    }

    public class ListViewAreaAtendimento : ListViewRepository<AreaAtendimentoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<AreaAtendimentoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from are in db.AreaAtendimentos
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || are.descricao.StartsWith(_descricao.Trim()))
                    orderby are.descricao
                    select new AreaAtendimentoViewModel
                    {
                        areaAtendimentoId = are.areaAtendimentoId,
                        descricao = are.descricao,
                        codigo = are.codigo,
                        usuario1Id = are.usuario1Id,
                        usuario2Id = are.usuario2Id,
                        PageSize = pageSize,
                        TotalCount = (from are1 in db.AreaAtendimentos
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || are1.descricao.StartsWith(_descricao.Trim()))
                                      select are1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new AreaAtendimentoModel().getObject((AreaAtendimentoViewModel)id);
        }
        #endregion
    }

    public class LookupAreaAtendimentoModel : ListViewAreaAtendimento
    {
        public override string action()
        {
            return "../AreaAtendimento/ListAreaAtendimentoModal";
        }
    }

    public class LookupAreaAtendimentoFiltroModel : ListViewAreaAtendimento
    {
        public override string action()
        {
            return "../AreaAtendimento/_ListAreaAtendimentoModal";
        }
    }

}