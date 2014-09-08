using App_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DWM.Models.Entidades
{
    public class ApplicationContext : App_DominioContext
    {
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<EspecialidadeMedica> EspecialidadeMedicas { get; set; }
        public DbSet<GrupoAssociado> GrupoAssociados { get; set; }
        public DbSet<AreaAtuacao> AreaAtuacaos { get; set; }
        public DbSet<AreaAtendimento> AreaAtendimentos { get; set; }
        public DbSet<Comunicacao> Comunicacaos { get; set; }
        public DbSet<Associado> Associados { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<AssociadoGrupo> AssociadoGrupos { get; set; }
        public DbSet<ComunicacaoGrupo> ComunicacaoGrupos { get; set; }
        public DbSet<AssociadoDocumento> AssociadoDocumentos { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<ChamadoMotivo> ChamadoMotivos { get; set; }
        public DbSet<ChamadoStatus> ChamadoStatuss { get; set; }
        public DbSet<DocFolder> DocFolders { get; set; }
        public DbSet<DocInterno> DocInternos { get; set; }
        public DbSet<ComunicacaoClick> ComunicacaoClicks { get; set; }
        public DbSet<DocClick> DocClicks { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<NaoLocalizadoCorreio> NaoLocalizadoCorreios { get; set; }
        public DbSet<Medico> Medicos { get; set; }
    }
}
