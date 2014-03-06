using App_Dominio.Component;
using App_Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sindemed.Models.Repositories
{
    public class RelacaoGeralViewModel : Repository, IReportRepository<RelacaoGeralViewModel>
    {
        public int associadoId { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string crm { get; set; }
        public string email { get; set; }
        public string nome_especialidade { get; set; }
        public Nullable<DateTime> dt_nascimento { get; set; }
        public string sexo { get; set; }
        public string endereco { get; set; }
        public string complementoEnd { get; set; }
        public string cep { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string telParticular1 { get; set; }
        public string telParticular2 { get; set; }
        public string telCom1 { get; set; }
        public string isSindicalizado { get; set; }

        #region métodos da Interface
        public object getValueColumn1()
        {
            return nome;
        }

        public object getValueColumn2()
        {
            return cpf;
        }

        public void ClearColumn1()
        {
            nome = null;
        }

        public void ClearColumn2()
        {
            cpf = null;
        }

        public RelacaoGeralViewModel getKey(object group = null, object subGroup = null)
        {
            return new RelacaoGeralViewModel() 
            { 
                nome = (string)group ?? "", 
                cpf = (string)subGroup ?? ""
            };
        }

        public RelacaoGeralViewModel Create(RelacaoGeralViewModel key, IEnumerable<RelacaoGeralViewModel> list)
        {
            return new RelacaoGeralViewModel();
        }

        #endregion

    }
}