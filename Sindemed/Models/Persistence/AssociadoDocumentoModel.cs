using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.IO;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using App_Dominio.Enumeracoes;
using Sindemed.Models.Repositories;
using Sindemed.Models.Entidades;


namespace Sindemed.Models.Persistence
{
    public class AssociadoDocumentoModel : ProcessContext<AssociadoDocumento, AssociadoDocumentoViewModel, ApplicationContext>
    {
        private void CreateTextFile(AssociadoDocumentoViewModel value)
        {
            System.Web.HttpContext web = System.Web.HttpContext.Current;
            if (!String.IsNullOrWhiteSpace(value.documento))
            {
                var fileName = Path.Combine(web.Server.MapPath("~/App_Data/Users_Data"), value.fileId);
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
                    break;
                case Crud.ALTERAR:
                    db.Entry(entity).State = EntityState.Modified;
                    break;
                case Crud.EXCLUIR:
                    entity = this.Find(value);
                    if (entity == null)
                        throw new ArgumentException("Objeto não identificado para exclusão");
                    this.db.Set<AssociadoDocumento>().Remove(entity);
                    break;
            }

            return entity;
        }

        public override Validate AfterInsert(AssociadoDocumentoViewModel value)
        {
            #region Criar o arquivo fisicamente
            CreateTextFile(value);
            #endregion

            return new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };
        }

        public override Validate AfterUpdate(AssociadoDocumentoViewModel value)
        {
            #region Criar o arquivo fisicamente
            CreateTextFile(value);
            #endregion

            return new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };
        }

        public override Validate AfterDelete(AssociadoDocumentoViewModel value)
        {
            #region Excluir o arquivo Fisicamente
            System.Web.HttpContext web = System.Web.HttpContext.Current;
            File.Delete(Path.Combine(web.Server.MapPath("~/App_Data/Users_Data"), value.fileId));
            #endregion

            return new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };
        }   

        public override AssociadoDocumento MapToEntity(AssociadoDocumentoViewModel value)
        {
            return new AssociadoDocumento()
            {
                associadoId = value.associadoId,
                fileId = value.fileId,
                nomeArquivoOriginal = value.nomeArquivoOriginal,
                dt_arquivo = value.dt_arquivo
            };
        }

        public override AssociadoDocumentoViewModel MapToRepository(AssociadoDocumento entity)
        {
            AssociadoDocumentoViewModel value = new AssociadoDocumentoViewModel()
            {
                associadoId = entity.associadoId,
                fileId = entity.fileId,
                nome = (from ass in db.Associados where ass.associadoId == entity.associadoId select ass).Select(info => info.nome).FirstOrDefault() ?? "",
                nomeArquivoOriginal = entity.nomeArquivoOriginal,
                dt_arquivo = entity.dt_arquivo,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };

            System.Web.HttpContext web = System.Web.HttpContext.Current;
            File.Delete(Path.Combine(web.Server.MapPath("~/App_Data/Users_Data"), value.fileId));

            System.IO.FileInfo file = new FileInfo(Path.Combine(web.Server.MapPath("~/App_Data/Users_Data"), value.fileId));

            if (file.Extension.Contains(".htm") || file.Extension.Contains(".txt"))
                value.documento = File.ReadAllText(Path.Combine(web.Server.MapPath("~/App_Data/Users_Data"), value.fileId));

            return value;
        }

        public override AssociadoDocumento Find(AssociadoDocumentoViewModel key)
        {
            return db.AssociadoDocumentos.Find(key.associadoId, key.fileId);
        }

        public override Validate Validate(AssociadoDocumentoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.associadoId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Associado").ToString();
                value.mensagem.MessageBase = "Campo Associado deve ser informado.";
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


            return value.mensagem;
        }

        public AssociadoDocumentoViewModel CreateRepository()
        {
            AssociadoDocumentoViewModel doc = new AssociadoDocumentoViewModel()
            {
                fileId = String.Format("{0}.htm", Guid.NewGuid().ToString())
            };
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
            string _descricao = param != null && param.Count() > 1 && param[1] != null ? param[1].ToString() : null;

            return (from ad in db.AssociadoDocumentos
                    join ass in db.Associados on ad.associadoId equals ass.associadoId
                    where ad.associadoId == _associadoId &&
                          (_descricao == null || String.IsNullOrEmpty(_descricao) || ad.nomeArquivoOriginal.StartsWith(_descricao.Trim()))
                    orderby ad.nomeArquivoOriginal
                    select new AssociadoDocumentoViewModel
                    {
                        associadoId = ad.associadoId,
                        fileId = ad.fileId,
                        nomeArquivoOriginal = ad.nomeArquivoOriginal,
                        dt_arquivo = ad.dt_arquivo,
                        nome = ass.nome,
                        PageSize = pageSize,
                        TotalCount = (from ad1 in db.AssociadoDocumentos
                                      join ass1 in db.Associados on ad1.associadoId equals ass1.associadoId
                                      where ad1.associadoId == _associadoId &&
                                            (_descricao == null || String.IsNullOrEmpty(_descricao) || ad1.nomeArquivoOriginal.StartsWith(_descricao.Trim()))
                                      orderby ad.nomeArquivoOriginal
                                      select ad1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new AssociadoDocumentoModel().getObject((AssociadoDocumentoViewModel)id);
        }
        #endregion
    }
}