using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("Chamado")]
    public class Chamado
    {
        [Key]
        [DisplayName("ID")]
        public int chamadoId { get; set; }

        [DisplayName("ID_Associado")]
        public int associadoId { get; set; }

        [DisplayName("ID_Área_Atendimento")]
        public int areaAtendimentoId { get; set; }

        [DisplayName("Motivo")]
        public int chamadoMotivoId { get; set; }

        [DisplayName("Status")]
        public int chamadoStatusId { get; set; }

        [DisplayName("Dt_Chamado")]
        public DateTime dt_chamado { get; set; }

        [DisplayName("Assunto")]
        public string assunto { get; set; }

        [DisplayName("Situação")]
        public string situacao { get; set; }

        [DisplayName("ID_Usuário")]
        public Nullable<int> usuarioId { get; set; }

        [DisplayName("xml")]
        public string mensagemOriginal { get; set; }

        [DisplayName("Associado")]
        public virtual Associado Associado { get; set; }

        [DisplayName("Área_Atendimento")]
        public virtual AreaAtendimento AreaAtendimento { get; set; }

    }
}