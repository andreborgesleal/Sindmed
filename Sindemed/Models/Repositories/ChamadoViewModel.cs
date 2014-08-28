using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace DWM.Models.Repositories
{
    public class ChamadoViewModel : Repository
    {
        [DisplayName("ID")]
        public int chamadoId { get; set; }

        [DisplayName("Condômino ID")]
        public int associadoId { get; set; }

        [DisplayName("Condômino")]
        public string nome_associado { get; set; }

        [DisplayName("Unidade")]
        public string apto { get; set; }

        [DisplayName("Área Atendimento")]
        [Required(ErrorMessage = "Por favor, informe a área de atendimento")]
        public int areaAtendimentoId { get; set; }

        public string descricao_areaAtendimento { get; set; }

        [DisplayName("Status")]
        public int chamadoStatusId { get; set; }
        public string descricao_status { get; set; }

        [DisplayName("Motivo")]
        public int chamadoMotivoId { get; set; }
        public string descricao_motivo { get; set; }

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

        [DisplayName("Usuário")]
        public string nome_usuario { get; set; }

        [DisplayName("Mensagem")]
        [Required(ErrorMessage = "Por favor, informe  a Mensagem")]
        public string mensagemOriginal { get; set; }
    }
}