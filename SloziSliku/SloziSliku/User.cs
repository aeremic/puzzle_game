using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloziSliku {
    class User {
        private int id;
        private string username;
        private string timeElapsed;
        private int movesNum;

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string TimeElapsed { get => timeElapsed; set => timeElapsed = value; }
        public int MovesNum { get => movesNum; set => movesNum = value; }
    }
}
