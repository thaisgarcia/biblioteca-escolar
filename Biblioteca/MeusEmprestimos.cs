using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class MeusEmprestimos : Form
    {
        public MeusEmprestimos()
        {
            InitializeComponent();
        }

        private void telaInicialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TelaInicial telaInicial = new TelaInicial();
            telaInicial.Show();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void picSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
