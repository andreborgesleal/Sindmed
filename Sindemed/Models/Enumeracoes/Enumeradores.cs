﻿using App_Dominio.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DWM.Models.Enumeracoes
{
    public class Enumeradores
    {
        public enum Param
        {
            GRUPO_USUARIO = 1,
            AREA_ATENDIMENTO = 2,
            SISTEMA = 3,
            EMPRESA = 4,
            EMAIL_SINDICO = 5,
            HABILITA_EMAIL = 6,
            FUSO_HORARIO = 7
        }

    }
}