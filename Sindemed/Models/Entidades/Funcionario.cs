using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("Funcionario")]
    public class Funcionario
    {
        [Key, Column(Order = 0)]
        [DisplayName("Condomino_ID")]
        public int associadoId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("Funcionario_ID")]
        public int funcionarioId { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("Funcao")]
        public string funcao { get; set; }

        [DisplayName("Sexo")]
        public string sexo { get; set; }

        [DisplayName("RG")]
        public string rg { get; set; }

        [DisplayName("Dia_Semana")]
        public string dia_semana { get; set; }

        [DisplayName("Horario_Inicial")]
        public string horario_ini { get; set; }

        [DisplayName("Horario_Final")]
        public string horario_fim { get; set; }
    }
}