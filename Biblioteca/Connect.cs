using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Biblioteca
{
    class Connect
    {
        MySqlConnection conn = new MySqlConnection("server=localhost; database=biblioteca; user=root; pwd=''");

        public MySqlConnection Conectar()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        public MySqlConnection Desconectar()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            return conn;
        }
    }
}
