using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Biblioteca
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
        }

        Livros livros = new Livros();
        Connect conn = new Connect();

        private void picSair_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void lblRM_Click(object sender, EventArgs e)
        {

        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {
            txtRM.Text = Login.id_usuario;
            txtTelefone.Text = Login.telefone;
            txtNome.Text = Login.nome;
            txtTurma.Text = Login.turma;

            if (txtTurma.Text == "")
            {
                lblTurma.Visible = false;
                txtTurma.Visible = false;
            }

            string selecionar = "SELECT emprestimos.isbn as 'ISBN', nome_livro as 'Livro', data_empr as 'Data Emp.',";
            selecionar += " data_dev as 'Data Devol.', img_livro as 'Imagem'";
            selecionar += " FROM emprestimos JOIN livros ON emprestimos.isbn = livros.isbn and"; 
            selecionar += " nome_livro = livros.nome_livro and img_livro = livros.img_livro"; 
            selecionar += " WHERE id_usuario = '" + txtRM.Text + "';";
            MySqlCommand command = new MySqlCommand(selecionar);
            command.Connection = conn.Conectar();
            MySqlDataReader empr = command.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(empr);
            dataGridView1.DataSource = data;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[4].Value == null) //para não haver conflito com as imagens já convertidas
                {
                    row.Cells[4].Value = livros.ConverteByteParaImagem(row.Cells[4].Value.ToString());
                }
            }

            var imagecolumn = (DataGridViewImageColumn)dataGridView1.Columns[4]; //layout da imagem
            imagecolumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

            for (var i = 0; i <= dataGridView1.Rows.Count - 1; i++) //tamanho dos campos no DataGridView
            {
                DataGridViewRow r = dataGridView1.Rows[i];
                r.Height = 85;
                dataGridView1.Columns[0].Width = 176;
                dataGridView1.Columns[1].Width = 370;
                dataGridView1.Columns[2].Width = 108;
                dataGridView1.Columns[3].Width = 107;
            }

            conn.Desconectar();
        }

        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
