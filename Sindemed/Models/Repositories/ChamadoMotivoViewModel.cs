using App_Dominio.Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DWM.Models.Repositories
{
    public class ChamadoMotivoViewModel : Repository
    {
        [DisplayName("ID")]
        public int chamadoMotivoId { get; set; }

        [DisplayName("Descricao")]
        [Required(ErrorMessage = "Descrição")]
        public string descricao { get; set; }

        [DisplayName("Fixo")]
        public string ind_fixo { get; set; }
    }
}