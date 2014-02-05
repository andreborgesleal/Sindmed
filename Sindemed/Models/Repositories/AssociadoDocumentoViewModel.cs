using App_Dominio.Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Sindemed.Models.Repositories
{
    public class AssociadoDocumentoViewModel : Repository
    {
        [DisplayName("ID")]
        public int associadoId { get; set; }

        [DisplayName("Associado")]
        public string nome { get; set; }

        [DisplayName("ID Arquivo")]
        public string fileId { get; set; }

        [DisplayName("Arquivo")]
        public string nomeArquivoOriginal { get; set; }

        [DisplayName("Data")]
        public DateTime dt_arquivo { get; set; }

        [DisplayName("Documento")]
        public string documento { get; set; }
    }
}