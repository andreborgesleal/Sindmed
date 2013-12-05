using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("EspecialidadeMedica")]
    public class EspecialidadeMedica
    {
        [Key]
        public int especialidadeId { get; set; }

        public string descricao { get; set; }

        public string codigo { get; set; }
    }
}