using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;

namespace Cadastro.Formularios
{
    public partial class CadUsu : Form
    {
        public CadUsu()
        {
            InitializeComponent();
        }

        private void btnCadUsuario_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvUsuario_DoubleClick(object sender, EventArgs e)
        {
            //verifica se existe itens na grid
            if (dgvUsuario.RowCount == 0)
            {
                return;
            }
            //carrega a tela com todos os dados do cliente
            SqlDataReader drReader;
            clUsuarios clUsuarios = new clUsuarios();
            clUsuarios.banco = Properties.Settings.Default.conexaoDB;
            drReader = clUsuarios.PesquisarCodigo(Convert.ToInt32(dgvUsuario.CurrentRow.Cells[0].Value));
            if (drReader.Read())
            {
                //transfere os dados do banco de dados para os campos
                //do formulario
                txtUsuario.Text = drReader["usuNome"].ToString();
                txtSenha.Text = drReader["usuSenha"].ToString();
                if (Convert.ToBoolean(drReader["usuCliente"].ToString()) == true)
                {
                    ckbClientes.Checked = true;
                }
                else
                {
                    ckbClientes.Checked = false;
                }
                if (Convert.ToBoolean(drReader["usuProduto"].ToString()) == true)
                {
                    ckbProdutos.Checked = true;
                }
                else
                {
                    ckbProdutos.Checked = false;
                }
                ////habilita o frame e envia o cursor para o campo nome
                tabControl1.SelectedTab = tabPage2;
                txtUsuario.Focus();
            }
            drReader.Close();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbLimpar_Click(object sender, EventArgs e)
        {
            clClearForm.ClearForms(this);
        }
        public void Salvar()
        {
            {
                if (txtUsuario.Text == "")
                {
                    errP.SetError(lblMensagem, "Campo Obrigatório");
                    return;
                }
                else
                {
                    errP.SetError(lblMensagem, "");
                }
                clUsuarios clUsuarios = new clUsuarios();

                DialogResult resultado;
                resultado = MessageBox.Show("Confirmar Cadastro?", "Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (resultado.Equals(DialogResult.No))
                {
                    return;
                }
                clUsuarios.usuNome = txtUsuario.Text;
                clUsuarios.usuSenha = txtSenha.Text;

                clUsuarios.banco = Properties.Settings.Default.conexaoDB;
                if (txtCodigo.Text == "")
                {
                    clUsuarios.Gravar();
                }
                else
                {
                    clUsuarios.usuCod = Convert.ToInt32(txtCodigo.Text);
                    clUsuarios.Alterar();
                }
                //Pesquisar();
                MessageBox.Show("Cliente Cadastrado com Sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            //validação do conteúdo
            if (txtCodigo.Text == "")
            {
                return;
            }
            //pergunta para o usuário se ele confirma a exclusão do cadastro
            DialogResult resposta;
            resposta = MessageBox.Show("Confirma a Exclusão Usuário?", "Atenção",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (resposta.Equals(DialogResult.No))
            {
                return;
            }
            //instancia a classe de negócio
            clUsuarios clUsuarios = new clUsuarios();


            //variável com a string de conexão com o banco
            clUsuarios.banco = Properties.Settings.Default.conexaoDB;
            clUsuarios.usuCod = Convert.ToInt32(txtCodigo.Text);
            clUsuarios.Excluir();


            //atualiza o datagridview
            //Pesquisar();

            clClearForm.ClearForms(this);

            //mensagem de confirmação da exclusão
            MessageBox.Show("Usuario Excluído com Sucesso!", "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Pesquisar()
        {
            string campo = "";
            //selecione o campo de pesquisa
            if (cboOpcao.Text == "Usuário")
            {
                campo = "usuNome";
            }

            clUsuarios clUsuarios = new clUsuarios();
            clUsuarios.banco = Properties.Settings.Default.conexaoDB;
            dgvUsuario.DataSource = clUsuarios.Pesquisar(campo, txtFiltro.Text).Tables[0];
            dgvUsuario.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void CadUsu_Load(object sender, EventArgs e)
        {
            Pesquisar();
        }
    }
}