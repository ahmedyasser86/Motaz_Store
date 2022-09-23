namespace Motaz_Store
{
    partial class Password
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
            this.label12 = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.Btn_Show = new System.Windows.Forms.Button();
            this.lbl_Error = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(230)))));
            this.label12.Font = new System.Drawing.Font("Cairo SemiBold", 17F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(494, 192);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label12.Size = new System.Drawing.Size(145, 43);
            this.label12.TabIndex = 19;
            this.label12.Text = "كلمة المرور :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel22
            // 
            this.panel22.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(64)))), ((int)(((byte)(59)))));
            this.panel22.Location = new System.Drawing.Point(316, 281);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(324, 2);
            this.panel22.TabIndex = 18;
            // 
            // txt_Password
            // 
            this.txt_Password.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(230)))));
            this.txt_Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Password.Font = new System.Drawing.Font("Cairo SemiBold", 17F, System.Drawing.FontStyle.Bold);
            this.txt_Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(64)))), ((int)(((byte)(59)))));
            this.txt_Password.Location = new System.Drawing.Point(316, 238);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(324, 43);
            this.txt_Password.TabIndex = 17;
            this.txt_Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Btn_Show
            // 
            this.Btn_Show.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Btn_Show.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(202)))), ((int)(((byte)(168)))));
            this.Btn_Show.FlatAppearance.BorderSize = 0;
            this.Btn_Show.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(178)))), ((int)(((byte)(140)))));
            this.Btn_Show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Show.Font = new System.Drawing.Font("Cairo SemiBold", 15F, System.Drawing.FontStyle.Bold);
            this.Btn_Show.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(25)))), ((int)(((byte)(2)))));
            this.Btn_Show.Location = new System.Drawing.Point(429, 289);
            this.Btn_Show.Name = "Btn_Show";
            this.Btn_Show.Size = new System.Drawing.Size(99, 46);
            this.Btn_Show.TabIndex = 22;
            this.Btn_Show.Text = "عرض";
            this.Btn_Show.UseVisualStyleBackColor = false;
            this.Btn_Show.Click += new System.EventHandler(this.Btn_Show_Click);
            // 
            // lbl_Error
            // 
            this.lbl_Error.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Error.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(54)))), ((int)(((byte)(12)))));
            this.lbl_Error.Location = new System.Drawing.Point(0, 0);
            this.lbl_Error.Name = "lbl_Error";
            this.lbl_Error.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_Error.Size = new System.Drawing.Size(957, 43);
            this.lbl_Error.TabIndex = 23;
            this.lbl_Error.Text = "يوجد خطأ هنا";
            this.lbl_Error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Error.Visible = false;
            // 
            // Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(229)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(957, 527);
            this.Controls.Add(this.lbl_Error);
            this.Controls.Add(this.Btn_Show);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panel22);
            this.Controls.Add(this.txt_Password);
            this.Font = new System.Drawing.Font("Cairo SemiBold", 9.749999F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(64)))), ((int)(((byte)(59)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "Password";
            this.Text = "Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel22;
        public System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Button Btn_Show;
        private System.Windows.Forms.Label lbl_Error;
    }
}