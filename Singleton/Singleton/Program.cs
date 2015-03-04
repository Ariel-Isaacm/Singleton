using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            //prueba en un solo hilo
            Database db = Database.getDatabase();
           // Database db2 = Database.getDatabase();
            //Console.WriteLine(db==db2); true
            
            

            //prueba con hilos
            Thread oThread = new Thread(new ThreadStart(Alpha));

            // Start the thread
            oThread.Start();
            Console.ReadKey();
        }
        
        public static void Alpha()
        {
            Database.getDatabase();

        }
    }
}
