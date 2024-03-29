﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("ChamadoMotivo")]
    public class ChamadoMotivo
    {
        [Key]
        [DisplayName("ID")]
        public int chamadoMotivoId { get; set; }

        [DisplayName("Descricao")]
        public string descricao { get; set; }

        [DisplayName("Fixo")]
        public string ind_fixo { get; set; }

    }
}