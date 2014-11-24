using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IO;

namespace DIDCOM_ASP
{
    class Conexion
    {
        public string cadenaConexion;
        SqlConnection con;

        public Conexion()
        {


            //cadenaConexion = "Data Source=DNB-PC\\SQLEXPRESS;Initial Catalog=lciFinal;User ID=denneb;Password=estaciones2;Trusted_Connection=False";
            try
            {
                // cadenaConexion = "Data Source=" + AdmLCI.Properties.Settings.Default.servidor
                //+ ";Initial Catalog=" + AdmLCI.Properties.Settings.Default.basedatos
                //+ ";User ID=" + AdmLCI.Properties.Settings.Default.usuario +
                //";Password=" + AdmLCI.Properties.Settings.Default.contrasenia +
                //";Trusted_Connection=False ;";
                //esta es la buena

                cadenaConexion = "SERVER=java.isi.uson.mx,1433;DATABASE=DIDCOMDDAY;User ID=Alien;Password=Pringles92;Trusted_Connection=False";
                // cadenaConexion = "Data Source=Win8\\ISI;Initial Catalog=LCIFinal;User ID=admin;Password=admin  ;Trusted_Connection=False";

                con = new SqlConnection(cadenaConexion);

            }
            catch (SqlException se)
            {

            }
        }

        public string obtenerBD()
        {
            String line = "";
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("config.txt");

                //Read the first line of text
                line = sr.ReadLine();

                string bd = "";
                //Continue to read until you reach end of file
                while (line != null)
                {

                    if (line.StartsWith("basedatos:"))
                    {
                        bd = line.Substring(line.IndexOf(":") + 1);

                    }

                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                return bd;
                //Console.ReadLine();
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error al obtener los datos");
                //con.Close();
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");
            }
            return "";
        }

        public string obtenerServidor()
        {
            String line = "";
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("config.txt");

                //Read the first line of text
                line = sr.ReadLine();

                string srv = "";
                //Continue to read until you reach end of file
                while (line != null)
                {

                    if (line.StartsWith("servidor:"))
                    {
                        srv = line.Substring(line.IndexOf(":") + 1);

                    }

                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                return srv;
                //Console.ReadLine();
            }
            catch (Exception e)
            {
                //con.Close();
                //MessageBox.Show("Error al obtener los datos");
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");
            }
            return "";
        }

        public string obtenerUsr()
        {
            String line = "";
            try
            {

                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("config.txt");

                //Read the first line of text
                line = sr.ReadLine();

                string usr = "";
                //Continue to read until you reach end of file
                while (line != null)
                {

                    if (line.StartsWith("usuario:"))
                    {
                        usr = line.Substring(line.IndexOf(":") + 1);

                    }

                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                return usr;
                //Console.ReadLine();
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error al obtener los datos");
                //con.Close();
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");
            }
            return "localhost";
        }

        public string obtenerPass()
        {
            String line = "";
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("config.txt");

                //Read the first line of text
                line = sr.ReadLine();

                string pass = "";
                //Continue to read until you reach end of file
                while (line != null)
                {

                    if (line.StartsWith("contrasenia:"))
                    {
                        pass = line.Substring(line.IndexOf(":") + 1);

                    }

                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                return pass;
                //Console.ReadLine();
            }
            catch (Exception e)
            {
                //con.Close();
                //MessageBox.Show("Error al obtener los datos");
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");
            }
            return "localhost";
        }

        public bool Exists(String Id, string sql)
        {

            SqlCommand command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("Id", Id);

            con.Open();

            int count = Convert.ToInt32(command.ExecuteScalar());
            con.Close();
            if (count == 0)
                return false;
            else
                return true;


        }



        public bool modificar(string consulta)
        {
            try
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sql)
                {

                    return false;
                }
                SqlCommand comando = new SqlCommand(consulta, con);
                comando.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (SqlException ex)
            {
                MensajeError(ex);
                con.Close();
                return false;

            }
        }

        public int contarRegistros(string consulta)
        {
            int tot = 0;
            try
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sql)
                {

                    return 0;
                }
                SqlCommand comando = new SqlCommand(consulta, con);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    tot = int.Parse(reader[0].ToString());
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                //con.Close();
                MensajeError(ex);
            }
            return tot;
        }


        public bool existe(string columna, string tabla, string condicion)
        {
            bool bandera = false;

            try
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sql)
                {


                    return false;
                }
                SqlCommand comando = new SqlCommand("select " + columna + " from " + tabla + " where " + condicion, con);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    bandera = true;
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                // con.Close();
                MensajeError(ex);
            }

            return bandera;
        }

        public bool existe(string consulta)
        {
            bool bandera = false;

            try
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sql)
                {

                    return false;
                }
                SqlCommand comando = new SqlCommand(consulta, con);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    bandera = true;
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MensajeError(ex);
                // con.Close();
            }


            return bandera;
        }

        public void MensajeError(SqlException ex)
        {
            StringBuilder errorMessages = new StringBuilder();
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errorMessages.Append("Index #" + i + "\n" +
                    "Message: " + ex.Errors[i].Message + "\n" +
                    "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                    "Source: " + ex.Errors[i].Source + "\n" +
                    "Numero: " + ex.Errors[i].Number + "\n");
            }

        }

        public void abrirConexion()
        {

            try
            {
                con.Open();

            }
            catch (Exception e)
            {

                con.Close();
            }

        }

        public void cerrarConexion()
        {
            con.Close();
        }

        /*Recibe la linea de consulta completa y regresa un string con el resultado*/

        public string consultaLibreS(string consulta)
        {
            string resultado = "";
            try
            {
                abrirConexion();
                SqlCommand comm = new SqlCommand(consulta, con);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                    resultado = reader[0].ToString();
                cerrarConexion();
                return resultado;
            }
            catch (SqlException ex)
            {
                MensajeError(ex);
                //con.Close();
            }
            catch (Exception e)
            {

                // con.Close();
            }
            return "";
        }

        /*Recibe la linea de consulta completa y regresa un DataTable con el resultado*/
        public DataTable consultaLibreDT(string lineaConsulta)
        {
            DataTable t = new DataTable();
            try
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sql)
                {

                    con.Close();
                    return t;
                }
                SqlDataAdapter adapter = new SqlDataAdapter(lineaConsulta, con);
                adapter.Fill(t);
                cerrarConexion();
            }
            catch (SqlException ex)
            {
                MensajeError(ex);
                con.Close();
            }
            catch (Exception e)
            {

            }

            return t;
        }


        //Permite insertar datos en una tabla.
        public string insertarVarchar(string[] campos, string[] datos, string tabla)
        {
            string consulta = "INSERT INTO " + tabla + " (";
            for (int i = 0; i < campos.Length; i++)
            {
                consulta = consulta + campos[i];
                if (i < campos.Length - 1)
                    consulta = consulta + ",";
            }
            consulta = consulta + ") VALUES (";
            for (int i = 0; i < datos.Length; i++)
            {
                consulta = consulta + "\'" + datos[i] + "\'";
                if (i < datos.Length - 1)
                    consulta = consulta + ",";
            }
            consulta = consulta + ")";
            try
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sql)
                {

                    con.Close();
                    return "";
                }
                SqlCommand comando = new SqlCommand(consulta, con);
                comando.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                MensajeError(ex);
            }
            catch (Exception e)
            {

            }
            return consulta;
        }

        //Permite insertar datos en una tabla.
        public string insertarEnTabla(string[] campos, string[] datos, string tabla)
        {
            string consulta = "INSERT INTO " + tabla + " (";
            for (int i = 0; i < campos.Length; i++)
            {
                consulta = consulta + campos[i];
                if (i < campos.Length - 1)
                    consulta = consulta + ",";
            }
            consulta = consulta + ") VALUES (";
            for (int i = 0; i < datos.Length; i++)
            {
                consulta = consulta + datos[i];
                if (i < datos.Length - 1)
                    consulta = consulta + ",";
            }
            consulta = consulta + ")";
            try
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sql)
                {

                    return "";
                }
                SqlCommand comando = new SqlCommand(consulta, con);
                comando.ExecuteNonQuery();
                cerrarConexion();
            }
            catch (SqlException ex)
            {
                MensajeError(ex);
                // con.Close();
            }
            catch (Exception e)
            {

            }
            return consulta;
        }



        public bool borrarRegistro(string condicion, string tabla)
        {
            string consulta = "DELETE FROM " + tabla + " WHERE " + condicion;
            try
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sql)
                {

                    return false;
                }
                SqlCommand comando = new SqlCommand(consulta, con);
                comando.ExecuteNonQuery();
                cerrarConexion();
                return true;
            }
            catch (Exception e)
            {
                con.Close();
                return false;
            }


        }

        public bool Validar_exp(string a)
        {
            bool flag = true;

            if ((!a.Equals("")) && (a.Length == 9))
            {
                for (int n = 0; n < a.Length; n++)
                {
                    if (!Char.IsNumber(a, n))

                        flag = false;
                }

            }
            else
            {

                flag = false;
            }
            return flag;
        }

        public DataTable consulta(string[] campos, string tabla, string[] nombreCampos)
        {
            string consulta = "SELECT ";
            DataTable t = new DataTable();
            try
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sql)
                {

                    con.Close();
                    return t;
                }
                //El ciclo crea la cadena consulta con los campos recibidos.
                for (int i = 0; i < campos.Length; i++)
                {
                    consulta = consulta + campos[i] + " AS " + "\"" + nombreCampos[i] + "\"";
                    if (i < campos.Length - 1)
                        consulta = consulta + ",";
                }
                consulta = consulta + " FROM " + tabla;
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, con);
                adapter.Fill(t);
                cerrarConexion();
            }
            catch (Exception e) { }

            return t;
        }

        public DataTable consulta(string[] campos, string tabla, string[] nombreCampos, string condicion)
        {
            string consulta = "SELECT ";
            DataTable t = new DataTable();
            try
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sql)
                {

                    return t;
                }
                //El ciclo crea la cadena consulta con los campos recibidos.
                for (int i = 0; i < campos.Length; i++)
                {
                    consulta = consulta + campos[i] + " AS " + "\"" + nombreCampos[i] + "\"";
                    if (i < campos.Length - 1)
                        consulta = consulta + ",";
                }
                consulta = consulta + " FROM " + tabla + " WHERE " + condicion;
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, con);
                adapter.Fill(t);
                con.Close();
            }
            catch (Exception e) { }

            return t;
        }


    }
}
