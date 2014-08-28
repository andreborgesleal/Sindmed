using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWM.Models.Entidades
{
    [Table("AssociadoDocumento")]
    public class AssociadoDocumento
    {
        [Key]
        [DisplayName("ID_Arquivo")]
        public string fileId { get; set; }

        [DisplayName("ID")]
        public Nullable<int> associadoId { get; set; }

        [DisplayName("Tipo_Documento")]
        public int tipoDocumentoId { get; set; }

        [DisplayName("Documento")]
        public string nomeArquivoOriginal { get; set; }

        [DisplayName("Data")]
        public DateTime dt_arquivo { get; set; }

        [DisplayName("Palavra_Chave")]
        public string palavra_chave { get; set; }
    }
}