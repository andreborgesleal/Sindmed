﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace Sindemed.Models.Repositories
{
    public class AssociadoViewModel : Repository
    {
        [DisplayName("ID")]
        public decimal associadoId { get; set; }

        [DisplayName("Nome *")]
        [Required(ErrorMessage = "O campo Nome do associado dever ser informado")]
        [StringLength(40, ErrorMessage = "O nome do associado deve ter no mínimo 10 e no máximo 40 caracteres", MinimumLength = 10)]
        public string nome { get; set; }
        
        [DisplayName("Nascimento")]
        public Nullable<DateTime> dt_nascimento { get; set; }

        [DisplayName("CPF")]
        public string cpf { get; set; }

        [DisplayName("RG")]
        [StringLength(10, ErrorMessage = "O RG deve ter no máximo 10 dígitos")]
        public string rg { get; set; }
        
        [DisplayName("Órgão Emissor")]
        [StringLength(15, ErrorMessage = "O Órgão Emissor deve ter no máximo 15 caracteres")]
        public string orgaoEmissor { get; set; }

        [DisplayName("End.Residencial")]
        [StringLength(50, ErrorMessage = "O Endereço residencial deve ter no máximo 50 caracteres")]
        public string endereco { get; set; }

        [DisplayName("Complemento")]
        [StringLength(40, ErrorMessage = "O Complemento do endereço deve ter no máximo 40 caracteres")]
        public string complementoEnd { get; set; }

        [DisplayName("CEP")]
        public string cep { get; set; }

        [DisplayName("Cidade")]
        public Nullable<int> cidadeId { get; set; }

        [DisplayName("UF")]
        [StringLength(2, ErrorMessage = "A UF deve possuir 2 caracteres", MinimumLength=2)]
        public string uf { get; set; }

        [DisplayName("End.Comercial")]
        [StringLength(50, ErrorMessage = "O Endereço comercial deve er no máximo 50 caracteres", MinimumLength = 2)]
        public string enderecoCom { get; set; }

        [DisplayName("Complemento")]
        [StringLength(40, ErrorMessage = "O Complemento do endereço deve ter no máximo 40 caracteres")]
        public string complementoEndCom { get; set; }

        [DisplayName("CEP")]
        public string cepCom { get; set; }

        [DisplayName("Cidade")]
        public Nullable<int> cidadeComId { get; set; }

        [DisplayName("UF")]
        [StringLength(2, ErrorMessage = "A UF deve possuir 2 caracteres", MinimumLength = 2)]
        public string ufCom { get; set; }

        [DisplayName("Tel. Particular")]
        public string telParticular1 { get; set; }

        [DisplayName("Tel. Particular 2")]
        public string telParticular2 { get; set; }

        [DisplayName("Tel. Particular 3")]
        public string telParticular3 { get; set; }

        [DisplayName("Tel. Particular 4")]
        public string telParticular4 { get; set; }

        [DisplayName("Tel. Comercial")]
        public string telCom1 { get; set; }

        [DisplayName("Tel. Comercial 2")]
        public string telCom2 { get; set; }

        [DisplayName("Fax")]
        public string fax { get; set; }

        [DisplayName("Sindicalizado")]
        public string isSindicalizado { get; set; }

        [DisplayName("Motivo Correios")]
        public Nullable<int> correioId { get; set; }

        [DisplayName("Área de Atuação")]
        public Nullable<int> areaAtuacao1Id { get; set; }

        [DisplayName("Área de Atuação 2")]
        public Nullable<int> areaAtuacao2Id { get; set; }

        [DisplayName("Área de Atuação 3")]
        public Nullable<int> areaAtuacao3Id { get; set; }

        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [EmailAddress(ErrorMessage = "Informe o E-mail com um formato válido")]
        public string email1 { get; set; }

        [DisplayName("E-mail 2")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [EmailAddress(ErrorMessage = "Informe o E-mail 2 com um formato válido")]
        public string email2 { get; set; }

        [DisplayName("E-mail 3")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [EmailAddress(ErrorMessage = "Informe o E-mail 3 com um formato válido")]
        public string email3 { get; set; }

        [DisplayName("Usuário")]
        public Nullable<decimal> usuarioId { get; set; }

        
    }
}
