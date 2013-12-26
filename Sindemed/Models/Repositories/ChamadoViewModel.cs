using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace Sindemed.Models.Repositories
{
    public class ChamadoViewModel : Repository
    {
        [DisplayName("ID")]
        public int chamadoId { get; set; }

        [DisplayName("Associado ID")]
        public int associadoId { get; set; }

        [DisplayName("Associado")]
        public string nome_associado { get; set; }

        [DisplayName("Área Atendimento")]
        [Required(ErrorMessage = "Por favor, informe a área de atendimento")]
        public int areaAtendimentoId { get; set; }

        public string descricao_areaAtendimento { get; set; }

        [DisplayName("Data")]
        public DateTime dt_chamado { get; set; }

        [DisplayName("Assunto")]
        [Required(ErrorMessage = "Por favor, informe o assunto")]
        [StringLength(50, ErrorMessage = "O Assunto deve ter no mínimo 4 e no máximo 50 caracteres", MinimumLength = 4)]
        public string assunto { get; set; }

        [DisplayName("Situação")]
        public string situacao { get; set; }

        [DisplayName("Atendente")]
        public Nullable<int> usuarioId { get; set; }

        public string nome_usuario { get; set; }

        [DisplayName("Mensagem")]
        [Required(ErrorMessage = "Por favor, informe  a Mensagem")]
        public string mensagemOriginal { get; set; }
    }
}