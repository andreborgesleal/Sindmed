using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sindemed.Models.Entidades
{
    [Table("Associado")]
    public class Associado
    {
        [Key]
        [DisplayName("ID")]
        public int associadoId { get; set; }
        [DisplayName("Nome")]
        public string nome { get; set; }
        [DisplayName("Nascimento")]
        public Nullable<DateTime> dt_nascimento { get; set; }
        [DisplayName("CPF")]
        public string cpf { get; set; }
        [DisplayName("RG")]
        public string rg { get; set; }
        [DisplayName("O_Emissor")]
        public string orgaoEmissor { get; set; }
        [DisplayName("Sexo")]
        public string sexo { get; set; }
        [DisplayName("Situação")]
        public string situacao { get; set; }
        [DisplayName("Endereço_Residencial")]
        public string endereco { get; set; }
        [DisplayName("Complemento_Res")]
        public string complementoEnd { get; set; }
        [DisplayName("CEP")]
        public string cep { get; set; }
        [DisplayName("ID_Cidade_Res")]
        public Nullable<int> cidadeId { get; set; }
        [DisplayName("UF_Res")]
        public string uf { get; set; }
        [DisplayName("Bairro_Res")]
        public string bairro { get; set; }
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
        [DisplayName("Tel_Com_1")]
        public string telCom1 { get; set; }
        [DisplayName("Tel_Com_2")]
        public string telCom2 { get; set; }
        [DisplayName("Fax")]
        public string fax { get; set; }
        [DisplayName("Sindicalizado")]
        public string isSindicalizado { get; set; }
        [DisplayName("Admissão")]
        public Nullable<DateTime> dt_admin_sindicato { get; set; }
        [DisplayName("ID_Correio")]
        public Nullable<int> correioId { get; set; }
        [DisplayName("ID_Área_Atuação_1")]
        public Nullable<int> areaAtuacao1Id { get; set; }
        [DisplayName("ID_Área_Atuação_2")]
        public Nullable<int> areaAtuacao2Id { get; set; }
        [DisplayName("ID_Área_Atuação_3")]
        public Nullable<int> areaAtuacao3Id { get; set; }
        [DisplayName("E-mail_1")]
        public string email1 { get; set; }
        [DisplayName("E-mail_2")]
        public string email2 { get; set; }
        [DisplayName("E-mail_3")]
        public string email3 { get; set; }
        [DisplayName("ID_Usuário")]
        public Nullable<int> usuarioId { get; set; }
        [DisplayName("xml")]
        public string observacao { get; set; }
    }

}