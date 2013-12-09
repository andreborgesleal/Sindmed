using App_Dominio.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sindemed.Models
{
    public class LookupUsuarioModel : ListViewUsuario
    {
        public override string action()
        {
            return "../Usuario/ListUsuarioModal";
        }
    }

    public class LookupUsuarioFiltroModel : ListViewUsuario
    {
        public override string action()
        {
            return "../Usuario/_ListUsuarioModal";
        }
    }

    #region Usuario2
    public class LookupUsuario2Model : ListViewUsuario
    {
        public override string action()
        {
            return "../Usuario/ListUsuario2Modal";
        }
    }

    #endregion

}