using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("ComunicacaoGrupo")]
    public class ComunicacaoGrupo
    {
        [Key, Column(Order = 0)]
        [DisplayName("ComunicadoID")]
        public Nullable<int> comunicacaoId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("Grupo_AssociadoID")]
        public Nullable<int> grupoAssociadoId { get; set; }            
    }
}