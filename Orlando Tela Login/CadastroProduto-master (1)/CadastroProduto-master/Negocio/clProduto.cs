using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class clProduto
    {

        public string banco { get; set; }
        public int proCodigo { get; set; }
        public string proDescricao { get; set; }
        public string proMarca { get; set; }
        public string proPreco { get; set; }
        public string proData { get; set; }
        public void Gravar()
        {
            //variavel utilizada para "concatenar" texto
            //de forma estruturada
            StringBuilder strQuery = new StringBuilder();
            //montagem INSERT
            strQuery.Append(" INSERT INTO tb_Produtos ");

            strQuery.Append("(");

            strQuery.Append(" proDescricao  ");
            strQuery.Append(", proMarca ");
            strQuery.Append(", proPreco ");
            strQuery.Append(", proData ");

            strQuery.Append(" ) ");

            strQuery.Append(" VALUES ( ");

            strQuery.Append(" '" + proDescricao + "'");
            strQuery.Append(", '" + proMarca + "'");
            strQuery.Append(", '" + proPreco + "'");
            strQuery.Append(", '" + proData + "'");

            strQuery.Append(" ); ");

            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            clAcessoDB.ExecutaComando(strQuery.ToString());
        }
        public void Alterar()
        {
            StringBuilder strQuery = new StringBuilder();
            //montagem de uptade
            strQuery.Append(" UPDATE tb_Produtos ");
            strQuery.Append(" SET ");
            strQuery.Append(" proDescricao = '" + proDescricao + "'");
            strQuery.Append(", proMarca = '" + proMarca + "'");
            strQuery.Append(", proPreco = '" + proPreco + "'");
            strQuery.Append(", proData = '" + proData + "'");

            strQuery.Append(" WHERE ");

            strQuery.Append(" proCodigo = " + proCodigo);
            //instacia a classe clAcessoDB e executa o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            clAcessoDB.ExecutaComando(strQuery.ToString());
        }
        public void Excluir()
        {
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append(" DELETE FROM tb_Produtos ");
            strQuery.Append(" WHERE ");
            strQuery.Append(" proCodigo = " + proCodigo);

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
            strQuery.Append(" FROM tb_Produtos ");
            if (Campo != string.Empty && Filtro != string.Empty)
            {
                strQuery.Append(" WHERE ");
                strQuery.Append(Campo + " LIKE '" + "%" + Filtro + "%" + "'");
            }
            strQuery.Append(" ORDER BY proDescricao ");

            //excuta o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            return clAcessoDB.RetornaDataSet(strQuery.ToString());

        }
        public SqlDataReader PesquisarCodigo(int codCli)
        {
            StringBuilder strQuery = new StringBuilder();
            //montagem do select
            strQuery.Append(" SELECT * ");
            strQuery.Append(" FROM tb_Produtos ");
            strQuery.Append(" WHERE ");
            strQuery.Append(" proCodigo = " + codCli);
            //executa o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            return clAcessoDB.RetornaDataReader(strQuery.ToString());
        }
    }
}
