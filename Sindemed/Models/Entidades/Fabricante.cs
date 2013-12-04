using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockLite.Models.Entidades
{
    [Table("Fabricante")]
    public class Fabricante
    {
        [Key]
        public int fabricanteId { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome do fabricante")]
        [StringLength(40, ErrorMessage = "O nome deve ter no máximo 40 caracteres")]
        public string nome { get; set; }

    }
}