using App_Dominio.Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DWM.Models.Repositories
{
    public class TipoDocumentoViewModel : Repository
    {
        [DisplayName("ID")]
        public int tipoDocumentoId { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage="Descrição")]
        public string descricao { get; set; }

    }
}