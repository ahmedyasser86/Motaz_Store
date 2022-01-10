namespace Motaz_Store
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_Buttons = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Settings = new System.Windows.Forms.Label();
            this.lbl_Store = new System.Windows.Forms.Label();
            this.lbl_Sells = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_Holder = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlp_Buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(133)))), ((int)(((byte)(114)))));
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(957, 61);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.54546F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            this.tableLayoutPanel1.Controls.Add(this.tlp_Buttons, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(957, 61);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tlp_Buttons
            // 
            this.tlp_Buttons.ColumnCount = 7;
            this.tlp_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.438559F));
            this.tlp_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.65454F));
            this.tlp_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.439822F));
            this.tlp_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.6514F));
            this.tlp_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.439822F));
            this.tlp_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.6514F));
            this.tlp_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.72445F));
            this.tlp_Buttons.Controls.Add(this.lbl_Settings, 1, 0);
            this.tlp_Buttons.Controls.Add(this.lbl_Store, 3, 0);
            this.tlp_Buttons.Controls.Add(this.lbl_Sells, 5, 0);
            this.tlp_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Buttons.Location = new System.Drawing.Point(0, 0);
            this.tlp_Buttons.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Buttons.Name = "tlp_Buttons";
            this.tlp_Buttons.RowCount = 1;
            this.tlp_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Buttons.Size = new System.Drawing.Size(522, 61);
            this.tlp_Buttons.TabIndex = 0;
            // 
            // lbl_Settings
            // 
            this.lbl_Settings.AutoSize = true;
            this.lbl_Settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Settings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Settings.Font = new System.Drawing.Font("Cairo", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lbl_Settings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(241)))), ((int)(((byte)(240)))));
            this.lbl_Settings.Location = new System.Drawing.Point(31, 0);
            this.lbl_Settings.Name = "lbl_Settings";
            this.lbl_Settings.Size = new System.Drawing.Size(117, 61);
            this.lbl_Settings.TabIndex = 2;
            this.lbl_Settings.Text = "الإعدادات";
            this.lbl_Settings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Settings.Click += new System.EventHandler(this.TopBtnClick);
            // 
            // lbl_Store
            // 
            this.lbl_Store.AutoSize = true;
            this.lbl_Store.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Store.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Store.Font = new System.Drawing.Font("Cairo", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lbl_Store.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(241)))), ((int)(((byte)(240)))));
            this.lbl_Store.Location = new System.Drawing.Point(182, 0);
            this.lbl_Store.Name = "lbl_Store";
            this.lbl_Store.Size = new System.Drawing.Size(117, 61);
            this.lbl_Store.TabIndex = 1;
            this.lbl_Store.Text = "المخزن";
            this.lbl_Store.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Store.Click += new System.EventHandler(this.TopBtnClick);
            // 
            // lbl_Sells
            // 
            this.lbl_Sells.AutoSize = true;
            this.lbl_Sells.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Sells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Sells.Font = new System.Drawing.Font("Cairo", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lbl_Sells.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(241)))), ((int)(((byte)(240)))));
            this.lbl_Sells.Location = new System.Drawing.Point(333, 0);
            this.lbl_Sells.Name = "lbl_Sells";
            this.lbl_Sells.Size = new System.Drawing.Size(117, 61);
            this.lbl_Sells.TabIndex = 0;
            this.lbl_Sells.Text = "المبيعات";
            this.lbl_Sells.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Sells.Click += new System.EventHandler(this.TopBtnClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font(" Abdoullah Ashgar EL-kharef", 33.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(241)))), ((int)(((byte)(240)))));
            this.label1.Location = new System.Drawing.Point(525, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(429, 61);
            this.label1.TabIndex = 1;
            this.label1.Text = "معتز";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_Holder
            // 
            this.pnl_Holder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(241)))), ((int)(((byte)(240)))));
            this.pnl_Holder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Holder.Location = new System.Drawing.Point(0, 61);
            this.pnl_Holder.Name = "pnl_Holder";
            this.pnl_Holder.Size = new System.Drawing.Size(957, 566);
            this.pnl_Holder.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(957, 627);
            this.Controls.Add(this.pnl_Holder);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cairo SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(6)))), ((int)(((byte)(38)))));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "MainForm";
            this.Text = "Motaz Store";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlp_Buttons.ResumeLayout(false);
            this.tlp_Buttons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tlp_Buttons;
        private System.Windows.Forms.Label lbl_Store;
        private System.Windows.Forms.Label lbl_Sells;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Settings;
        private System.Windows.Forms.Panel pnl_Holder;
    }
}

