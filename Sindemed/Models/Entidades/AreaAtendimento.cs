using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("AreaAtendimento")]
    public class AreaAtendimento
    {
        [Key]
        public int areaAtendimentoId { get; set; }
        public string descricao { get; set; }
        public string codigo { get; set; }
        public int usuario1Id { get; set; }
        public Nullable<int> usuario2Id { get; set; }
    }
}