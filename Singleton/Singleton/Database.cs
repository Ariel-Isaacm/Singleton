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
        private Database db;
        private static Object thisLock =new Object();
        public string cadenaConexion;
        SqlConnection con;
        private Database()
        {
            cadenaConexion = "SERVER=localhost,1433;DATABASE=bd;User ID=Alien;Password=Pringles92;Trusted_Connection=False";
            con = new SqlConnection(cadenaConexion);
            
        }

        public Database getDatabase()
        {
            lock (thisLock)
            {
                if (db == null)
                {
                    return new Database();
                }
                else
                {
                    return db;
                }
            }
        }
    }
}
