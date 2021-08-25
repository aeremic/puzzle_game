using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloziSliku {
    class ConnectionDB {
        private static SqlConnection instance;
        private static readonly object padlock = new object();

        public static SqlConnection Instance {
            get {
                lock (padlock) {
                    if (instance == null)
                        instance = new SqlConnection(Properties.Settings.Default.states_dbConnectionString);
                    return instance;
                }
            }
        }
    }
}
