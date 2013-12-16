using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("Cidade")]
    public class Cidade
    {
        [Key]
        public int cidadeId { get; set; }

        [StringLength(30, ErrorMessage = "O nome da cidade deve ter no máximo 30 caracteres")]
        public string nome { get; set; }

        public string uf { get; set; }
    }
}