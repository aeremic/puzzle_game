using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloziSliku {
    class StatesDB {
        private SqlConnection connection;
        private List<User> users;

        public void add(string username, TimeSpan timeElapsed, int movesNum) {
            connection = ConnectionDB.Instance;
            connection.Open();

            String query = "insert into states values(@username, @timeElapsed, @movesNum)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@timeElapsed", timeElapsed);
            command.Parameters.AddWithValue("@movesNum", movesNum);

            int res = command.ExecuteNonQuery();
            if (res == 0)
                throw new Exception("err while inserting");

            connection.Close();
        }

        public List<User> getUsers() {
            connection = ConnectionDB.Instance;
            connection.Open();

            users = new List<User>();

            String query = "select top 10 * from states order by moves_number asc";
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                users.Add(new User {
                    Id = int.Parse(reader["id"].ToString()),
                    Username = reader["username"].ToString(),
                    TimeElapsed = reader["time_elapsed"].ToString(),
                    MovesNum = int.Parse(reader["moves_number"].ToString())
                });
            }

            connection.Close();
            return users;
        }
    }
}
