using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class clUsuarios
    {
        public string banco { get; set; }

        public int usuCod { get; set; }

        public string usuNome { get; set; }

        public string usuSenha { get; set; }

        public string usuCliente { get; set; }

        public string usuProduto { get; set; }
        public void Gravar()
        {
            //variavel utilizada para "concatenar" texto
            //de forma estruturada
            StringBuilder strQuery = new StringBuilder();
            //montagem INSERT
            strQuery.Append("INSERT INTO tb_Usuarios");

            strQuery.Append("(");
            strQuery.Append(" usuNome  ");
            strQuery.Append(", usuSenha ");
            strQuery.Append(", usuCliente ");
            strQuery.Append(", usuProduto ");

            strQuery.Append(" ) ");

            strQuery.Append(" VALUES ( ");

            strQuery.Append(" '" + usuNome + "'");
            strQuery.Append(", '" + usuSenha + "'");
            strQuery.Append(", '" + usuCliente + "'");
            strQuery.Append(", '" + usuProduto + "'");

            strQuery.Append(" ); ");

            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            clAcessoDB.ExecutaComando(strQuery.ToString());
        }
        public SqlDataReader PesquisarCodigo(string usuNome, string usuSenha)
        {
            StringBuilder strQuery = new StringBuilder();

            //montagem do select
            strQuery.Append(" SELECT usuNome, usuSenha, ");
            strQuery.Append(" usuCliente, usuProduto ");
            strQuery.Append(" FROM tb_Usuarios ");
            strQuery.Append(" WHERE ");
            strQuery.Append(" usuNome = '" + usuNome + "'");
            strQuery.Append(" AND ");
            strQuery.Append(" usuSenha = '" + usuSenha + "'");


            //executa o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            return clAcessoDB.RetornaDataReader(strQuery.ToString());
        }
        public SqlDataReader PesquisarCodigo(int usuCod)
        {

            StringBuilder strQuery = new StringBuilder();

            //montagem do select
            strQuery.Append(" SELECT * ");
            strQuery.Append(" FROM tb_Usuarios ");
            strQuery.Append(" WHERE ");
            strQuery.Append(" usuCod = '" + usuCod + "'");


            //executa o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            return clAcessoDB.RetornaDataReader(strQuery.ToString());
        }
        public void Alterar()
        {
            StringBuilder strQuery = new StringBuilder();
            //montagem de uptade
            strQuery.Append(" UPDATE tb_Usuarios ");
            strQuery.Append(" SET ");
            strQuery.Append(" usuNome = '" + usuNome + "'");
            strQuery.Append(", usuSenha = '" + usuSenha + "'");
            strQuery.Append(", usuCliente = '" + usuSenha + "'");
            strQuery.Append(", usuProduto = '" + usuProduto + "'");

            strQuery.Append(" WHERE ");

            strQuery.Append(" usuCod = " + usuCod);
            //instacia a classe clAcessoDB e executa o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            clAcessoDB.ExecutaComando(strQuery.ToString());
        }
        public void Excluir()
        {
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append(" DELETE FROM tb_Usuarios ");
            strQuery.Append(" WHERE ");
            strQuery.Append(" usuCod = " + usuCod);

            //instacia a classe clAcessoDB e executa o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            clAcessoDB.ExecutaComando(strQuery.ToString());
        }        
        public DataSet Pesquisar(string Campo, string Filtro)
        {
            StringBuilder strQuery = new StringBuilder();
            //montagem de select
            strQuery.Append(" SELECT * ");
            strQuery.Append(" FROM tb_Usuarios ");
            if (Campo != string.Empty && Filtro != string.Empty)
            {
                strQuery.Append(" WHERE ");
                strQuery.Append(Campo + " LIKE '" + "%" + Filtro + "%" + "'");
            }
            strQuery.Append(" ORDER BY usuNome ");

            //excuta o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            return clAcessoDB.RetornaDataSet(strQuery.ToString());

        }
    }
}