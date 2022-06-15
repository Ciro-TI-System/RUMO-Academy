using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaDataBase.Entidades
{
    public class Propaganda
    {
        public string Identificador { get; set; }
        public string EmpresaDivulgadora { get; set; }

        public decimal Custo { get; set; }
        public DateTime DataPropaganda { get; set; }

}
}
