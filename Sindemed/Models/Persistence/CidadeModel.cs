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
    public class CidadeModel : CrudContext<Cidade, CidadeViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override Cidade MapToEntity(CidadeViewModel value)
        {
            return new Cidade()
            {
                cidadeId = value.cidadeId,
                nome = value.nome,
                uf = value.uf.ToUpper()
            };
        }

        public override CidadeViewModel MapToRepository(Cidade entity)
        {
            return new CidadeViewModel()
            {
                cidadeId = entity.cidadeId,
                nome = entity.nome,
                uf = entity.uf,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override Cidade Find(CidadeViewModel key)
        {
            return db.Cidades.Find(key.cidadeId);
        }

        public override Validate Validate(CidadeViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.nome.Trim().Length == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Nome da Cidade").ToString();
                value.mensagem.MessageBase = "Campo Cidade deve ser informado";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }

        public override CidadeViewModel CreateRepository()
        {
            return new CidadeViewModel();
        }
        #endregion
    }

    public class ListViewCidade : ListViewRepository<CidadeViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<CidadeViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from cid in db.Cidades
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || cid.nome.StartsWith(_descricao.Trim()))
                    orderby cid.nome
                    select new CidadeViewModel
                    {
                        cidadeId = cid.cidadeId,
                        nome = cid.nome,
                        uf = cid.uf,
                        PageSize = pageSize,
                        TotalCount = (from cid1 in db.Cidades
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || cid1.nome.StartsWith(_descricao.Trim()))
                                      select cid1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new CidadeModel().getObject((CidadeViewModel)id);
        }
        #endregion
    }

    public class LookupCidadeModel : ListViewCidade
    {
        public override string action()
        {
            return "../Cidade/ListCidadeModal";
        }
    }

    public class LookupCidadeFiltroModel : ListViewCidade
    {
        public override string action()
        {
            return "../Cidade/_ListCidadeModal";
        }
    }
}