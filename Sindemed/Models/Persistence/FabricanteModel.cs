using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using StockLite.Models.Repositories;
using StockLite.Models.Entidades;
using App_Dominio.Enumeracoes;

namespace StockLite.Models.Persistence
{
    public class FabricanteModel : CrudContext<Fabricante, FabricanteViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override Fabricante MapToEntity(FabricanteViewModel value)
        {
            return new Fabricante()
            {
                fabricanteId = value.fabricanteId,
                nome = value.nome
            };
        }

        public override FabricanteViewModel MapToRepository(Fabricante entity)
        {
            return new FabricanteViewModel()
            {
                fabricanteId = entity.fabricanteId,
                nome = entity.nome,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override Fabricante Find(FabricanteViewModel key)
        {
            using (db = new ApplicationContext())
            {
                return db.Fabricantes.Find(key.fabricanteId);
            }            
        }

        public override Validate Validate(FabricanteViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.nome.Trim().Length == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Nome do Fabricante").ToString();
                value.mensagem.MessageBase = "Campo Nome do Fabricante deve ser informado";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }

        public override FabricanteViewModel CreateRepository()
        {
            return new FabricanteViewModel();
        }
        #endregion
    }

    public class ListViewFabricante : ListViewRepository<FabricanteViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<FabricanteViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {           
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from fab in db.Fabricantes 
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || fab.nome.StartsWith(_descricao.Trim()))
                    orderby fab.nome
                    select new FabricanteViewModel
                    {
                        fabricanteId = fab.fabricanteId,
                        nome = fab.nome,
                        PageSize = pageSize,
                        TotalCount = (from fab1 in db.Fabricantes
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || fab1.nome.StartsWith(_descricao.Trim()))
                                      select fab1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new FabricanteModel().getObject((FabricanteViewModel)id);
        }
        #endregion
    }

    public class LookupFabricanteModel : ListViewFabricante
    {
        public override string action()
        {
            return "../Fabricante/ListFabricanteModal";
        }
    }

    public class LookupFabricanteFiltroModel : ListViewFabricante
    {
        public override string action()
        {
            return "../Fabricante/_ListFabricanteModal";
        }
    }
}