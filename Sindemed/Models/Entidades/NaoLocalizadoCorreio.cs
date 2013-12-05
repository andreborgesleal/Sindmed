using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("NaoLocalizadoCorreio")]
    public class NaoLocalizadoCorreio
    {
        [Key]
        public int correioId { get; set; }

        public string descricao { get; set; }

        public string codigo { get; set; }
    }
}