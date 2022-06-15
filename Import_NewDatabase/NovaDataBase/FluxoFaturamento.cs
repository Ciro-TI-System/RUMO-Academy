using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NovaDataBase
{
    public class FluxoFaturamento
    {
        public void StartFaturamentos()
        {
            int quantidadeProcessados = CsvFaturamentosParaSql();
            GravarQuantidadeProcessados(quantidadeProcessados);
        }
        private int CsvFaturamentosParaSql()
        {
            List<Entidades.Faturamento> faturamentos = ObterFaturamentos();

            Conexoes.SqlServer sql = new Conexoes.SqlServer();

            int quantidadeProcessadas = 0;
            foreach (var faturamento in faturamentos)
            {
                sql.InserirFaturamento(faturamento);
                quantidadeProcessadas++;
            }

            return quantidadeProcessadas;
        }
        private List<Entidades.Faturamento> ObterFaturamentos()
        {
            string conteudoArquivo = File.ReadAllText(@"C:\\Users\\cirof\\Desktop\\RUMO Academy\\Importacao NovaDataBase\\Faturamentos.csv");
            string[] linhas = conteudoArquivo.Split("\n");

            int contadorLinhas = 0;

            List<Entidades.Faturamento> faturamentos = new List<Entidades.Faturamento>();
            foreach (var linha in linhas)
            {
                if (contadorLinhas == 0)
                {
                    contadorLinhas++;
                    continue;
                }

                if (linha == "")
                    continue;

                string[] colunas = linha.Split(",");

                Entidades.Faturamento faturamento = new Entidades.Faturamento();



                string Identificador = colunas[0];
                string TotalEntrada = colunas[1];
                string TotalSaida = colunas[2];
                string DiaReferencia = colunas[3];


                faturamento.Identificador = Identificador;
                faturamento.TotalEntrada = Convert.ToDecimal(TotalEntrada);
                faturamento.TotalSaida = Convert.ToDecimal(TotalSaida);
                faturamento.DiaReferencia = Convert.ToDateTime(DiaReferencia);

                faturamentos.Add(faturamento);
            }

            return faturamentos;
        }

        private void GravarQuantidadeProcessados(int qtde)
        {
            string conteudo = "Teste\nQuantidade de registros processados: " + qtde;

            File.WriteAllText("C:\\Users\\cirof\\Desktop\\RUMO Academy\\Importacao NovaDataBase\\Relatorios\\RelatorioFaturamento.txt", conteudo);
        }
    }
}
