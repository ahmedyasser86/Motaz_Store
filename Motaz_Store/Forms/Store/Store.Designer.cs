namespace Motaz_Store
{
    partial class Store
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
            this.pnl_Top = new System.Windows.Forms.Panel();
            this.btn_View = new System.Windows.Forms.Button();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.btn_Del = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.pnl_Line = new System.Windows.Forms.Panel();
            this.pnl_Holder = new System.Windows.Forms.Panel();
            this.btn_Other = new System.Windows.Forms.Button();
            this.pnl_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Top
            // 
            this.pnl_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(113)))), ((int)(((byte)(44)))));
            this.pnl_Top.Controls.Add(this.btn_Other);
            this.pnl_Top.Controls.Add(this.btn_View);
            this.pnl_Top.Controls.Add(this.btn_Edit);
            this.pnl_Top.Controls.Add(this.btn_Del);
            this.pnl_Top.Controls.Add(this.btn_Add);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.Size = new System.Drawing.Size(957, 39);
            this.pnl_Top.TabIndex = 0;
            // 
            // btn_View
            // 
            this.btn_View.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_View.FlatAppearance.BorderSize = 0;
            this.btn_View.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(71)))), ((int)(((byte)(31)))));
            this.btn_View.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(71)))), ((int)(((byte)(31)))));
            this.btn_View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_View.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_View.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(229)))), ((int)(((byte)(213)))));
            this.btn_View.Location = new System.Drawing.Point(503, 0);
            this.btn_View.Name = "btn_View";
            this.btn_View.Size = new System.Drawing.Size(121, 39);
            this.btn_View.TabIndex = 3;
            this.btn_View.Text = "عرض المخزون";
            this.btn_View.UseVisualStyleBackColor = true;
            this.btn_View.Click += new System.EventHandler(this.Top_Btns_CLick);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Edit.FlatAppearance.BorderSize = 0;
            this.btn_Edit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(71)))), ((int)(((byte)(31)))));
            this.btn_Edit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(71)))), ((int)(((byte)(31)))));
            this.btn_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Edit.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Edit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(229)))), ((int)(((byte)(213)))));
            this.btn_Edit.Location = new System.Drawing.Point(624, 0);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(113, 39);
            this.btn_Edit.TabIndex = 2;
            this.btn_Edit.Text = "تعديل منتج";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.Top_Btns_CLick);
            // 
            // btn_Del
            // 
            this.btn_Del.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Del.FlatAppearance.BorderSize = 0;
            this.btn_Del.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(71)))), ((int)(((byte)(31)))));
            this.btn_Del.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(71)))), ((int)(((byte)(31)))));
            this.btn_Del.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Del.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Del.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(229)))), ((int)(((byte)(213)))));
            this.btn_Del.Location = new System.Drawing.Point(737, 0);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Size = new System.Drawing.Size(107, 39);
            this.btn_Del.TabIndex = 1;
            this.btn_Del.Text = "إرجاع منتج";
            this.btn_Del.UseVisualStyleBackColor = true;
            this.btn_Del.Click += new System.EventHandler(this.Top_Btns_CLick);
            // 
            // btn_Add
            // 
            this.btn_Add.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Add.FlatAppearance.BorderSize = 0;
            this.btn_Add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(71)))), ((int)(((byte)(31)))));
            this.btn_Add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(71)))), ((int)(((byte)(31)))));
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(229)))), ((int)(((byte)(213)))));
            this.btn_Add.Location = new System.Drawing.Point(844, 0);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(113, 39);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "إضافة منتج";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.Top_Btns_CLick);
            // 
            // pnl_Line
            // 
            this.pnl_Line.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(113)))), ((int)(((byte)(44)))));
            this.pnl_Line.Location = new System.Drawing.Point(88, 21);
            this.pnl_Line.Name = "pnl_Line";
            this.pnl_Line.Size = new System.Drawing.Size(200, 3);
            this.pnl_Line.TabIndex = 1;
            this.pnl_Line.Visible = false;
            // 
            // pnl_Holder
            // 
            this.pnl_Holder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Holder.Location = new System.Drawing.Point(0, 39);
            this.pnl_Holder.Name = "pnl_Holder";
            this.pnl_Holder.Size = new System.Drawing.Size(957, 527);
            this.pnl_Holder.TabIndex = 2;
            // 
            // btn_Other
            // 
            this.btn_Other.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Other.FlatAppearance.BorderSize = 0;
            this.btn_Other.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(71)))), ((int)(((byte)(31)))));
            this.btn_Other.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(71)))), ((int)(((byte)(31)))));
            this.btn_Other.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Other.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Other.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(229)))), ((int)(((byte)(213)))));
            this.btn_Other.Location = new System.Drawing.Point(407, 0);
            this.btn_Other.Name = "btn_Other";
            this.btn_Other.Size = new System.Drawing.Size(96, 39);
            this.btn_Other.TabIndex = 4;
            this.btn_Other.Text = "الفرع الأخر";
            this.btn_Other.UseVisualStyleBackColor = true;
            this.btn_Other.Click += new System.EventHandler(this.Top_Btns_CLick);
            // 
            // Store
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(229)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(957, 566);
            this.Controls.Add(this.pnl_Line);
            this.Controls.Add(this.pnl_Holder);
            this.Controls.Add(this.pnl_Top);
            this.Font = new System.Drawing.Font("Cairo SemiBold", 9.749999F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(6)))), ((int)(((byte)(38)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "Store";
            this.Text = "Store";
            this.Load += new System.EventHandler(this.Store_Load);
            this.pnl_Top.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Top;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_View;
        private System.Windows.Forms.Button btn_Edit;
        private System.Windows.Forms.Button btn_Del;
        private System.Windows.Forms.Panel pnl_Line;
        private System.Windows.Forms.Panel pnl_Holder;
        private System.Windows.Forms.Button btn_Other;
    }
}