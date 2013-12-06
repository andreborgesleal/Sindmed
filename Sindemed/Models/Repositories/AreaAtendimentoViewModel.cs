﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App_Dominio.Component;
using System;

namespace Sindemed.Models.Repositories
{
    public class AreaAtendimentoViewModel : Repository
    {
        [DisplayName("ID")]
        public int areaAtendimentoId { get; set; }

        [DisplayName("Descrição *")]
        [Required(ErrorMessage = "Por favor, informe a descrição da área de atendimento")]
        [StringLength(50, ErrorMessage = "A descrição da área de atendimento deve ter no mínimo 4 e no máximo 50 caracteres", MinimumLength = 4)]
        public string descricao { get; set; }

        [DisplayName("Código")]
        [StringLength(6, ErrorMessage = "O código, se informado, deve ter no máximo 6 caracteres")]
        public string codigo { get; set; }

        [DisplayName("Atendente 1")]
        [Required(ErrorMessage = "Por favor, informe o Atendente 1")]
        public decimal usuario1Id { get; set; }

        [DisplayName("Atendente 2")]
        public Nullable<decimal> usuario2Id { get; set; }

    }
}