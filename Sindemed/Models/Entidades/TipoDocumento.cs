using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("TipoDocumento")]
    public class TipoDocumento
    {
        [Key]
        [DisplayName("ID")]
        public int tipoDocumentoId { get; set; }

        [DisplayName("Descricao")]
        public string descricao { get; set; }
    }
}