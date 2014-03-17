using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System.Collections.Generic;
namespace Sindemed.Models.Repositories
{
    public class EnviarEmailViewModel : Repository
    {
        public IEnumerable<MedicoViewModel> Destinatarios { get; set; }

        [DisplayName("Asunto")]
        [Required(ErrorMessage="Assunto deve ser informado")]
        public string assunto { get; set; }
        
        [DisplayName("Mensagem")]
        [Required(ErrorMessage = "Mensagem deve ser informada")]
        public string mensagemEmail { get; set; }
    }
}