using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("Unidade")]
    public class Unidade
    {
        [Key, Column(Order = 0)]
        [DisplayName("Torre")]
        public string torreId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("Unidade")]
        public int unidadeId { get; set; }

        [DisplayName("codigoBarra")]
        public string codigoBarra { get; set; }
    }
}