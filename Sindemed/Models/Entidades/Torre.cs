using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("Torre")]
    public class Torre
    {
        [Key]
        [DisplayName("ID")]
        public string torreId { get; set; }
        
        [DisplayName("Descrição")]
        public string descricao { get; set; }
    }
}