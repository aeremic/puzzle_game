using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SloziSliku {
    public partial class Form2 : Form {
        StatesDB statesDB;

        public Form2() {
            InitializeComponent();
            statesDB = new StatesDB();
        }

        private void Form2_Load(object sender, EventArgs e) {
            List<User> users = statesDB.getUsers();

            listView1.FullRowSelect = true;
            listView1.Columns.Clear();
            listView1.Items.Clear();

            listView1.Columns.Add("Ime igraca", 150);
            listView1.Columns.Add("Potrebno vreme", 150);
            listView1.Columns.Add("Potrebnih poteza", 150);

            foreach (var user in users) {
                ListViewItem item = new ListViewItem(
                    new[] {
                        user.Username,
                        user.TimeElapsed,
                        user.MovesNum.ToString()
                    });
                listView1.Items.Add(item);
            }
        }
    }
}
