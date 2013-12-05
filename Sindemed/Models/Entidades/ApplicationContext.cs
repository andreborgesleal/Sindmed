using App_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sindemed.Models.Entidades
{
    public class ApplicationContext : App_DominioContext
    {
        public DbSet<Cidade> Cidades { get; set; }
    }
}