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


    public class LookupUsuarioMedicoModel : ListViewUsuario
    {
        public override string action()
        {
            return "../Usuario/ListUsuarioMedicoModal";
        }
    }

    public class LookupUsuarioMedicoFiltroModel : ListViewUsuario
    {
        public override string action()
        {
            return "../Usuario/_ListUsuarioMedicoModal";
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