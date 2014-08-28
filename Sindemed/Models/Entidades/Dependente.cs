using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("Dependente")]
    public class Dependente
    {
        [Key, Column(Order = 0)]
        [DisplayName("Condomino_ID")]
        public int associadoId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("Dependente_ID")]
        public int dependenteId { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Relacao_Dependencia")]
        public string tx_relacao_associado { get; set; }

        [DisplayName("Sexo")]
        public string sexo { get; set; }
    }
}

