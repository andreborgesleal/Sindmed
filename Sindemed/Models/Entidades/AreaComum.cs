using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("AreaComum")]
    public class AreaComum
    {
        [Key]
        [DisplayName("ID")]
        public int areaComumId { get; set; }

        [DisplayName("Descrição")]
        public string descricao { get; set; }

        [DisplayName("Qte")]
        public int quantidade { get; set; }

        [DisplayName("Ind_Morador")]
        public string ind_morador { get; set; }
    }
}