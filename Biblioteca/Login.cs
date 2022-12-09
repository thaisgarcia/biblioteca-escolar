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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtSenha.PasswordChar = '*';
        }

        public static string id_usuario;
        public static string nome;
        public static string turma;
        public static string telefone;
        Connect conn = new Connect();

        private void picSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void Limpar()
        {
            txtRM.Clear();
            txtSenha.Clear();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if(cboxUsuarios.Text == "Aluno")
            {
                string cmd = "SELECT * FROM usuario WHERE id_usuario='" + txtRM.Text + "' AND senha='" + txtSenha.Text; 
                cmd += "' AND tipo_usuario=1";
                MySqlCommand command = new MySqlCommand(cmd);
                command.Connection = conn.Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    id_usuario = dt.Rows[0]["id_usuario"].ToString();
                    nome = dt.Rows[0]["nome_usuario"].ToString();
                    turma = dt.Rows[0]["turma"].ToString();
                    telefone = dt.Rows[0]["telefone"].ToString();

                    this.Hide();
                    TelaInicial inicial = new TelaInicial();
                    inicial.Show();
                }
                else
                {
                    MessageBox.Show("Dados inválidos, verifique seu RM ou senha!");
                    Limpar();
                }

                conn.Desconectar();
            }
            else if (cboxUsuarios.Text == "Professor")
            {
                string cmd = "SELECT * FROM usuario WHERE id_usuario='" + txtRM.Text + "' AND senha='" + txtSenha.Text;
                cmd += "' AND tipo_usuario=2";
                MySqlCommand command = new MySqlCommand(cmd);
                command.Connection = conn.Conectar();
                DataTable dt = new System.Data.DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    id_usuario = dt.Rows[0]["id_usuario"].ToString();
                    nome = dt.Rows[0]["nome_usuario"].ToString();
                    turma = dt.Rows[0]["turma"].ToString();
                    telefone = dt.Rows[0]["telefone"].ToString();

                    this.Hide();
                    TelaInicial inicial = new TelaInicial();
                    inicial.Show();
                }
                else
                {
                    MessageBox.Show("Dados inválidos, verifique seu RM ou senha!");
                    Limpar();
                }

                conn.Desconectar();
            }
            else if(cboxUsuarios.Text == "Bibliotecário")
            {
                string cmd = "SELECT COUNT(*) FROM usuario WHERE id_usuario='" + txtRM.Text + "' AND senha='" + txtSenha.Text;
                cmd += "' AND tipo_usuario=3";
                MySqlCommand command = new MySqlCommand(cmd);
                command.Connection = conn.Conectar();
                DataTable dt = new System.Data.DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                da.Fill(dt);

                foreach (DataRow list in dt.Rows)
                {
                    if (Convert.ToInt32(list.ItemArray[0]) > 0)
                    {
                        AreaBibliotecario bibliotecario = new AreaBibliotecario();
                        this.Hide();
                        bibliotecario.Show();
                    }
                    else
                    {
                        MessageBox.Show("Dados inválidos, verifique seu RM ou senha!");
                        Limpar();
                    }
                }
                conn.Desconectar();   
            }
        }

        private void cboxUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
       
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
