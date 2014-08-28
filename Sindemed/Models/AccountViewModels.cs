using DWM.Models.Repositories;
using App_Dominio.Component;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DWM.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)] 
        [Display(Name = "Confirmação da nova senha")]
        [Compare("Confirmação de senha", ErrorMessage = "As senhas não combinam.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage="O campo login é de preenhcimento obrigatório e deve ser um e-mail válido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [EmailAddress(ErrorMessage="Informe o login com um formato de e-mail válido")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Senha é de preenhcimento obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : AssociadoViewModel
    {
        [Required(ErrorMessage="Senha deve ser informada")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "A senha deve possuir no mínimo 6 dígitos e no máximo 20 dígitos", MinimumLength = 6)]
        public string senha { get; set; }

        [Required(ErrorMessage = "Confirmação de senha deve ser informada")]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        [Compare("senha", ErrorMessage = "As senhas não conferem.")]
        public string confirmacaoSenha { get; set; }

        [DisplayName("Código de Barras")]
        public string codigo_barra { get; set; }
    }
}
