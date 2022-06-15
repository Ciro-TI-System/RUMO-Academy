using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MinhaPrimeiraApi.Controllers
{
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly Services.ReservaService _service;
        public ReservaController()
        {
            _service = new Services.ReservaService();
        }
        /// <summary>
        /// Consulta a disponibilidade de cadeiras
        /// Metodologia Filtragem de Listas via codigo
        /// </summary>
        /// <returns></returns>
        [HttpGet("Reserva/ConsultarDisponibilidade")]
        public List<Entidades.Mesa> ConsultarDisponibilidade(short quantidadeCadeiras, DateTime dataInicio, DateTime dataFim)
        {
            return _service.ConsultarDisponibilidade(quantidadeCadeiras, dataInicio, dataFim);
        }

        /// <summary>
        /// Consulta a disponibilidade de cadeiras
        /// Metodologia Filtragem de Listas via sql
        /// </summary>
        /// <returns></returns>
        [HttpGet("Reserva/ConsultarDisponibilidadeV2")]
        public List<Entidades.Mesa> ConsultarDisponibilidadeV2(short quantidadeCadeiras, DateTime dataInicio, DateTime dataFim)
        {
            return _service.ConsultarDisponibilidadeV2(quantidadeCadeiras, dataInicio, dataFim);
        }
    }
}
