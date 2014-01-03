using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace Sindemed.Models.Repositories
{
    public class ComunicacaoViewModel : Repository
    {
        [DisplayName("ID")]
        public int comunicacaoId { get; set; }

        [DisplayName("Data *")]
        [Required(ErrorMessage = "Por favor, informe a data do cadastro")]
        public DateTime dt_comunicacao { get; set; }

        [DisplayName("Cabeçalho *")]
        [Required(ErrorMessage="Por favor, informe o cabeçalho do comunicado")]
        [StringLength(20, ErrorMessage="O tamanho do cabeçalho deve possuir no máximo 20 caracteres")]
        public string cabecalho { get; set; }

        [DisplayName("Resumo *")]
        [Required(ErrorMessage = "Por favor, informe o resumo do comunicado")]
        [StringLength(500, ErrorMessage = "O tamanho do resumo deve possuir no máximo 500 e no mínimo 30 caracteres", MinimumLength=30)]
        public string resumo { get; set; }

        [DisplayName("Detalhe *")]
        [Required(ErrorMessage = "Por favor, informe o detalhe do comunicado")]
        public string mensagemDetalhada { get; set; }

        public Nullable<int> grupoAssociadoId { get; set; }
    }
}