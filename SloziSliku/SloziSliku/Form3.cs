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
    public partial class Form3 : Form {

        private string username;
        public static Boolean closed;

        public Form3() {
            InitializeComponent();

            closed = false;
        }

        public string getUsername() {
            return username;
        }

        private void btn_start_Click(object sender, EventArgs e) {
            username = tb_username.Text;
            closed = true;
            this.Close();
        }
    }
}
