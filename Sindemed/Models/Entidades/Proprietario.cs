using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("Proprietario")]
    public class Proprietario
    {
        [Key]
        [DisplayName("ID")]
        public int proprietarioId { get; set; }

        [DisplayName("Torre")]
        public string torreId { get; set; }

        [DisplayName("Unidade")]
        public int unidadeId { get; set; }

        [DisplayName("Início")]
        public DateTime dt_inicio { get; set; }

        [DisplayName("Fim")]
        public System.Nullable<DateTime> dt_fim { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("Tipo Pessoa")]
        public string ind_tipo_pessoa { get; set; }

        [DisplayName("CPF/CNPJ")]
        public string cpf_cnpj { get; set; }

        [DisplayName("E-Mail")]
        public string email { get; set; }

        [DisplayName("Est.Civil")]
        public string ind_est_civil { get; set; }

        [DisplayName("Tel.Contato 1")]
        public string tel_contato1 { get; set; }

        [DisplayName("Tel.Contato 2")]
        public string tel_contato2 { get; set; }

        [DisplayName("Cônjuge")]
        public string nome_conjuge { get; set; }

        [DisplayName("CPF Cônjuge")]
        public string cpf_cnpj_conjuge { get; set; }

        [DisplayName("Endereço")]
        public string endereco { get; set; }

        [DisplayName("Complemento")]
        public string complemento { get; set; }

        [DisplayName("Cidade")]
        public System.Nullable<int> cidadeId { get; set; }

        [DisplayName("UF")]
        public string uf { get; set; }

        [DisplayName("CEP")]
        public string cep { get; set; }
    }
}