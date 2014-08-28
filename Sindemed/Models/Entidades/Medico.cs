using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWM.Models.Entidades
{
    [Table("Medico")]
    public class Medico : Associado
    {
        [DisplayName("UF_CRM")]
        public string ufCRM { get; set; }
        [DisplayName("CRM")]
        public string CRM { get; set; }
        [DisplayName("UF_CRM_Seg")]
        public string ufCRM_Seg { get; set; }
        [DisplayName("CRM_Seg")]
        public string CRM_Seg { get; set; }
        [DisplayName("ID_Especialidade_1")]
        public int especialidade1Id { get; set; }
        [DisplayName("ID_Especialidade_2")]
        public Nullable<int> especialidade2Id { get; set; }
        [DisplayName("ID_Especialidade_3")]
        public Nullable<int> especialidade3Id { get; set; }
    }
}