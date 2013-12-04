using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;

namespace StockLite.Models.Repositories
{
    public class FabricanteViewModel : Repository
    {
        [DisplayName("Fabricante ID")]
        public int fabricanteId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Por favor, informe o nome do fabricante")]
        [StringLength(40, ErrorMessage = "O nome do fabricante deve ter no máximo 40 caracteres")]
        public string nome { get; set; }

    }
}