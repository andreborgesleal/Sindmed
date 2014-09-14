using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace DWM.Models.Repositories
{
    public class ComunicacaoViewModel : Repository
    {
        [DisplayName("ID")]
        public int comunicacaoId { get; set; }

        [DisplayName("Data")]
        public DateTime dt_comunicacao { get; set; }

        [DisplayName("Publicação *")]
        [Required(ErrorMessage = "Por favor, informe a data da publicação")]
        public DateTime dt_publicacao { get; set; }

        [DisplayName("Expiração *")]
        [Required(ErrorMessage = "Por favor, informe a data da expiração")]
        public DateTime dt_expiracao { get; set; }

        [DisplayName("Cabeçalho *")]
        [Required(ErrorMessage="Por favor, informe o cabeçalho do comunicado")]
        [StringLength(30, ErrorMessage="O tamanho do cabeçalho deve possuir no máximo 30 caracteres")]
        public string cabecalho { get; set; }

        [DisplayName("Resumo *")]
        [Required(ErrorMessage = "Por favor, informe o resumo do comunicado")]
        [StringLength(160, ErrorMessage = "O tamanho do resumo deve possuir no máximo 160 e no mínimo 140 caracteres", MinimumLength=140)]
        public string resumo { get; set; }

        [DisplayName("Detalhe *")]
        [Required(ErrorMessage = "Por favor, informe o detalhe do comunicado")]
        public string mensagemDetalhada { get; set; }

        [DisplayName("Imagem 300x200")]
        public string arq_imagem_300x200 { get; set; }

        [DisplayName("Imagem 100x100")]
        public string arq_imagem_100x100 { get; set; }

        [DisplayName("Imagem 400x300")]
        public string arq_imagem_400x300 { get; set; }

        public Nullable<int> grupoAssociadoId { get; set; }
    }
}