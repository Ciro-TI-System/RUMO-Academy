using System;
using System.Collections.Generic;
using System.Linq;

namespace MinhaPrimeiraApi.Services
{
    public class ReservaService
    {
        public List<Entidades.Mesa> ConsultarDisponibilidade(short quantidadeCadeiras, DateTime dataInicio, DateTime dataFim)
        {
            var sql = new Conexoes.SqlServer();
            var mesas = sql.ListarMesas();
            var reservas = sql.ListarReservas();


            // tres maneiras de fazer a filtragem da mesa
            // expressao lambda
            //mesas = mesas.Where(x => x.QuantidadeCadeiras == quantidadeCadeiras).ToList();
            // loop raiz
            //mesas = FiltrarPorQuantidadeCadeiras(mesas, quantidadeCadeiras);
            // iQueryable linq
            mesas = (from m in mesas
                     where m.QuantidadeCadeiras == quantidadeCadeiras
                     select m).ToList();


            // expressao lambda
            //reservas = reservas
            //    .Where(
            //    x =>
            //        (
            //            x.InicioReserva >= dataInicio
            //            && x.InicioReserva <= dataFim
            //        )
            //    &&
            //        (
            //            x.FimReserva >= dataInicio
            //            && x.FimReserva <= dataFim
            //        )
            //    ).ToList();

            // loop raiz
            //reservas = FiltrarReservasPorHorario(reservas, dataInicio, dataFim);

            reservas = (from r in reservas
                        where (
                         r.InicioReserva >= dataInicio
                         && r.InicioReserva <= dataFim
                     )
                 &&
                     (
                         r.FimReserva >= dataInicio
                         && r.FimReserva <= dataFim
                     )
                        select r).ToList();

            mesas = mesas
                .Where(x =>
                !reservas.Any(z => z.Mesa.Identificador == x.Identificador)
                ).ToList();

            return mesas;
        }

        private List<Entidades.Mesa> FiltrarPorQuantidadeCadeiras(List<Entidades.Mesa> mesas, short quantidadeCadeiras)
        {
            var mesasFiltradas = new List<Entidades.Mesa>();

            foreach (var mesa in mesas)
            {
                if (mesa.QuantidadeCadeiras == quantidadeCadeiras)
                {
                    mesasFiltradas.Add(mesa);
                }
            }
            return mesasFiltradas;
        }

        private List<Entidades.Reserva> FiltrarReservasPorHorario(List<Entidades.Reserva> reservas, DateTime dataInicio, DateTime dataFim)
        {
            var reservasFiltradas = new List<Entidades.Reserva>();

            foreach (var reserva in reservas)
            {
                if (
                    (reserva.InicioReserva >= dataInicio
                    && reserva.InicioReserva <= dataFim)
                    &&
                    (reserva.FimReserva >= dataInicio
                    && reserva.FimReserva <= dataFim)
                    )
                {
                    reservasFiltradas.Add(reserva);
                }
            }
            return reservasFiltradas;
        }

        public List<Entidades.Mesa> ConsultarDisponibilidadeV2(short quantidadeCadeiras, DateTime dataInicio, DateTime dataFim)
        {
            var sql = new Conexoes.SqlServer();
            return sql.ConsultarDisponibilidadeMesas(quantidadeCadeiras, dataInicio, dataFim);
        }
    }
}
