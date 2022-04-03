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

            public Sizes(int s, int q = 1)
            {
                Size = s;
                Qty = q;
            }
        }
        Msgs msg;
        List<int> ManSizes = new List<int>();
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
                    cbox_Color.DataSource = GetDataArray("SELECT DISTINCT(Color) FROM Products WHERE Art=@Art",
                        new string[] { "@Art", txt_Art.Text });

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
            // Leave
            txt_Sizes.Leave += (S, E) =>
            {
                if (String.IsNullOrWhiteSpace(txt_Sizes.Text) || txt_Sizes.Text == "جميع المقاسات") return;
                // Clear Spaces
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
                    lbl_Qty.Text = ManSizes.Count.ToString();
                }
                catch
                {
                    msg.ShowError("هناك خطأ في البيانات المكتوبة\nإلقي نظرة على الطريقة الصحيحة للأستخدام");
                    txt_Sizes.Text = "";
                    txt_Sizes.Focus();
                    ManSizes.Clear();
                    lbl_Qty.Text = "0";
                }
            };

            #endregion
        }

        private void Btn_AllSizes_Click(object sender, EventArgs e)
        {
            // TODO: Get All Avalibale Sizes From The Store..
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
                // TODO : Get all avalable From Store
            }
            #endregion

            #region Delete
            Transaction trans = new Transaction();
            foreach(Sizes s in sizes)
            {
                trans.AddCmd("UPDATE Products SET Qty=Qty-" + s.Qty + " WHERE Art=@Art AND Color=@Color AND Size=@Size",
                    new string[] { "@Art", txt_Art.Text, "@Color", cbox_Color.Text, "@Size", s.Size.ToString() });
            }
            string tran = trans.StartTrans();
            if(tran == null)
            {
                msg.ShowError("تم إرجاع المنتجات بنجاح", true);
                txt_Art.Text = "";
                ch_All.Checked = false;
                cbox_Color.DataSource = null;
                txt_Sizes.Text = "";
                lbl_Qty.Text = "0";
                txt_Art.Focus();
            }
            else
            {
                msg.ShowError("حدث خطأ ما، تأكد من الكمية");
                // TODO : If Userandmin Show Error Details
            }
            #endregion
        }
    }
}
