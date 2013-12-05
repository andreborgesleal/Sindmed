using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("AreaAtuacao")]
    public class AreaAtuacao
    {
        [Key]
        public int areaAtuacaoId { get; set; }
        public string descricao { get; set; }
        public string codigo { get; set; }

    }
}