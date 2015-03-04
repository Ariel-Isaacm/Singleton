using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    class Database
    {
        private static Database db;
        private static Object thisLock =new Object();
        public string cadenaConexion;
        SqlConnection con;
        private Database()
        {
            cadenaConexion = "SERVER=server;DATABASE=bd;User ID=User;Password=Passwrd;Trusted_Connection=False";
            con = new SqlConnection(cadenaConexion);
            
        }

        public static Database getDatabase()
        {
            lock (thisLock)
            {
                if (db == null)
                {
                    Console.WriteLine("La referencia no existe, la crearé y la regresaré");
                    return db=new Database();
                }
                else
                {
                    Console.WriteLine("Ya existe la referencia, te la regreso");
                    return db;
                }
            }
        }
    }
}
