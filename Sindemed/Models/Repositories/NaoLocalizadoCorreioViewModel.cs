using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;

namespace Sindemed.Models.Repositories
{
    public class NaoLocalizadoCorreioViewModel : Repository
    {
        [DisplayName("ID")]
        public int correioId { get; set; }

        [DisplayName("Descrição *")]
        [Required(ErrorMessage="Por favor, informe a descrição do motivo")]
        [StringLength(50, ErrorMessage = "A descrição do motivo deve ter no mínimo 4 e no máximo 50 caracteres", MinimumLength=4)]
        public string descricao { get; set; }

        [DisplayName("Código")]
        [StringLength(6, ErrorMessage = "O Código deve ter máximo 6 caracteres")]
        public string codigo { get; set; }

    }
}