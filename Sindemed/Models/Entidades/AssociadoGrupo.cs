using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("AssociadoGrupo")]
    public class AssociadoGrupo
    {
        [Key, Column(Order = 0)]
        public Nullable<int> grupoAssociadoId { get; set; }

        [Key, Column(Order = 1)]
        public Nullable<int> associadoId { get; set; }

    }
}