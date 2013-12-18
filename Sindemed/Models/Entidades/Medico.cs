﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("Medico")]
    public class Medico : Associado
    {
        public string ufCRM { get; set; }
        public string CRM { get; set; }
        public string ufCRM_Seg { get; set; }
        public string CRM_Seg { get; set; }
        public int especialidade1Id { get; set; }
        public Nullable<int> especialidade2Id { get; set; }
        public Nullable<int> especialidade3Id { get; set; }
    }
}