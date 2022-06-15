using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace RestauranteAPI.Conexoes
{
    public class SqlServer
    {
        private readonly SqlConnection _conexao;

        public SqlServer()
        {
            string stringConexao = File.ReadAllText("C:\\Users\\cirof\\Desktop\\RUMO Academy\\RUMO Soluções - Credenciais\\RestauranteAPI\\StringConexao.txt");
            _conexao = new SqlConnection(stringConexao);
        }


        //INSERT na tabela Cliente
        public void InserirCliente(Model.Cliente cliente)
        {
            try
            {
                // abre a conexao
                _conexao.Open();

                // apenas a query de insert
                string query = @"INSERT INTO Cliente
                                       (Cpf
                                       ,Nome)
                                 VALUES
                                       (@Cpf
                                       ,@Nome);";

                // recebe a conexao para preparar instrucoes a serem enviadas para a base
                using (var cmd = new SqlCommand(query, _conexao))

                {
                    // adiciona o parametro e valor mapeado na query acima
                    cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);

                    // envia o insert para o banco de dados
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                if (!Utils.ValidarCpf.IsCpf(cliente.Cpf))
                    throw new InvalidOperationException("Cpf inválido.");
            }


            finally
            {
                // fecha a conexao
                _conexao.Close();
            }
        }

        //UPDATE na tabela Cliente
        public void AtualizarCliente(Model.Cliente cliente)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Cliente
                                   SET Cpf = @Cpf
                                      ,Nome = @Nome";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        //CONSULTA na tabela Cliente
        public bool VerificarExistenciaCliente(string Cpf)
        {
            try
            {
                _conexao.Open();

                string query = @"select Count(Cpf) AS total 
                                 from Cliente WHERE Cpf = @Cpf;";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Cpf", Cpf);

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        //DELETE na tabela Cliente
        public void DeletarCliente(Model.Cliente cliente)
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Cliente
                                 WHERE Cpf = @Cpf";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        //SELECT na tabela Cliente
        public Model.Cliente SelecionarCliente(string Cpf)
        {
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Cliente
                                 WHERE Cpf = @Cpf";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Cpf", Cpf);
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        var cliente = new Model.Cliente();

                        cliente.Cpf = rdr["Cpf"].ToString();
                        cliente.Nome = rdr["Nome"].ToString();


                        return cliente;
                    }
                    else
                    {
                        throw new InvalidOperationException("Cpf " + Cpf + " não encontrado!");
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        //LISTAR Clientes
        public List<Model.Cliente> ListarClientes()
        {
            var clientes = new List<Model.Cliente>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Cliente";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var cliente = new Model.Cliente();
                        cliente.Cpf = rdr["Cpf"].ToString();
                        cliente.Nome = rdr["Nome"].ToString();
                        clientes.Add(cliente);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return clientes;
        }

        //INSERT na tabela Mesa
        public void InserirMesa(Model.Mesa mesa)
        {
            try
            {
                // abre a conexao
                _conexao.Open();

                // apenas a query de insert
                string query = @"INSERT INTO Mesa
                                       (Identificador
                                       ,QuantidadeCadeiras)
                                 VALUES
                                       (@Identificador
                                       ,@QuantidadeCadeiras);";

                // recebe a conexao para preparar instrucoes a serem enviadas para a base
                using (var cmd = new SqlCommand(query, _conexao))

                {
                    // adiciona o parametro e valor mapeado na query acima
                    cmd.Parameters.AddWithValue("@Identificador", mesa.Identificador);
                    cmd.Parameters.AddWithValue("@QuantidadeCadeiras", mesa.QuantidadeCadeiras);
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

        //UPDATE na tabela Mesa
        public void AtualizarMesa(Model.Mesa mesa)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Mesa
                                   SET Identificador = @Identificador
                                      ,QuantidadeCadeiras = @QuantidadeCadeiras";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@QuantidadeCadeiras", mesa.QuantidadeCadeiras);
                    
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        //CONSULTA na tabela Mesa
        public bool VerificarExistenciaMesa(string Identificador)
        {
            try
            {
                _conexao.Open();

                string query = @"select Count(Identificador) AS total 
                                 from Mesa WHERE Identificador = @Identificador;";

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

        //DELETE na tabela Mesa
        public void DeletarMesa(Model.Mesa mesa)
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Mesa
                                 WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", mesa.Identificador);
                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        //SELECT na tabela Mesa
        public Model.Mesa SelecionarMesa(string Identificador)
        {
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Mesa
                                 WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", Identificador);
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        var mesa = new Model.Mesa();

                        mesa.Identificador = Convert.ToInt32(rdr["Identificador"]);
                        mesa.QuantidadeCadeiras = Convert.ToInt32(rdr["QuantidadeCadeiras"]);


                        return mesa;
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

        //LISTAR Mesas
        public List<Model.Mesa> ListarMesas()
        {
            var mesas = new List<Model.Mesa>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Mesa";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var mesa = new Model.Mesa();

                        mesa.Identificador = Convert.ToInt32(rdr["Identificador"]);
                        mesa.QuantidadeCadeiras = Convert.ToInt32(rdr["QuantidadeCadeiras"]);

                        mesas.Add(mesa);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return mesas;
        }

    }
}