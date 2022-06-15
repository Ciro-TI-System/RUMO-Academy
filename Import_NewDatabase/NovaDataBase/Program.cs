using System;
using System.Collections.Generic;
using System.IO;

namespace NovaDataBase
{
    internal class Program
    {
        static void Main(string[] args)

        {
            var fluxoAluno = new FluxoAluno();
            fluxoAluno.StartAlunos();

            var fluxoFaturamento = new FluxoFaturamento();
            fluxoFaturamento.StartFaturamentos();

            var fluxoPropaganda = new FluxoPropaganda();
            fluxoPropaganda.StartPropagandas();
        }
    }
}
