using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Motaz_Store.myDB;

namespace Motaz_Store
{
    public partial class Settings_Bika : Form
    {
        public Settings_Bika()
        {
            InitializeComponent();
        }

        private void Btn_ex_Click(object sender, EventArgs e)
        {
            string msg = CmdExcuteMSG(txtQuery.Text);
            if (msg != null)
            {
                MessageBox.Show(msg);
            }
            else
            {
                MessageBox.Show("Done!");
            }
        }

        private void Btn_exRows_Click(object sender, EventArgs e)
        {
            int rows = CmdExcuteRows(txtQuery.Text);
            if(rows > 0)
            {
                MessageBox.Show("Effected Rows = " + rows);
            }
            else
            {
                MessageBox.Show("Failed!");
            }
        }

        private void Btn_dgv_Click(object sender, EventArgs e)
        {
            DGVs(txtQuery.Text, dgv_Data);
        }
    }
}
