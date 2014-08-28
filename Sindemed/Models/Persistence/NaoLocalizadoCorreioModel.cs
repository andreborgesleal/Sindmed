using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using DWM.Models.Repositories;
using DWM.Models.Entidades;
using App_Dominio.Enumeracoes;

namespace DWM.Models.Persistence
{
    public class NaoLocalizadoCorreioModel : CrudContext<NaoLocalizadoCorreio, NaoLocalizadoCorreioViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override NaoLocalizadoCorreio MapToEntity(NaoLocalizadoCorreioViewModel value)
        {
            return new NaoLocalizadoCorreio()
            {
                correioId = value.correioId,
                descricao = value.descricao,
                codigo = value.codigo
            };
        }

        public override NaoLocalizadoCorreioViewModel MapToRepository(NaoLocalizadoCorreio entity)
        {
            return new NaoLocalizadoCorreioViewModel()
            {
                correioId = entity.correioId,
                descricao = entity.descricao,
                codigo = entity.codigo,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override NaoLocalizadoCorreio Find(NaoLocalizadoCorreioViewModel key)
        {
            return db.NaoLocalizadoCorreios.Find(key.correioId);
        }

        public override Validate Validate(NaoLocalizadoCorreioViewModel value, Crud operation)
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

        #endregion
    }

    public class ListViewNaoLocalizadoCorreio : ListViewRepository<NaoLocalizadoCorreioViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<NaoLocalizadoCorreioViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from cor in db.NaoLocalizadoCorreios
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || cor.descricao.StartsWith(_descricao.Trim()))
                    orderby cor.descricao
                    select new NaoLocalizadoCorreioViewModel
                    {
                        correioId = cor.correioId,
                        descricao = cor.descricao,
                        codigo = cor.codigo,
                        PageSize = pageSize,
                        TotalCount = (from cor1 in db.NaoLocalizadoCorreios
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || cor1.descricao.StartsWith(_descricao.Trim()))
                                      select cor1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new NaoLocalizadoCorreioModel().getObject((NaoLocalizadoCorreioViewModel)id);
        }
        #endregion
    }

    public class LookupNaoLocalizadoCorreioModel : ListViewNaoLocalizadoCorreio
    {
        public override string action()
        {
            return "../NaoLocalizadoCorreio/ListNaoLocalizadoCorreioModal";
        }
    }

    public class LookupNaoLocalizadoCorreioFiltroModel : ListViewNaoLocalizadoCorreio
    {
        public override string action()
        {
            return "../NaoLocalizadoCorreio/_ListNaoLocalizadoCorreioModal";
        }
    }



}