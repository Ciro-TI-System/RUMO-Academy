using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace NovaDataBase.Conexoes
{
    public class SqlServer
    {
        private readonly SqlConnection _conexao;

        //Preencher a variável (nesse caso "SqlConnection" somente leitura e privada) e prepara-la para uma conexão com a base de dados.
        public SqlServer()
        {
             /*Exemplo quando é acessado de servidor local e autenticado pelo próprio Windows
              * 
            this._conexão = new SqlConnection (@"Server=localhost,Database=BaseDoUsuário,Trusted_Connection=True;");*/
            string stringConexao = File.ReadAllText("C:\\Users\\cirof\\Desktop\\RUMO Academy\\RUMO Soluções - Credenciais\\Nova DataBase\\StringConexao_aluno.txt");
            _conexao = new SqlConnection(stringConexao);
        }


            //INSERT na tabela Aluno
            public void InserirAluno(Entidades.Aluno aluno)
        {
            try
            {
                // abre a conexao
                _conexao.Open();

                // apenas a query de insert
                string query = @"INSERT INTO Aluno
                                       (Identificador
                                       ,Nome
                                       ,Email
                                       ,Telefone
                                       ,Endereco
                                       ,DataCadastro)
                                 VALUES
                                       (@Identificador
                                       ,@Nome
                                       ,@Email
                                       ,@Telefone
                                       ,@Endereco
                                       ,@DataCadastro);";

                // recebe a conexao para preparar instrucoes a serem enviadas para a base
                using (var cmd = new SqlCommand(query, _conexao))
                {
                    // adiciona o parametro e valor mapeado na query acima
                    cmd.Parameters.AddWithValue("@Identificador", aluno.Identificador);
                    cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                    cmd.Parameters.AddWithValue("@Email", aluno.Email);
                    cmd.Parameters.AddWithValue("@Telefone", aluno.Telefone);
                    cmd.Parameters.AddWithValue("@Endereço", aluno.Endereco);
                    cmd.Parameters.AddWithValue("@DataCadastro", aluno.DataCadastro);

                    // envia o insert para o banco de dados
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                // fecha a conexao
                _conexao.Close();
            }
        }

            //UPDATE na tabela Aluno
            public void AtualizarAluno(Entidades.Aluno aluno)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Aluno
                                   SET Nome = @Nome
                                      ,Email = @Email
                                      ,Telefone = @Telefone
                                      ,Endereco = @Endereco
                                      ,DataCadastro = DataCadastro
                                 WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", aluno.Identificador);
                    cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                    cmd.Parameters.AddWithValue("@Email", aluno.Email);
                    cmd.Parameters.AddWithValue("@Telefone", aluno.Telefone);
                    cmd.Parameters.AddWithValue("@Endereco", aluno.Endereco);
                    cmd.Parameters.AddWithValue("@DataCadastro", aluno.DataCadastro);

                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

            //CONSULTA na tabela Aluno
            public bool VerificarExistenciaAluno(string Identificador)
        {
            try
            {
                _conexao.Open();

                string query = @"select Count(Identificador) AS total 
                                 from Aluno WHERE Identificador = @Identificador;";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", Identificador);

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

            //DELETE na tabela Aluno
            public void DeletarCliente(Entidades.Aluno aluno)
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Aluno
                                 WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", aluno.Identificador);
                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

            //SELECT na tabela Aluno
            public Entidades.Aluno SelecionarAluno(string Identificador)
        {
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Aluno
                                 WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", Identificador);
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        var aluno = new Entidades.Aluno();

                        aluno.Identificador = rdr["Identificador"].ToString();
                        aluno.Nome = rdr["Nome"].ToString();
                        aluno.Email = rdr["Email"].ToString();
                        aluno.Telefone = Convert.ToInt64(rdr["Telefone"]);
                        aluno.Endereco = rdr["Endereco"].ToString();
                        aluno.DataCadastro = Convert.ToDateTime(rdr["DataCadastro"]);

                        return aluno;
                    }
                    else
                    {
                        throw new InvalidOperationException("Identificador " + Identificador + " não encontrado!");
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

            //LISTAR alunos
            public List<Entidades.Aluno> ListarAlunos()
        {
            var alunos = new List<Entidades.Aluno>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Aluno";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var aluno = new Entidades.Aluno();
                        aluno.Identificador = rdr["Identificador"].ToString();
                        aluno.Nome = rdr["Nome"].ToString();
                        aluno.Email = rdr["Email"].ToString();
                        aluno.Telefone = Convert.ToInt16(rdr["Telefone"]);
                        aluno.Endereco = rdr["Endereco"].ToString();
                        aluno.DataCadastro = Convert.ToDateTime(rdr["DataCadastro"]);

                        alunos.Add(aluno);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return alunos;
        }

        //INSERT na tabela Faturamento
        public void InserirFaturamento(Entidades.Faturamento faturamento)
        {
            try
            {
                // abre a conexao
                _conexao.Open();

                // apenas a query de insert
                string query = @"INSERT INTO Faturamento
                                       (Identificador
                                       ,TotalEntrada
                                       ,TotalSaida
                                       ,DiaReferencia)
                                 VALUES
                                       (@Identificador
                                       ,@TotalEntrada
                                       ,@TotalSaida
                                       ,@DiaReferencia);";

                // recebe a conexao para preparar instrucoes a serem enviadas para a base
                using (var cmd = new SqlCommand(query, _conexao))
                {
                    // adiciona o parametro e valor mapeado na query acima
                    cmd.Parameters.AddWithValue("@Identificador", faturamento.Identificador);
                    cmd.Parameters.AddWithValue("@TotalEntrada", faturamento.TotalEntrada);
                    cmd.Parameters.AddWithValue("@TotalSaida", faturamento.TotalSaida);
                    cmd.Parameters.AddWithValue("@DiaReferencia", faturamento.DiaReferencia);
                    

                    // envia o insert para o banco de dados
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                // fecha a conexao
                _conexao.Close();
            }
        }
        //UPDATE na tabela Faturamento
        public void AtualizarFaturamento(Entidades.Faturamento faturamento)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Faturamento
                                   SET Identificador = @Identificador
                                      ,TotalEntrada = @TotalEntrada
                                      ,TotalSaida = @TotalSaida
                                      ,DiaReferencia = @DiaReferencia";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", faturamento.Identificador);
                    cmd.Parameters.AddWithValue("@TotalEntrada", faturamento.TotalEntrada);
                    cmd.Parameters.AddWithValue("@TotalSaida", faturamento.TotalSaida);
                    cmd.Parameters.AddWithValue("@DiaReferencia", faturamento.DiaReferencia);

                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        //CONSULTA na tabela Faturamento
        public bool VerificarExistenciaFaturamento(string Identificador)
        {
            try
            {
                _conexao.Open();

                string query = @"select Count(Identificador) AS total 
                                 from Faturamento WHERE Identificador = @Identificador;";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", Identificador);

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        //DELETE na tabela Faturamento
        public void DeletarFaturamento(Entidades.Faturamento faturamento)
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Faturamento
                                 WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", faturamento.Identificador);
                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        //SELECT na tabela Faturamento
        public Entidades.Faturamento SelecionarFaturamento(string Identificador)
        {
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Faturamento
                                 WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", Identificador);
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        var faturamento = new Entidades.Faturamento();

                        faturamento.Identificador = rdr["Identificador"].ToString();
                        faturamento.TotalEntrada = Convert.ToDecimal(rdr["TotalEntrada"]);
                        faturamento.TotalSaida = Convert.ToDecimal(rdr["TotalSaida"]);
                        faturamento.DiaReferencia = Convert.ToDateTime(rdr["DiaReferencia"]);

                        return faturamento;
                    }
                    else
                    {
                        throw new InvalidOperationException("Identificador " + Identificador + " não encontrado!");
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        //LISTAR Faturamento
        public List<Entidades.Faturamento> ListarFaturamentos()
        {
            var faturamentos = new List<Entidades.Faturamento>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Faturamento";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var faturamento = new Entidades.Faturamento();
                        faturamento.Identificador = rdr["Identificador"].ToString();
                        faturamento.TotalEntrada = Convert.ToDecimal(rdr["TotalEntrada"]);
                        faturamento.TotalSaida = Convert.ToDecimal(rdr["TotalSaida"]);
                        faturamento.DiaReferencia = Convert.ToDateTime(rdr["DiaReferencia"]);

                        faturamentos.Add(faturamento);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return faturamentos;
        }

                   //INSERT na tabela Propaganda
                   public void InserirPropaganda(Entidades.Propaganda propaganda)
        {
            try
            {
                // abre a conexao
                _conexao.Open();

                // apenas a query de insert
                string query = @"INSERT INTO Propaganda
                                       (Identificador
                                       ,EmpresaDivulgadora
                                       ,Custo
                                       ,DataPropaganda)
                                 VALUES
                                       (@Identificador
                                       ,@EmpresaDivulgadora
                                       ,@Custo
                                       ,@DataPropaganda);";

                // recebe a conexao para preparar instrucoes a serem enviadas para a base
                using (var cmd = new SqlCommand(query, _conexao))
                {
                    // adiciona o parametro e valor mapeado na query acima
                    cmd.Parameters.AddWithValue("@Identificador", propaganda.Identificador);
                    cmd.Parameters.AddWithValue("@EmpresaDivulgadora", propaganda.EmpresaDivulgadora);
                    cmd.Parameters.AddWithValue("@Custo", propaganda.Custo);
                    cmd.Parameters.AddWithValue("@DataPropaganda", propaganda.DataPropaganda);


                    // envia o insert para o banco de dados
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                // fecha a conexao
                _conexao.Close();
            }
        }
                  //UPDATE na tabela Propaganda
                   public void AtualizarPropaganda(Entidades.Propaganda propaganda)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Propaganda
                                   SET Identificador = @Identificador
                                      ,EmpresaDivulgadora = @EmpresaDivulgadora
                                      ,Custo = @Custo
                                      ,DataPropaganda = @DataPropaganda";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", propaganda.Identificador);
                    cmd.Parameters.AddWithValue("@EmpresaDivulgadora", propaganda.EmpresaDivulgadora);
                    cmd.Parameters.AddWithValue("@Custo", propaganda.Custo);
                    cmd.Parameters.AddWithValue("@DataPropaganda", propaganda.DataPropaganda);

                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

                  //CONSULTA na tabela Propaganda
                   public bool VerificarExistenciaPropaganda(string Identificador)
        {
            try
            {
                _conexao.Open();

                string query = @"select Count(Identificador) AS total 
                                 from Propaganda WHERE Identificador = @Identificador;";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", Identificador);

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

                 //DELETE na tabela Propaganda
                   public void DeletarPropaganda(Entidades.Propaganda propaganda)
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Propaganda
                                 WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", propaganda.Identificador);
                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

                //SELECT na tabela Propaganda
                   public Entidades.Propaganda SelecionarPropaganda(string Identificador)
        {
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Propaganda
                                 WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", Identificador);
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        var propaganda = new Entidades.Propaganda();

                        propaganda.Identificador = rdr["Identificador"].ToString();
                        propaganda.EmpresaDivulgadora = rdr["EmpresaDivulgadora"].ToString();
                        propaganda.Custo = Convert.ToDecimal(rdr["Custo"]);
                        propaganda.DataPropaganda = Convert.ToDateTime(rdr["DataPropaganda"]);

                        return propaganda;
                    }
                    else
                    {
                        throw new InvalidOperationException("Identificador " + Identificador + " não encontrado!");
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

                //LISTAR Propaganda
                   public List<Entidades.Propaganda> ListarPropagandas()
        {
            var propagandas = new List<Entidades.Propaganda>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Propaganda";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var propaganda = new Entidades.Propaganda();
                        propaganda.Identificador = rdr["Identificador"].ToString();
                        propaganda.EmpresaDivulgadora = rdr["EmpresaDivulgadora"].ToString();
                        propaganda.Custo = Convert.ToDecimal(rdr["Custo"]);
                        propaganda.DataPropaganda = Convert.ToDateTime(rdr["DataPropaganda"]);

                        propagandas.Add(propaganda);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return propagandas;
        }

    }

}
