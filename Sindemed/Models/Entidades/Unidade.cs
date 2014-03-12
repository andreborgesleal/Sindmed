using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("Unidade")]
    public class Unidade
    {
        [Key, Column(Order = 0)]
        [DisplayName("Torre")]
        public string torreId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("Apto")]
        public int unidadeId { get; set; }

        [DisplayName("Morador")]
        public int associadoId { get; set; }
    }
}