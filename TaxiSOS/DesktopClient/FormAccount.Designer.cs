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
            this.buttonBackToMap = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBoxTotal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonMap
            // 
            this.buttonMap.Location = new System.Drawing.Point(12, 5);
            this.buttonMap.Name = "buttonMap";
            this.buttonMap.Size = new System.Drawing.Size(75, 40);
            this.buttonMap.TabIndex = 3;
            this.buttonMap.Text = "Главная";
            this.buttonMap.UseVisualStyleBackColor = true;
            this.buttonMap.Click += new System.EventHandler(this.buttonMap_Click);
            // 
            // buttonAccount
            // 
            this.buttonAccount.Enabled = false;
            this.buttonAccount.Location = new System.Drawing.Point(155, 5);
            this.buttonAccount.Name = "buttonAccount";
            this.buttonAccount.Size = new System.Drawing.Size(75, 40);
            this.buttonAccount.TabIndex = 4;
            this.buttonAccount.Text = "Личный счёт";
            this.buttonAccount.UseVisualStyleBackColor = true;
            this.buttonAccount.Click += new System.EventHandler(this.buttonAccount_Click);
            // 
            // checkBoxBusy
            // 
            this.checkBoxBusy.AutoSize = true;
            this.checkBoxBusy.Location = new System.Drawing.Point(290, 18);
            this.checkBoxBusy.Name = "checkBoxBusy";
            this.checkBoxBusy.Size = new System.Drawing.Size(56, 17);
            this.checkBoxBusy.TabIndex = 5;
            this.checkBoxBusy.Text = "Занят";
            this.checkBoxBusy.UseVisualStyleBackColor = true;
            this.checkBoxBusy.CheckedChanged += new System.EventHandler(this.checkBoxBusy_CheckedChanged);
            // 
            // buttonBackToMap
            // 
            this.buttonBackToMap.Location = new System.Drawing.Point(12, 470);
            this.buttonBackToMap.Name = "buttonBackToMap";
            this.buttonBackToMap.Size = new System.Drawing.Size(360, 80);
            this.buttonBackToMap.TabIndex = 8;
            this.buttonBackToMap.Text = "Вернуться";
            this.buttonBackToMap.UseVisualStyleBackColor = true;
            this.buttonBackToMap.Visible = false;
            this.buttonBackToMap.Click += new System.EventHandler(this.buttonBackToMap_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(360, 356);
            this.dataGridView1.TabIndex = 9;
            // 
            // textBoxTotal
            // 
            this.textBoxTotal.Location = new System.Drawing.Point(12, 414);
            this.textBoxTotal.Multiline = true;
            this.textBoxTotal.Name = "textBoxTotal";
            this.textBoxTotal.Size = new System.Drawing.Size(360, 50);
            this.textBoxTotal.TabIndex = 10;
            // 
            // FormAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 562);
            this.Controls.Add(this.textBoxTotal);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonBackToMap);
            this.Controls.Add(this.checkBoxBusy);
            this.Controls.Add(this.buttonAccount);
            this.Controls.Add(this.buttonMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
        private System.Windows.Forms.Button buttonBackToMap;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBoxTotal;
    }
}