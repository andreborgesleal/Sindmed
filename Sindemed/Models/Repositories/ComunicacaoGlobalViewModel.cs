using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sindemed.Models.Repositories
{
    public class ComunicacaoGlobalViewModel
    {
        public IEnumerable<ComunicacaoViewModel> comGrupo { get; set; }
        public IEnumerable<ComunicacaoViewModel> com { get; set; }

    }
}