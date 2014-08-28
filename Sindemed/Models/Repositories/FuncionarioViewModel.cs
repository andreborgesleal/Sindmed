using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace DWM.Models.Repositories
{
    public class FuncionarioViewModel : Repository
    {
        [DisplayName("Condômino ID")]
        public int associadoId { get; set; }

        [DisplayName("Funcionário ID")]
        public int funcionarioId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Nome do funcionário deve ser informado")]
        [StringLength(40, ErrorMessage = "Nome do funcionário deve ter no máximo 40 caracteres")]
        public string nome { get; set; }

        [DisplayName("Função")]
        [Required(ErrorMessage="Função do funcionário deve ser informada")]
        public string funcao { get; set; }

        [DisplayName("Sexo")]
        public string sexo { get; set; }

        [DisplayName("RG")]
        [StringLength(10, ErrorMessage = "RG deve ter no máximo 10 caracteres")]
        public string rg { get; set; }

        [DisplayName("Dia Semana")]
        public string dia_semana { get; set; }

        [DisplayName("Dom")]
        public string dom { get; set; }

        [DisplayName("Seg")]
        public string seg { get; set; }

        [DisplayName("Ter")]
        public string ter { get; set; }

        [DisplayName("Qua")]
        public string qua { get; set; }

        [DisplayName("Qui")]
        public string qui { get; set; }

        [DisplayName("Sex")]
        public string sex { get; set; }

        [DisplayName("Sab")]
        public string sab { get; set; }

        [DisplayName("Entrada")]
        [Required(ErrorMessage="Informar o horário de entrada do funcionário")]
        public string horario_ini { get; set; }

        [DisplayName("Saída")]
        [Required(ErrorMessage = "Informar o horário de saída do funcionário")]
        public string horario_fim { get; set; }
    }
}