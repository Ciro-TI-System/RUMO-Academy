using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace LogAplicationAPI.Conexoes
{
    public class SqlServer
    {
        private readonly SqlConnection _conexao;

        public SqlServer()
        {
            string stringConexao = File.ReadAllText("C:\\Users\\cirof\\Desktop\\RUMO Academy\\RUMO Soluções - Credenciais\\LogAplicationAPI\\StringConexao_aluno.txt");
            _conexao = new SqlConnection(stringConexao);
        }

        public void InserirLogAplicacao(Models.LogAplicacao log)
        {
            try
            {
                // abre a conexao
                _conexao.Open();

                // apenas a query de insert
                string query = @"INSERT INTO LogAplicacao
                                       (Identificador
                                       ,DataHora
                                       ,MensagemErro
                                       ,RastreioErro
                                       ,NomeMaquina
                                       ,NomeAplicacao
                                       ,Usuario)              
                                 VALUES
                                       (@Identificador
                                       ,@DataHora
                                       ,@MensagemErro
                                       ,@RastreioErro
                                       ,NomeMaquina
                                       ,NomeAplicacao
                                       ,@Usuario);";

                // recebe a conexao para preparar instrucoes a serem enviadas para a base
                using var cmd = new SqlCommand(query, _conexao);
                // adiciona o parametro e valor mapeado na query acima
                cmd.Parameters.AddWithValue("@Identificador", log.Identificador);
                cmd.Parameters.AddWithValue("@DataHora", log.DataHora);
                cmd.Parameters.AddWithValue("@MensagemErro", log.MensagemErro);
                cmd.Parameters.AddWithValue("@RastreioErro", log.RastreioErro);
                cmd.Parameters.AddWithValue("@NomeMaquina", log.NomeMaquina);
                cmd.Parameters.AddWithValue("@NomeAplicacao", log.NomeAplicacao);
                cmd.Parameters.AddWithValue("@Usuario", log.Usuario);

                // envia o insert para o banco de dados
                cmd.ExecuteNonQuery();
            }


            finally
            {
                // fecha a conexao
                _conexao.Close();
            }
        }

        }
    }

