using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;
using DWM.Models.Entidades;
using System.Collections.Generic;

namespace DWM.Models.Repositories
{
    public class AssociadoViewModel : Repository
    {
        [DisplayName("ID")]
        public int associadoId { get; set; }

        [DisplayName("Torre")]
        public string torreId { get; set; }

        [DisplayName("Unidade ID")]
        public int unidadeId { get; set; }

        [DisplayName("Unidade")]
        [Required(ErrorMessage = "O campo Unidade dever ser informado")]
        public string apto { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome do condômino dever ser informado")]
        [StringLength(60, ErrorMessage = "O nome do condômino deve ter no mínimo 10 e no máximo 60 caracteres", MinimumLength = 10)]
        public string nome { get; set; }

        public string nome_associado { get; set; }

        [DisplayName("Proprietário da Unidade")]
        public string ind_proprietario { get; set; }

        [DisplayName("Confirmação Proprietário")]
        public string ind_proprietario_confirmacao { get; set; }

        [DisplayName("Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> dt_nascimento { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage="O CPF do condômino deve ser informado")]
        public string cpf_cnpj { get; set; }

        [DisplayName("RG")]
        [StringLength(10, ErrorMessage = "O RG deve ter no máximo 10 dígitos")]
        public string rg { get; set; }

        [DisplayName("Órgão Emissor")]
        [StringLength(15, ErrorMessage = "O Órgão Emissor deve ter no máximo 15 caracteres")]
        public string orgaoEmissor { get; set; }

        [DisplayName("Sexo")]
        public string sexo { get; set; }

        [DisplayName("Est.Civil")]
        [Required(ErrorMessage="O Estado Civil do Condômino deve ser informado")]
        public string ind_est_civil { get; set; }

        [DisplayName("Empresa")]
        [StringLength(50, ErrorMessage = "A Empresa deve ter no máximo 50 caracteres")]
        public string empresa { get; set; }

        [DisplayName("Logradouro")]
        [StringLength(50, ErrorMessage = "O Endereço comercial deve ter no máximo 50 caracteres", MinimumLength = 2)]
        public string enderecoCom { get; set; }

        [DisplayName("Complemento")]
        [StringLength(40, ErrorMessage = "O Complemento do endereço deve ter no máximo 40 caracteres")]
        public string complementoEndCom { get; set; }

        [DisplayName("CEP")]
        public string cepCom { get; set; }

        [DisplayName("Cidade")]
        public Nullable<int> cidadeComId { get; set; }

        public string nome_cidadeCom { get; set; }

        [DisplayName("UF")]
        [StringLength(2, ErrorMessage = "A UF comercial deve possuir 2 caracteres", MinimumLength = 2)]
        public string ufCom { get; set; }

        [DisplayName("Bairro")]
        [StringLength(25, ErrorMessage = "O Bairro comercial deve possuir no máximo 25 caracteres")]
        public string bairroCom { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage="O telefone de contato deve ser informado")]
        public string telParticular1 { get; set; }

        [DisplayName("Telefone")]
        public string telParticular2 { get; set; }

        [DisplayName("Tel Comercial")]
        public string telParticular3 { get; set; }

        [DisplayName("Tel Comercial")]
        public string telParticular4 { get; set; }

        [DisplayName("Profissão")]
        public Nullable<int> areaAtuacaoId { get; set; }

        public string descricao_areaAtuacao { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "E-mail do condômino deve ser informado")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [EmailAddress(ErrorMessage = "Informe o E-mail com um formato válido")]
        public string email1 { get; set; }

        [DisplayName("Usuário")]
        public Nullable<int> usuarioId { get; set; }

        public string nome_usuario { get; set; }

        [DisplayName("Observação")]
        public string observacao { get; set; }

        [DisplayName("Responsável")]
        public string nome_contato { get; set; }

        [DisplayName("Dt.Cadastro")]
        [Required(ErrorMessage = "Data do cadastro deve ser informada")]
        public DateTime dt_inicio { get; set; }
        
        [DisplayName("Dt. Desativação")]
        public Nullable<DateTime> dt_fim { get; set; }

        [DisplayName("Dt.Atualização")]
        public System.Nullable<DateTime> dt_cadastro { get; set; }

        [DisplayName("Animal de Estimação")]
        public string ind_animal { get; set; }

        public string avatar { get; set; }

        [DisplayName("Código de Acesso")]
        public string accessCode { get; set; }

        public IEnumerable<VeiculoViewModel> Veiculos { get; set; }

        public IEnumerable<DependenteViewModel> Dependentes { get; set; }

        public IEnumerable<FuncionarioViewModel> Funcionarios { get; set; }
    }

    public class CondominoViewModel : AssociadoViewModel
    {
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "A senha deve possuir no mínimo 6 dígitos e no máximo 20 dígitos", MinimumLength = 6)]
        public string senha { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        [Compare("senha", ErrorMessage = "As senhas não conferem.")]
        public string confirmacaoSenha { get; set; }

        [DisplayName("Código de Barras")]
        [Required(ErrorMessage="Código de Barras deve ser informado")]
        public string codigo_barra { get; set; }

        // 39994389677000000001131011099426660280000046500
    }
}
