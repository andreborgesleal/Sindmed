using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using DWM.Models.Repositories;
using DWM.Models.Entidades;
using App_Dominio.Enumeracoes;

namespace DWM.Models.Persistence
{
    public class DocInternoModel : CrudContext<DocInterno, DocInternoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override DocInterno MapToEntity(DocInternoViewModel value)
        {
            return new DocInterno()
            {
                docId = value.docId,
                docFolderId = value.docFolderId,
                arquivo = value.arquivo,
                dt_arquivo = value.dt_arquivo,
                dt_novo = value.dt_novo,
                nome = value.nome,
                descricao = value.descricao,
                ind_enfatizar = value.ind_enfatizar == null || value.ind_enfatizar == "" ? "N" : value.ind_enfatizar
            };
        }

        public override DocInternoViewModel MapToRepository(DocInterno entity)
        {
            return new DocInternoViewModel()
            {
                docId = entity.docId,
                docFolderId = entity.docFolderId,
                nome_pasta = db.DocFolders.Find(entity.docFolderId).descricao,
                arquivo = entity.arquivo,
                dt_arquivo = entity.dt_arquivo,
                dt_novo = entity.dt_novo,
                nome = entity.nome,
                descricao = entity.descricao,
                ind_enfatizar = entity.ind_enfatizar,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override DocInterno Find(DocInternoViewModel key)
        {
            return db.DocInternos.Find(key.docId);
        }

        public override Validate Validate(DocInternoViewModel value, Crud operation)
        {
            if (value.mensagem != null && value.mensagem.Code.HasValue && value.mensagem.Code > 0)
                return value.mensagem;

            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.docFolderId == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Pasta deve ser informada").ToString();
                value.mensagem.MessageBase = "Campo Pasta deve ser informado";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (value.arquivo == null || value.arquivo == "")
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Arquivo deve ser informado").ToString();
                value.mensagem.MessageBase = "Campo arquivo deve ser informado";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (value.descricao.Trim().Length == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Descrição do arquivo").ToString();
                value.mensagem.MessageBase = "Campo descrição deve ser informado";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }

        public override DocInternoViewModel CreateRepository(System.Web.HttpRequestBase Request)
        {
            return new DocInternoViewModel()
            {
                dt_arquivo = DateTime.Today,
                dt_novo = DateTime.Today.AddDays(10),
                descricao = ""
            };
        }
        #endregion
    }

    public class ListViewDocInterno : ListViewRepository<DocInternoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<DocInternoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = null;
            System.Nullable<DateTime> _dt_inicio = null;
            Nullable<int> _docFolderId = null;

            _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            _dt_inicio = param != null && param.Count() > 1 && param[1] != null && (string)param[1] != "" ? DateTime.Parse(param[1].ToString()) : _dt_inicio;
            _docFolderId = param != null && param.Count() > 2 && param[2] != null && param[2].ToString() != "" ? int.Parse(param[2].ToString()) : _docFolderId;

            return (from file in db.DocInternos join folder in db.DocFolders on file.docFolderId equals folder.docFolderId
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || file.nome.Contains(_descricao) || file.descricao.Contains(_descricao)) &&
                          (_dt_inicio == null || file.dt_arquivo >= _dt_inicio) &&
                          (_docFolderId == null || file.docFolderId == _docFolderId)
                    orderby file.dt_arquivo descending
                    select new DocInternoViewModel
                    {
                        docId = file.docId,
                        dt_novo = file.dt_novo,
                        dt_arquivo = file.dt_arquivo,
                        docFolderId = folder.docFolderId,
                        arquivo = file.arquivo,
                        nome = file.nome,
                        descricao = file.descricao,
                        nome_pasta = folder.descricao,
                        ind_enfatizar = file.ind_enfatizar,
                        PageSize = pageSize,
                        TotalCount = (from file1 in db.DocInternos
                                      join folder1 in db.DocFolders on file1.docFolderId equals folder1.docFolderId
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || file1.nome.Contains(_descricao) || file1.descricao.Contains(_descricao)) &&
                                            (_dt_inicio == null || file1.dt_arquivo >= _dt_inicio) &&
                                            (_docFolderId == null || file1.docFolderId == _docFolderId)
                                      select file1).Count()

                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new DocInternoModel().getObject((DocInternoViewModel)id);
        }

        public override string action()
        {
            return "ListParam";
        }

        public override string DivId()
        {
            return "div-list-doc";
        }

        #endregion
    }
}