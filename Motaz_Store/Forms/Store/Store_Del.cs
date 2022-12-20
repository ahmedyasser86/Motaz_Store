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
using static Motaz_Store.Visual_Scripts;

namespace Motaz_Store
{
    public partial class Store_Del : Form
    {
        public class Sizes
        {
            public
            int Size,
            Qty = 0;
            public string color;

            public Sizes(int s, int q = 1 , string c = null)
            {
                Size = s;
                Qty = q;
                color = c;
            }
        }
        Msgs msg;
        List<int> ManSizes = new List<int>();
        string Art = "";
        public Store_Del()
        {
            InitializeComponent();

            // Error Message Handler
            msg = new Msgs(lbl_Error);

            // Focus txtArt
            txt_Art.Focus();

            #region Txts EnterClicks & Leave

            // --> txtArt
            // EnterClick
            txt_Art.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Art.Text) && E.KeyCode == Keys.Enter)
                {
                    if (ch_All.Checked)
                        Btn_Forward.PerformClick();
                    else
                        cbox_Color.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // Leave
            txt_Art.Leave += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    // If no Changes
                    if (Art == txt_Art.Text) return;

                    // Get Colors
                    cbox_Color.DataSource = GetDataArray("SELECT DISTINCT(Color) FROM Products WHERE Art=@Art",
                        new string[] { "@Art", txt_Art.Text });

                    Art = txt_Art.Text;

                    // Art not Found
                    if (cbox_Color.Items.Count < 1)
                    {
                        msg.ShowError("لا يوجد منتجات بهذا الأرتكل");
                        txt_Art.Text = "";
                        txt_Art.Focus();
                    }
                }
            };
            // --

            // --> checkbox All
            ch_All.CheckedChanged += (S, E) =>
            {
                if (ch_All.Checked)
                {
                    txt_Sizes.ReadOnly = true;
                    txt_Sizes.BackColor = Color.FromArgb(209, 178, 140);
                    txt_Sizes.Text = "جميع المقاسات";

                    cbox_Color.DataSource = null;
                    cbox_Color.Items.Add("جميع الألوان");
                    cbox_Color.Text = "جميع الألوان";
                    cbox_Color.BackColor = Color.FromArgb(209, 178, 140);
                }
                else
                {
                    txt_Sizes.ReadOnly = false;
                    txt_Sizes.BackColor = Color.FromArgb(255, 244, 230);
                    txt_Sizes.Text = "";

                    cbox_Color.DataSource = null;
                    cbox_Color.BackColor = Color.FromArgb(255, 244, 230);
                }
                txt_Art.Focus();
            };
            // --

            // --> Cbox Color
            // EnterClick
            cbox_Color.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                {
                    if (ch_All.Checked)
                        Btn_Forward.PerformClick();
                    else
                        txt_Sizes.Focus();
                }
            };
            // --

            // --> Sizes
            // EnterClick
            txt_Sizes.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Sizes.Text) && E.KeyCode == Keys.Enter)
                {
                    Btn_Forward.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };

            #endregion
        }

        private void Btn_AllSizes_Click(object sender, EventArgs e)
        {
            if(ch_All.Checked)
            { msg.ShowError("تم تحديد الجميع بالفعل"); return; }

            // Get All Avalibale Sizes From The Store..
            List<string> data = GetDataArray("SELECT p.Size, ISNULL(SUM((ISNULL(p.Qty, 0) - ISNULL(s.QTY, 0) - " +
                    "ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0))), 0) FROM Products p LEFT JOIN Online o ON o.P_ID = p.ID " +
                    "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM Sells GROUP BY P_ID) s ON s.P_ID = p.ID LEFT JOIN " +
                    "(SELECT SUM(Qty) Qty, P_ID FROM WithDraws GROUP BY P_ID) w ON w.P_ID = p.ID WHERE p.Art = @Art AND p.Color = @Color " +
                    "GROUP BY p.Size", 2, new string[] { "@Art", txt_Art.Text, "@Color", cbox_Color.Text });

            string s = "";
            for(int i = 0; i <= data.Count - 2; i += 2)
            {
                if(Convert.ToInt32(data[i + 1]) > 0)
                {
                    s += "-" + data[i] + "*" + data[i + 1];
                }
            }

            if (s.Length > 1)
                txt_Sizes.Text = s.Substring(1);
            else txt_Sizes.Text = "";
        }

        private void Btn_Forward_Click(object sender, EventArgs e)
        {
            #region Validation Of Empty Txts
            if (String.IsNullOrWhiteSpace(txt_Art.Text))
            { txt_Art.Focus(); msg.ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_Sizes.Text))
            { txt_Sizes.Focus(); msg.ShowError("قم بملئ البيانات أولا"); return; }
            #endregion

            // User Confirm
            if (!AskUser("هذه العملية لا يمكن التراجع عنها، تأكد من صحة البيانات\nهل انت متأكد من إضافة المنتجات؟", 'q'))
                return;

            #region Sizes (Man)
            // Clear Spaces
            if(!ch_All.Checked)
            {
                txt_Sizes.Text = String.Concat(txt_Sizes.Text.Where(c => !Char.IsWhiteSpace(c)));
                try
                {
                    ManSizes.Clear();
                    // Split 1
                    List<string> S1 = txt_Sizes.Text.Split('-').ToList();
                    foreach (string s in S1)
                    {
                        if (s.Split('*').Length > 1)
                        {
                            // Multiply
                            int size = Convert.ToInt32(s.Split('*')[0]);
                            for (int i = 0, n = Convert.ToInt32(s.Split('*')[1]); i < n; i++)
                            {
                                ManSizes.Add(size);
                            }
                        }
                        else if (s.Split('>').Length > 1)
                        {
                            // From .. To ..
                            int sStart = Convert.ToInt32(s.Split('>')[0]);
                            int sEnd = Convert.ToInt32(s.Split('>')[1]);
                            for (int i = sStart; i <= sEnd; i++)
                            {
                                ManSizes.Add(i);
                            }
                        }
                        else if (Convert.ToInt32(s) >= 0)
                        {
                            // Size
                            ManSizes.Add(Convert.ToInt32(s));
                        }
                        else throw new Exception();
                    }

                    // Msg Qty
                    if (!AskUser("الكمية " + ManSizes.Count + "\nهل تريد المتابعة في الارجاع؟", 'q'))
                        return;
                }
                catch
                {
                    msg.ShowError("هناك خطأ في البيانات المكتوبة\nإلقي نظرة على الطريقة الصحيحة للأستخدام");
                    txt_Sizes.Text = "";
                    txt_Sizes.Focus();
                    ManSizes.Clear();
                    return;
                }
            }
            #endregion

            List<Sizes> sizes = new List<Sizes>();
            #region Collect Sizes
            if(!ch_All.Checked)
            {
                foreach (int s in ManSizes)
                {
                    bool ava = false;
                    int i;
                    int L = sizes.Count;
                    for (i = 0; i < L; i++)
                    {
                        if (s == sizes[i].Size)
                        {
                            ava = true;
                            break;
                        }
                    }
                    if (ava)
                    {
                        sizes[i].Qty++;
                    }
                    else
                    {
                        sizes.Add(new Sizes(s));
                    }
                }
            }
            else
            {
                List<string> data = GetDataArray("SELECT p.Color, p.Size, ISNULL(SUM((ISNULL(p.Qty, 0) - ISNULL(s.QTY, 0) - " +
                    "ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0))), 0) FROM Products p LEFT JOIN Online o ON o.P_ID = p.ID " +
                    "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM Sells GROUP BY P_ID) s ON s.P_ID = p.ID LEFT JOIN " +
                    "(SELECT SUM(Qty) Qty, P_ID FROM WithDraws GROUP BY P_ID) w ON w.P_ID = p.ID WHERE p.Art = @Art " +
                    "GROUP BY p.Color, p.Size", 3, new string[] { "@Art", txt_Art.Text });

                for(int i = 0; i <= data.Count - 3; i += 3)
                {
                    sizes.Add(new Sizes(Convert.ToInt32(data[i + 1]), Convert.ToInt32(data[i + 2]), data[i]));
                }
            }
            #endregion

            #region Update
            Transaction trans = new Transaction();
            foreach(Sizes s in sizes)
            {
                string col;
                if (ch_All.Checked)
                    col = s.color;
                else col = cbox_Color.Text;

                // Check Sizes ava
                int code = GetDataInt("SELECT ID From Products WHERE Art=@Art AND Color=@Color AND Size=@Size",
                    new string[] { "@Art", txt_Art.Text, "@Color", col, "@Size", s.Size.ToString() });

                if (GetDataInt("SELECT ISNULL(SUM((ISNULL(p.Qty, 0) - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0))), 0) " +
                "FROM Products p LEFT JOIN Online o ON o.P_ID = p.ID " +
                "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM Sells GROUP BY P_ID) s ON s.P_ID = p.ID " +
                "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM WithDraws GROUP BY P_ID) w ON w.P_ID = p.ID WHERE p.ID = @ID",
                new string[] { "@ID", code.ToString() }) < s.Qty)
                { msg.ShowError("تأكد من الكمية والمقاسات المكتوبة"); txt_Sizes.Focus(); return; }

                trans.AddCmd("UPDATE Products SET Qty=Qty-" + s.Qty + " WHERE Art=@Art AND Color=@Color AND Size=@Size",
                    new string[] { "@Art", txt_Art.Text, "@Color", col, "@Size", s.Size.ToString() });
            }
            string tran = trans.StartTrans();
            if(tran == null)
            {
                msg.ShowError("تم إرجاع المنتجات بنجاح", true);
                txt_Art.Text = "";
                ch_All.Checked = false;
                cbox_Color.DataSource = null;
                txt_Sizes.Text = "";
                txt_Art.Focus();
            }
            else
            {
                msg.ShowError("حدث خطأ ما، تأكد من الكمية");
                if (Forms.settings.IsAdmin)
                    MessageBox.Show(tran);
            }
            #endregion
        }

        private void Btn_refresh_Click(object sender, EventArgs e)
        {
            txt_Art.Text = "";
            Art = "";
            txt_Art.Focus();
        }
    }
}
