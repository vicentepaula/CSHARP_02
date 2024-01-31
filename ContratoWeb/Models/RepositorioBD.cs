using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;

namespace ContratoWeb.Models
{
    public class RepositorioBD : IDisposable
    {
        private readonly OracleConnection conexao;
        public RepositorioBD()
        {
            conexao = new OracleConnection(ConfigurationManager.ConnectionStrings["conexaoBb"].ConnectionString);
            conexao.Open();
        }

        public void ExecutaCommando(string strQuery)
        {
            var cmdComando = new OracleCommand
            {
                CommandText = strQuery,
                CommandType = System.Data.CommandType.Text,
                Connection = conexao

            };
            cmdComando.ExecuteNonQuery();
        }


        public OracleDataReader ExecutaComandoComRetorno(string strQuery)
        {
            var cmdComandoSelect = new OracleCommand(strQuery, conexao);
            return cmdComandoSelect.ExecuteReader();

        }


        public void Dispose()
        {

            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }


        }
    }
}