using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NovaDataBase
{
    public class FluxoAluno
    {
        public void StartAlunos()
        {
            int quantidadeProcessados = CsvAlunosParaSql();
            GravarQuantidadeProcessados(quantidadeProcessados);
        }
        private int CsvAlunosParaSql()
        {
            List<Entidades.Aluno> alunos = ObterAlunos();

            Conexoes.SqlServer sql = new Conexoes.SqlServer();

            int quantidadeProcessadas = 0;
            foreach (var aluno in alunos)
            {
                sql.InserirAluno(aluno);
                quantidadeProcessadas++;
            }

            return quantidadeProcessadas;
        }
        private List<Entidades.Aluno> ObterAlunos()
        {
            string conteudoArquivo = File.ReadAllText(@"C:\\Users\\cirof\\Desktop\\RUMO Academy\\Importacao NovaDataBase\\AlunosV2.csv");
            string[] linhas = conteudoArquivo.Split("\n");

            int contadorLinhas = 0;

            List<Entidades.Aluno> alunos = new List<Entidades.Aluno>();
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

                Entidades.Aluno aluno = new Entidades.Aluno();

                

                string Identificador = colunas[0];
                string Nome = colunas[1];
                string Email = colunas[2];
                string Telefone = (colunas[3].Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""));
                string Endereco = colunas[4];
                string DataCadastro = colunas[5];


                aluno.Identificador = Identificador;
                aluno.Nome = Nome;
                aluno.Email = Email;
                aluno.Telefone = Convert.ToInt64(Telefone);
                aluno.Endereco = Endereco;
                aluno.DataCadastro = Convert.ToDateTime(DataCadastro);

                alunos.Add(aluno);
            }

            return alunos;
        }

        private void GravarQuantidadeProcessados(int qtde)
        {
            string conteudo = "Teste\nQuantidade de registros processados: " + qtde;

            File.WriteAllText("C:\\Users\\cirof\\Desktop\\RUMO Academy\\Importacao NovaDataBase\\Relatorios\\RelatorioAluno.txt", conteudo);
        }
    }
}
