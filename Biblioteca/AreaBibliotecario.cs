using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Biblioteca
{
    public partial class AreaBibliotecario : Form
    {
        public AreaBibliotecario()
        {
            InitializeComponent();
        }

        //classes e objetos
        Connect conn = new Connect();
        Livros livros = new Livros();
        Emprestimos emprestimos = new Emprestimos();
        MySqlCommand command = new MySqlCommand();

        //variáveis
        int codCateg;
        int cod;
        string isbn;
        string caminhofoto = null;

        private void cboxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxCategoria.Text == "Romance")
            {
                codCateg = 1;
            }
            else if (cboxCategoria.Text == "Literatura infantil")
            {
                codCateg = 2;
            }
            else if (cboxCategoria.Text == "Autoajuda")
            {
                codCateg = 3;
            }
            else if (cboxCategoria.Text == "História em Quadrinhos")
            {
                codCateg = 4;
            }
        }

        public void Limpar()
        {
            txtNome.Clear();
            txtAutor.Clear();
            txtEditora.Clear();
            txtISBN.Clear();
            txtAno.Clear();
            lblImage.Text = "";

            cboxCategoria.SelectedItem = null;
            txtRM1.Clear();
            txtisbn1.Clear();
        }

        public void Recarregar() //atualiza a tabela
        {   
            MySqlDataReader temp = this.livros.ListarLivros(); //preenche o dataGrid
            DataTable dt = new DataTable();
            dt.Load(temp);
            dvgConsulta.DataSource = dt;
            foreach (DataGridViewRow row in dvgConsulta.Rows)
            {
                if (row.Cells[6].Value == null) //para não haver conflito com as imagens já convertidas
                {
                    row.Cells[6].Value = livros.ConverteByteParaImagem(row.Cells[6].Value.ToString());
                }
            }

            for (var i=0;i <= dvgConsulta.Rows.Count -1; i++) //tamanho dos campos no DataGridView
            {
                DataGridViewRow r = dvgConsulta.Rows[i];
                r.Height = 75;
                dvgConsulta.Columns[4].Width = 65;
                dvgConsulta.Columns[6].Width = 70;
            }

            var imagecolumn = (DataGridViewImageColumn)dvgConsulta.Columns[6]; //layout da imagem
            imagecolumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            conn.Desconectar();

            MySqlDataReader empr = this.emprestimos.ListarEmprestimos();
            DataTable data = new DataTable();
            data.Load(empr);
            dgvEmprest.DataSource = data;
            conn.Desconectar();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if(lblImage.Text != "") //se estiver vazio não tem imagem, pois não tem caminho para converte-la
                {
                    byte[] img = this.livros.ConverteImagemParaByte(caminhofoto);
                    //adiciona os livros ao banco de dados
                    string incluir = "insert into livros (isbn, nome_livro, nome_autor, editora, ano_public, cod_categ, img_livro) values ('";
                    incluir += txtISBN.Text + "',";
                    incluir += "'" + txtNome.Text + "',";
                    incluir += "'" + txtAutor.Text + "',";
                    incluir += "'" + txtEditora.Text + "',";
                    incluir += "'" + txtAno.Text + "',";
                    incluir += "'" + codCateg + "',";
                    incluir += "@img);";
                    MySqlCommand command = new MySqlCommand(incluir);
                    command.Parameters.Add("@img", MySqlDbType.Binary, img.Length).Value = img;
                    command.Connection = conn.Conectar();
                    command.ExecuteNonQuery();
                    conn.Desconectar();
                }
                else
                {
                    string incluir = "insert into livros (isbn, nome_livro, nome_autor, editora, ano_public, cod_categ) values ('";
                    incluir += txtISBN.Text + "',";
                    incluir += "'" + txtNome.Text + "',";
                    incluir += "'" + txtAutor.Text + "',";
                    incluir += "'" + txtEditora.Text + "',";
                    incluir += "'" + txtAno.Text + "',";
                    incluir += "'" + codCateg + "');";
                    MySqlCommand command = new MySqlCommand(incluir);
                    command.Connection = conn.Conectar();
                    command.ExecuteNonQuery();
                    conn.Desconectar();
                }

                MessageBox.Show("Cadastro realizado com sucesso!");

                Limpar();

                Recarregar();
            }
            catch (Exception)
            {
                MessageBox.Show("Algum campo está vazio!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            //desabilita o foreign key
            string des = "set foreign_key_checks=0;";
            MySqlCommand command1 = new MySqlCommand(des);
            command1.Connection = conn.Conectar();
            command1.ExecuteNonQuery();
            conn.Desconectar();

            //deleta o registro selecionado
            string registro = dvgConsulta.CurrentRow.Cells[0].Value.ToString();
            string delete = "DELETE FROM livros WHERE isbn = @isbn";
            MySqlCommand command = new MySqlCommand(delete);
            command.Connection = conn.Conectar();
            command.Parameters.Add(new MySqlParameter("@isbn", registro));
            command.ExecuteNonQuery();
            conn.Desconectar();

            //o foreign key volta a funcionar
            string ab = "set foreign_key_checks=0;";
            MySqlCommand command2 = new MySqlCommand(ab);
            command2.Connection = conn.Conectar();
            command2.ExecuteNonQuery();
            conn.Desconectar();

            Recarregar();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void btnDelTudo_Click(object sender, EventArgs e)
        {
            //desabilita o foreign key
            string des = "set foreign_key_checks=0;";
            MySqlCommand command1 = new MySqlCommand(des);
            command1.Connection = conn.Conectar(); 
            command1.ExecuteNonQuery();
            conn.Desconectar();

            //apaga todos registros
            string excluir = "TRUNCATE TABLE livros";
            MySqlCommand command = new MySqlCommand(excluir);
            command.Connection = conn.Conectar(); 
            command.ExecuteNonQuery();
            conn.Desconectar();

            //o foreign key volta a funcionar
            string ab = "set foreign_key_checks=0;";
            MySqlCommand command2 = new MySqlCommand(ab);
            command2.Connection = conn.Conectar(); 
            command2.ExecuteNonQuery();
            conn.Desconectar();

            Recarregar();
        }

        private void AreaBibliotecario_Load(object sender, EventArgs e)
        {
            Recarregar();
        }

        private void telaInicialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog(); 
            dialog.Filter = "jpg files(*.jpg)|*.jpg| png files(*.png)|*.png| All Files(*.*)|*.*";
            dialog.Title = "Select livros img_livro";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                caminhofoto = dialog.FileName.ToString();
            }
            else
            {
                caminhofoto = null;
            }
            lblImage.Text = caminhofoto;
        }

        private void picSair_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void btnCadastrar1_Click(object sender, EventArgs e)
        {
            try
            {
                //desabilita o foreign key
                string des = "set foreign_key_checks=0;";
                MySqlCommand command1 = new MySqlCommand(des);
                command1.Connection = conn.Conectar();
                command1.ExecuteNonQuery();
                conn.Desconectar();

                string incluir = "insert into emprestimos (cod_empr, data_empr, data_dev, id_usuario, isbn) values ('null',";
                incluir += "'" + dataEmpr.Value.ToString("yyyy/MM/dd") + "',";
                incluir += "'" + dataDevol.Value.ToString("yyyy/MM/dd") + "',";
                incluir += "'" + txtRM1.Text + "',";
                incluir += "'" + txtisbn1.Text + "');";
                MySqlCommand command = new MySqlCommand(incluir);
                command.Connection = conn.Conectar();
                command.ExecuteNonQuery();
                conn.Desconectar();

                MessageBox.Show("Cadastro realizado com sucesso!");

                Limpar();

                //o foreign key volta a funcionar
                string ab = "set foreign_key_checks=0;";
                MySqlCommand command2 = new MySqlCommand(ab);
                command2.Connection = conn.Conectar();
                command2.ExecuteNonQuery();
                conn.Desconectar();

                Recarregar();
            }
            catch (Exception)
            {
                MessageBox.Show("An error ocured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dvgConsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLupa_Click(object sender, EventArgs e)
        {
            String filtro = txtProcura.Text;
            String campo = cmbCampo.Text;
            if(campo == "ISBN")
            {
                campo = "isbn";
            }
            else if (campo == "Nome do Livro")
            {
                campo = "nome_livro";
            }
            else if (campo == "Autor")
            {
                campo = "nome_autor";
            }
            else
            {
                campo = "categoria.nome_categ";
            }
            MySqlDataReader temp = this.livros.ListarLivros(campo, filtro);
            DataTable dt = new DataTable();
            dt.Load(temp);
            dvgConsulta.DataSource = dt;
            for (var i = 0; i <= dvgConsulta.Rows.Count - 1; i++) //tamanho dos campos no DataGridView
            {
                DataGridViewRow r = dvgConsulta.Rows[i];
                r.Height = 75;
                dvgConsulta.Columns[4].Width = 65;
                dvgConsulta.Columns[6].Width = 70;
            }
            conn.Desconectar();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            isbn = dvgConsulta.CurrentRow.Cells[0].Value.ToString(); //linha selecionada
            //puxa os dados do dataGrid para os campos txt
            txtISBN.Text = dvgConsulta.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dvgConsulta.CurrentRow.Cells[1].Value.ToString();
            txtAutor.Text = dvgConsulta.CurrentRow.Cells[2].Value.ToString();
            txtEditora.Text = dvgConsulta.CurrentRow.Cells[3].Value.ToString();
            txtAno.Text = dvgConsulta.CurrentRow.Cells[4].Value.ToString();
            cboxCategoria.SelectedItem = dvgConsulta.CurrentRow.Cells[5].Value.ToString();
            lblImage.Text = "Nenhuma alteração";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //desabilita o foreign key
            string des = "set foreign_key_checks=0;";
            MySqlCommand command1 = new MySqlCommand(des);
            command1.Connection = conn.Conectar();
            command1.ExecuteNonQuery();
            conn.Desconectar();

            //deleta o campo selecionado
            int cod = int.Parse(dgvEmprest.CurrentRow.Cells[0].Value.ToString());
            string delete = "DELETE FROM emprestimos WHERE cod_empr = @cod";
            MySqlCommand command = new MySqlCommand(delete);
            command.Connection = conn.Conectar();
            command.Parameters.Add(new MySqlParameter("@cod", cod));
            command.ExecuteNonQuery();
            conn.Desconectar();

            //o foreign key volta a funcionar
            string ab = "set foreign_key_checks=0;";
            MySqlCommand command2 = new MySqlCommand(ab);
            command2.Connection = conn.Conectar();
            command2.ExecuteNonQuery();
            conn.Desconectar();

            Recarregar();
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            cod = int.Parse(dgvEmprest.CurrentRow.Cells[0].Value.ToString());
            //puxa os dados do dataGrid para os campos txt
            txtRM1.Text = dgvEmprest.CurrentRow.Cells[1].Value.ToString();
            txtisbn1.Text = dgvEmprest.CurrentRow.Cells[2].Value.ToString();
            dataEmpr.Text = dgvEmprest.CurrentRow.Cells[3].Value.ToString();
            dataDevol.Text = dgvEmprest.CurrentRow.Cells[4].Value.ToString();
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAlterar1_Click(object sender, EventArgs e)
        {
            try
            {
                //desabilita o foreign key
                string des = "set foreign_key_checks=0;";
                MySqlCommand command1 = new MySqlCommand(des);
                command1.Connection = conn.Conectar();
                command1.ExecuteNonQuery();
                conn.Desconectar();

                //atualiza os dados dos empréstimos
                string update = "UPDATE emprestimos SET data_empr ='" + dataEmpr.Value.ToString("yyyy/MM/dd") +  "' WHERE cod_empr = '" + cod + "';";
                update += "UPDATE emprestimos SET data_dev ='" + dataDevol.Value.ToString("yyyy/MM/dd") + "' WHERE cod_empr = '" + cod + "';";
                update += "UPDATE emprestimos SET isbn ='" + txtisbn1.Text + "' WHERE cod_empr = '" + cod + "';";
                update += "UPDATE emprestimos SET id_usuario ='" + txtRM1.Text + "' WHERE cod_empr = '" + cod + "';";
                MySqlCommand command = new MySqlCommand(update);
                command.Connection = conn.Conectar();
                command.ExecuteNonQuery();
                conn.Desconectar();

                MessageBox.Show("Alteração realizada com sucesso!");

                Limpar();

                //ativa o foreign key
                string ab = "set foreign_key_checks=0;";
                MySqlCommand command2 = new MySqlCommand(ab);
                command2.Connection = conn.Conectar();
                command2.ExecuteNonQuery();
                conn.Desconectar();

                Recarregar();
            }
            catch (Exception)
            {
                MessageBox.Show("An error ocured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("Arquivo baixado em " + fbd.SelectedPath);
            }

            var pxPorMM = 72 / 25.2F;
            Document doc = new Document(PageSize.A4, 15 * pxPorMM, 15 * pxPorMM, 15 * pxPorMM, 20 * pxPorMM);
            string caminho = fbd.SelectedPath + @"\" + "Relatório." + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf";

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

            //título
            Paragraph titulo = new Paragraph();
            titulo.Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 30, iTextSharp.text.Font.BOLD);
            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.Add("DEVOLUÇÕES ATRASADAS");
            doc.Add(titulo);

            //tabela do relatório
            PdfPTable table = new PdfPTable(4);
            table.DefaultCell.BorderWidth = 0;
            table.WidthPercentage = 100;

            string cmd = "SELECT emprestimos.isbn as 'ISBN', nome_livro as 'Livro', data_empr as 'Data Emp.', data_dev as 'Data Devol.'";
            cmd += " FROM emprestimos JOIN livros ON emprestimos.isbn = livros.isbn and nome_livro = livros.nome_livro ";
            cmd += " WHERE data_dev < '" + DateTime.Now.ToString("dd-MM-yyyy") + "';";
            MySqlCommand command = new MySqlCommand(cmd);
            command.Connection = conn.Conectar();
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;

            while (i <= dt.Rows.Count)
            {
                table.AddCell(dt.Rows[i].ItemArray[0].ToString());
                table.AddCell(dt.Rows[i].ItemArray[1].ToString());
                table.AddCell(dt.Rows[i].ItemArray[2].ToString());
                table.AddCell(dt.Rows[i].ItemArray[3].ToString());
            }

            doc.Add(table);

            doc.Close();
            System.Diagnostics.Process.Start(caminho);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                //desabilita o foreign key
                string des = "set foreign_key_checks=0;";
                MySqlCommand command1 = new MySqlCommand(des);
                command1.Connection = conn.Conectar();
                command1.ExecuteNonQuery();
                conn.Desconectar();

                //atualiza os dados dos livros 
                string update = "UPDATE livros SET isbn ='" + txtISBN.Text + "' WHERE isbn = '" + isbn + "';";
                update += "UPDATE livros SET nome_livro ='" + txtNome.Text + "' WHERE isbn = '" + isbn + "';";
                update += "UPDATE livros SET nome_autor ='" + txtAutor.Text + "' WHERE isbn = '" + isbn + "';";
                update += "UPDATE livros SET editora ='" + txtEditora.Text + "' WHERE isbn = '" + isbn + "';";
                update += "UPDATE livros SET ano_public ='" + txtAno.Text + "' WHERE isbn = '" + isbn + "';";
                update += "UPDATE livros SET cod_categ ='" + codCateg + "' WHERE isbn = '" + isbn + "';";
                MySqlCommand command = new MySqlCommand(update);
                command.Connection = conn.Conectar();
                command.ExecuteNonQuery();
                conn.Desconectar();

                if (lblImage.Text != "Nenhuma alteração")
                {
                    byte[] img = this.livros.ConverteImagemParaByte(caminhofoto);
                    string update1 = "UPDATE livros SET img_livro = @img WHERE isbn = '" + isbn + "';";

                    MySqlCommand cmd = new MySqlCommand(update1);
                    cmd.Parameters.Add("@img", MySqlDbType.Binary, img.Length).Value = img;
                    cmd.Connection = conn.Conectar();
                    cmd.ExecuteNonQuery();
                    conn.Desconectar();
                }

                MessageBox.Show("Alteração realizada com sucesso!");

                Limpar();

                //o foreign key volta a funcionar
                string ab = "set foreign_key_checks=0;";
                MySqlCommand command2 = new MySqlCommand(ab);
                command2.Connection = conn.Conectar();
                command2.ExecuteNonQuery();
                conn.Desconectar();

                Recarregar();
            }
            catch (Exception)
            {
                MessageBox.Show("An error ocured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
