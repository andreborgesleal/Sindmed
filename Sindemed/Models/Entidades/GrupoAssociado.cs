using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("GrupoAssociado")]
    public class GrupoAssociado
    {
        [Key]
        public int grupoAssociadoId { get; set; }

        public string descricao { get; set; }

        public string objetivo { get; set; }
    }
}