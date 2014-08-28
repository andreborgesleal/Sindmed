using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using DWM.Models.Repositories;
using DWM.Models.Entidades;
using App_Dominio.Enumeracoes;
using System.Data.Entity.SqlServer;

namespace DWM.Models.Persistence
{
    public class ProprietarioModel : CrudContext<Proprietario, ProprietarioViewModel, ApplicationContext>
    {
        public  ProprietarioModel() : base()
        {

        }

        public ProprietarioModel(ApplicationContext _db)
        {
            this.db = _db;
        }


        #region Métodos da classe CrudContext
        public override Proprietario MapToEntity(ProprietarioViewModel value)
        {
            return new Proprietario()
            {
                proprietarioId = value.proprietarioId,
                torreId = value.torreId,
                unidadeId = value.unidadeId,
                dt_inicio = value.dt_inicio,
                dt_fim = value.dt_fim,
                nome = value.nome.ToUpper(),
                ind_tipo_pessoa = value.ind_tipo_pessoa,
                cpf_cnpj = value.cpf_cnpj,
                email = value.email != null ? value.email.ToLower() : value.email,
                ind_est_civil = value.ind_est_civil,
                tel_contato1 = value.tel_contato1 != null ? value.tel_contato1.Replace("(","").Replace(")","").Replace("-","").Replace(" ","") : value.tel_contato1,
                tel_contato2 = value.tel_contato2 != null ? value.tel_contato2.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : value.tel_contato2,
                nome_conjuge = value.nome_conjuge != null ? value.nome_conjuge.ToUpper() : value.nome_conjuge,
                cpf_cnpj_conjuge = value.cpf_cnpj_conjuge != null ? value.cpf_cnpj_conjuge.Replace("-", "").Replace(".", "") : value.cpf_cnpj_conjuge,
                endereco = value.endereco,
                complemento = value.complemento,
                cidadeId = value.cidadeId,
                cep = value.cep != null ? value.cep.Replace("-", "") : value.cep,
                uf = value.uf != null ? value.uf.ToUpper() : ""
            };
        }

        public override ProprietarioViewModel MapToRepository(Proprietario entity)
        {
            return new ProprietarioViewModel()
            {
                proprietarioId = entity.proprietarioId,
                torreId = entity.torreId,
                unidadeId = entity.unidadeId,
                dt_inicio = entity.dt_inicio,
                dt_fim = entity.dt_fim,
                nome = entity.nome,
                ind_tipo_pessoa = entity.ind_tipo_pessoa,
                cpf_cnpj = entity.cpf_cnpj,
                email = entity.email,
                ind_est_civil = entity.ind_est_civil,
                tel_contato1 = entity.tel_contato1,
                tel_contato2 = entity.tel_contato2,
                nome_conjuge = entity.nome_conjuge,
                cpf_cnpj_conjuge = entity.cpf_cnpj_conjuge,
                endereco = entity.endereco,
                complemento = entity.complemento,
                cidadeId = entity.cidadeId,
                cep = entity.cep,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override Proprietario Find(ProprietarioViewModel key)
        {
            return db.Proprietarios.Find(key.proprietarioId);
        }

        public override Validate Validate(ProprietarioViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            #region Verifica se já existe um proprietário para a respectiva torre
            var prop = from p in db.Proprietarios 
                       where p.torreId == value.torreId && p.unidadeId == value.unidadeId && p.dt_fim == null select p;

            if (prop != null && prop.Count() > 0)
            {
                value.mensagem.Code = 19;
                value.mensagem.Message = MensagemPadrao.Message(19).ToString();
                value.mensagem.MessageBase = "Já existe um proprietário cadastrado para esta torre e unidade. Favor desativar o proprietário anterior para ativar este novo proprietário.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }
            #endregion

            #region Verifica se a data início é maior que a data fim
            if (value.dt_fim != null && value.dt_fim < value.dt_inicio)
            {
                value.mensagem.Code = 12;
                value.mensagem.Message = MensagemPadrao.Message(12, "Dt.Desativação", "Dt.Cadastro").ToString();
                value.mensagem.MessageBase = "Dt.Cadastro e Dt.Desativação incorretos";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }
            #endregion

            return value.mensagem;
        }

        #endregion

    }

    public class ListViewProprietario : ListViewRepository<ProprietarioViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<ProprietarioViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from prop in db.Proprietarios
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || prop.nome.StartsWith(_descricao.Trim()) || prop.torreId == _descricao || SqlFunctions.StringConvert((decimal)prop.unidadeId).Contains(_descricao) )
                    orderby prop.torreId, prop.unidadeId
                    select new ProprietarioViewModel
                    {
                        proprietarioId = prop.proprietarioId,
                        torreId = prop.torreId,
                        unidadeId = prop.unidadeId,
                        dt_inicio = prop.dt_inicio,
                        dt_fim = prop.dt_fim,
                        nome = prop.nome,
                        ind_tipo_pessoa = prop.ind_tipo_pessoa,
                        cpf_cnpj = prop.cpf_cnpj,
                        email = prop.email,
                        ind_est_civil = prop.ind_est_civil,
                        tel_contato1 = prop.tel_contato1,
                        tel_contato2 = prop.tel_contato2,
                        nome_conjuge = prop.nome_conjuge,
                        cpf_cnpj_conjuge = prop.cpf_cnpj_conjuge,
                        endereco = prop.endereco,
                        complemento = prop.complemento,
                        cidadeId = prop.cidadeId,
                        cep = prop.cep,
                        PageSize = pageSize,
                        TotalCount = (from prop1 in db.Proprietarios
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || prop1.nome.StartsWith(_descricao.Trim()) || prop1.torreId == _descricao || SqlFunctions.StringConvert((decimal)prop1.unidadeId).Contains(_descricao))
                                      orderby prop.torreId, prop.unidadeId
                                      select prop1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new ProprietarioModel().getObject((ProprietarioViewModel)id);
        }
        #endregion
    }

    public class LookupProprietarioModel : ListViewProprietario
    {
        public override string action()
        {
            return "../Proprietario/ListProprietarioModal";
        }
    }

    public class LookupProprietarioFiltroModel : ListViewProprietario
    {
        public override string action()
        {
            return "../Proprietario/_ListProprietarioModal";
        }
    }
}

