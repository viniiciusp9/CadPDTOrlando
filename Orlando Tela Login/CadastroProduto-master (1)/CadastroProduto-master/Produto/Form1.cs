using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;

namespace Produto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tbsSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Server = AME0556330W10-1\\SQLEXPRESS;Database=db_google; Trusted_Connection = Yes; "))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO tb_Produto1(proDescricao, proMarca, proPreco, proData)VALUES (@proDescricao, @proMarca, @proPreco, @proData)", con))
                {
                    cmd.Parameters.AddWithValue("proDescricao", txtDescricao.Text);
                    cmd.Parameters.AddWithValue("proMarca", txtMarca.Text);
                    cmd.Parameters.AddWithValue("proPreco", txtPreco.Text);
                    cmd.Parameters.AddWithValue("proData", dtpData.Text);
                    try
                    {
                        con.Open();
                        if (txtDescricao.Text == "")
                        {
                            errorErro.SetError(txtDescricao, "Preencha todos os campos!");
                            lblMensagem.Text = "Preencha todos os campos";
                            return;
                        }
                        if (cmd.ExecuteNonQuery() > 1)
                        {
                            DialogResult resultado;
                            resultado = MessageBox.Show("Confirmar Produto?", "Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (resultado == DialogResult.No)
                            {
                                return;
                            }                            
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "Erro ao cadastrar post!\n" + ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}