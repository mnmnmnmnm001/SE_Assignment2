namespace UI
{
    partial class Order1Form : System.Windows.Forms.Form
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
            txtOrderDate = new System.Windows.Forms.TextBox();
            lbl2 = new System.Windows.Forms.Label();
            txtOrderID = new System.Windows.Forms.TextBox();
            lbl1 = new System.Windows.Forms.Label();
            btnAdd = new System.Windows.Forms.Button();
            btnDel = new System.Windows.Forms.Button();
            txtAgentID = new System.Windows.Forms.TextBox();
            lbl3 = new System.Windows.Forms.Label();
            dgv = new System.Windows.Forms.DataGridView();
            btnReload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // txtOrderDate
            // 
            txtOrderDate.Location = new System.Drawing.Point(121, 49);
            txtOrderDate.Margin = new System.Windows.Forms.Padding(4);
            txtOrderDate.Name = "txtOrderDate";
            txtOrderDate.Size = new System.Drawing.Size(169, 33);
            txtOrderDate.TabIndex = 15;
            // 
            // lbl2
            // 
            lbl2.AutoSize = true;
            lbl2.Location = new System.Drawing.Point(13, 49);
            lbl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl2.Name = "lbl2";
            lbl2.Size = new System.Drawing.Size(99, 25);
            lbl2.TabIndex = 14;
            lbl2.Text = "OrderDate";
            // 
            // txtOrderID
            // 
            txtOrderID.Location = new System.Drawing.Point(121, 6);
            txtOrderID.Margin = new System.Windows.Forms.Padding(4);
            txtOrderID.Name = "txtOrderID";
            txtOrderID.Size = new System.Drawing.Size(169, 33);
            txtOrderID.TabIndex = 13;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Location = new System.Drawing.Point(13, 9);
            lbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl1.Name = "lbl1";
            lbl1.Size = new System.Drawing.Size(74, 25);
            lbl1.TabIndex = 12;
            lbl1.Text = "OrderID";
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
            // txtAgentID
            // 
            txtAgentID.Location = new System.Drawing.Point(121, 90);
            txtAgentID.Margin = new System.Windows.Forms.Padding(4);
            txtAgentID.Name = "txtAgentID";
            txtAgentID.Size = new System.Drawing.Size(169, 33);
            txtAgentID.TabIndex = 22;
            // 
            // lbl3
            // 
            lbl3.AutoSize = true;
            lbl3.Location = new System.Drawing.Point(13, 93);
            lbl3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl3.Name = "lbl3";
            lbl3.Size = new System.Drawing.Size(77, 25);
            lbl3.TabIndex = 21;
            lbl3.Text = "AgentID";
            // 
            // dgv
            // 
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new System.Drawing.Point(297, 6);
            dgv.Name = "dgv";
            dgv.RowHeadersWidth = 62;
            dgv.Size = new System.Drawing.Size(769, 547);
            dgv.TabIndex = 25;
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
            // Order1Form
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1078, 644);
            Controls.Add(dgv);
            Controls.Add(txtAgentID);
            Controls.Add(lbl3);
            Controls.Add(txtOrderDate);
            Controls.Add(lbl2);
            Controls.Add(btnReload);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(txtOrderID);
            Controls.Add(lbl1);
            Name = "Order1Form";
            Text = "Order1Form";
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtOrderDate;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.TextBox txtAgentID;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnReload;
    }
}
