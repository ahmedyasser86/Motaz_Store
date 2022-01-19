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
    public partial class Store_Add : Form
    {
        public Store_Add()
        {
            InitializeComponent();

            // Make Selected Index of cBox Type
            cbox_Type.SelectedIndex = 0;

            // Get Colors From DB
            cbox_Color.DataSource = GetDataArray("SELECT * FROM Colors");

            // Focus txt_Art
            txt_Art.Focus();
        }

        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_New.Checked)
            {
                // Add New Product
                RemoveReadOnly(txt_Price);
                RemoveReadOnly(txt_Des);
                cbox_Type.Items.Clear();
                cbox_Type.Items.Add("حذاء"); cbox_Type.Items.Add("إكسسوار");
                cbox_Type.SelectedIndex = 0;
                cbox_Type.BackColor = Color.FromArgb(255, 244, 230);

            }
            else
            {
                // Add Tekrar
                MakeReadOnly(txt_Price, "0");
                MakeReadOnly(txt_Des, "");
                cbox_Type.Items.Clear();
                cbox_Type.BackColor = Color.FromArgb(209, 178, 140);
            }
        }

        void MakeReadOnly(TextBox txt, string text)
        {
            txt.ReadOnly = true;
            txt.BackColor = Color.FromArgb(209, 178, 140);
            txt.Text = text;
        }
        void RemoveReadOnly(TextBox txt)
        {
            txt.ReadOnly = false;
            txt.BackColor = Color.FromArgb(255, 244, 230);
            txt.Text = "";
        }

        private void rb_Size_CheckChanged(object sender, EventArgs e)
        {
            if (rb_SManual.Checked)
            {
                // Qty Not Manual
                MakeReadOnly(txt_Qty, "0");

                // Show Manual TextBox
                pnl_SizeMan.Show();

                // Hide Auto TextBox
                pnl_SizeAuto.Hide();
            }

            else
            {
                // Qty Manual
                RemoveReadOnly(txt_Qty);

                // Hide Manual TextBox
                pnl_SizeMan.Hide();

                // Show Auto TextBox
                pnl_SizeAuto.Show();
            }
        }
    }
}
