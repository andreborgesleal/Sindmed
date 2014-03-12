using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("Torre")]
    public class Torre
    {
        [Key]
        [DisplayName("ID")]
        public string torreId { get; set; }

        [DisplayName("Descrição")]
        [StringLength(20, ErrorMessage = "A descrição da torre deve ter no máximo 20 caracteres")]
        public string descricao { get; set; }
    }
}