using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DWM.Models.Repositories
{
    public class ComunicacaoGlobalViewModel
    {
        public IEnumerable<ComunicacaoViewModel> comGrupo { get; set; }
        public IEnumerable<ComunicacaoViewModel> com { get; set; }
        public IEnumerable<DocInternoViewModel> docInterno { get; set; }
        public IEnumerable<DocInternoViewModel> favoritos { get; set; }
    }
}