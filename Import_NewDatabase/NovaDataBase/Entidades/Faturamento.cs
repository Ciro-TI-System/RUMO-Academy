using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaDataBase.Entidades
{
    public class Faturamento
    {
        public string Identificador { get; set; }
        public decimal TotalEntrada { get; set; }

        public decimal TotalSaida { get; set; }
        public DateTime DiaReferencia { get; set; }
        
    }
}
