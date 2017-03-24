using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Negocio;

namespace Cadastro.Formularios
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

       private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            bool Clientes;
            bool Produto;
            //verifica se o nome do usuário foi digitado
            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Nome do Usuário Inválido!", "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsuario.Focus();
                return;

            }
            //verifica se a senha do usuário foi digitada
            if (txtSenha.Text == "")
            {
                MessageBox.Show("Senha do Usuário Inválida!", "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsuario.Focus();
                return;

            }
            //verifica se o usuário e senha existem no banco de dados


            SqlDataReader drReader;
            clUsuarios clUsuarios = new clUsuarios();
            clUsuarios.banco = Properties.Settings.Default.conexaoDB;
            drReader = clUsuarios.PesquisarCodigo(txtUsuario.Text, txtSenha.Text);
            if (!drReader.Read())
            {
                txtUsuario.Text = "";
                txtSenha.Text = "";
                MessageBox.Show("Acesso Negado!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //verifica a permissão de acesso do usuário
                if (Convert.ToBoolean(drReader["usuCliente"].ToString()) == true)
                {
                    Clientes = true;
                }
                else
                {
                    Clientes = false;
                }
                if (Convert.ToBoolean(drReader["usuProduto"].ToString()) == true)
                {
                    Produto = true;
                }
                else
                {
                    Produto = false;
                }
                //oculta o form~lário de login
                Hide();
                //cria a instância do formulário principal
                Form principal = new Principal();
                //transfere as permissões de acesso
                //para o form principal
                Principal.Clientes = Clientes;
                Principal.Produto = Produto;
                //abre o formulário principal
                principal.Show();
            }
            //fecha o datareader
            drReader.Close();
        }
    }
}