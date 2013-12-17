using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Sindemed.Models.Entidades
{
    [Table("Associado")]
    public class Associado
    {
        [Key]
        public int associadoId { get; set; }
        public string nome { get; set; }
        public Nullable<DateTime> dt_nascimento { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string orgaoEmissor { get; set; }
        public string sexo { get; set; }
        public string situacao { get; set; }
        public string endereco { get; set; }
        public string complementoEnd { get; set; }
        public string cep { get; set; }
        public Nullable<int> cidadeId { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public string enderecoCom { get; set; }
        public string complementoEndCom { get; set; }
        public string cepCom { get; set; }
        public Nullable<int> cidadeComId { get; set; }
        public string ufCom { get; set; }
        public string bairroCom { get; set; }
        public string telParticular1 { get; set; }
        public string telParticular2 { get; set; }
        public string telParticular3 { get; set; }
        public string telParticular4 { get; set; }
        public string telCom1 { get; set; }
        public string telCom2 { get; set; }
        public string fax { get; set; }
        public string isSindicalizado { get; set; }
        public Nullable<DateTime> dt_admin_sindicato { get; set; }
        public Nullable<int> correioId { get; set; }
        public Nullable<int> areaAtuacao1Id { get; set; }
        public Nullable<int> areaAtuacao2Id { get; set; }
        public Nullable<int> areaAtuacao3Id { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }
        public string email3 { get; set; }
        public Nullable<decimal> usuarioId { get; set; }
        public string observacao { get; set; }

        public decimal getCodigo()
        {
            return associadoId;
        }
    }

}