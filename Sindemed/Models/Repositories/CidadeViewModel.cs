using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;

namespace Sindemed.Models.Repositories
{
    public class CidadeViewModel : Repository
    {
        [DisplayName("Cidade ID")]
        public int cidadeId { get; set; }

        [DisplayName("Cidade *")]
        [Required(ErrorMessage = "Por favor, informe o nome da cidade")]
        [StringLength(30, ErrorMessage = "O nome da cidade deve ter no mínimo 4 e no máximo 30 caracteres", MinimumLength=4)]
        public string nome { get; set; }

        [DisplayName("UF")]
        [StringLength(2, ErrorMessage = "A UF deve possuir 2 caracateres", MinimumLength=2)]
        public string uf { get; set; }
    }
}