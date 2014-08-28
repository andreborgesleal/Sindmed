using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWM.Models.Entidades
{
    [Table("NaoLocalizadoCorreio")]
    public class NaoLocalizadoCorreio
    {
        [Key]
        [DisplayName("ID")]
        public int correioId { get; set; }

        [DisplayName("Descrição")]
        public string descricao { get; set; }

        [DisplayName("Código_Interno")]
        public string codigo { get; set; }
    }
}