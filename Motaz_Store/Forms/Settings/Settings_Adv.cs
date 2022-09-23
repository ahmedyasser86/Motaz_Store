using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using static Motaz_Store.myDB;
using System.IO;

namespace Motaz_Store
{
    public partial class Settings_Adv : Form
    {
        Msgs msg;
        public string Barcode_Printer = "Xprinter XP-235B";
        public string Recipt_Printer = "XP-80C";
        public string Backup = "NA";
        public Settings_Adv()
        {
            InitializeComponent();

            // Error Msgs
            msg = new Msgs(lbl_Error);

            // Get Data
            Barcode_Printer = GetDataString("SELECT Value FROM Settings WHERE Setting='Barcode'");
            txt_Barcode.Text = Barcode_Printer;
            Recipt_Printer = GetDataString("SELECT Value FROM Settings WHERE Setting='Reciept'");
            txt_Reciept.Text = Recipt_Printer;
            Backup = GetDataString("SELECT Value FROM Settings WHERE Setting='Backup'");
            txt_Backup.Text = Backup;
            Program.Backup = Backup;

            #region txt's Events
            // KeyDown
            txt_Barcode.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Barcode.Text))
                {
                    btn_ChangeBar.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            txt_Reciept.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Reciept.Text))
                {
                    btn_ChangeRec.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            txt_Backup.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Backup.Text))
                {
                    btn_ChangeBack.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // Leave
            txt_Barcode.Leave += (S, E) =>
            { if (txt_Barcode.Text == Barcode_Printer && !txt_Barcode.ReadOnly) MakeReadOnly(txt_Barcode, Barcode_Printer); };
            txt_Reciept.Leave += (S, E) =>
            { if (txt_Reciept.Text == Recipt_Printer && !txt_Reciept.ReadOnly) MakeReadOnly(txt_Reciept, Recipt_Printer); };
            #endregion
        }

        private void OpenPrinterSettings(string printerName)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C rundll32 printui.dll,PrintUIEntry /p /n \"" + printerName + "\"";
            process.StartInfo = startInfo;
            process.Start();
        }

        private bool CheckPrinterName(string name)
        {
            PrinterSettings s = new PrinterSettings();
            s.PrinterName = name;
            if (s.IsValid)
                return true;
            else return false;
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

        private void Btn_BarcodeSettings_Click(object sender, EventArgs e)
        {
            OpenPrinterSettings(Barcode_Printer);
        }

        private void Btn_RecieptSettings_Click(object sender, EventArgs e)
        {
            OpenPrinterSettings(Recipt_Printer);
        }

        private void Cb_IsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_IsAdmin.Checked)
                Forms.settings.IsAdmin = true;
            else Forms.settings.IsAdmin = false;
        }

        private void Btn_ChangeBar_Click(object sender, EventArgs e)
        {
            if(txt_Barcode.ReadOnly == true)
            { RemoveReadOnly(txt_Barcode); txt_Barcode.Focus(); }
            else
            {
                // If No Updates
                if(txt_Barcode.Text == Barcode_Printer)
                { MakeReadOnly(txt_Barcode, Barcode_Printer); return; }
                // Else
                // Check Printer name
                if(!CheckPrinterName(txt_Barcode.Text))
                { msg.ShowError("لا يوجد طابعة بهذا الأسم"); MakeReadOnly(txt_Barcode, Barcode_Printer); return; }
                // Update
                if(CmdExcuteRows("UPDATE Settings SET Value='" + txt_Barcode.Text + "' WHERE Setting='Barcode'") == 1)
                {
                    msg.ShowError("تم تعديل أسم الطابعة بنجاح", true);
                    Barcode_Printer = txt_Barcode.Text;
                    MakeReadOnly(txt_Barcode, Barcode_Printer);
                }
                else
                {
                    msg.ShowError("حدث خطأ ما، حاول مرة أخرى");
                    MakeReadOnly(txt_Barcode, Barcode_Printer);
                }
            }
        }

        private void Btn_ChangeRec_Click(object sender, EventArgs e)
        {
            if (txt_Reciept.ReadOnly == true)
            { RemoveReadOnly(txt_Reciept); txt_Reciept.Focus(); }
            else
            {
                // If No Updates
                if (txt_Reciept.Text == Recipt_Printer)
                { MakeReadOnly(txt_Reciept, Recipt_Printer); return; }
                // Else
                // Check Printer name
                if (!CheckPrinterName(txt_Reciept.Text))
                { msg.ShowError("لا يوجد طابعة بهذا الأسم"); MakeReadOnly(txt_Reciept, Recipt_Printer); return; }
                // Update
                if (CmdExcuteRows("UPDATE Settings SET Value='" + txt_Reciept.Text + "' WHERE Setting='Reciept'") == 1)
                {
                    msg.ShowError("تم تعديل أسم الطابعة بنجاح", true);
                    Recipt_Printer = txt_Reciept.Text;
                    MakeReadOnly(txt_Reciept, Recipt_Printer);
                }
                else
                {
                    msg.ShowError("حدث خطأ ما، حاول مرة أخرى");
                    MakeReadOnly(txt_Reciept, Recipt_Printer);
                }
            }
        }

        private void Btn_ChangeBack_Click(object sender, EventArgs e)
        {
            // If Admin
            if (!cb_IsAdmin.Checked) { msg.ShowError("المسئول فقط من يمكنه التعديل"); return; }

            // Show Dialog
            FolderBrowserDialog D = new FolderBrowserDialog();
            if (D.ShowDialog() == DialogResult.OK)
            {
                if(CmdExcuteRows("UPDATE Settings SET Value='" + D.SelectedPath + "' WHERE Setting='Backup'") == 1)
                {
                    // Done
                    msg.ShowError("تم تعديل المسار بنجاح", true);
                    Backup = D.SelectedPath;
                    txt_Backup.Text = Backup;
                    Program.Backup = Backup;
                }
                else
                {
                    msg.ShowError("خدث خطأ ما");
                }
            }
        }

        private void Btn_Restore_Click(object sender, EventArgs e)
        {
            // If Admin
            if (!cb_IsAdmin.Checked) { msg.ShowError("المسئول فقط من يمكنه إستعادة نسخة إحتياطية"); return; }

            // Show Dialog
            OpenFileDialog D = new OpenFileDialog();
            D.Filter = "Database (*.Bak)|*.Bak";
            if (D.ShowDialog() == DialogResult.OK)
            {
                if (!Visual_Scripts.AskUser("هذه العملية تقوم بتغير قاعدة البيانات إلى قاعدة بيانات أخرى\n" +
                    "هذه العملية لايمكن التراجع عنها\nهل انت متأكد من انك تريد المواصلة؟", 'q'))
                    return;

                // Restore
                if (File.Exists(Path.GetFullPath(D.FileName)))
                {
                    Restore(Path.GetFullPath(D.FileName));
                    Application.Restart();
                }
                else msg.ShowError("فشل إيجاد النسخة");
            }
        }
    }
}
