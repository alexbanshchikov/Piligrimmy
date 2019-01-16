namespace DesktopClient
{
    partial class FormAccount
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
            this.buttonMap = new System.Windows.Forms.Button();
            this.buttonAccount = new System.Windows.Forms.Button();
            this.checkBoxBusy = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonMap
            // 
            this.buttonMap.Location = new System.Drawing.Point(16, 6);
            this.buttonMap.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMap.Name = "buttonMap";
            this.buttonMap.Size = new System.Drawing.Size(100, 49);
            this.buttonMap.TabIndex = 3;
            this.buttonMap.Text = "Главная";
            this.buttonMap.UseVisualStyleBackColor = true;
            this.buttonMap.Click += new System.EventHandler(this.buttonMap_Click);
            // 
            // buttonAccount
            // 
            this.buttonAccount.Enabled = false;
            this.buttonAccount.Location = new System.Drawing.Point(207, 6);
            this.buttonAccount.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAccount.Name = "buttonAccount";
            this.buttonAccount.Size = new System.Drawing.Size(100, 49);
            this.buttonAccount.TabIndex = 4;
            this.buttonAccount.Text = "Личный счёт";
            this.buttonAccount.UseVisualStyleBackColor = true;
            this.buttonAccount.Click += new System.EventHandler(this.buttonAccount_Click);
            // 
            // checkBoxBusy
            // 
            this.checkBoxBusy.AutoSize = true;
            this.checkBoxBusy.Location = new System.Drawing.Point(387, 22);
            this.checkBoxBusy.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxBusy.Name = "checkBoxBusy";
            this.checkBoxBusy.Size = new System.Drawing.Size(70, 21);
            this.checkBoxBusy.TabIndex = 5;
            this.checkBoxBusy.Text = "Занят";
            this.checkBoxBusy.UseVisualStyleBackColor = true;
            this.checkBoxBusy.CheckedChanged += new System.EventHandler(this.checkBoxBusy_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(37, 64);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(445, 602);
            this.dataGridView1.TabIndex = 9;
            // 
            // FormAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 692);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.checkBoxBusy);
            this.Controls.Add(this.buttonAccount);
            this.Controls.Add(this.buttonMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAccount";
            this.Text = "FormAccount";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMap;
        private System.Windows.Forms.Button buttonAccount;
        private System.Windows.Forms.CheckBox checkBoxBusy;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}