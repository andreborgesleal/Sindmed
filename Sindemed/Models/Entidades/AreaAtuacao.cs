using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWM.Models.Entidades
{
    [Table("AreaAtuacao")]
    public class AreaAtuacao
    {
        [Key]
        [DisplayName("ID")]
        public int areaAtuacaoId { get; set; }
        [DisplayName("Descrição")]
        public string descricao { get; set; }
        [DisplayName("Código_Interno")]
        public string codigo { get; set; }

    }
}