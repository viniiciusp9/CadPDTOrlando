using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class clAcessoDB
    {
        //variavel para reecebe string de conexao
        public string vConexao = "";

        //método responsavel por abrir a conexao com o banco de dados
        public SqlConnection AbreBanco()
        {
            //abre a conexao com o banco de dados
            SqlConnection conn = new SqlConnection(vConexao);
            conn.Open();
            return conn;
        }
        //método responsavel por fechar a conexao com o banco de dados
        public void FechaBanco(SqlConnection conn)
        {
            //fecha a conexao com a base de dados
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public void ExecutaComando(string strQuery)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = AbreBanco();
                SqlCommand cmdComando = new SqlCommand();
                cmdComando.CommandText = strQuery;
                cmdComando.CommandType = CommandType.Text;
                cmdComando.Connection = conn;

                //passa os valores da quer SQL, tipo do comando
                //conexao e executa o comando
                cmdComando.ExecuteNonQuery();
            }
            //tratamento de excessoes
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                //em caso de erro ou não, finaliza
                FechaBanco(conn);
            }
        }
        //DataSet é utilizado para retornar um volume
        //grandede registros utilizado principalmente
        //para o componente datagridview
        public DataSet RetornaDataSet(string strQuery)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                //abre a conexão com o banco de dados
                conn = AbreBanco();
                //cria o objeto de comando
                SqlCommand cmdComando = new SqlCommand();
                //passa os valores do query SQL, tipo de comando, conexão
                //e executa o comando
                cmdComando.CommandText = strQuery;
                cmdComando.CommandType = CommandType.Text;
                cmdComando.Connection = conn;
                //declara um dataAdapter
                SqlDataAdapter daAdaptador = new SqlDataAdapter();
                //declara  um dataset                
                DataSet dsDataSet = new DataSet();
                //passa o comando a ser executado pelo dataAdpter
                daAdaptador.SelectCommand = cmdComando;
                //o dataAdapter faz a conexao com o banco de dados,
                //carrega o dataset e fecha o banco
                daAdaptador.Fill(dsDataSet);
                //retorna o data carregado
                return dsDataSet;
                //tratamento de excessoes
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                //em caso de erro ou não, finally
                //é executado para fechar a conexão com o banco de dados
                FechaBanco(conn);
            }
        }
        public  SqlDataReader RetornaDataReader(string strQuery)
        {
            //cria o obejto de conexao
            SqlConnection conn = new SqlConnection();
            try
            {
                //abre a conexao com o banco de dados
                conn = AbreBanco();
                //cria o objeto de comando
                SqlCommand cmdComando = new SqlCommand();
                //passa os valores da query SQL, tipo de comando,
                //conexao e executa o comando
                cmdComando.CommandText = strQuery;
                cmdComando.CommandType = CommandType.Text;
                cmdComando.Connection = conn;
                //retorna p comando executando a leitura
                return cmdComando.ExecuteReader(CommandBehavior.CloseConnection);
                //tratamento de excessoes
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}