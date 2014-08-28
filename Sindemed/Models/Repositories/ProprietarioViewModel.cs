using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;


namespace DWM.Models.Repositories
{
    public class ProprietarioViewModel : Repository
    {
        [DisplayName("ID")]
        public int proprietarioId { get; set; }

        [DisplayName("Torre")]
        [Required(ErrorMessage = "O campo Torre dever ser informado")]
        public string torreId { get; set; }

        [DisplayName("Unidade ID")]
        [Required(ErrorMessage = "O campo Unidade dever ser informado")]
        public int unidadeId { get; set; }

        [DisplayName("Unidade")]
        [Required(ErrorMessage = "O campo Unidade dever ser informado")]
        public string apto { get; set; }

        [DisplayName("Dt.Cadastro")]
        [Required(ErrorMessage="Data do cadastro deve ser informada")]
        public DateTime dt_inicio { get; set; }

        [DisplayName("Dt.Desativação")]
        public System.Nullable<DateTime> dt_fim { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome do proprietário dever ser informado")]
        [StringLength(50, ErrorMessage = "O nome do proprietário deve ter no mínimo 10 e no máximo 50 caracteres", MinimumLength = 10)]
        public string nome { get; set; }

        [DisplayName("Tipo Pessoa")]
        public string ind_tipo_pessoa { get; set; }

        [DisplayName("CPF")]
        public string cpf_cnpj { get; set; }

        [DisplayName("E-Mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [EmailAddress(ErrorMessage = "Informe o E-mail com um formato válido")]
        public string email { get; set; }

        [DisplayName("Est.Civil")]
        public string ind_est_civil { get; set; }

        [DisplayName("Tel.Contato 1")]
        public string tel_contato1 { get; set; }

        [DisplayName("Tel.Contato 2")]
        public string tel_contato2 { get; set; }

        [DisplayName("Cônjuge")]
        [StringLength(40, ErrorMessage = "O nome do cônjuge deve ter no mínimo 10 e no máximo 40 caracteres", MinimumLength = 10)]
        public string nome_conjuge { get; set; }

        [DisplayName("CPF Cônjuge")]
        public string cpf_cnpj_conjuge { get; set; }

        [DisplayName("Endereço")]
        [StringLength(50, ErrorMessage = "O endereço deve ter no mínimo 10 e no máximo 50 caracteres", MinimumLength = 50)]
        public string endereco { get; set; }

        [DisplayName("Complemento")]
        [StringLength(25, ErrorMessage = "O complemento do endereço deve ter no máximo 25 caracteres")]
        public string complemento { get; set; }

        [DisplayName("Cidade")]
        public System.Nullable<int> cidadeId { get; set; }

        public string nome_cidade { get; set; }

        [DisplayName("UF")]
        [StringLength(2, ErrorMessage = "A UF deve possuir 2 caracteres", MinimumLength = 2)]
        public string uf { get; set; }

        [DisplayName("CEP")]
        public string cep { get; set; }
    }
}