using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motaz_Store
{
    public partial class Store_View : Form
    {
        public Store_View()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            myDB.DGVs(richTextBox1.Text, dataGridView1);
        }
    }
}
