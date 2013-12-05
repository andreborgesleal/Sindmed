using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;

namespace Sindemed.Models.Repositories
{
    public class EspecialidadeMedicaViewModel : Repository
    {
        [DisplayName("ID")]
        public int especialidadeId { get; set; }

        [DisplayName("Descrição *")]
        [Required(ErrorMessage = "Por favor, informe a descrição da especialidade")]
        [StringLength(50, ErrorMessage = "A descrição da especialidade deve ter no mínimo 4 e no máximo 50 caracteres", MinimumLength = 4)]
        public string descricao { get; set; }

        [DisplayName("Código")]
        [StringLength(6, ErrorMessage = "O Código deve ter máximo 6 caracteres")]
        public string codigo { get; set; }
    }
}