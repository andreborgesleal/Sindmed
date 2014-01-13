using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("Cidade")]
    public class Cidade
    {
        [Key]
        [DisplayName("ID")]
        public int cidadeId { get; set; }

        [DisplayName("Nome")]
        [StringLength(30, ErrorMessage = "O nome da cidade deve ter no máximo 30 caracteres")]
        public string nome { get; set; }

        [DisplayName("UF")]
        public string uf { get; set; }
    }
}