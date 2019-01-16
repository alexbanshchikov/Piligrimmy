namespace DesktopClient
{
    partial class FormMap
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.checkBoxBusy = new System.Windows.Forms.CheckBox();
            this.buttonMap = new System.Windows.Forms.Button();
            this.buttonAccount = new System.Windows.Forms.Button();
            this.buttonGetRoute = new System.Windows.Forms.Button();
            this.buttonOnPlace = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(12, 52);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 18;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(360, 423);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 5D;
            // 
            // checkBoxBusy
            // 
            this.checkBoxBusy.AutoSize = true;
            this.checkBoxBusy.Location = new System.Drawing.Point(290, 18);
            this.checkBoxBusy.Name = "checkBoxBusy";
            this.checkBoxBusy.Size = new System.Drawing.Size(56, 17);
            this.checkBoxBusy.TabIndex = 1;
            this.checkBoxBusy.Text = "Занят";
            this.checkBoxBusy.UseVisualStyleBackColor = true;
            this.checkBoxBusy.CheckedChanged += new System.EventHandler(this.checkBoxBusy_CheckedChanged);
            // 
            // buttonMap
            // 
            this.buttonMap.Enabled = false;
            this.buttonMap.Location = new System.Drawing.Point(12, 5);
            this.buttonMap.Name = "buttonMap";
            this.buttonMap.Size = new System.Drawing.Size(75, 40);
            this.buttonMap.TabIndex = 2;
            this.buttonMap.Text = "Главная";
            this.buttonMap.UseVisualStyleBackColor = true;
            // 
            // buttonAccount
            // 
            this.buttonAccount.Location = new System.Drawing.Point(155, 5);
            this.buttonAccount.Name = "buttonAccount";
            this.buttonAccount.Size = new System.Drawing.Size(75, 40);
            this.buttonAccount.TabIndex = 3;
            this.buttonAccount.Text = "Личный счёт";
            this.buttonAccount.UseVisualStyleBackColor = true;
            this.buttonAccount.Click += new System.EventHandler(this.buttonAccount_Click);
            // 
            // buttonGetRoute
            // 
            this.buttonGetRoute.Location = new System.Drawing.Point(12, 481);
            this.buttonGetRoute.Name = "buttonGetRoute";
            this.buttonGetRoute.Size = new System.Drawing.Size(180, 80);
            this.buttonGetRoute.TabIndex = 6;
            this.buttonGetRoute.Text = "Проложить маршрут";
            this.buttonGetRoute.UseVisualStyleBackColor = true;
            this.buttonGetRoute.Click += new System.EventHandler(this.buttonGetRoute_Click);
            // 
            // buttonOnPlace
            // 
            this.buttonOnPlace.Location = new System.Drawing.Point(192, 481);
            this.buttonOnPlace.Name = "buttonOnPlace";
            this.buttonOnPlace.Size = new System.Drawing.Size(180, 80);
            this.buttonOnPlace.TabIndex = 7;
            this.buttonOnPlace.Text = "На месте";
            this.buttonOnPlace.UseVisualStyleBackColor = true;
            this.buttonOnPlace.Click += new System.EventHandler(this.buttonOnPlace_Click);
            // 
            // FormMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 562);
            this.Controls.Add(this.buttonAccount);
            this.Controls.Add(this.buttonMap);
            this.Controls.Add(this.checkBoxBusy);
            this.Controls.Add(this.buttonGetRoute);
            this.Controls.Add(this.buttonOnPlace);
            this.Controls.Add(this.gmap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormMap";
            this.Text = "Таксометр";
            this.Load += new System.EventHandler(this.FormMap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.CheckBox checkBoxBusy;
        private System.Windows.Forms.Button buttonMap;
        private System.Windows.Forms.Button buttonAccount;
        private System.Windows.Forms.Button buttonGetRoute;
        private System.Windows.Forms.Button buttonOnPlace;
    }
}

