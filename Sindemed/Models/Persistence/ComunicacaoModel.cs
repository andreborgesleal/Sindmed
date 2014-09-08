using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using DWM.Models.Repositories;
using DWM.Models.Entidades;
using App_Dominio.Enumeracoes;
using App_Dominio.Security;

namespace DWM.Models.Persistence
{
    public class ComunicacaoModel : CrudContext<Comunicacao, ComunicacaoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override Comunicacao MapToEntity(ComunicacaoViewModel value)
        {
            return new Comunicacao()
            {
                comunicacaoId = value.comunicacaoId,
                dt_comunicacao = value.dt_comunicacao,
                dt_publicacao = value.dt_publicacao,
                dt_expiracao = value.dt_expiracao,
                cabecalho = value.cabecalho,
                resumo = value.resumo,
                mensagemDetalhada = value.mensagemDetalhada,
                arq_imagem_100x100 = value.arq_imagem_100x100,
                arq_imagem_300x200 = value.arq_imagem_300x200,
                arq_imagem_400x300 = value.arq_imagem_400x300
            };
        }

        public override ComunicacaoViewModel MapToRepository(Comunicacao entity)
        {
            return new ComunicacaoViewModel()
            {
                comunicacaoId = entity.comunicacaoId,
                dt_comunicacao = entity.dt_comunicacao,
                dt_publicacao = entity.dt_publicacao,
                dt_expiracao = entity.dt_expiracao,
                cabecalho = entity.cabecalho,
                resumo = entity.resumo,
                mensagemDetalhada = entity.mensagemDetalhada,
                arq_imagem_300x200 = entity.arq_imagem_300x200,
                arq_imagem_100x100 = entity.arq_imagem_100x100,
                arq_imagem_400x300 = entity.arq_imagem_400x300,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override Comunicacao Find(ComunicacaoViewModel key)
        {
            return db.Comunicacaos.Find(key.comunicacaoId);
        }

        public override Validate Validate(ComunicacaoViewModel value, Crud operation)
        {
            if (value.mensagem != null && value.mensagem.Code.HasValue && value.mensagem.Code > 0)
                return value.mensagem;

            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.dt_publicacao >= value.dt_expiracao)
            {
                value.mensagem.Code = 11;
                value.mensagem.Message = MensagemPadrao.Message(11, "Dt.Expiração", "Dt.Publicação").ToString();
                value.mensagem.MessageBase = "Data de publicação inválida.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }

        public override ComunicacaoViewModel CreateRepository(System.Web.HttpRequestBase Request)
        {
            return new ComunicacaoViewModel() { 
                dt_comunicacao = DateTime.Now, 
                dt_publicacao = DateTime.Today,
                dt_expiracao = DateTime.Today.AddMonths(1),
                arq_imagem_300x200 = "default_300x200.png",
                arq_imagem_100x100 = "default_100x100.jpg",
                arq_imagem_400x300 = "default_400x300.jpg"
            };
        }
        #endregion
    }

    public class ListViewComunicacao : ListViewRepository<ComunicacaoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<ComunicacaoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["Comunicados"].Replace("~", "..") + "/";

            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from com in db.Comunicacaos 
                    join comGrupo in db.ComunicacaoGrupos on com.comunicacaoId equals comGrupo.comunicacaoId into COM
                    from comGrupo in COM.DefaultIfEmpty()
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || com.cabecalho.StartsWith(_descricao.Trim()) || com.resumo.StartsWith(_descricao.Trim()) || com.mensagemDetalhada.StartsWith(_descricao.Trim()))
                    orderby com.dt_publicacao descending
                    select new ComunicacaoViewModel
                    {
                        comunicacaoId = com.comunicacaoId,
                        dt_comunicacao = com.dt_comunicacao,
                        dt_publicacao = com.dt_publicacao,
                        dt_expiracao = com.dt_expiracao,
                        cabecalho = com.cabecalho,
                        resumo = com.resumo,
                        mensagemDetalhada = com.mensagemDetalhada,
                        arq_imagem_300x200 = path + com.arq_imagem_300x200,
                        arq_imagem_100x100 = path + com.arq_imagem_100x100,
                        arq_imagem_400x300 = path + com.arq_imagem_400x300,
                        grupoAssociadoId = comGrupo.grupoAssociadoId,
                        PageSize = pageSize,
                        TotalCount = (from com1 in db.Comunicacaos
                                      join comGrupo1 in db.ComunicacaoGrupos on com1.comunicacaoId equals comGrupo1.comunicacaoId into COM1
                                      from comGrupo1 in COM1.DefaultIfEmpty()
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || com1.cabecalho.StartsWith(_descricao.Trim()) || com1.resumo.StartsWith(_descricao.Trim()) || com1.mensagemDetalhada.StartsWith(_descricao.Trim()))
                                      select com1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new ComunicacaoModel().getObject((ComunicacaoViewModel)id);
        }
        #endregion
    }

    public class ListViewComunicacaoGrupoEspecifico : ListViewRepository<ComunicacaoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<ComunicacaoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            int _usuarioId = security.getUsuario().usuarioId;

            return (from ass in db.Associados
                    join asg in db.AssociadoGrupos on ass.associadoId equals asg.associadoId
                    join gas in db.GrupoAssociados on asg.grupoAssociadoId equals gas.grupoAssociadoId
                    join cog in db.ComunicacaoGrupos on gas.grupoAssociadoId equals cog.grupoAssociadoId
                    join com in db.Comunicacaos on cog.comunicacaoId equals com.comunicacaoId
                    where ass.usuarioId == _usuarioId
                    orderby com.dt_publicacao descending
                    select new ComunicacaoViewModel
                    {
                        comunicacaoId = com.comunicacaoId,
                        dt_comunicacao = com.dt_comunicacao,
                        dt_publicacao = com.dt_publicacao,
                        dt_expiracao = com.dt_expiracao,
                        cabecalho = com.cabecalho,
                        resumo = com.resumo,
                        mensagemDetalhada = com.mensagemDetalhada,
                        arq_imagem_300x200 = com.arq_imagem_300x200,
                        arq_imagem_100x100 = com.arq_imagem_100x100,
                        arq_imagem_400x300 = com.arq_imagem_400x300,
                        PageSize = pageSize,
                        TotalCount = (from ass1 in db.Associados
                                      join asg1 in db.AssociadoGrupos on ass1.associadoId equals asg1.associadoId
                                      join gas1 in db.GrupoAssociados on asg1.grupoAssociadoId equals gas1.grupoAssociadoId
                                      join cog1 in db.ComunicacaoGrupos on gas1.grupoAssociadoId equals cog1.grupoAssociadoId
                                      join com1 in db.Comunicacaos on cog1.comunicacaoId equals com1.comunicacaoId
                                      where ass1.usuarioId == _usuarioId
                                      orderby com1.dt_comunicacao descending
                                      select com1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new ComunicacaoGrupoModel().getObject((ComunicacaoGrupoViewModel)id);
        }
        #endregion
    }

    public class LookupComunicacaoModel : ListViewComunicacao
    {
        public override string action()
        {
            return "../ComunicadoGeral/ListComunicacaoModal";
        }
    }

    public class LookupComunicacaoFiltroModel : ListViewComunicacao
    {
        public override string action()
        {
            return "../ComunicadoGeral/_ListComunicacaoModal";
        }
    }

}