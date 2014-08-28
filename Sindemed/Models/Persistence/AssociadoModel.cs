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
using App_Dominio.Models;
using App_Dominio.Security;
using App_Dominio.Repositories;

namespace DWM.Models.Persistence
{
    public class AssociadoModel : CrudContext<Associado, AssociadoViewModel, ApplicationContext>
    {
        public  AssociadoModel() : base()
        {

        }

        public AssociadoModel(ApplicationContext _db)
        {
            this.db = _db;
        }

        #region Métodos da classe CrudContext
        public override Associado MapToEntity(AssociadoViewModel value)
        {
            int? usuarioId = (from u in seguranca_db.Usuarios where u.login == value.email1 && u.empresaId == value.empresaId select u.usuarioId).FirstOrDefault();
            int _fuso_horario = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.FUSO_HORARIO).valor);

            Associado associado = null;

            if (value.associadoId == 0)
            {
                associado = new Associado();
                associado.Veiculos = new List<Veiculo>();
                associado.Dependentes = new List<Dependente>();
                associado.Funcionarios = new List<Funcionario>();
            }
            else
            {
                associado = Find(value);
                associado.Veiculos.Clear();
                associado.Dependentes.Clear();
                associado.Funcionarios.Clear();
            }

            associado.associadoId = value.associadoId;
            associado.torreId = value.torreId;
            associado.unidadeId = value.unidadeId;
            associado.nome = value.nome.ToUpper();
            associado.ind_proprietario = value.ind_proprietario;
            associado.ind_proprietario_confirmacao = value.ind_proprietario_confirmacao == null || value.ind_proprietario_confirmacao == "" ? "N" : value.ind_proprietario_confirmacao;
            associado.dt_nascimento = value.dt_nascimento;
            associado.cpf_cnpj = value.cpf_cnpj.Replace(".", "").Replace("-", "");
            associado.rg = value.rg;
            associado.orgaoEmissor = value.orgaoEmissor != null ? value.orgaoEmissor.ToUpper() : value.orgaoEmissor;
            associado.sexo = value.sexo;
            associado.ind_est_civil = value.ind_est_civil;
            associado.empresa = value.empresa;
            associado.enderecoCom = value.enderecoCom != null ? value.enderecoCom.ToUpper() : value.enderecoCom;
            associado.complementoEndCom = value.complementoEndCom != null ? value.complementoEndCom.ToUpper() : value.complementoEndCom;
            associado.cepCom = value.cepCom != null ? value.cepCom.Replace("-", "") : value.cepCom;
            associado.cidadeComId = value.cidadeComId;
            associado.ufCom = value.ufCom != null ? value.ufCom.ToUpper() : value.ufCom;
            associado.bairroCom = value.bairroCom;
            associado.telParticular1 = value.telParticular1 != null ? value.telParticular1.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "") : value.telParticular1;
            associado.telParticular2 = value.telParticular2 != null ? value.telParticular2.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "") : value.telParticular2;
            associado.telParticular3 = value.telParticular3 != null ? value.telParticular3.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "") : value.telParticular3;
            associado.telParticular4 = value.telParticular4 != null ? value.telParticular4.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "") : value.telParticular4;
            associado.areaAtuacaoId = value.areaAtuacaoId;
            associado.email1 = value.email1.ToLower();
            associado.usuarioId = usuarioId  > 0 ? usuarioId : null;
            associado.observacao = value.observacao;
            associado.nome_contato = value.nome_contato != null ? value.nome_contato.ToUpper() : value.nome_contato;
            associado.dt_inicio = value.dt_inicio;
            associado.dt_fim = value.dt_fim;
            associado.ind_animal = value.ind_animal;
            associado.dt_cadastro = DateTime.Now.AddHours(_fuso_horario);
            associado.avatar = value.avatar;

            foreach (VeiculoViewModel v in value.Veiculos)
            {
                if (v.placa != null && v.placa.Trim() != "")
                {
                    Veiculo v1 = new Veiculo()
                    {
                        associadoId = value.associadoId,
                        placa = v.placa.ToUpper(),
                        cor = v.cor != null ? v.cor.ToUpper() : v.cor,
                        descricao = v.descricao,
                        marca = v.marca,
                        condutor = v.condutor,
                        num_serie = v.num_serie
                    };

                    associado.Veiculos.Add(v1);
                }
            };

            foreach (DependenteViewModel d in value.Dependentes)
            {
                if (d.nome != null && d.nome.Trim() != "")
                {
                    Dependente d1 = new Dependente()
                    {
                        associadoId = value.associadoId,
                        dependenteId = d.dependenteId,
                        nome = d.nome.ToUpper(),
                        email = d.email != null ? d.email.ToLower() : d.email,
                        tx_relacao_associado = d.tx_relacao_associado,
                        sexo = d.sexo.ToUpper()
                    };

                    associado.Dependentes.Add(d1);
                }
            };

            foreach (FuncionarioViewModel f in value.Funcionarios)
            {
                if (f.nome != null && f.nome.Trim() != "")
                {
                    Funcionario f1 = new Funcionario()
                    {
                        associadoId = value.associadoId,
                        funcionarioId = f.funcionarioId,
                        nome = f.nome.ToUpper(),
                        funcao = f.funcao,
                        sexo = f.sexo.ToUpper(),
                        rg = f.rg,
                        dia_semana = f.dia_semana,
                        horario_ini = f.horario_ini,
                        horario_fim = f.horario_fim
                    };

                    associado.Funcionarios.Add(f1);
                }
            };


            return associado;
        }

        public override AssociadoViewModel MapToRepository(Associado entity)
        {
            App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext> security = new App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext>();
            Sessao sessaoCorrente = security.getSessaoCorrente();

            if (sessaoCorrente != null && sessaoCorrente.value1 != "0")
            {
                if (entity.associadoId != int.Parse(sessaoCorrente.value1))
                    return new AssociadoViewModel()
                    {
                        mensagem = new Validate() { Code = 202 }
                    };
            }

            AssociadoViewModel value = new AssociadoViewModel()
            {
                associadoId = entity.associadoId,
                torreId = entity.torreId,
                unidadeId = entity.unidadeId,
                apto = entity.torreId + entity.unidadeId.ToString(),
                nome = entity.nome,
                ind_proprietario = entity.ind_proprietario,
                ind_proprietario_confirmacao = entity.ind_proprietario_confirmacao,
                dt_nascimento = entity.dt_nascimento,
                cpf_cnpj = entity.cpf_cnpj,
                rg = entity.rg,
                orgaoEmissor = entity.orgaoEmissor,
                sexo = entity.sexo,
                ind_est_civil = entity.ind_est_civil,
                empresa = entity.empresa,
                enderecoCom = entity.enderecoCom,
                complementoEndCom = entity.complementoEndCom,
                cepCom = entity.cepCom,
                cidadeComId = entity.cidadeComId,
                nome_cidadeCom = entity.cidadeComId != null ? db.Cidades.Find(entity.cidadeComId).nome : null,
                ufCom = entity.ufCom,
                bairroCom = entity.bairroCom,
                telParticular1 = entity.telParticular1,
                telParticular2 = entity.telParticular2,
                telParticular3 = entity.telParticular3,
                telParticular4 = entity.telParticular4,
                areaAtuacaoId = entity.areaAtuacaoId,
                descricao_areaAtuacao = entity.areaAtuacaoId != null ? db.AreaAtuacaos.Find(entity.areaAtuacaoId).descricao : null,
                email1 = entity.email1,
                usuarioId = entity.usuarioId,
                observacao = entity.observacao,
                nome_contato = entity.nome_contato,
                dt_inicio = entity.dt_inicio,
                dt_fim = entity.dt_fim,
                ind_animal = entity.ind_animal,
                dt_cadastro = entity.dt_cadastro,
                avatar = entity.avatar,
                Veiculos = new List<VeiculoViewModel>(),
                Dependentes = new List<DependenteViewModel>(),
                Funcionarios = new List<FuncionarioViewModel>(),
                mensagem = new Validate() {  Code = 0, Message = "Registro incluído com sucesso !" }
            };

            IList<VeiculoViewModel> Veiculos = new List<VeiculoViewModel>();
            IList<DependenteViewModel> Dependentes = new List<DependenteViewModel>();
            IList<FuncionarioViewModel> Funcionarios = new List<FuncionarioViewModel>();

            foreach (Veiculo v in entity.Veiculos)
            {
                VeiculoViewModel v1 = new VeiculoViewModel()
                {
                    associadoId = entity.associadoId,
                    placa = v.placa,
                    cor = v.cor,
                    descricao = v.descricao,
                    marca = v.marca,
                    condutor = v.condutor,
                    num_serie = v.num_serie
                };

                Veiculos.Add(v1);
            };

            foreach (Dependente d in entity.Dependentes)
            {
                DependenteViewModel d1 = new DependenteViewModel()
                {
                    associadoId = entity.associadoId,
                    dependenteId = d.dependenteId,
                    nome = d.nome,
                    email = d.email,
                    tx_relacao_associado = d.tx_relacao_associado,
                    sexo = d.sexo
                };

                Dependentes.Add(d1);
            };

            foreach (Funcionario f in entity.Funcionarios)
            {
                FuncionarioViewModel f1 = new FuncionarioViewModel()
                {
                    associadoId = entity.associadoId,
                    funcionarioId = f.funcionarioId,
                    nome = f.nome.ToUpper(),
                    funcao = f.funcao,
                    sexo = f.sexo.ToUpper(),
                    rg = f.rg,
                    dia_semana = f.dia_semana,
                    dom = f.dia_semana.Substring(0, 1),
                    seg = f.dia_semana.Substring(1, 1),
                    ter = f.dia_semana.Substring(2, 1),
                    qua = f.dia_semana.Substring(3, 1),
                    qui = f.dia_semana.Substring(4, 1),
                    sex = f.dia_semana.Substring(5, 1),
                    sab = f.dia_semana.Substring(6, 1),
                    horario_ini = f.horario_ini,
                    horario_fim = f.horario_fim
                };

                Funcionarios.Add(f1);
            };

            value.Veiculos = Veiculos;
            value.Dependentes = Dependentes;
            value.Funcionarios = Funcionarios;

            return value;
        }

        public override Associado Find(AssociadoViewModel key)
        {
            return db.Associados.Find(key.associadoId);
        }

        public override Validate Validate(AssociadoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.associadoId == 0 && operation != Crud.INCLUIR)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "ID do Condômino").ToString();
                value.mensagem.MessageBase = "ID do condômino deve ser informado para realizar esta operação.";
                return value.mensagem;
            }
            else if (operation == Crud.EXCLUIR)
                return value.mensagem;

            #region verifica se o nome do condômino está abreviado
            if (value.nome.Contains("."))
            {
                value.mensagem.Code = 4;
                value.mensagem.Message = MensagemPadrao.Message(4, "Nome", "Informar o nome completo sem abreviações").ToString();
                value.mensagem.MessageBase = "Nome do Condômino inválido.";
                return value.mensagem;
            }
            #endregion

            #region Valida Torre e Unidade (não pode incluir a mesma torre e a mesma unidade para um condômino ativo)
            if (operation == Crud.ALTERAR)
            {
                if (db.Associados.Where(info => new {info.torreId, info.unidadeId} == new {value.torreId, value.unidadeId} && info.associadoId != value.associadoId && info.dt_fim == null).Count() > 0)
                {
                    value.mensagem.Code = 19;
                    value.mensagem.Message = MensagemPadrao.Message(19).ToString();
                    value.mensagem.MessageBase = "Esta unidade já foi atribuída a outro condômino. Se esta é a sua unidade, é possível que um outro condômino tenha feito equivocadamente o cadastro com a sua unidade. Se este for o caso, favor entrar em contato com a administração para regularizar. ";
                    return value.mensagem;
                }
            }
            else
            {
                if (db.Associados.Where(info => new { info.torreId, info.unidadeId } == new { value.torreId, value.unidadeId } && info.dt_fim == null).Count() > 0)
                {
                    value.mensagem.Code = 19;
                    value.mensagem.Message = MensagemPadrao.Message(19).ToString();
                    value.mensagem.MessageBase = "Esta unidade já foi atribuída a outro condômino. Se esta é a sua unidade, é possível que um outro condômino tenha feito equivocadamente o cadastro com a sua unidade. Se este for o caso, favor entrar em contato com a administração para regularizar. ";
                    return value.mensagem;
                }
            }

            #endregion

            #region valida cpf/cnpj
            if (!Funcoes.ValidaCpf(value.cpf_cnpj.Replace(".", "").Replace("-", "")))
            {
                value.mensagem.Code = 29;
                value.mensagem.Message = MensagemPadrao.Message(29).ToString();
                value.mensagem.MessageBase = "Número de CPF incorreto.";
                return value.mensagem;
            }

            if (operation == Crud.ALTERAR)
            {
                if (db.Associados.Where(info => info.cpf_cnpj == value.cpf_cnpj.Replace(".", "").Replace("-", "") && info.associadoId != value.associadoId).Count() > 0)
                {
                    value.mensagem.Code = 31;
                    value.mensagem.Message = MensagemPadrao.Message(31).ToString();
                    value.mensagem.MessageBase = "CPF informado para o condômino já se encontra cadastrado para outro condômino.";
                    return value.mensagem;
                }
            }
            else
            {
                if (db.Associados.Where(info => info.cpf_cnpj == value.cpf_cnpj.Replace(".", "").Replace("-", "")).Count() > 0)
                {
                    value.mensagem.Code = 31;
                    value.mensagem.Message = MensagemPadrao.Message(31).ToString();
                    value.mensagem.MessageBase = "CPF informado para o condômino já se encontra cadastrado para outro condômino.";
                    return value.mensagem;
                }
            }
            #endregion

            #region Verifica se o e-mail do associado já foi atribuído para outro associado ou para outro dependente
            if (operation == Crud.ALTERAR)
            {
                if (db.Associados.Where(info => info.email1 == value.email1 && info.associadoId != value.associadoId).Count() > 0)
                {
                    value.mensagem.Code = 41;
                    value.mensagem.Message = MensagemPadrao.Message(41, "E-mail").ToString();
                    value.mensagem.MessageBase = "E-mail informado para o condômino já se encontra cadastrado para outro condômino.";
                    return value.mensagem;
                }
            }
            else if (operation == Crud.INCLUIR)
            {
                if (db.Associados.Where(info => info.email1 == value.email1).Count() > 0)
                {
                    value.mensagem.Code = 41;
                    value.mensagem.Message = MensagemPadrao.Message(41, "E-mail").ToString();
                    value.mensagem.MessageBase = "E-mail informado para o condômino já se encontra cadastrado para outro condômino.";
                    return value.mensagem;
                }
            }

            // dependente
            if (db.Dependentes.Where(info => info.email == value.email1).Count() > 0)
            {
                value.mensagem.Code = 41;
                value.mensagem.Message = MensagemPadrao.Message(41, "E-mail").ToString();
                value.mensagem.MessageBase = "E-mail informado para o condômino já se encontra cadastrado para um dependente.";
                return value.mensagem;
            }
            #endregion

            if (value.dt_fim != null && value.dt_fim < value.dt_inicio)
            {
                value.mensagem.Code = 12;
                value.mensagem.Message = MensagemPadrao.Message(12, "Dt.Desativação", "Dt.Cadastro").ToString();
                value.mensagem.MessageBase = "Dt.Cadastro e Dt Desativação estão inconsistentes.";
                return value.mensagem;
            }

            #region Validar Dependentes
            foreach (DependenteViewModel d in value.Dependentes)
            {
                if (d.nome != null && d.nome.Trim() != "") 
                {
                    if (d.tx_relacao_associado.Trim() == "")
                    {
                        value.mensagem.Code = 5;
                        value.mensagem.Message = MensagemPadrao.Message(5, "Dependente :: Grau de relação com o Condômino").ToString();
                        value.mensagem.MessageBase = "Quando informar o dependente, o campo [Grau de Relação] deve ser preenchido";
                        return value.mensagem;
                    }

                    #region verifica se o e-mail do dependente já não existe para algum condômino
                    if (d.email != null && d.email != "" && db.Associados.Where(info => info.email1 == d.email).Count() > 0)
                    {
                        value.mensagem.Code = 41;
                        value.mensagem.Message = MensagemPadrao.Message(41, "E-mail").ToString();
                        value.mensagem.MessageBase = "E-mail informado para o dependente já se encontra cadastrado para outro condômino.";
                        return value.mensagem;
                    }
                    #endregion

                    #region verifica se o e-mail do dependente já não existe para outro dependente 
                    if (operation == Crud.INCLUIR)
                    {
                        if (d.email != null && d.email != "" && db.Dependentes.Where(info => info.email == d.email).Count() > 0)
                        {
                            value.mensagem.Code = 41;
                            value.mensagem.Message = MensagemPadrao.Message(41, "E-mail").ToString();
                            value.mensagem.MessageBase = "E-mail informado para o dependente já se encontra cadastrado para outro dependente.";
                            return value.mensagem;
                        }
                    }
                    else
                    {
                        if (d.email != null && d.email != "" && db.Dependentes.Where(info => info.email == d.email && new { info.associadoId, info.dependenteId } != new { d.associadoId, d.dependenteId }).Count() > 0)
                        {
                            value.mensagem.Code = 41;
                            value.mensagem.Message = MensagemPadrao.Message(41, "E-mail").ToString();
                            value.mensagem.MessageBase = "E-mail informado para o dependente já se encontra cadastrado para outro dependente.";
                            return value.mensagem;
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Validar Veículos
            foreach (VeiculoViewModel v in value.Veiculos)
            {
                if (v.placa != null && v.placa.Trim() != "")
                {
                    if (v.condutor == null || v.condutor == "" || String.IsNullOrEmpty(v.condutor))
                    {
                        value.mensagem.Code = 5;
                        value.mensagem.Message = MensagemPadrao.Message(5, "Condutor Responsável").ToString();
                        value.mensagem.MessageBase = "Condutor responsável deve ser informado.";
                        return value.mensagem;
                    }

                    int quantidade = 0;

                    if (operation == Crud.INCLUIR )
                        quantidade = db.Veiculos.Where(info => info.placa == v.placa).Count();
                    else if (operation == Crud.ALTERAR)
                        quantidade = db.Veiculos.Where(info => info.placa == v.placa && info.associadoId != value.associadoId).Count();

                    if (quantidade > 0)
                    {
                        value.mensagem.Code = 41;
                        value.mensagem.Message = MensagemPadrao.Message(41, "Veículo :: Placa").ToString();
                        value.mensagem.MessageBase = "A placa do veículo informada já existe em nosso banco de dados.";
                        return value.mensagem;
                    }
                }
            }
            #endregion

            #region validar funcionários
            foreach (FuncionarioViewModel f in value.Funcionarios)
            {
                if (f.nome != null && f.nome.Trim() != "")
                {
                    if (int.Parse(f.horario_fim.Substring(0, 2)) < int.Parse(f.horario_ini.Substring(0, 2)))
                    {
                        value.mensagem.Code = 22;
                        value.mensagem.Message = MensagemPadrao.Message(22, "Funcionário :: Saída", "Funcionário :: Entrada").ToString();
                        value.mensagem.MessageBase = "O horário do funcionário deve ser preenchido corretamente.";
                        return value.mensagem;
                    }
                    else if (f.funcao.Trim() == "")
                    {
                        value.mensagem.Code = 5;
                        value.mensagem.Message = MensagemPadrao.Message(22, "Funcionário :: Função").ToString();
                        value.mensagem.MessageBase = "A função do funcionário deve ser informada.";
                        return value.mensagem;
                    }
                }
            }
            #endregion

            return value.mensagem;
        }
        #endregion

        #region Métodos customizados
        public AssociadoViewModel getAssociadoByUsuario(int usuarioId)
        {
            using (db = getContextInstance())
            {
                Associado entity = db.Associados.Where(info => info.usuarioId == usuarioId).FirstOrDefault();
                if (entity != null)
                    return MapToRepository(entity);
                else
                    return new AssociadoViewModel();
            }
        }

        public int? getAssociadoIdByLogin(string login, EmpresaSecurity<App_DominioContext> security)
        {
            int? associadoId = null;
            
            #region retorna o usuário para verificar se o mesmo é um condômino
            using (ApplicationContext db = new ApplicationContext())
            {
                int empresaId = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.EMPRESA).valor);
                UsuarioRepository usuarioRepository = security.getUsuarioByLogin(login, empresaId);

                if (usuarioRepository != null)
                    if (db.Associados.Where(info => info.usuarioId == usuarioRepository.usuarioId).Count() > 0)
                    {
                        Associado a = db.Associados.Where(info => info.usuarioId == usuarioRepository.usuarioId).FirstOrDefault();
                        if (a.dt_fim.HasValue)
                            associadoId = -1; // associado está desativado
                        else
                            associadoId = db.Associados.Where(info => info.usuarioId == usuarioRepository.usuarioId).FirstOrDefault().associadoId;
                    }
            }
            #endregion
            return associadoId;
        }

        public AssociadoViewModel getAssociadoByLogin(string login, EmpresaSecurity<App_DominioContext> security)
        {
            AssociadoViewModel a = null;
            #region retorna o usuário para verificar se o mesmo é um condômino
            using (ApplicationContext db = new ApplicationContext())
            {
                int empresaId = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.EMPRESA).valor);
                UsuarioRepository usuarioRepository = security.getUsuarioByLogin(login, empresaId);

                if (usuarioRepository != null)
                    if (db.Associados.Where(info => info.usuarioId == usuarioRepository.usuarioId).Count() > 0)
                    {
                        Associado a1 = db.Associados.Where(info => info.usuarioId == usuarioRepository.usuarioId).FirstOrDefault();
                        if (a1.dt_fim.HasValue)
                            a = new AssociadoViewModel() { associadoId = -1 }; // associado está desativado
                        else
                        {
                            this.db = db;
                            Associado a2 = db.Associados.Find(a1.associadoId);
                            a = MapToRepository(a2);
                        }
                    }
            }
            #endregion
            return a;
        }


        #endregion

    }

    public class ListViewAssociado : ListViewRepository<AssociadoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<AssociadoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            if (_descricao != null)
            {
                if (_descricao.ToUpper() == "GE" || _descricao.ToUpper() == "OL" || _descricao.ToUpper() == "ED" || _descricao.ToUpper() == "OA")
                    return (from socio in db.Associados
                            join unidade in db.Unidades on new { socio.torreId, socio.unidadeId } equals new { unidade.torreId, unidade.unidadeId } into U
                            from unidade in U.DefaultIfEmpty()
                            where socio.torreId == _descricao && socio.dt_fim == null
                            orderby socio.torreId, socio.unidadeId
                            select new AssociadoViewModel
                            {
                                associadoId = socio.associadoId,
                                torreId = socio.torreId,
                                unidadeId = socio.unidadeId,
                                nome = socio.nome,
                                ind_proprietario = socio.ind_proprietario,
                                dt_nascimento = socio.dt_nascimento,
                                cpf_cnpj = socio.cpf_cnpj,
                                rg = socio.rg,
                                orgaoEmissor = socio.orgaoEmissor,
                                sexo = socio.sexo,
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
                                areaAtuacaoId = socio.areaAtuacaoId,
                                email1 = socio.email1,
                                usuarioId = socio.usuarioId,
                                observacao = socio.observacao,
                                nome_contato = socio.nome_contato,
                                dt_inicio = socio.dt_inicio,
                                dt_fim = socio.dt_fim,
                                ind_animal = socio.ind_animal,
                                accessCode = unidade.codigoBarra,
                                avatar = socio.avatar,
                                //Veiculos = new List<VeiculoViewModel>(),
                                //Dependentes = new List<DependenteViewModel>(),
                                PageSize = pageSize,
                                TotalCount = (from socio1 in db.Associados // db.Associados join med in db.Medicos on socio.associadoId equals med.associadoId
                                              where socio1.torreId == _descricao && socio1.dt_fim == null
                                              orderby socio1.torreId, socio1.unidadeId
                                              select socio1).Count()
                            }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
            }

            return (from socio in db.Associados
                    join unidade in db.Unidades on new { socio.torreId, socio.unidadeId } equals new { unidade.torreId, unidade.unidadeId } into U
                    from unidade in U.DefaultIfEmpty()
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || socio.nome.StartsWith(_descricao.Trim()) ||
                            socio.unidadeId.ToString() == _descricao || socio.torreId + socio.unidadeId.ToString() == _descricao ||
                            socio.associadoId == (from v in db.Veiculos where v.placa == _descricao select v.associadoId).FirstOrDefault() ||
                            (from d in db.Dependentes where d.nome.Contains(_descricao) select d.associadoId).Contains(socio.associadoId))
                            && socio.dt_fim == null
                    orderby socio.torreId, socio.unidadeId
                    select new AssociadoViewModel
                    {
                        associadoId = socio.associadoId,
                        torreId = socio.torreId,
                        unidadeId = socio.unidadeId,
                        nome = socio.nome,
                        ind_proprietario = socio.ind_proprietario,
                        dt_nascimento = socio.dt_nascimento,
                        cpf_cnpj = socio.cpf_cnpj,
                        rg = socio.rg,
                        orgaoEmissor = socio.orgaoEmissor,
                        sexo = socio.sexo,
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
                        areaAtuacaoId = socio.areaAtuacaoId,
                        email1 = socio.email1,
                        usuarioId = socio.usuarioId,
                        observacao = socio.observacao,
                        nome_contato = socio.nome_contato,
                        dt_inicio = socio.dt_inicio,
                        dt_fim = socio.dt_fim,
                        ind_animal = socio.ind_animal,
                        accessCode = unidade.codigoBarra,
                        avatar = socio.avatar,
                        //Veiculos = new List<VeiculoViewModel>(),
                        //Dependentes = new List<DependenteViewModel>(),
                        PageSize = pageSize,
                        TotalCount = (from socio1 in db.Associados
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || socio1.nome.StartsWith(_descricao.Trim()) ||
                                             socio1.unidadeId.ToString() == _descricao || socio1.torreId + socio1.unidadeId.ToString() == _descricao ||
                                             socio1.associadoId == (from v in db.Veiculos where v.placa == _descricao select v.associadoId).FirstOrDefault() ||
                                             (from d in db.Dependentes where d.nome.Contains(_descricao) select d.associadoId).Contains(socio1.associadoId))
                                             && socio1.dt_fim == null
                                      orderby socio1.torreId, socio1.unidadeId
                                      select socio1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }


        public override Repository getRepository(Object id)
        {
            return new AssociadoModel().getObject((AssociadoViewModel)id);
        }
        #endregion
    }

    public class LookupAssociadoModel : ListViewAssociado
    {
        public override string action()
        {
            return "../Associado/ListAssociadoModal";
        }
    }

    public class LookupAssociadoFiltroModel : ListViewAssociado
    {
        public override string action()
        {
            return "../Associado/_ListAssociadoModal";
        }
    }


}