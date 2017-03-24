using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class clClientes
    {
        public string banco { get; set; }
        public int cliCodigo { get; set; }
        public string cliNome { get; set; }
        public string cliEndereco { get; set; }
        public string cliNumero { get; set; }
        public string cliBairro { get; set; }
        public string cliCidade { get; set; }
        public string cliEstado { get; set; }
        public string cliCep { get; set; }
        public string cliCelular { get; set; }
        public void Gravar()
        {
            //variavel utilizada para "concatenar" texto
            //de forma estruturada
            StringBuilder strQuery = new StringBuilder();
            //montagem INSERT
            strQuery.Append("INSERT INTO tb_Clientes");

            strQuery.Append("(");
            strQuery.Append(" cliNome  ");
            strQuery.Append(", cliEndereco ");
            strQuery.Append(", cliNumero ");
            strQuery.Append(", cliBairro ");
            strQuery.Append(", cliCidade ");
            strQuery.Append(", cliEstado ");
            strQuery.Append(", cliCep ");
            strQuery.Append(", cliCelular ");

            strQuery.Append(" ) ");

            strQuery.Append(" VALUES ( ");

            strQuery.Append(" '" + cliNome + "'");
            strQuery.Append(", '" + cliEndereco + "'");
            strQuery.Append(", '" + cliNumero + "'");
            strQuery.Append(", '" + cliBairro + "'");
            strQuery.Append(", '" + cliCidade + "'");
            strQuery.Append(", '" + cliEstado + "'");
            strQuery.Append(", '" + cliCep + "'");
            strQuery.Append(", '" + cliCelular + "'");

            strQuery.Append(" ); ");

            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            clAcessoDB.ExecutaComando(strQuery.ToString());
        }
        public void Alterar()
        {
            StringBuilder strQuery = new StringBuilder();
            //montagem de uptade
            strQuery.Append(" UPDATE tb_Clientes ");
            strQuery.Append(" SET ");
            strQuery.Append(" cliNome = '" + cliNome + "'");
            strQuery.Append(", cliEndereco = '" + cliEndereco + "'");
            strQuery.Append(", cliNumero = '" + cliNumero + "'");
            strQuery.Append(", cliBairro = '" + cliBairro + "'");
            strQuery.Append(", cliCidade = '" + cliCidade + "'");
            strQuery.Append(", cliEstado = '" + cliEstado + "'");
            strQuery.Append(", cliCep = '" + cliCep + "'");
            strQuery.Append(", cliCelular = '" + cliCelular + "'");

            strQuery.Append(" WHERE ");

            strQuery.Append(" cliCodigo = " + cliCodigo);
            //instacia a classe clAcessoDB e executa o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            clAcessoDB.ExecutaComando(strQuery.ToString());
        }
        public void Excluir()
        {
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append(" DELETE FROM tb_Clientes ");
            strQuery.Append(" WHERE ");
            strQuery.Append(" cliCodigo = " + cliCodigo);

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
            strQuery.Append(" FROM tb_Clientes ");
            if (Campo != string.Empty && Filtro != string.Empty)
            {
                strQuery.Append(" WHERE ");
                strQuery.Append(Campo + " LIKE '" + "%" + Filtro + "%" + "'");
            }
            strQuery.Append(" ORDER BY cliNome ");

            //excuta o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            return clAcessoDB.RetornaDataSet(strQuery.ToString());

        }
        public SqlDataReader PesquisarCodigo(int cliCodigo)
        {
            StringBuilder strQuery = new StringBuilder();
            //montagem do select
            strQuery.Append(" SELECT * ");
            strQuery.Append(" FROM tb_Clientes");
            strQuery.Append(" WHERE ");
            strQuery.Append(" cliCodigo = " + cliCodigo);
            //executa o comando
            clAcessoDB clAcessoDB = new clAcessoDB();
            clAcessoDB.vConexao = banco;
            return clAcessoDB.RetornaDataReader(strQuery.ToString());
        }
    }
}