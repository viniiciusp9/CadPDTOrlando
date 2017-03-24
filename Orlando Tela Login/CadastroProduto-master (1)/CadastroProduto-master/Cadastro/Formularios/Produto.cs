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

namespace Cadastro
{
    public partial class Produto : Form
    {
        public Produto()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbsSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbLimpar_Click(object sender, EventArgs e)
        {
            clClearForm.ClearForms(this);
        }

        private void txtFiltro1_TextChanged(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void Pesquisar()
        {
            string campo = "";
            //selecione o campo de pesquisa
            if (cboOpcao1.Text == "Código")
            {
                campo = "proCodigo";
            }
            else if (cboOpcao1.Text == "Descrição")
            {
                campo = "proDescricao";
            }
            else if (cboOpcao1.Text == "Marca")
            {
                campo = "proMarca";
            }
            clProduto clProduto = new clProduto();
            clProduto.banco = Properties.Settings.Default.conexaoDB;
            dgvProdutos.DataSource = clProduto.Pesquisar(campo, txtFiltro1.Text).Tables[0];
            dgvProdutos.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            Salvar();
        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }
        public void Salvar()
        {
            //validação do conteúdo
            if (txtDescricao.Text == "")
            {
                errorErro.SetError(txtDescricao, "Campo Obrigatório");
                return;
            }
            else
            {
                errorErro.SetError(txtDescricao, "");
            }
            clProduto clProduto = new clProduto();

            DialogResult resultado;
            resultado = MessageBox.Show("Confirmar Produto?", "Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (resultado.Equals(DialogResult.No))
            {
                return;
            }
            clProduto.proDescricao = txtDescricao.Text;
            clProduto.proMarca = txtMarca.Text;
            clProduto.proPreco = txtPreco.Text;
            clProduto.proData = mtbData.Text;
            
            clProduto.banco = Properties.Settings.Default.conexaoDB;
            if (txtCodigo.Text == "")
            {
                clProduto.Gravar();
            }
            else
            {
                clProduto.proCodigo = Convert.ToInt32(txtCodigo.Text);
                clProduto.Alterar();
            }
            Pesquisar();
            MessageBox.Show("Produto Cadastrado com Sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void Produto_Load(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void dgvProdutos_DoubleClick(object sender, EventArgs e)
        {
            //verifica se existe itens na grid
            if (dgvProdutos.RowCount == 0)
            {
                return;
            }
            //carrega a tela com todos os dados do cliente
            SqlDataReader drReader;
            clProduto clProduto = new clProduto();
            clProduto.banco = Properties.Settings.Default.conexaoDB;
            drReader = clProduto.PesquisarCodigo(Convert.ToInt32(dgvProdutos.CurrentRow.Cells[0].Value));
            if (drReader.Read())
            {
                //transfere os dados do banco de dados para os campos
                //do formulario
                txtCodigo.Text = drReader["proCodigo"].ToString();
                txtDescricao.Text = drReader["proDescricao"].ToString();
                txtMarca.Text = drReader["proMarca"].ToString();
                txtPreco.Text = drReader["proPreco"].ToString();
                mtbData.Text = drReader["proData"].ToString();


                ////habilita o frame e envia o cursor para o campo nome
                tabControl1.SelectedTab = tabPage2;
                txtDescricao.Focus();
            }
            drReader.Close();
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
            resposta = MessageBox.Show("Confirma a Exclusão Cliente", "Atenção",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (resposta.Equals(DialogResult.No))
            {
                return;
            }
            //instancia a classe de negócio
            clProduto clProduto = new clProduto();


            //variável com a string de conexão com o banco
            clProduto.banco = Properties.Settings.Default.conexaoDB;
            clProduto.proCodigo = Convert.ToInt32(txtCodigo.Text);
            clProduto.Excluir();


            //atualiza o datagridview
            Pesquisar();

            clClearForm.ClearForms(this);

            //mensagem de confirmação da exclusão
            MessageBox.Show("Cliente Excluído com Sucesso!", "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}