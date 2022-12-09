using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Biblioteca
{
    class Livros
    {
        Connect conn = new Connect();

        //listar todos os itens
        public MySqlDataReader ListarLivros()
        {
            string selecionar = "SELECT isbn as 'ISBN', nome_livro as 'NOME DO LIVRO', nome_autor as 'AUTOR', editora as 'EDITORA',";
            selecionar += " ano_public as 'ANO PUBLIC', nome_categ as 'CATEGORIA', img_livro as 'IMAGEM' From livros";
            selecionar += " JOIN categoria ON livros.cod_categ=categoria.cod_categ ORDER BY nome_livro ASC;";
            MySqlCommand command = new MySqlCommand(selecionar);
            command.Connection = conn.Conectar(); //abre a conexão
            return command.ExecuteReader();
        }

        //prucrar item na tabela
        public MySqlDataReader ListarLivros(String campo, String filtro)
        {
            if(filtro == "")
            {
                return ListarLivros();
            }
            string procurar = "SELECT isbn as 'ISBN', nome_livro as 'NOME DO LIVRO', nome_autor as 'AUTOR', editora as 'EDITORA',";
            procurar += " ano_public as 'ANO PUBLIC', nome_categ as 'CATEGORIA', img_livro as 'IMAGEM' From livros";
            procurar += " JOIN categoria ON livros.cod_categ=categoria.cod_categ WHERE " + campo + " like '" + filtro + "%';";
            MySqlCommand command = new MySqlCommand(procurar);
            command.Connection = conn.Conectar(); //abre a conexão
            return command.ExecuteReader();  //executa o comando
        }

        public byte[] ConverteImagemParaByte(string caminhofoto)
        {
            byte[] img = null;

            FileStream fs = new FileStream(caminhofoto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            img = br.ReadBytes((int)fs.Length);
            return img;
        }

        public Image ConverteByteParaImagem(Object valor)
        {
            Image imagem = null;

            try
            {
                if (valor != System.DBNull.Value)
                {
                    //converte os bytes vindos do banco em uma imagem
                    byte[] img = (byte[])valor;
                    MemoryStream ms = new MemoryStream(img);
                    imagem = Image.FromStream(ms);
                }
            }
            catch { imagem = null; }

            return imagem; 
        }
    }
}
