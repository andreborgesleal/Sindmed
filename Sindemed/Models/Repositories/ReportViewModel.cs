﻿using App_Dominio.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DWM.Models.Repositories
{
    public class ReportViewModel : Repository
    {
        public IDictionary<string, string> header { get; set; }
    }
}