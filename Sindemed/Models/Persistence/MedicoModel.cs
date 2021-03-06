﻿using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using DWM.Models.Repositories;
using DWM.Models.Entidades;
using App_Dominio.Enumeracoes;
using System.Data.Entity.SqlServer;
using App_Dominio.Models;
using App_Dominio.Security;
using App_Dominio.Repositories;

namespace DWM.Models.Persistence
{
    public class MedicoModel : CrudContext<Medico, MedicoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override Medico MapToEntity(MedicoViewModel value)
        {
            DateTime? dt_admissao = null;
            if (value.isSindicalizado == "S")
                dt_admissao = value.dt_admin_sindicato;

            Medico medico = new Medico()
            {
                associadoId = value.associadoId,
                nome = value.nome.ToUpper(),
                dt_nascimento = value.dt_nascimento,
                cpf = value.cpf != null ? value.cpf.Replace("-","").Replace(".","") : null,
                rg = value.rg,
                orgaoEmissor = value.orgaoEmissor,
                sexo = value.sexo,
                situacao = value.situacao,
                endereco = value.endereco,
                complementoEnd = value.complementoEnd,
                cep = value.cep != null ? value.cep.Replace("-","") : null,
                cidadeId = value.cidadeId,
                uf = value.uf != null ? value.uf.ToUpper() : null,
                bairro = value.bairro,
                enderecoCom = value.enderecoCom,
                complementoEndCom = value.complementoEndCom,
                cepCom = value.cepCom != null ? value.cepCom.Replace("-", "") : null,
                cidadeComId = value.cidadeComId,
                ufCom = value.ufCom != null ? value.ufCom.ToUpper() : null,
                bairroCom = value.bairroCom,
                telParticular1 = value.telParticular1 != null ? value.telParticular1.Replace("-", "").Replace(".", "").Replace(" ", "") : null,
                telParticular2 = value.telParticular2 != null ? value.telParticular2.Replace("-", "").Replace(".", "").Replace(" ", "") : null,
                telParticular3 = value.telParticular3 != null ? value.telParticular3.Replace("-", "").Replace(".", "").Replace(" ", "") : null,
                telParticular4 = value.telParticular4 != null ? value.telParticular4.Replace("-", "").Replace(".", "").Replace(" ", "") : null,
                telCom1 = value.telCom1 != null ?  value.telCom1.Replace("-", "").Replace(".", "").Replace(" ", "") : null,
                telCom2 = value.telCom2 != null ?  value.telCom2.Replace("-", "").Replace(".", "").Replace(" ", "") : null,
                fax = value.fax != null ?  value.fax.Replace("-", "").Replace(".", "").Replace(" ", "") : null,
                isSindicalizado = value.isSindicalizado,
                dt_admin_sindicato = dt_admissao,
                correioId = value.correioId,
                areaAtuacao1Id = value.areaAtuacao1Id,
                areaAtuacao2Id = value.areaAtuacao2Id,
                areaAtuacao3Id = value.areaAtuacao3Id,
                email1 = value.email1 != null ? value.email1.ToLower() : null,
                email2 = value.email2 != null ? value.email2.ToLower() : null,
                email3 = value.email3 != null ? value.email3.ToLower() : null,
                usuarioId = value.usuarioId,
                observacao = value.observacao,
                ufCRM = value.ufCRM != null ? value.ufCRM.ToUpper() : null,
                CRM = value.CRM,
                ufCRM_Seg = value.ufCRM_Seg != null ? value.ufCRM_Seg.ToUpper() : null,
                CRM_Seg = value.CRM_Seg,
                especialidade1Id = value.especialidade1Id,
                especialidade2Id = value.especialidade2Id,
                especialidade3Id = value.especialidade3Id,
                avatar = value.avatar
            };

            return medico;
        }

        public override MedicoViewModel MapToRepository(Medico entity)
        {
            App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext> security = new App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext>();
            Sessao sessaoCorrente = security.getSessaoCorrente();

            if (sessaoCorrente != null && sessaoCorrente.value1 != "0")
            {
                if (entity.associadoId != int.Parse(sessaoCorrente.value1))
                    return new MedicoViewModel()
                    {
                        mensagem = new Validate() { Code = 202 }
                    };
            }

            MedicoViewModel medicoViewModel = new MedicoViewModel()
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
                cidadeId = entity.cidadeId,
                nome_cidade = (from cid in db.Cidades where cid.cidadeId == entity.cidadeId select cid).Select(info => info.nome).FirstOrDefault() ?? "",
                uf = entity.uf,
                bairro = entity.bairro,
                enderecoCom = entity.enderecoCom,
                complementoEndCom = entity.complementoEndCom,
                cepCom = entity.cepCom,
                cidadeComId = entity.cidadeComId,
                nome_cidadeCom = (from cid in db.Cidades where cid.cidadeId == entity.cidadeComId select cid).Select(info => info.nome).FirstOrDefault() ?? "",
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
                nome_correio = (from n in db.NaoLocalizadoCorreios where n.correioId == entity.correioId select n).Select(info => info.descricao).FirstOrDefault() ?? "",
                areaAtuacao1Id = entity.areaAtuacao1Id,
                descricao_areaAtuacao1 = (from a in db.AreaAtuacaos where a.areaAtuacaoId == entity.areaAtuacao1Id select a).Select(info => info.descricao).FirstOrDefault() ?? "",
                areaAtuacao2Id = entity.areaAtuacao2Id,
                descricao_areaAtuacao2 = (from a in db.AreaAtuacaos where a.areaAtuacaoId == entity.areaAtuacao2Id select a).Select(info => info.descricao).FirstOrDefault() ?? "",
                areaAtuacao3Id = entity.areaAtuacao3Id,
                descricao_areaAtuacao3 = (from a in db.AreaAtuacaos where a.areaAtuacaoId == entity.areaAtuacao3Id select a).Select(info => info.descricao).FirstOrDefault() ?? "",
                email1 = entity.email1,
                email3 = entity.email2,
                email2 = entity.email3,
                usuarioId = entity.usuarioId,
                observacao = entity.observacao,
                avatar = entity.avatar,
                ufCRM = entity.ufCRM,
                CRM = entity.CRM,
                ufCRM_Seg = entity.ufCRM_Seg,
                CRM_Seg = entity.CRM_Seg,
                especialidade1Id = entity.especialidade1Id,
                nome_especialidade1 = (from e in db.EspecialidadeMedicas where e.especialidadeId == entity.especialidade1Id select e).Select(info => info.descricao).FirstOrDefault() ?? "",
                especialidade2Id = entity.especialidade2Id,
                nome_especialidade2 = (from e in db.EspecialidadeMedicas where e.especialidadeId == entity.especialidade2Id select e).Select(info => info.descricao).FirstOrDefault() ?? "",
                especialidade3Id = entity.especialidade3Id,
                nome_especialidade3 = (from e in db.EspecialidadeMedicas where e.especialidadeId == entity.especialidade3Id select e).Select(info => info.descricao).FirstOrDefault() ?? "",
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
            
            //EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            //sessaoCorrente = security.getSessaoCorrente();
            Medico m = null;
            if (entity.usuarioId.HasValue)
                m = (from med in db.Medicos.AsEnumerable() where med.usuarioId == entity.usuarioId select med).FirstOrDefault();

            if (m != null && m.associadoId != entity.associadoId)
                medicoViewModel = new MedicoViewModel() { mensagem = new Validate() { Code = 202, Message = MensagemPadrao.Message(202).text } };
            else if (entity.usuarioId.HasValue)
                medicoViewModel.nome_usuario = security.getUsuarioById(entity.usuarioId.Value).nome;

            return medicoViewModel;
        }

        public override Medico Find(MedicoViewModel key)
        {
            return db.Medicos.Find(key.associadoId);
        }

        public override Validate Validate(MedicoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.associadoId == 0 && operation != Crud.INCLUIR)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "ID do Associado").ToString();
                value.mensagem.MessageBase = "ID do associado deve ser informado para realizar esta operação.";
                return value.mensagem;
            }
            else if (operation == Crud.EXCLUIR)
                return value.mensagem;

            #region verifica se o nome do associado está abreviado
            if (value.nome.Contains("."))
            {
                value.mensagem.Code = 4;
                value.mensagem.Message = MensagemPadrao.Message(4, "Nome", "Informar o nome completo sem abreviações").ToString();
                value.mensagem.MessageBase = "Nome do associado inválido.";
                return value.mensagem;
            }
            #endregion

            #region valida cpf/cnpj
            if (!Funcoes.ValidaCpf(value.cpf.Replace(".", "").Replace("-", "")))
            {
                value.mensagem.Code = 29;
                value.mensagem.Message = MensagemPadrao.Message(29).ToString();
                value.mensagem.MessageBase = "Número de CPF incorreto.";
                return value.mensagem;
            }

            if (operation == Crud.ALTERAR)
            {
                if (db.Associados.Where(info => info.cpf == value.cpf.Replace(".", "").Replace("-", "") && info.associadoId != value.associadoId).Count() > 0)
                {
                    value.mensagem.Code = 31;
                    value.mensagem.Message = MensagemPadrao.Message(31).ToString();
                    value.mensagem.MessageBase = "CPF informado para o associado já se encontra cadastrado para outro associado.";
                    return value.mensagem;
                }
            }
            else
            {
                if (db.Associados.Where(info => info.cpf == value.cpf.Replace(".", "").Replace("-", "")).Count() > 0)
                {
                    value.mensagem.Code = 31;
                    value.mensagem.Message = MensagemPadrao.Message(31).ToString();
                    value.mensagem.MessageBase = "CPF informado para o associado já se encontra cadastrado para outro associado.";
                    return value.mensagem;
                }
            }
            #endregion

            if (value.isSindicalizado == "S" && !value.dt_admin_sindicato.HasValue)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Dt.Admissão").ToString();
                value.mensagem.MessageBase = "Quando o associado é sindicalizado a Dt.Admissão é obrigatória";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (value.especialidade1Id == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Especialidade").ToString();
                value.mensagem.MessageBase = "Campo Especialidade Médica deve ser informada nos dados do profissional";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (operation == Crud.ALTERAR && value.usuarioId.HasValue && (from ass in db.Associados where ass.usuarioId == value.usuarioId && ass.associadoId != value.associadoId select ass.usuarioId).Count() > 0)
            {
                value.mensagem.Code = 4;
                value.mensagem.Message = MensagemPadrao.Message(4, "Usuario", "Este usuário já está vinculado a outro associado.").ToString();
                value.mensagem.MessageBase = "Este usuário já está vinculado a outro associado.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            #region Verifica se o e-mail do associado já foi atribuído para outro associado
            if (operation == Crud.ALTERAR)
            {
                if (db.Associados.Where(info => info.email1 == value.email1 && info.associadoId != value.associadoId).Count() > 0)
                {
                    value.mensagem.Code = 41;
                    value.mensagem.Message = MensagemPadrao.Message(41, "E-mail").ToString();
                    value.mensagem.MessageBase = "E-mail informado para o associado já se encontra cadastrado para outro associado.";
                    return value.mensagem;
                }
            }
            else if (operation == Crud.INCLUIR)
            {
                if (db.Associados.Where(info => info.email1 == value.email1).Count() > 0)
                {
                    value.mensagem.Code = 41;
                    value.mensagem.Message = MensagemPadrao.Message(41, "E-mail").ToString();
                    value.mensagem.MessageBase = "E-mail informado para o associado já se encontra cadastrado para outro associado.";
                    return value.mensagem;
                }
            }
            #endregion

            #region verifica se informou a UF do CRM (caso o CRM tenha sido informado)
            if (value.CRM != null && value.CRM != "" && (value.ufCRM == null || value.ufCRM == ""))
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "UF CRM").ToString();
                value.mensagem.MessageBase = "UF do CRM deve ser informado.";
                return value.mensagem;
            }
            #endregion

            #region Verifica se o CRM do associado já não foi atribuído para outro associado
            if (value.CRM != null && value.CRM != "")
            {
                if (operation == Crud.ALTERAR)
                {
                    if (db.Medicos.Where(info => info.CRM == value.CRM && info.ufCRM == value.ufCRM && info.associadoId != value.associadoId).Count() > 0)
                    {
                        value.mensagem.Code = 41;
                        value.mensagem.Message = MensagemPadrao.Message(41, "CRM").ToString();
                        value.mensagem.MessageBase = "CRM informado para o médico já se encontra cadastrado para outro médico.";
                        return value.mensagem;
                    }
                }
                else if (operation == Crud.INCLUIR)
                {
                    if (db.Medicos.Where(info => info.CRM == value.CRM && info.ufCRM == value.ufCRM).Count() > 0)
                    {
                        value.mensagem.Code = 41;
                        value.mensagem.Message = MensagemPadrao.Message(41, "E-mail").ToString();
                        value.mensagem.MessageBase = "CRM informado para o médico já se encontra cadastrado para outro médico.";
                        return value.mensagem;
                    }
                }
            }
            #endregion

            return value.mensagem;
        }
        #endregion

        #region Métodos customizados
        public MedicoViewModel getAssociadoByUsuario(int usuarioId)
        {
            using (db = getContextInstance())
            {
                Medico entity = db.Medicos.Where(info => info.usuarioId == usuarioId).FirstOrDefault();
                if (entity != null)
                    return MapToRepository(entity);
                else
                    return new MedicoViewModel();
            }
        }

        public int? getAssociadoIdByLogin(string login, EmpresaSecurity<App_DominioContext> security)
        {
            int? associadoId = null;

            #region retorna o usuário para verificar se o mesmo é um associado
            using (ApplicationContext db = new ApplicationContext())
            {
                int empresaId = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.EMPRESA).valor);
                UsuarioRepository usuarioRepository = security.getUsuarioByLogin(login, empresaId);

                if (usuarioRepository != null)
                    if (db.Associados.Where(info => info.usuarioId == usuarioRepository.usuarioId).Count() > 0)
                    {
                        Associado a = db.Associados.Where(info => info.usuarioId == usuarioRepository.usuarioId).FirstOrDefault();
                        if (a.situacao != "A")
                            associadoId = -1; // associado está desativado
                        else
                            associadoId = db.Associados.Where(info => info.usuarioId == usuarioRepository.usuarioId).FirstOrDefault().associadoId;
                    }
            }
            #endregion
            return associadoId;
        }

        public MedicoViewModel getAssociadoByLogin(string login, EmpresaSecurity<App_DominioContext> security)
        {
            MedicoViewModel a = null;
            #region retorna o usuário para verificar se o mesmo é um associado
            using (ApplicationContext db = new ApplicationContext())
            {
                int empresaId = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.EMPRESA).valor);
                UsuarioRepository usuarioRepository = security.getUsuarioByLogin(login, empresaId);

                if (usuarioRepository != null)
                    if (db.Associados.Where(info => info.usuarioId == usuarioRepository.usuarioId).Count() > 0)
                    {
                        Associado a1 = db.Associados.Where(info => info.usuarioId == usuarioRepository.usuarioId).FirstOrDefault();
                        if (a1.situacao != "A")
                            a = new MedicoViewModel() { associadoId = -1 }; // associado está desativado
                        else
                        {
                            this.db = db;
                            Medico a2 = db.Medicos.Find(a1.associadoId);
                            a = MapToRepository(a2);
                        }
                    }
            }
            #endregion
            return a;
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
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || socio.nome.Contains(_descricao.Trim()) || socio.CRM == _descricao || socio.CRM_Seg == _descricao || socio.cpf == _descricao)
                    orderby socio.nome
                    select new MedicoViewModel
                    {
                        //codigo = SqlFunctions.StringConvert((decimal)med.associadoId),
                        associadoId = socio.associadoId,
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
            return "../Associado/ListMedicoModal";
        }
    }

    public class LookupMedicoFiltroModel : ListViewMedico
    {
        public override string action()
        {
            return "../Associado/_ListMedicoModal";
        }
    }

}