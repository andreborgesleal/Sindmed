using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;

namespace Sindemed.Models.Repositories
{
    public class GrupoAssociadoViewModel : Repository
    {
        [DisplayName("ID")]
        public int grupoAssociadoId { get; set; }

        [DisplayName("Descrição *")]
        [Required(ErrorMessage = "Por favor, informe a descrição do grupo")]
        [StringLength(50, ErrorMessage = "A descrição do grupo deve ter no mínimo 4 e no máximo 50 caracteres", MinimumLength = 4)]
        public string descricao { get; set; }

        [DisplayName("Objetivo")]
        [StringLength(100, ErrorMessage = "O objetivo do grupo, se informado, deve ter no mínimo 4 e no máximo 100 caracteres", MinimumLength = 4)]
        public string objetivo { get; set; }
    }
}