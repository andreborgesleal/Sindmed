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
    public class MedicoModel : CrudContext<Medico, MedicoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override Medico MapToEntity(MedicoViewModel value)
        {
            return new Medico()
            {
                associadoId = value.associadoId,
                nome = value.nome,
                dt_nascimento = value.dt_nascimento,
                cpf = value.cpf,
                rg = value.rg,
                orgaoEmissor = value.orgaoEmissor,
                endereco = value.endereco,
                complementoEnd = value.complementoEnd,
                cep = value.cep,
                uf = value.uf,
                enderecoCom = value.enderecoCom,
                complementoEndCom = value.complementoEndCom,
                cepCom = value.cepCom,
                cidadeComId = value.cidadeComId,
                ufCom = value.ufCom,
                telParticular1 = value.telParticular1,
                telParticular2 = value.telParticular2,
                telParticular3 = value.telParticular3,
                telParticular4 = value.telParticular4,
                telCom1 = value.telCom1,
                telCom2 = value.telCom2,
                fax = value.fax,
                isSindicalizado = value.isSindicalizado,
                correioId = value.correioId,
                areaAtuacao1Id = value.areaAtuacao1Id,
                areaAtuacao2Id = value.areaAtuacao2Id,
                areaAtuacao3Id = value.areaAtuacao3Id,
                email1 = value.email1,
                email3 = value.email2,
                email2 = value.email3,
                usuarioId = value.usuarioId,
                ufCRM = value.ufCRM,
                CRM = value.CRM,
                ufCRM_Seg = value.ufCRM_Seg,
                CRM_Seg = value.CRM_Seg,
                especialidade1Id = value.especialidade1Id,
                especialidade2Id = value.especialidade2Id,
                especialidade3Id = value.especialidade3Id
            };
        }

        public override MedicoViewModel MapToRepository(Medico entity)
        {
            return new MedicoViewModel()
            {
                associadoId = entity.associadoId,
                nome = entity.nome,
                dt_nascimento = entity.dt_nascimento,
                cpf = entity.cpf,
                rg = entity.rg,
                orgaoEmissor = entity.orgaoEmissor,
                endereco = entity.endereco,
                complementoEnd = entity.complementoEnd,
                cep = entity.cep,
                uf = entity.uf,
                enderecoCom = entity.enderecoCom,
                complementoEndCom = entity.complementoEndCom,
                cepCom = entity.cepCom,
                cidadeComId = entity.cidadeComId,
                ufCom = entity.ufCom,
                telParticular1 = entity.telParticular1,
                telParticular2 = entity.telParticular2,
                telParticular3 = entity.telParticular3,
                telParticular4 = entity.telParticular4,
                telCom1 = entity.telCom1,
                telCom2 = entity.telCom2,
                fax = entity.fax,
                isSindicalizado = entity.isSindicalizado,
                correioId = entity.correioId,
                areaAtuacao1Id = entity.areaAtuacao1Id,
                areaAtuacao2Id = entity.areaAtuacao2Id,
                areaAtuacao3Id = entity.areaAtuacao3Id,
                email1 = entity.email1,
                email3 = entity.email2,
                email2 = entity.email3,
                usuarioId = entity.usuarioId,
                ufCRM = entity.ufCRM,
                CRM = entity.CRM,
                ufCRM_Seg = entity.ufCRM_Seg,
                CRM_Seg = entity.CRM_Seg,
                especialidade1Id = entity.especialidade1Id,
                especialidade2Id = entity.especialidade2Id,
                especialidade3Id = entity.especialidade3Id,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override Medico Find(MedicoViewModel key)
        {
            return db.Medicos.Find(key.associadoId);
        }

        public override Validate Validate(MedicoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            //if (value.nome.Trim().Length == 0)
            //{
            //    value.mensagem.Code = 5;
            //    value.mensagem.Message = MensagemPadrao.Message(5, "Nome da Medico").ToString();
            //    value.mensagem.MessageBase = "Campo Medico deve ser informado";
            //    value.mensagem.MessageType = MsgType.WARNING;
            //    return value.mensagem;
            //}

            return value.mensagem;
        }

        public override MedicoViewModel CreateRepository()
        {
            return new MedicoViewModel();
        }
        #endregion
    }

    public class ListViewMedico : ListViewRepository<MedicoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<MedicoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from med in db.Medicos
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || med.nome.StartsWith(_descricao.Trim()) || med.CRM == _descricao || med.CRM_Seg == _descricao)
                    orderby med.nome
                    select new MedicoViewModel
                    {
                        associadoId = med.associadoId,
                        nome = med.nome,
                        dt_nascimento = med.dt_nascimento,
                        cpf = med.cpf,
                        rg = med.rg,
                        orgaoEmissor = med.orgaoEmissor,
                        endereco = med.endereco,
                        complementoEnd = med.complementoEnd,
                        cep = med.cep,
                        uf = med.uf,
                        enderecoCom = med.enderecoCom,
                        complementoEndCom = med.complementoEndCom,
                        cepCom = med.cepCom,
                        cidadeComId = med.cidadeComId,
                        ufCom = med.ufCom,
                        telParticular1 = med.telParticular1,
                        telParticular2 = med.telParticular2,
                        telParticular3 = med.telParticular3,
                        telParticular4 = med.telParticular4,
                        telCom1 = med.telCom1,
                        telCom2 = med.telCom2,
                        fax = med.fax,
                        isSindicalizado = med.isSindicalizado,
                        correioId = med.correioId,
                        areaAtuacao1Id = med.areaAtuacao1Id,
                        areaAtuacao2Id = med.areaAtuacao2Id,
                        areaAtuacao3Id = med.areaAtuacao3Id,
                        email1 = med.email1,
                        email3 = med.email2,
                        email2 = med.email3,
                        usuarioId = med.usuarioId,
                        ufCRM = med.ufCRM,
                        CRM = med.CRM,
                        ufCRM_Seg = med.ufCRM_Seg,
                        CRM_Seg = med.CRM_Seg,
                        especialidade1Id = med.especialidade1Id,
                        especialidade2Id = med.especialidade2Id,
                        especialidade3Id = med.especialidade3Id,
                        PageSize = pageSize,
                        TotalCount = (from med1 in db.Medicos
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || med1.nome.StartsWith(_descricao.Trim()) || med1.CRM == _descricao || med1.CRM_Seg == _descricao)
                                      select med1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new MedicoModel().getObject((MedicoViewModel)id);
        }
        #endregion
    }

    public class LookupMedicoModel : ListViewMedico
    {
        public override string action()
        {
            return "../Medico/ListMedicoModal";
        }
    }

    public class LookupMedicoFiltroModel : ListViewMedico
    {
        public override string action()
        {
            return "../Medico/_ListMedicoModal";
        }
    }
}