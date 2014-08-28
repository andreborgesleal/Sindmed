using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWM.Models.Entidades
{
    [Table("GrupoAssociado")]
    public class GrupoAssociado
    {
        [Key]
        [DisplayName("ID")]
        public int grupoAssociadoId { get; set; }

        [DisplayName("Descrição")]
        public string descricao { get; set; }

        [DisplayName("Objetivo")]
        public string objetivo { get; set; }
    }
}