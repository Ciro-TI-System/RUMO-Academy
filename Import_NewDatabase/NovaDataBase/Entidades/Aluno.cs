using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaDataBase.Entidades
{
    public class Aluno
    {
        public string Identificador { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }
        public Int64 Telefone { get; set; }
        public string Endereco { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

