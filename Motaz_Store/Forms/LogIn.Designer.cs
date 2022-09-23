namespace Motaz_Store
{
    partial class LogIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_Error = new System.Windows.Forms.Label();
            this.lbl_pass = new System.Windows.Forms.Label();
            this.lbl_user = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.pnl_password = new System.Windows.Forms.Panel();
            this.pnl_username = new System.Windows.Forms.Panel();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ch_Pass = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.72F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(844, 572);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ch_Pass);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lbl_pass);
            this.panel1.Controls.Add(this.lbl_user);
            this.panel1.Controls.Add(this.btn_Login);
            this.panel1.Controls.Add(this.pnl_password);
            this.panel1.Controls.Add(this.pnl_username);
            this.panel1.Controls.Add(this.txt_pass);
            this.panel1.Controls.Add(this.txt_user);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(478, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 566);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbl_Error);
            this.panel2.Location = new System.Drawing.Point(3, 385);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(357, 56);
            this.panel2.TabIndex = 25;
            // 
            // lbl_Error
            // 
            this.lbl_Error.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Error.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_Error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(54)))), ((int)(((byte)(12)))));
            this.lbl_Error.Location = new System.Drawing.Point(0, 0);
            this.lbl_Error.Name = "lbl_Error";
            this.lbl_Error.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_Error.Size = new System.Drawing.Size(357, 56);
            this.lbl_Error.TabIndex = 25;
            this.lbl_Error.Text = "حدث خطأ هنا";
            this.lbl_Error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Error.Visible = false;
            // 
            // lbl_pass
            // 
            this.lbl_pass.AutoSize = true;
            this.lbl_pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(220)))), ((int)(((byte)(202)))));
            this.lbl_pass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_pass.Font = new System.Drawing.Font("Cairo SemiBold", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_pass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(178)))), ((int)(((byte)(140)))));
            this.lbl_pass.Location = new System.Drawing.Point(128, 321);
            this.lbl_pass.Name = "lbl_pass";
            this.lbl_pass.Size = new System.Drawing.Size(106, 33);
            this.lbl_pass.TabIndex = 23;
            this.lbl_pass.Text = "كلمة المرور";
            this.lbl_pass.Click += new System.EventHandler(this.Lbl_pass_Click);
            // 
            // lbl_user
            // 
            this.lbl_user.AutoSize = true;
            this.lbl_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(220)))), ((int)(((byte)(202)))));
            this.lbl_user.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_user.Font = new System.Drawing.Font("Cairo SemiBold", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_user.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(178)))), ((int)(((byte)(140)))));
            this.lbl_user.Location = new System.Drawing.Point(117, 220);
            this.lbl_user.Name = "lbl_user";
            this.lbl_user.Size = new System.Drawing.Size(128, 33);
            this.lbl_user.TabIndex = 23;
            this.lbl_user.Text = "أسم المستخدم";
            this.lbl_user.Click += new System.EventHandler(this.Lbl_user_Click);
            // 
            // btn_Login
            // 
            this.btn_Login.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Login.AutoSize = true;
            this.btn_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(64)))), ((int)(((byte)(59)))));
            this.btn_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Login.FlatAppearance.BorderSize = 0;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.Font = new System.Drawing.Font("Cairo SemiBold", 15F, System.Drawing.FontStyle.Bold);
            this.btn_Login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(230)))));
            this.btn_Login.Location = new System.Drawing.Point(97, 447);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(168, 47);
            this.btn_Login.TabIndex = 2;
            this.btn_Login.Text = "تسجيل دخول";
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.Click += new System.EventHandler(this.Btn_Login_Click);
            // 
            // pnl_password
            // 
            this.pnl_password.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnl_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(178)))), ((int)(((byte)(140)))));
            this.pnl_password.Location = new System.Drawing.Point(47, 357);
            this.pnl_password.Name = "pnl_password";
            this.pnl_password.Size = new System.Drawing.Size(269, 2);
            this.pnl_password.TabIndex = 17;
            // 
            // pnl_username
            // 
            this.pnl_username.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnl_username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(178)))), ((int)(((byte)(140)))));
            this.pnl_username.Location = new System.Drawing.Point(47, 256);
            this.pnl_username.Name = "pnl_username";
            this.pnl_username.Size = new System.Drawing.Size(269, 2);
            this.pnl_username.TabIndex = 17;
            // 
            // txt_pass
            // 
            this.txt_pass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(220)))), ((int)(((byte)(202)))));
            this.txt_pass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_pass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txt_pass.Font = new System.Drawing.Font("Cairo SemiBold", 17F, System.Drawing.FontStyle.Bold);
            this.txt_pass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(64)))), ((int)(((byte)(59)))));
            this.txt_pass.Location = new System.Drawing.Point(47, 314);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.PasswordChar = '*';
            this.txt_pass.Size = new System.Drawing.Size(269, 43);
            this.txt_pass.TabIndex = 1;
            this.txt_pass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_pass.Enter += new System.EventHandler(this.Txt_pass_Enter);
            this.txt_pass.Leave += new System.EventHandler(this.Txt_pass_Leave);
            // 
            // txt_user
            // 
            this.txt_user.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(220)))), ((int)(((byte)(202)))));
            this.txt_user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_user.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txt_user.Font = new System.Drawing.Font("Cairo SemiBold", 17F, System.Drawing.FontStyle.Bold);
            this.txt_user.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(64)))), ((int)(((byte)(59)))));
            this.txt_user.Location = new System.Drawing.Point(47, 213);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(269, 43);
            this.txt_user.TabIndex = 0;
            this.txt_user.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_user.Enter += new System.EventHandler(this.Txt_user_Enter);
            this.txt_user.Leave += new System.EventHandler(this.Txt_user_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cairo SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(25)))), ((int)(((byte)(2)))));
            this.label1.Location = new System.Drawing.Point(92, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 75);
            this.label1.TabIndex = 0;
            this.label1.Text = "مرحباً بك";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Motaz_Store.Properties.Resources.Motaz_cover;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(475, 572);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ch_Pass
            // 
            this.ch_Pass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ch_Pass.AutoSize = true;
            this.ch_Pass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ch_Pass.Font = new System.Drawing.Font("Cairo SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.ch_Pass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(64)))), ((int)(((byte)(59)))));
            this.ch_Pass.Location = new System.Drawing.Point(126, 363);
            this.ch_Pass.Name = "ch_Pass";
            this.ch_Pass.Size = new System.Drawing.Size(111, 24);
            this.ch_Pass.TabIndex = 26;
            this.ch_Pass.Text = "إظهار كلمة المرور";
            this.ch_Pass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ch_Pass.UseVisualStyleBackColor = true;
            this.ch_Pass.CheckedChanged += new System.EventHandler(this.Ch_Pass_CheckedChanged);
            // 
            // LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(229)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(844, 572);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Cairo SemiBold", 9.749999F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(860, 611);
            this.MinimumSize = new System.Drawing.Size(860, 611);
            this.Name = "LogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogIn - Motaz Store";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_password;
        private System.Windows.Forms.Panel pnl_username;
        public System.Windows.Forms.TextBox txt_pass;
        public System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Label lbl_pass;
        private System.Windows.Forms.Label lbl_user;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_Error;
        private System.Windows.Forms.CheckBox ch_Pass;
    }
}