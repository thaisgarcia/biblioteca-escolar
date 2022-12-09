using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Biblioteca
{
    class Emprestimos
    {
        Connect conn = new Connect();

        //listar todos os itens
        public MySqlDataReader ListarEmprestimos()
        {
            string selecionar = "SELECT cod_empr as 'Codigo', id_usuario as 'ID Usuario', isbn as 'ISBN', data_empr as 'Data Emp.', data_dev as 'Data Devol.' FROM emprestimos ORDER BY cod_empr ASC;";
            MySqlCommand command = new MySqlCommand(selecionar);
            command.Connection = conn.Conectar(); //abre a conexão
            return command.ExecuteReader();
        }
    }
}
