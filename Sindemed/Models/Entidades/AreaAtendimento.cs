using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("AreaAtendimento")]
    public class AreaAtendimento
    {
        [Key]
        [DisplayName("ID")]
        public int areaAtendimentoId { get; set; }
        [DisplayName("Descrição")]
        public string descricao { get; set; }
        [DisplayName("Código_Interno")]
        public string codigo { get; set; }
        [DisplayName("ID_Usuário_1")]
        public int usuario1Id { get; set; }
        [DisplayName("ID_Usuário_2")]
        public Nullable<int> usuario2Id { get; set; }
    }
}