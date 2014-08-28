using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;

namespace DWM.Models.Repositories
{
    public class AreaAtuacaoViewModel : Repository
    {
        [DisplayName("ID")]
        public int areaAtuacaoId { get; set; }

        [DisplayName("Descrição *")]
        [Required(ErrorMessage = "Por favor, informe a descrição da área de atuação")]
        [StringLength(50, ErrorMessage = "A descrição da área de atuação deve ter no mínimo 4 e no máximo 50 caracteres", MinimumLength = 4)]
        public string descricao { get; set; }

        [DisplayName("Código")]
        [StringLength(6, ErrorMessage = "O código, se informado, deve ter no máximo 6 caracteres")]
        public string codigo{ get; set; }

    }
}