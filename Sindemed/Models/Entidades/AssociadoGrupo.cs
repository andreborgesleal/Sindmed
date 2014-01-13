using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("AssociadoGrupo")]
    public class AssociadoGrupo
    {
        [Key, Column(Order = 0)]
        [DisplayName("ID_Grupo_Associado")]
        public int grupoAssociadoId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("ID_Associado")]
        public int associadoId { get; set; }

    }
}