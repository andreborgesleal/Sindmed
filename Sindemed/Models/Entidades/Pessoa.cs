using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        [DisplayName("ID")]
        public int pessoaId { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("Cpf")]
        public string cpf { get; set; }

        [DisplayName("Sexo")]
        public string sexo { get; set; }

        [DisplayName("RG")]
        public string rg { get; set; }

        [DisplayName("Tipo")]
        public string tipo { get; set; }

        [DisplayName("Telefone")]
        public string telefone { get; set; }

        [DisplayName("Referencia")]
        public string referencia { get; set; }

    }
}