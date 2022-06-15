using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NovaDataBase
{
    public class FluxoPropaganda
    {
        public void StartPropagandas()
        {
            int quantidadeProcessados = CsvPropagandasParaSql();
            GravarQuantidadeProcessados(quantidadeProcessados);
        }
        private int CsvPropagandasParaSql()
        {
            List<Entidades.Propaganda> alunos = ObterPropagandas();

            Conexoes.SqlServer sql = new Conexoes.SqlServer();

            int quantidadeProcessadas = 0;
            foreach (var propaganda in propagandas)
            {
                sql.InserirPropaganda(propaganda);
                quantidadeProcessadas++;
            }

            return quantidadeProcessadas;
        }
        private List<Entidades.Propaganda> ObterPropagandas()
        {
            string conteudoArquivo = File.ReadAllText(@"C:\\Users\\cirof\\Desktop\\RUMO Academy\\Importacao NovaDataBase\\Propagandas.csv");
            string[] linhas = conteudoArquivo.Split("\n");

            int contadorLinhas = 0;

            List<Entidades.Propaganda> propagandas = new List<Entidades.Propaganda>();
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

                Entidades.Propaganda propaganda = new Entidades.Propaganda();



                string Identificador = colunas[0];
                string EmpresaDivulgadora = colunas[1];
                string Custo = colunas[2];
                string DataPropaganda = colunas[3];


                propaganda.Identificador = Identificador;
                propaganda.EmpresaDivulgadora = EmpresaDivulgadora;
                propaganda.Custo = Convert.ToDecimal(Custo);
                propaganda.DataPropaganda = Convert.ToDateTime(DataPropaganda);

                propagandas.Add(propaganda);
            }

            return propagandas;
        }

        private void GravarQuantidadeProcessados(int qtde)
        {
            string conteudo = "Teste\nQuantidade de registros processados: " + qtde;

            File.WriteAllText("C:\\Users\\cirof\\Desktop\\RUMO Academy\\Importacao NovaDataBase\\Relatorios\\RelatorioPropaganda.txt", conteudo);
        }
    }
}
