using App_Dominio.Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DWM.Models.Repositories
{
    public class AssociadoDocumentoViewModel : Repository
    {
        [DisplayName("ID Arquivo")]
        public string fileId { get; set; }
        
        [DisplayName("ID")]
        public Nullable<int> associadoId { get; set; }

        [DisplayName("Condômino")]
        public string nome { get; set; }

        [DisplayName("Arquivo")]
        public string nomeArquivoOriginal { get; set; }

        [DisplayName("Tag")]
        public int? tipoDocumentoId { get; set; }

        public string tag { get; set; }

        [DisplayName("Data")]
        public DateTime dt_arquivo { get; set; }

        [DisplayName("Documento")]
        public string documento { get; set; }

        [DisplayName("Palavra Chave")]
        public string palavra_chave { get; set; }

    }
}