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
    public partial class Cadastro : Form
    {
        private object ex;

        public Cadastro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void tsbLimpar_Click(object sender, EventArgs e)
        {
            clClearForm.ClearForms(this);
        }
        public void Salvar()
        {
            clClientes clClientes = new clClientes();
            //validação do conteúdo
            if (txtNome.Text == "")
            {
                errError.SetError(lblNome, "Campo Obrigatório");
                return;
            }
            else
            {
                errError.SetError(lblNome, "");
            }
            clClientes clCliente = new clClientes();

            DialogResult resultado;
            resultado = MessageBox.Show("Confirmar Cliente?", "Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (resultado.Equals(DialogResult.No))
            {
                return;
            }
            clClientes.cliNome = txtNome.Text;
            clClientes.cliEndereco = txtEndereco.Text;
            clClientes.cliNumero = txtNumero.Text;
            clClientes.cliBairro = txtBairro.Text;
            clClientes.cliCidade = txtCidade.Text;
            clClientes.cliEstado = cboEstado.Text;
            clClientes.cliCep = mskCep.Text;
            clClientes.cliCelular = mtbCelular.Text;

            clClientes.banco = Properties.Settings.Default.conexaoDB;
            if (txtCodigo.Text == "")
            {
                clClientes.Gravar();
            }
            else
            {
                clClientes.cliCodigo = Convert.ToInt32(txtCodigo.Text);
                clClientes.Alterar();
            }
            Pesquisar();
            MessageBox.Show("Cliente Cadastrado com Sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCep_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            string xml = "http://cep.republicavirtual.com.br/web_cep.php?cep=@cep&formato=xml".Replace("@cep", mskCep.Text);
            ds.ReadXml(xml);
            if (ds.Tables[0].Rows[0]["resultado_txt"].ToString() == "sucesso - cep completo" || ds.Tables[0].Rows[0]["resultado_txt"].ToString() == "sucesso - cep único")
            {
                txtEndereco.Text = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString() + " " + ds.Tables[0].Rows[0]["logradouro"].ToString();
                txtBairro.Text = ds.Tables[0].Rows[0]["bairro"].ToString();
                txtCidade.Text = ds.Tables[0].Rows[0]["cidade"].ToString();
                cboEstado.Text = ds.Tables[0].Rows[0]["uf"].ToString();
                txtNumero.Focus();
            }
            else
            {
                MessageBox.Show("CEP não Encontrado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Cadastro_Load(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Salvar();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            Pesquisar();
        }
        private void Pesquisar()
        {
            string campo = "";
            //selecione o campo de pesquisa
            if (cboOpcao.Text == "Código")
            {
                campo = "cliCodigo";
            }
            else if (cboOpcao.Text == "Nome")
            {
                campo = "cliNome";
            }
            else if (cboOpcao.Text == "Celular")
            {
                campo = "cliCelular";
            }
            clClientes clClientes = new clClientes();
            clClientes.banco = Properties.Settings.Default.conexaoDB;
            dgvClientes.DataSource = clClientes.Pesquisar(campo, txtFiltro.Text).Tables[0];
            dgvClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvClientes_DoubleClick(object sender, EventArgs e)
        {
            //verifica se existe itens na grid
            if (dgvClientes.RowCount == 0)
            {
                return;
            }
            //carrega a tela com todos os dados do cliente
            SqlDataReader drReader;
            clClientes clClientes = new clClientes();
            clClientes.banco = Properties.Settings.Default.conexaoDB;
            drReader = clClientes.PesquisarCodigo(Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value));
            if (drReader.Read())
            {
                //transfere os dados do banco de dados para os campos
                //do formulario
                txtCodigo.Text = drReader["cliCodigo"].ToString();
                txtNome.Text = drReader["cliNome"].ToString();
                txtEndereco.Text = drReader["cliEndereco"].ToString();
                txtNumero.Text = drReader["cliNumero"].ToString();
                txtBairro.Text = drReader["cliBairro"].ToString();
                txtCidade.Text = drReader["cliCidade"].ToString();
                cboEstado.Text = drReader["cliEstado"].ToString();
                mskCep.Text = drReader["cliCep"].ToString();
                mtbCelular.Text = drReader["cliCelular"].ToString();


                ////habilita o frame e envia o cursor para o campo nome
                tabControl1.SelectedTab = tabPage2;
                txtNome.Focus();
            }
            drReader.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
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
            clClientes clClientes = new clClientes();


            //variável com a string de conexão com o banco
            clClientes.banco = Properties.Settings.Default.conexaoDB;
            clClientes.cliCodigo = Convert.ToInt32(txtCodigo.Text);
            clClientes.Excluir();


            //atualiza o datagridview
            Pesquisar();

            clClearForm.ClearForms(this);

            //mensagem de confirmação da exclusão
            MessageBox.Show("Cliente Excluído com Sucesso!", "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}