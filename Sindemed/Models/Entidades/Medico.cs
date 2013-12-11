﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("Medico")]
    public class Medico
    {
        [Key]
        public decimal associadoId { get; set; }
        public string ufCRM { get; set; }
        public decimal CRM { get; set; }
        public string ufCRM_Seg { get; set; }
        public decimal CRM_Seg { get; set; }
        public Nullable<int> especialidade1Id { get; set; }
        public Nullable<int> especialidade2Id { get; set; }
        public Nullable<int> especialidade3Id { get; set; }

        public virtual Associado Associado { get; set; }
        public EspecialidadeMedica Especialidade1 { get; set; }
        public EspecialidadeMedica Especialidade2 { get; set; }
        public EspecialidadeMedica Especialidade3 { get; set; }
    }
}