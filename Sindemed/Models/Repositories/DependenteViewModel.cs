using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace DWM.Models.Repositories
{
    public class DependenteViewModel : Repository
    {
        [DisplayName("Condômino ID")]
        public int associadoId { get; set; }

        [DisplayName("Dependente ID")]
        public int dependenteId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage="Nome do dependente deve ser informado")]
        [StringLength(50, ErrorMessage = "Nome do dependente deve ter no máximo 50 caracteres")]
        public string nome { get; set; }

        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage="E-mail deve ter no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [EmailAddress(ErrorMessage = "Informe o E-mail com um formato válido")]
        public string email { get; set; }

        [DisplayName("Relação Dependência")]
        public string tx_relacao_associado { get; set; }

        [DisplayName("Sexo")]
        public string sexo { get; set; }
    }
}