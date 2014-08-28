using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace DWM.Models.Repositories
{
    public class VeiculoViewModel : Repository
    {
        [DisplayName("Condômino ID")]
        public int associadoId { get; set; }

        [DisplayName("Placa")]
        [Required(ErrorMessage="A placa do veículo deve ser informada.")]
        [StringLength(7, ErrorMessage = "Placa deve possuir 7 dígitos", MinimumLength = 7)]
        public string placa { get; set; }

        [DisplayName("Cor")]
        [StringLength(15, ErrorMessage = "Cor do veículo deve ter no máximo 15 caracteres")]
        public string cor { get; set; }

        [DisplayName("Descrição")]
        [StringLength(20, ErrorMessage="Descrição do veículo deve ter no máximo 20 caracteres")]
        public string descricao { get; set; }

        [DisplayName("Marca")]
        [StringLength(20, ErrorMessage = "Marca do veículo deve ter no máximo 20 caracteres")]
        public string marca { get; set; }

        [DisplayName("Condutor")]
        [Required(ErrorMessage="O nome do condutor deve ser informado")]
        [StringLength(40, ErrorMessage = "Condutor do veículo deve ter no máximo 40 caracteres")]
        public string condutor { get; set; }

        [DisplayName("Num_Serie")]
        [StringLength(10, ErrorMessage = "Número de Série deve ter no máximo 10 caracteres")]
        public string num_serie { get; set; }
    }
}