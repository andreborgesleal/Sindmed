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
        public DbSet<NaoLocalizadoCorreio> NaoLocalizadoCorreios { get; set; }
        public DbSet<EspecialidadeMedica> EspecialidadeMedicas { get; set; }
        public DbSet<GrupoAssociado> GrupoAssociados { get; set; }
        public DbSet<AreaAtuacao> AreaAtuacaos { get; set; }
        public DbSet<AreaAtendimento> AreaAtendimentos { get; set; }
        public DbSet<Comunicacao> Comunicacaos { get; set; }
        public DbSet<Associado> Associados { get; set; }
        public DbSet<Medico> Medicos { get; set; }
    }
}
