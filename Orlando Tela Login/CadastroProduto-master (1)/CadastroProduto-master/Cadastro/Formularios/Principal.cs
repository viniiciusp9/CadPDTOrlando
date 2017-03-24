using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cadastro.Formularios
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        public static bool Clientes { get; internal set; }
        public static bool Produto { get; internal set; }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            new Produto().Show();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            new CadUsu().Show();
        }
    }
}
