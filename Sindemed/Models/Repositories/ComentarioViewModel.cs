using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;
using System.Collections.Generic;

namespace DWM.Models.Repositories
{
    public class ComentarioViewModel : Repository
    {
        [DisplayName("ID")]
        public int comentarioId { get; set; }

        [DisplayName("Associado")]
        [Required(ErrorMessage="ID do associado deve ser informado")]
        public int associadoId { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("Avatar")]
        public string avatar { get; set; }

        [DisplayName("CRM")]
        public string CRM { get; set; }

        [DisplayName("Comunicado ID")]
        [Required(ErrorMessage="ID do comunicado deve ser informado")]
        public int comunicacaoId { get; set; }

        [DisplayName("Data")]
        public DateTime dt_comentario { get; set; }

        [DisplayName("Comentário")]
        [Required(ErrorMessage="Comentário deve ser informado")]
        public string descricao { get; set; }

        public ComunicacaoViewModel comunicacaoViewModel { get; set; }

        public IEnumerable<ComentarioViewModel> Comentarios { get; set; }
    }
}