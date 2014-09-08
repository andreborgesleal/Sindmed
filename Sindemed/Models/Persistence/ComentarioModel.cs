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
    public class ComentarioModel : CrudContext<Comentario, ComentarioViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override Comentario MapToEntity(ComentarioViewModel value)
        {
            return new Comentario()
            {
                comentarioId = value.comentarioId,
                dt_comentario = value.dt_comentario,
                associadoId = value.associadoId,
                comunicacaoId = value.comunicacaoId,
                descricao = value.descricao
            };
        }

        public override ComentarioViewModel MapToRepository(Comentario entity)
        {
            return new ComentarioViewModel()
            {
                comentarioId = entity.comentarioId,
                dt_comentario = entity.dt_comentario,
                associadoId = entity.associadoId,
                nome = db.Associados.Find(entity.associadoId).nome,
                CRM = db.Medicos.Find(entity.associadoId).CRM,
                comunicacaoId = entity.comunicacaoId,
                descricao = entity.descricao,
                comunicacaoViewModel = new ComunicacaoViewModel(),
                Comentarios = new List<ComentarioViewModel>(),
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override Comentario Find(ComentarioViewModel key)
        {
            return db.Comentarios.Find(key.comentarioId);
        }

        public override Validate Validate(ComentarioViewModel value, Crud operation)
        {
            if (value.mensagem != null && value.mensagem.Code.HasValue && value.mensagem.Code > 0)
                return value.mensagem;

            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (sessaoCorrente.value1 == "0")
            {
                value.mensagem.Code = 45;
                value.mensagem.Message = MensagemPadrao.Message(45).ToString();
                value.mensagem.MessageBase = "Usuário precisa ser um associado para comentar esta publicação.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (value.descricao == null || value.descricao.Trim().Length == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Comentário").ToString();
                value.mensagem.MessageBase = "Comentário deve ser informado.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }

        public override ComentarioViewModel CreateRepository(System.Web.HttpRequestBase Request)
        {
            int comunicacaoId = int.Parse(Request["comunicacaoId"]);
            return Create(comunicacaoId);
        }
        #endregion

        #region Métodos customizados
        public ComentarioViewModel Create(int comunicacaoId)
        {
            ComunicacaoModel com = new ComunicacaoModel();
            ListViewComentario listCom = new ListViewComentario();
            string path = System.Configuration.ConfigurationManager.AppSettings["Avatar"].Replace("~", "");

            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();

            using (ApplicationContext db = new ApplicationContext())
                if (sessaoCorrente.value1 != "0")
                    return new ComentarioViewModel()
                    {
                        dt_comentario = DateTime.Now,
                        associadoId = int.Parse(sessaoCorrente.value1),
                        nome = db.Associados.Find(int.Parse(sessaoCorrente.value1)).nome,
                        avatar = db.Associados.Find(int.Parse(sessaoCorrente.value1)).avatar != null ? path + "/" + db.Associados.Find(int.Parse(sessaoCorrente.value1)).avatar : null,
                        CRM = db.Medicos.Find(int.Parse(sessaoCorrente.value1)).CRM,
                        comunicacaoViewModel = (ComunicacaoViewModel)com.getObject(new ComunicacaoViewModel() { comunicacaoId = comunicacaoId }),
                        Comentarios = (List<ComentarioViewModel>)listCom.ListRepository(0, 50, comunicacaoId.ToString())
                    };
                else
                    return new ComentarioViewModel()
                    {
                        dt_comentario = DateTime.Now,
                        comunicacaoViewModel = (ComunicacaoViewModel)com.getObject(new ComunicacaoViewModel() { comunicacaoId = comunicacaoId }),
                        Comentarios = (List<ComentarioViewModel>)listCom.ListRepository(0, 50, comunicacaoId.ToString())
                    };
        }
        #endregion
    }

    public class ListViewComentario : ListViewRepository<ComentarioViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<ComentarioViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["Avatar"].Replace("~", "");
            int _comunicacaoId = int.Parse(param[0].ToString());
            return (from com in db.Comentarios
                    join ass in db.Associados on com.associadoId equals ass.associadoId 
                    join med in db.Medicos on ass.associadoId equals med.associadoId
                    where com.comunicacaoId == _comunicacaoId
                    orderby com.dt_comentario 
                    select new ComentarioViewModel
                    {
                        comentarioId = com.comentarioId,
                        comunicacaoId = com.comunicacaoId,
                        dt_comentario = com.dt_comentario,
                        associadoId = com.associadoId,
                        nome = ass.nome,
                        avatar = ass.avatar == null ? null : path + "/" + ass.avatar,
                        CRM = med.CRM,
                        descricao = com.descricao,
                        PageSize = pageSize,
                        TotalCount = (from com1 in db.Comentarios
                                      join ass1 in db.Associados on com1.associadoId equals ass1.associadoId
                                      where com1.comunicacaoId == _comunicacaoId
                                      select com1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new ComentarioModel().getObject((ComentarioViewModel)id);
        }
        #endregion
    }

}
