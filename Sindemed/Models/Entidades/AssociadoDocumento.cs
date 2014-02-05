using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sindemed.Models.Entidades
{
    [Table("AssociadoDocumento")]
    public class AssociadoDocumento
    {
        [Key, Column(Order = 0)]
        [DisplayName("ID")]
        public int associadoId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("ID_Arquivo")]
        public string fileId { get; set; }

        [DisplayName("Documento")]
        public string nomeArquivoOriginal { get; set; }

        [DisplayName("Data")]
        public DateTime dt_arquivo { get; set; }

    }
}