using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;

namespace Sindemed.Models.Repositories
{
    public class CidadeViewModel : Repository
    {
        [DisplayName("Cidade ID")]
        public int cidadeId { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "Por favor, informe o nome da cidade")]
        [StringLength(30, ErrorMessage = "O nome da cidade deve ter no máximo 30 caracteres")]
        public string nome { get; set; }

        [DisplayName("Cidade")]
        public string uf { get; set; }
    }
}