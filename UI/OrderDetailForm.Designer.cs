namespace UI
{
    partial class OrderDetailForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtOrderID = new System.Windows.Forms.TextBox();
            lbl2 = new System.Windows.Forms.Label();
            txtID = new System.Windows.Forms.TextBox();
            lbl1 = new System.Windows.Forms.Label();
            btnAdd = new System.Windows.Forms.Button();
            btnDel = new System.Windows.Forms.Button();
            txtItemID = new System.Windows.Forms.TextBox();
            lbl3 = new System.Windows.Forms.Label();
            txtQuantity = new System.Windows.Forms.TextBox();
            lbl4 = new System.Windows.Forms.Label();
            txtUnitAmount = new System.Windows.Forms.TextBox();
            lbl5 = new System.Windows.Forms.Label();
            dgv = new System.Windows.Forms.DataGridView();
            btnReload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // txtOrderID
            // 
            txtOrderID.Location = new System.Drawing.Point(121, 49);
            txtOrderID.Margin = new System.Windows.Forms.Padding(4);
            txtOrderID.Name = "txtOrderID";
            txtOrderID.Size = new System.Drawing.Size(169, 33);
            txtOrderID.TabIndex = 15;
            // 
            // lbl2
            // 
            lbl2.AutoSize = true;
            lbl2.Location = new System.Drawing.Point(13, 49);
            lbl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl2.Name = "lbl2";
            lbl2.Size = new System.Drawing.Size(73, 25);
            lbl2.TabIndex = 14;
            lbl2.Text = "OrderID";
            // 
            // txtID
            // 
            txtID.Location = new System.Drawing.Point(121, 6);
            txtID.Margin = new System.Windows.Forms.Padding(4);
            txtID.Name = "txtID";
            txtID.Size = new System.Drawing.Size(169, 33);
            txtID.TabIndex = 13;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Location = new System.Drawing.Point(13, 9);
            lbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl1.Name = "lbl1";
            lbl1.Size = new System.Drawing.Size(31, 25);
            lbl1.TabIndex = 12;
            lbl1.Text = "ID";
            // 
            // btnAdd
            // 
            btnAdd.Location = new System.Drawing.Point(13, 325);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(120, 70);
            btnAdd.TabIndex = 18;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDel
            // 
            btnDel.Location = new System.Drawing.Point(13, 483);
            btnDel.Margin = new System.Windows.Forms.Padding(4);
            btnDel.Name = "btnDel";
            btnDel.Size = new System.Drawing.Size(120, 70);
            btnDel.TabIndex = 19;
            btnDel.Text = "Delete";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // txtItemID
            // 
            txtItemID.Location = new System.Drawing.Point(121, 90);
            txtItemID.Margin = new System.Windows.Forms.Padding(4);
            txtItemID.Name = "txtItemID";
            txtItemID.Size = new System.Drawing.Size(169, 33);
            txtItemID.TabIndex = 22;
            // 
            // lbl3
            // 
            lbl3.AutoSize = true;
            lbl3.Location = new System.Drawing.Point(13, 93);
            lbl3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl3.Name = "lbl3";
            lbl3.Size = new System.Drawing.Size(64, 25);
            lbl3.TabIndex = 21;
            lbl3.Text = "ItemID";
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new System.Drawing.Point(121, 133);
            txtQuantity.Margin = new System.Windows.Forms.Padding(4);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new System.Drawing.Size(169, 33);
            txtQuantity.TabIndex = 24;
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Location = new System.Drawing.Point(13, 136);
            lbl4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl4.Name = "lbl4";
            lbl4.Size = new System.Drawing.Size(86, 25);
            lbl4.TabIndex = 23;
            lbl4.Text = "Quantity";
            // 
            // txtUnitAmount
            // 
            txtUnitAmount.Location = new System.Drawing.Point(121, 182);
            txtUnitAmount.Margin = new System.Windows.Forms.Padding(4);
            txtUnitAmount.Name = "txtUnitAmount";
            txtUnitAmount.Size = new System.Drawing.Size(169, 33);
            txtUnitAmount.TabIndex = 26;
            // 
            // lbl5
            // 
            lbl5.AutoSize = true;
            lbl5.Location = new System.Drawing.Point(13, 185);
            lbl5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl5.Name = "lbl5";
            lbl5.Size = new System.Drawing.Size(110, 25);
            lbl5.TabIndex = 25;
            lbl5.Text = "UnitAmount";
            // 
            // dgv
            // 
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new System.Drawing.Point(297, 6);
            dgv.Name = "dgv";
            dgv.RowHeadersWidth = 62;
            dgv.Size = new System.Drawing.Size(769, 547);
            dgv.TabIndex = 27;
            dgv.SelectionChanged += dgv_SelectionChanged;
            // 
            // btnReload
            // 
            btnReload.Location = new System.Drawing.Point(13, 402);
            btnReload.Margin = new System.Windows.Forms.Padding(4);
            btnReload.Name = "btnReload";
            btnReload.Size = new System.Drawing.Size(120, 70);
            btnReload.TabIndex = 20;
            btnReload.Text = "Reload";
            btnReload.UseVisualStyleBackColor = true;
            btnReload.Click += btnReload_Click;
            // 
            // OrderDetailForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1078, 644);
            Controls.Add(dgv);
            Controls.Add(txtUnitAmount);
            Controls.Add(lbl5);
            Controls.Add(txtQuantity);
            Controls.Add(lbl4);
            Controls.Add(txtItemID);
            Controls.Add(lbl3);
            Controls.Add(btnReload);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(txtOrderID);
            Controls.Add(lbl2);
            Controls.Add(txtID);
            Controls.Add(lbl1);
            Name = "OrderDetailForm";
            Text = "OrderDetailForm";
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.TextBox txtItemID;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.TextBox txtUnitAmount;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnReload;
    }
}
