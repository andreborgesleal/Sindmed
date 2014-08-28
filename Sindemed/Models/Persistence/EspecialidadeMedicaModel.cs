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
    public class EspecialidadeMedicaModel : CrudContext<EspecialidadeMedica, EspecialidadeMedicaViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override EspecialidadeMedica MapToEntity(EspecialidadeMedicaViewModel value)
        {
            return new EspecialidadeMedica()
            {
                especialidadeId = value.especialidadeId,
                descricao = value.descricao,
                codigo = value.codigo
            };
        }

        public override EspecialidadeMedicaViewModel MapToRepository(EspecialidadeMedica entity)
        {
            return new EspecialidadeMedicaViewModel()
            {
                especialidadeId = entity.especialidadeId,
                descricao = entity.descricao,
                codigo = entity.codigo,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };
        }

        public override EspecialidadeMedica Find(EspecialidadeMedicaViewModel key)
        {
            return db.EspecialidadeMedicas.Find(key.especialidadeId);
        }

        public override Validate Validate(EspecialidadeMedicaViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.descricao.Trim().Length == 0)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Descrição").ToString();
                value.mensagem.MessageBase = "Campo Descrição deve ser informado";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }

        #endregion
    }

    public class ListViewEspecialidadeMedica : ListViewRepository<EspecialidadeMedicaViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<EspecialidadeMedicaViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from esp in db.EspecialidadeMedicas
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || esp.descricao.StartsWith(_descricao.Trim()))
                    orderby esp.descricao
                    select new EspecialidadeMedicaViewModel
                    {
                        especialidadeId = esp.especialidadeId,
                        descricao = esp.descricao,
                        codigo = esp.codigo,
                        PageSize = pageSize,
                        TotalCount = (from esp1 in db.EspecialidadeMedicas
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || esp1.descricao.StartsWith(_descricao.Trim()))
                                      select esp1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new EspecialidadeMedicaModel().getObject((EspecialidadeMedicaViewModel)id);
        }
        #endregion
    }

    public class LookupEspecialidadeMedica1Model : ListViewEspecialidadeMedica
    {
        public override string action()
        {
            return "../Especialidade/ListEspecialidadeMedica1Modal";
        }
    }

    public class LookupEspecialidadeMedica2Model : ListViewEspecialidadeMedica
    {
        public override string action()
        {
            return "../Especialidade/ListEspecialidadeMedica2Modal";
        }
    }

    public class LookupEspecialidadeMedica3Model : ListViewEspecialidadeMedica
    {
        public override string action()
        {
            return "../Especialidade/ListEspecialidadeMedica3Modal";
        }
    }

    public class LookupEspecialidadeMedicaFiltroModel : ListViewEspecialidadeMedica
    {
        public override string action()
        {
            return "../Especialidade/_ListEspecialidadeMedicaModal";
        }
    }
}