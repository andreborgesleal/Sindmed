using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWM.Models.Entidades
{
    [Table("EspecialidadeMedica")]
    public class EspecialidadeMedica
    {
        [Key]
        [DisplayName("ID")]
        public int especialidadeId { get; set; }

        [DisplayName("Descrição")]
        public string descricao { get; set; }

        [DisplayName("Código_Interno")]
        public string codigo { get; set; }
    }
}