namespace UI
{
    partial class ItemForm : Form
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
            txtItemName = new TextBox();
            lbl2 = new Label();
            txtItemID = new TextBox();
            lbl1 = new Label();
            btnAdd = new Button();
            btnDel = new Button();
            txtPrice = new TextBox();
            lbl4 = new Label();
            txtSize = new TextBox();
            lbl3 = new Label();
            dgv = new DataGridView();
            btnReload = new Button();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // txtItemName
            // 
            txtItemName.Location = new Point(121, 49);
            txtItemName.Margin = new Padding(4);
            txtItemName.Name = "txtItemName";
            txtItemName.PasswordChar = '?';
            txtItemName.Size = new Size(169, 33);
            txtItemName.TabIndex = 15;
            // 
            // lbl2
            // 
            lbl2.AutoSize = true;
            lbl2.Location = new Point(13, 49);
            lbl2.Margin = new Padding(4, 0, 4, 0);
            lbl2.Name = "lbl2";
            lbl2.Size = new Size(99, 25);
            lbl2.TabIndex = 14;
            lbl2.Text = "ItemName";
            // 
            // txtItemID
            // 
            txtItemID.Location = new Point(121, 6);
            txtItemID.Margin = new Padding(4);
            txtItemID.Name = "txtItemID";
            txtItemID.Size = new Size(169, 33);
            txtItemID.TabIndex = 13;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Location = new Point(13, 9);
            lbl1.Margin = new Padding(4, 0, 4, 0);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(67, 25);
            lbl1.TabIndex = 12;
            lbl1.Text = "ItemID";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(13, 325);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 70);
            btnAdd.TabIndex = 18;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDel
            // 
            btnDel.Location = new Point(13, 483);
            btnDel.Margin = new Padding(4);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(120, 70);
            btnDel.TabIndex = 19;
            btnDel.Text = "Delete";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(121, 133);
            txtPrice.Margin = new Padding(4);
            txtPrice.Name = "txtPrice";
            txtPrice.PasswordChar = '?';
            txtPrice.Size = new Size(169, 33);
            txtPrice.TabIndex = 24;
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Location = new Point(13, 133);
            lbl4.Margin = new Padding(4, 0, 4, 0);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(54, 25);
            lbl4.TabIndex = 23;
            lbl4.Text = "Price";
            // 
            // txtSize
            // 
            txtSize.Location = new Point(121, 90);
            txtSize.Margin = new Padding(4);
            txtSize.Name = "txtSize";
            txtSize.Size = new Size(169, 33);
            txtSize.TabIndex = 22;
            // 
            // lbl3
            // 
            lbl3.AutoSize = true;
            lbl3.Location = new Point(13, 93);
            lbl3.Margin = new Padding(4, 0, 4, 0);
            lbl3.Name = "lbl3";
            lbl3.Size = new Size(46, 25);
            lbl3.TabIndex = 21;
            lbl3.Text = "Size";
            // 
            // dgv
            // 
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new Point(297, 6);
            dgv.Name = "dgv";
            dgv.RowHeadersWidth = 62;
            dgv.Size = new Size(769, 547);
            dgv.TabIndex = 25;
            dgv.SelectionChanged += dgv_SelectionChanged;
            // 
            // btnReload
            // 
            btnReload.Location = new Point(13, 402);
            btnReload.Margin = new Padding(4);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(120, 70);
            btnReload.TabIndex = 20;
            btnReload.Text = "Reload";
            btnReload.UseVisualStyleBackColor = true;
            btnReload.Click += btnReload_Click;
            // 
            // ItemForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1078, 644);
            Controls.Add(dgv);
            Controls.Add(txtPrice);
            Controls.Add(lbl4);
            Controls.Add(txtSize);
            Controls.Add(lbl3);
            Controls.Add(btnReload);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(txtItemName);
            Controls.Add(lbl2);
            Controls.Add(txtItemID);
            Controls.Add(lbl1);
            Name = "ItemForm";
            Text = "ItemForm";
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtItemName;
        private Label lbl2;
        private TextBox txtItemID;
        private Label lbl1;
        private Button btnAdd;
        private Button btnDel;
        private TextBox txtPrice;
        private Label lbl4;
        private TextBox txtSize;
        private Label lbl3;
        private DataGridView dgv;
        private Button btnReload;
    }
}