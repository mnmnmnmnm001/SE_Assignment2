namespace UI
{
    partial class UserForm : System.Windows.Forms.Form
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
            txtUserName = new System.Windows.Forms.TextBox();
            lbl2 = new System.Windows.Forms.Label();
            txtUserID = new System.Windows.Forms.TextBox();
            lbl1 = new System.Windows.Forms.Label();
            btnAdd = new System.Windows.Forms.Button();
            btnDel = new System.Windows.Forms.Button();
            txtEmail = new System.Windows.Forms.TextBox();
            lbl4 = new System.Windows.Forms.Label();
            txtPassword = new System.Windows.Forms.TextBox();
            lbl3 = new System.Windows.Forms.Label();
            chkLock = new System.Windows.Forms.CheckBox();
            dgv = new System.Windows.Forms.DataGridView();
            btnReload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // txtUserName
            // 
            txtUserName.Location = new System.Drawing.Point(121, 49);
            txtUserName.Margin = new System.Windows.Forms.Padding(4);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new System.Drawing.Size(169, 33);
            txtUserName.TabIndex = 15;
            // 
            // lbl2
            // 
            lbl2.AutoSize = true;
            lbl2.Location = new System.Drawing.Point(13, 49);
            lbl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl2.Name = "lbl2";
            lbl2.Size = new System.Drawing.Size(98, 25);
            lbl2.TabIndex = 14;
            lbl2.Text = "UserName";
            // 
            // txtUserID
            // 
            txtUserID.Location = new System.Drawing.Point(121, 6);
            txtUserID.Margin = new System.Windows.Forms.Padding(4);
            txtUserID.Name = "txtUserID";
            txtUserID.Size = new System.Drawing.Size(169, 33);
            txtUserID.TabIndex = 13;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Location = new System.Drawing.Point(13, 9);
            lbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl1.Name = "lbl1";
            lbl1.Size = new System.Drawing.Size(67, 25);
            lbl1.TabIndex = 12;
            lbl1.Text = "UserID";
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
            // txtEmail
            // 
            txtEmail.Location = new System.Drawing.Point(121, 90);
            txtEmail.Margin = new System.Windows.Forms.Padding(4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new System.Drawing.Size(169, 33);
            txtEmail.TabIndex = 22;
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Location = new System.Drawing.Point(13, 93);
            lbl4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl4.Name = "lbl4";
            lbl4.Size = new System.Drawing.Size(54, 25);
            lbl4.TabIndex = 23;
            lbl4.Text = "Email";
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(121, 133);
            txtPassword.Margin = new System.Windows.Forms.Padding(4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(169, 33);
            txtPassword.TabIndex = 24;
            // 
            // lbl3
            // 
            lbl3.AutoSize = true;
            lbl3.Location = new System.Drawing.Point(13, 136);
            lbl3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl3.Name = "lbl3";
            lbl3.Size = new System.Drawing.Size(86, 25);
            lbl3.TabIndex = 21;
            lbl3.Text = "Password";
            // 
            // chkLock
            // 
            chkLock.AutoSize = true;
            chkLock.Location = new System.Drawing.Point(121, 182);
            chkLock.Name = "chkLock";
            chkLock.Size = new System.Drawing.Size(82, 29);
            chkLock.TabIndex = 26;
            chkLock.Text = "Lock";
            chkLock.UseVisualStyleBackColor = true;
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
            // UserForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1078, 644);
            Controls.Add(dgv);
            Controls.Add(chkLock);
            Controls.Add(txtPassword);
            Controls.Add(lbl3);
            Controls.Add(txtEmail);
            Controls.Add(lbl4);
            Controls.Add(btnReload);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(txtUserName);
            Controls.Add(lbl2);
            Controls.Add(txtUserID);
            Controls.Add(lbl1);
            Name = "UserForm";
            Text = "UserForm";
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.CheckBox chkLock;
    }
}
