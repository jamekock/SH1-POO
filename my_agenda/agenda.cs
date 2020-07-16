using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace my_agenda
{
    class agenda
    {
        private SQLiteCommand cmd = null;
        private SQLiteDataReader reader = null;
        private DataTable table = null;
        private SQLiteConnection cn = null;

        //metodo para insertar en la base de datos
        public bool insertar(string nombre, string telefono)
        {
            try
            {
                string query = "INSERT INTO directorio(nombre, telefono)VALUES('" + nombre + "','" + telefono + "')";
                cn = Conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;

        }

        private void nombresColumnas()
        {
            table = new DataTable();
            table.Columns.Add("Id");
            table.Columns.Add("Nombre");
            table.Columns.Add("Telefono");
        }
        //metodo para consultar
        public DataTable consultar()
        {
            try
            {
                nombresColumnas();
                string query = "SELECT * FROM directorio";
                cn = Conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add(new object[] { reader["id"], reader["nombre"], reader["telefono"] });
                }
                reader.Close();
                cn.Close();
                return table;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return table;
        }
        //metodo para eliminar
        public bool eliminar(int id)
        {
            try
            {
                string query = "DELETE FROM directorio WHERE id= '" + id + "'";
                cn = Conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    cn.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ocurrio un Error en el proceso");
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;

        }
        //metodo para filtrar
        //public DataTable filtrar(string filtro)
        //{
        //}
        //metodo para actualizar
        public bool actualizar(int id, string nombre, string telefono)
        {
            try
            {
                string query = "UPDATE directorio SET nombre='" + nombre + "',telefono='" + telefono + "'WHERE id='" + id.ToString() + "'";
                MessageBox.Show(query);
                cn = Conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    cn.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ocurrion un Error en el proceso");
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;
        }
    }
}