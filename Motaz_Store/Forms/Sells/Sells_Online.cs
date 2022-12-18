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
    public partial class Sells_Online : Form
    {
        Msgs msg;
        bool showdeleted = false;
        public Sells_Online()
        {
            InitializeComponent();

            // Messages Handler
            msg = new Msgs(lbl_Error);

            // Load Colors
            cbox_Color.DataSource = GetDataArray("SELECT * FROM Colors");

            #region txts' Control
            // txt_Code
            // -----
            // - EnterClick
            txt_Code.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Code.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_Search.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // -------------------
            // txt_Art
            // ------
            // - EnterClick
            txt_Art.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Art.Text) && E.KeyCode == Keys.Enter)
                {
                    cbox_Color.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // -------------------
            // cbox_Color
            // ------
            // - EnterClick
            cbox_Color.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                {
                    txt_Size.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // -------------------
            // txt_Size
            // ------
            // - EnterClick
            txt_Size.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Size.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_Search.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // - TextChanged -> Handle Entering non-numbers
            txt_Size.TextChanged += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Size.Text))
                {
                    try
                    {
                        Convert.ToInt32(txt_Size.Text);
                    }
                    catch
                    {
                        txt_Size.Text = "";
                        msg.ShowError("يجب إدخال ارقام صحيحة فقط للمقاس");
                        txt_Size.Focus();
                    }
                }
            };
            #endregion
        }

        private void LoadDataToDGV(string where, string[] p = null)
        {
            DGVs("SELECT P_ID كود, Des وصف, Price السعر, Dis الخصم, Qty الكمية, Seller البائع, Date_Time التاريخ FROM Online " + where, dgv_Online, p);

            // Add Delete Button
            DataGridViewButtonColumn c = new DataGridViewButtonColumn();
            c.HeaderText = "إرجاع";
            c.FlatStyle = FlatStyle.Flat;
            c.DefaultCellStyle.BackColor = Color.FromArgb(89, 25, 2);
            c.DefaultCellStyle.ForeColor = Color.FromArgb(255, 244, 230);
            c.Text = "إرجاع";
            c.UseColumnTextForButtonValue = true;
            c.Name = "Delete";
            dgv_Online.Columns.Add(c);
        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataToDGV("WHERE Status=1");
                showdeleted = false;
            }
            catch
            {
                msg.ShowError("قم بإضافة التحديث أولا");
            }
        }

        private void Ch_Code_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_Code.Checked)
            {
                pnl_Code.Hide();
                pnl_Art.Show();
                txt_Art.Focus();
            }
            else
            {
                pnl_Code.Show();
                pnl_Art.Hide();
                txt_Code.Focus();
            }
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            int ID;
            if(ch_Code.Checked)
            {
                ID = GetDataInt("SELECT ID FROM Products WHERE Art=@Art AND Color=@Color AND Size=@Size", new string[]
                { "@Art", txt_Art.Text, "@Color", cbox_Color.Text, "@Size", txt_Size.Text });
            }
            else
            {
                ID = Convert.ToInt32(txt_Code.Text);
            }

            LoadDataToDGV("WHERE P_ID=@P_ID", new string[] { "@P_ID", ID.ToString() });
        }

        private void Dgv_Online_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 7)
            {
                if(Visual_Scripts.AskUser("هل انت متأكد من انك تريد إرجاع هذا المنتج؟", 'q'))
                {
                    if(CmdExcuteRows("DELETE FROM ONLINE WHERE P_ID=@P_ID AND Date_Time=@Date_Time", new string[]
                        { "@P_ID", dgv_Online.Rows[e.RowIndex].Cells[0].Value.ToString(),
                            "@Date_Time", dgv_Online.Rows[e.RowIndex].Cells[6].Value.ToString() }) != 1)
                    {
                        msg.ShowError("حدث خطأ ما");
                    }
                    else
                    {
                        msg.ShowError("تم الإرجاع بنجاح", true);
                        btn_Refresh.PerformClick();
                    }
                }
            }
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            if (showdeleted) return;

            // Ask User
            if (!Visual_Scripts.AskUser("هل انت متأكد من أنك تريد أرشفة التحولات؟\nهذه العملية لا يمكن التراجع عنها", 'q') ||
                !Session.isManager)
            {
                msg.ShowError("لا تمتلك صلاحيات القيام بذلك");
                return;
            }

            if (CmdExcuteRows("UPDATE Online SET Status = 0 WHERE Status = 1") > 0)
            {
                // DONE
                msg.ShowError("تم الأرشفة بنجاح", true);
                btn_Refresh.PerformClick();
            }
            else
            {
                // FAILED
                msg.ShowError("حدث خطأ ما");
            }
        }

        private void Btn_ShowDeleted_Click(object sender, EventArgs e)
        {
            LoadDataToDGV(" WHERE Status = 0");
            showdeleted = true;
        }
    }
}
