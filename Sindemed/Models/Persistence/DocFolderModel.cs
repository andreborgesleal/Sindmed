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
    public class DocFolderModel : CrudContext<DocFolder, DocFolderViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override DocFolder MapToEntity(DocFolderViewModel value)
        {
            return new DocFolder()
            {
                docFolderId = value.docFolderId,
                descricao = value.descricao.ToUpper(),
                ind_fixo = value.ind_fixo == null || value.ind_fixo == "" ? "N" : value.ind_fixo
            };
        }

        public override DocFolderViewModel MapToRepository(DocFolder entity)
        {
            if (entity != null)
                return new DocFolderViewModel()
                {
                    docFolderId = entity.docFolderId,
                    descricao = entity.descricao,
                    ind_fixo = entity.ind_fixo,
                    mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
                };
            else
                return new DocFolderViewModel()
                {
                    mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
                };
        }

        public override DocFolder Find(DocFolderViewModel key)
        {
            return db.DocFolders.Find(key.docFolderId);
        }

        public override Validate Validate(DocFolderViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.descricao.Trim().Length == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Descrição da pasta").ToString();
                value.mensagem.MessageBase = "Campo descrição deve ser informado";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            if (value.ind_fixo == "S")
            {
                value.mensagem.Code = 4;
                value.mensagem.Message = MensagemPadrao.Message(4, "Pasta", "Este registro é de uso interno do sistema e não pode ser modificado nem excluído.").ToString();
                value.mensagem.MessageBase = "Esta pasta não pode ser excluída nem editada porque é de uso interno do sistema.";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }


            if (operation == Crud.EXCLUIR)
            {
                if (db.DocInternos.Where(info => info.docFolderId == value.docFolderId).Count() > 0)
                {
                    value.mensagem.Code = 16;
                    value.mensagem.Message = MensagemPadrao.Message(16).ToString();
                    value.mensagem.MessageBase = "Esta pasta não pode ser exluída porque tem documentos dentro dela. Exclua os documentos para depois excluir esta pasta.";
                    value.mensagem.MessageType = MsgType.WARNING;
                    return value.mensagem;
                }
            }
            else if (db.DocFolders.Where(info => info.descricao == value.descricao && (operation == Crud.INCLUIR || operation == Crud.ALTERAR && info.docFolderId != value.docFolderId)).Count() > 0)
            {
                value.mensagem.Code = 40;
                value.mensagem.Message = MensagemPadrao.Message(40, "Descrição").ToString();
                value.mensagem.MessageBase = "Já existe uma pasta com essa descrição: " + value.descricao;
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }

        #endregion
    }

    public class ListViewDocFolder : ListViewRepository<DocFolderViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<DocFolderViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from folder in db.DocFolders
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || folder.descricao.StartsWith(_descricao.Trim()))
                    orderby folder.descricao
                    select new DocFolderViewModel
                    {
                        docFolderId = folder.docFolderId,
                        descricao = folder.descricao,
                        ind_fixo = folder.ind_fixo,
                        PageSize = pageSize,
                        TotalCount = (from folder1 in db.DocFolders
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || folder1.descricao.StartsWith(_descricao.Trim()))
                                      select folder1).Count()

                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            // Observação: O id passado como argumento deverá ser a descrição da pasta
            using (db = getContextInstance())
                return new DocFolderModel().MapToRepository(db.DocFolders.Where(info => info.descricao == id.ToString()).FirstOrDefault());
        }
        #endregion
    }

}