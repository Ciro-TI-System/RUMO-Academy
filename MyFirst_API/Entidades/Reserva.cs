using System;

namespace MinhaPrimeiraApi.Entidades
{
    public class Reserva
    {
        public int Identificador { get; set; }
        public DateTime InicioReserva { get; set; }
        public DateTime FimReserva { get; set; }
        public Mesa Mesa { get; set; }
    }
}
