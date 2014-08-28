using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("Associado")]
    public class Associado
    {
        [Key]
        [DisplayName("ID")]
        public int associadoId { get; set; }
        [DisplayName("Torre")]
        public string torreId { get; set; }
        [DisplayName("Unidade")]
        public int unidadeId { get; set; }
        [DisplayName("Nome")]
        public string nome { get; set; }
        [DisplayName("Ind_Proprietário")]
        public string ind_proprietario { get; set; }
        [DisplayName("Ind_Proprietario_Confirmacao")]
        public string ind_proprietario_confirmacao { get; set; }
        [DisplayName("Nascimento")]
        public Nullable<DateTime> dt_nascimento { get; set; }
        [DisplayName("CPF")]
        public string cpf_cnpj { get; set; }
        [DisplayName("RG")]
        public string rg { get; set; }
        [DisplayName("O_Emissor")]
        public string orgaoEmissor { get; set; }
        [DisplayName("Sexo")]
        public string sexo { get; set; }
        [DisplayName("Est.Civil")]
        public string ind_est_civil { get; set; }
        [DisplayName("Empresa")]
        public string empresa { get; set; }
        [DisplayName("Endereco_Com")]
        public string enderecoCom { get; set; }
        [DisplayName("Complemento_Com")]
        public string complementoEndCom { get; set; }
        [DisplayName("CEP_Com")]
        public string cepCom { get; set; }
        [DisplayName("ID_Cidade_Com")]
        public Nullable<int> cidadeComId { get; set; }
        [DisplayName("UF_Com")]
        public string ufCom { get; set; }
        [DisplayName("Bairro_Com")]
        public string bairroCom { get; set; }
        [DisplayName("Tel_Particular_1")]
        public string telParticular1 { get; set; }
        [DisplayName("Tel_Particular_2")]
        public string telParticular2 { get; set; }
        [DisplayName("Tel_Particular_3")]
        public string telParticular3 { get; set; }
        [DisplayName("Tel_Particular_4")]
        public string telParticular4 { get; set; }
        [DisplayName("ID_Area_Atuacao")]
        public Nullable<int> areaAtuacaoId { get; set; }
        [DisplayName("E-mail")]
        public string email1 { get; set; }
        [DisplayName("ID_Usuario")]
        public Nullable<int> usuarioId { get; set; }
        [DisplayName("xml")]
        public string observacao { get; set; }
        [DisplayName("Nome_Contato")]
        public string nome_contato { get; set; }
        [DisplayName("Dt_Cadastro")]
        public DateTime dt_inicio { get; set; }
        [DisplayName("Dt_Desativacao")]
        public Nullable<DateTime> dt_fim { get; set; }
        [DisplayName("Animal_Estimacao")]
        public string ind_animal { get; set; }
        [DisplayName("Dt_Cadastro")]
        public System.Nullable<DateTime> dt_cadastro { get; set; }
        public string avatar { get; set; }
        public virtual ICollection<Dependente> Dependentes { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
    }

}