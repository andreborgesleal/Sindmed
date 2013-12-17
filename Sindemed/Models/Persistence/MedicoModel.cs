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

namespace Sindemed.Models.Persistence
{
    public class MedicoModel : CrudContext<Medico, MedicoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override Medico MapToEntity(MedicoViewModel value)
        {
            Medico medico = new Medico()
            {
                associadoId = value.associadoId,
                nome = value.nome.ToUpper(),
                dt_nascimento = value.dt_nascimento,
                cpf = value.cpf.Replace("-","").Replace(".",""),
                rg = value.rg,
                orgaoEmissor = value.orgaoEmissor,
                sexo = value.sexo,
                situacao = value.situacao,
                endereco = value.endereco,
                complementoEnd = value.complementoEnd,
                cep = value.cep.Replace("-",""),
                cidadeId = value.cidadeId,
                uf = value.uf,
                bairro = value.bairro,
                enderecoCom = value.enderecoCom,
                complementoEndCom = value.complementoEndCom,
                cepCom = value.cepCom.Replace("-",""),
                cidadeComId = value.cidadeComId,
                ufCom = value.ufCom,
                bairroCom = value.bairroCom,
                telParticular1 = value.telParticular1.Replace("-","").Replace(".","").Replace(" ",""),
                telParticular2 = value.telParticular2.Replace("-", "").Replace(".", "").Replace(" ", ""),
                telParticular3 = value.telParticular3.Replace("-", "").Replace(".", "").Replace(" ", ""),
                telParticular4 = value.telParticular4.Replace("-", "").Replace(".", "").Replace(" ", ""),
                telCom1 = value.telCom1.Replace("-", "").Replace(".", "").Replace(" ", ""),
                telCom2 = value.telCom2.Replace("-", "").Replace(".", "").Replace(" ", ""),
                fax = value.fax.Replace("-", "").Replace(".", "").Replace(" ", ""),
                isSindicalizado = value.isSindicalizado,
                dt_admin_sindicato = value.dt_admin_sindicato,
                correioId = value.correioId,
                areaAtuacao1Id = value.areaAtuacao1Id,
                areaAtuacao2Id = value.areaAtuacao2Id,
                areaAtuacao3Id = value.areaAtuacao3Id,
                email1 = value.email1,
                email3 = value.email2,
                email2 = value.email3,
                usuarioId = value.usuarioId,
                observacao = value.observacao,
                ufCRM = value.ufCRM,
                CRM = value.CRM,
                ufCRM_Seg = value.ufCRM_Seg,
                CRM_Seg = value.CRM_Seg,
                especialidade1Id = value.especialidade1Id,
                especialidade2Id = value.especialidade2Id,
                especialidade3Id = value.especialidade3Id
            };

            return medico;
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
                situacao = entity.situacao,
                sexo = entity.sexo,
                endereco = entity.endereco,
                complementoEnd = entity.complementoEnd,
                cep = entity.cep,
                uf = entity.uf,
                bairro = entity.bairro,
                enderecoCom = entity.enderecoCom,
                complementoEndCom = entity.complementoEndCom,
                cepCom = entity.cepCom,
                cidadeComId = entity.cidadeComId,
                ufCom = entity.ufCom,
                bairroCom = entity.bairroCom,
                telParticular1 = entity.telParticular1,
                telParticular2 = entity.telParticular2,
                telParticular3 = entity.telParticular3,
                telParticular4 = entity.telParticular4,
                telCom1 = entity.telCom1,
                telCom2 = entity.telCom2,
                fax = entity.fax,
                isSindicalizado = entity.isSindicalizado,
                dt_admin_sindicato = entity.dt_admin_sindicato,
                correioId = entity.correioId,
                areaAtuacao1Id = entity.areaAtuacao1Id,
                areaAtuacao2Id = entity.areaAtuacao2Id,
                areaAtuacao3Id = entity.areaAtuacao3Id,
                email1 = entity.email1,
                email3 = entity.email2,
                email2 = entity.email3,
                usuarioId = entity.usuarioId,
                observacao = entity.observacao,
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
            return (from socio in db.Medicos // db.Associados join med in db.Medicos on socio.associadoId equals med.associadoId
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || socio.nome.StartsWith(_descricao.Trim()) || socio.CRM == _descricao || socio.CRM_Seg == _descricao)
                    orderby socio.nome
                    select new MedicoViewModel
                    {
                        //codigo = SqlFunctions.StringConvert((decimal)med.associadoId),
                        codigo = socio.associadoId,
                        nome = socio.nome,
                        dt_nascimento = socio.dt_nascimento,
                        cpf = socio.cpf,
                        rg = socio.rg,
                        sexo = socio.sexo,
                        situacao = socio.situacao,
                        orgaoEmissor = socio.orgaoEmissor,
                        endereco = socio.endereco,
                        complementoEnd = socio.complementoEnd,
                        cep = socio.cep,
                        uf = socio.uf,
                        bairro = socio.bairro,
                        enderecoCom = socio.enderecoCom,
                        complementoEndCom = socio.complementoEndCom,
                        cepCom = socio.cepCom,
                        cidadeComId = socio.cidadeComId,
                        ufCom = socio.ufCom,
                        bairroCom = socio.bairroCom,
                        telParticular1 = socio.telParticular1,
                        telParticular2 = socio.telParticular2,
                        telParticular3 = socio.telParticular3,
                        telParticular4 = socio.telParticular4,
                        telCom1 = socio.telCom1,
                        telCom2 = socio.telCom2,
                        fax = socio.fax,
                        isSindicalizado = socio.isSindicalizado,
                        dt_admin_sindicato = socio.dt_admin_sindicato,
                        correioId = socio.correioId,
                        areaAtuacao1Id = socio.areaAtuacao1Id,
                        areaAtuacao2Id = socio.areaAtuacao2Id,
                        areaAtuacao3Id = socio.areaAtuacao3Id,
                        email1 = socio.email1,
                        email3 = socio.email2,
                        email2 = socio.email3,
                        usuarioId = socio.usuarioId,
                        ufCRM = socio.ufCRM,
                        CRM = socio.CRM,
                        ufCRM_Seg = socio.ufCRM_Seg,
                        CRM_Seg = socio.CRM_Seg,
                        especialidade1Id = socio.especialidade1Id,
                        especialidade2Id = socio.especialidade2Id,
                        especialidade3Id = socio.especialidade3Id,
                        PageSize = pageSize,
                        TotalCount = (from socio1 in db.Medicos
                                      //join med1 in db.Medicos on socio.associadoId equals med.associadoId
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || socio1.nome.StartsWith(_descricao.Trim()) || socio1.CRM == _descricao || socio1.CRM_Seg == _descricao)
                                      select socio1).Count()
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