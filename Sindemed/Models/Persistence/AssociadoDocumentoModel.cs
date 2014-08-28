using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.IO;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using App_Dominio.Enumeracoes;
using DWM.Models.Repositories;
using DWM.Models.Entidades;
using App_Dominio.Security;


namespace DWM.Models.Persistence
{
    public class AssociadoDocumentoModel : ProcessContext<AssociadoDocumento, AssociadoDocumentoViewModel, ApplicationContext>
    {

        public  AssociadoDocumentoModel() : base()
        {
        }

        public AssociadoDocumentoModel(ApplicationContext _db)
        {
            this.db = _db;
        }


        private void CreateTextFile(AssociadoDocumentoViewModel value)
        {
            System.Web.HttpContext web = System.Web.HttpContext.Current;
            if (value.documento != null && !String.IsNullOrWhiteSpace(value.documento))
            {
                var fileName = Path.Combine(web.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Users_Data"]), value.fileId);
                using (StreamWriter sw = File.CreateText(fileName))
                    sw.WriteLine(value.documento);
            }
        }

        #region Métodos da classe CrudContext
        public override AssociadoDocumento ExecProcess(AssociadoDocumentoViewModel value, Crud operation)
        {
            AssociadoDocumento entity = MapToEntity(value);
            switch (operation)
            {
                case Crud.INCLUIR:
                    this.db.Set<AssociadoDocumento>().Add(entity);

                    #region Criar o arquivo fisicamente
                    CreateTextFile(value);
                    #endregion

                    break;
                case Crud.ALTERAR:
                    db.Entry(entity).State = EntityState.Modified;

                    #region Criar o arquivo fisicamente
                    CreateTextFile(value);
                    #endregion

                    break;
                case Crud.EXCLUIR:
                    entity = this.Find(value);
                    if (entity == null)
                        throw new ArgumentException("Objeto não identificado para exclusão");
                    this.db.Set<AssociadoDocumento>().Remove(entity);

                    #region Excluir o arquivo Fisicamente
                    System.Web.HttpContext web = System.Web.HttpContext.Current;
                    File.Delete(Path.Combine(web.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Users_Data"]), value.fileId));
                    #endregion

                    break;
            }

            return entity;
        }

        public override AssociadoDocumento MapToEntity(AssociadoDocumentoViewModel value)
        {
            int _fuso_horario = int.Parse(db.Parametros.Find((int)DWM.Models.Enumeracoes.Enumeradores.Param.FUSO_HORARIO).valor);

            return new AssociadoDocumento()
            {
                fileId = value.fileId,
                associadoId = value.associadoId,
                tipoDocumentoId = value.tipoDocumentoId.Value,
                palavra_chave = value.palavra_chave,
                nomeArquivoOriginal = value.nomeArquivoOriginal,
                dt_arquivo = DateTime.Now.AddHours(_fuso_horario)
            };
        }

        public override AssociadoDocumentoViewModel MapToRepository(AssociadoDocumento entity)
        {
            AssociadoDocumentoViewModel value = new AssociadoDocumentoViewModel()
            {
                fileId = entity.fileId,
                associadoId = entity.associadoId,
                tipoDocumentoId = entity.tipoDocumentoId, 
                palavra_chave = entity.palavra_chave,
                nome = (from ass in db.Associados where ass.associadoId == entity.associadoId select ass).Select(info => info.nome).FirstOrDefault() ?? "",
                nomeArquivoOriginal = entity.nomeArquivoOriginal,
                dt_arquivo = entity.dt_arquivo,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };

            System.Web.HttpContext web = System.Web.HttpContext.Current;
            System.IO.FileInfo file = new FileInfo(Path.Combine(web.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Users_Data"]), value.fileId));

            if ((file.Extension.Contains("htm") || file.Extension.Contains("txt")) && file.Exists)
                value.documento = File.ReadAllText(Path.Combine(web.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Users_Data"]), value.fileId));

            return value;
        }

        public override AssociadoDocumento Find(AssociadoDocumentoViewModel key)
        {
            return db.AssociadoDocumentos.Find(key.fileId);
        }

        public override Validate Validate(AssociadoDocumentoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.associadoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Condômino").ToString();
                value.mensagem.MessageBase = "Campo Condômino deve ser informado.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (value.fileId == "")
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Especialidade").ToString();
                value.mensagem.MessageBase = "Identificador interno do arquivo deve ser informado.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (value.nomeArquivoOriginal != "" && value.tipoDocumentoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Tipo do anexo").ToString();
                value.mensagem.MessageBase = "Campo Tipo do Anexo deve ser informado.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }

        public override AssociadoDocumentoViewModel CreateRepository(System.Web.HttpRequestBase Request)
        {
            App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext> security = new App_Dominio.Security.EmpresaSecurity<App_Dominio.Entidades.SecurityContext>();
            App_Dominio.Entidades.Sessao sessaoCorrente = security.getSessaoCorrente();

            string _associadoId = null;
            if (Request != null)
                _associadoId = Request["associadoId"];

            if (sessaoCorrente.value1 != "0")
                _associadoId = sessaoCorrente.value1;

            AssociadoDocumentoViewModel doc = new AssociadoDocumentoViewModel()
            {
                fileId = String.Format("{0}.htm", Guid.NewGuid().ToString()),
                mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString() }
            };

            if (_associadoId != null)
                using (db = new ApplicationContext())
                {
                    doc.associadoId = int.Parse(_associadoId);
                    doc.nome = db.Associados.Find(int.Parse(_associadoId)).nome;
                }

            return doc;
        }


        #endregion
    }

    public class ListViewAssociadoDocumento : ListViewRepository<AssociadoDocumentoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<AssociadoDocumentoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            int _associadoId = (int)param[0];

            #region Verifica se o associadoId é o mesmo da sessaoCorrente
            Sessao sessaoCorrente = new EmpresaSecurity<SecurityContext>().getSessaoCorrente();
            if (sessaoCorrente.value1 != "0" && sessaoCorrente.value1 != _associadoId.ToString())
                return new List<AssociadoDocumentoViewModel>();
            #endregion

            
            string _descricao = param != null && param.Count() > 1 && param[1] != null ? param[1].ToString() : null;

            return (from ad in db.AssociadoDocumentos
                    join ass in db.Associados on ad.associadoId equals ass.associadoId
                    join tag in db.TipoDocumentos on ad.tipoDocumentoId equals tag.tipoDocumentoId
                    where ad.associadoId == _associadoId &&
                          (_descricao == null || String.IsNullOrEmpty(_descricao) || ad.nomeArquivoOriginal.StartsWith(_descricao.Trim()) || ad.palavra_chave.Contains(_descricao) || tag.descricao.Contains(_descricao)) 
                    orderby ad.dt_arquivo descending
                    select new AssociadoDocumentoViewModel
                    {
                        associadoId = ad.associadoId,
                        fileId = ad.fileId,
                        tipoDocumentoId = ad.tipoDocumentoId,
                        tag = tag.descricao,
                        palavra_chave = ad.palavra_chave,
                        nomeArquivoOriginal = ad.nomeArquivoOriginal,
                        dt_arquivo = ad.dt_arquivo,
                        nome = ass.nome,
                        PageSize = pageSize,
                        TotalCount = (from ad1 in db.AssociadoDocumentos
                                      join ass1 in db.Associados on ad1.associadoId equals ass1.associadoId
                                      join tag1 in db.TipoDocumentos on ad1.tipoDocumentoId equals tag1.tipoDocumentoId
                                      where ad1.associadoId == _associadoId &&
                                            (_descricao == null || String.IsNullOrEmpty(_descricao) || ad1.nomeArquivoOriginal.StartsWith(_descricao.Trim()) || ad1.palavra_chave.Contains(_descricao) || tag1.descricao.Contains(_descricao)) 
                                      orderby ad.nomeArquivoOriginal
                                      select ad1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new AssociadoDocumentoModel().getObject((AssociadoDocumentoViewModel)id);
        }
        #endregion

        public override string action()
        {
            return "ListAssociadoDocumento";
        }

    }
}