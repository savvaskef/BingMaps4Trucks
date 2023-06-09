namespace output
{
    partial class Form1
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.reverseGeo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.latt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lng = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.distance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cummdistance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.volume = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cummVolume = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.percvolume = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cummCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.mymap = new GMap.NET.WindowsForms.GMapControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.reverseGeo,
            this.latt,
            this.lng,
            this.distance,
            this.cummdistance,
            this.volume,
            this.cummVolume,
            this.percvolume,
            this.cost,
            this.cummCost});
            this.listView1.Location = new System.Drawing.Point(3, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(694, 462);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // id
            // 
            this.id.Text = "#";
            this.id.Width = 84;
            // 
            // reverseGeo
            // 
            this.reverseGeo.Text = "σημείο";
            this.reverseGeo.Width = 92;
            // 
            // latt
            // 
            this.latt.Text = "γ.πλάτος";
            // 
            // lng
            // 
            this.lng.Text = "γ.μήκος";
            // 
            // distance
            // 
            this.distance.Text = "απόσταση";
            this.distance.Width = 99;
            // 
            // cummdistance
            // 
            this.cummdistance.Text = "Σ(απόσταση)";
            this.cummdistance.Width = 92;
            // 
            // volume
            // 
            this.volume.Text = "όγκος";
            this.volume.Width = 67;
            // 
            // cummVolume
            // 
            this.cummVolume.Text = "Σ(όγκος)";
            this.cummVolume.Width = 84;
            // 
            // percvolume
            // 
            this.percvolume.Text = "Πληρες(%)";
            // 
            // cost
            // 
            this.cost.Text = "Κόστος";
            this.cost.Width = 93;
            // 
            // cummCost
            // 
            this.cummCost.Text = "Σ(Κόστος)";
            this.cummCost.Width = 91;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(106, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // mymap
            // 
            this.mymap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mymap.Bearing = 0F;
            this.mymap.CanDragMap = true;
            this.mymap.GrayScaleMode = false;
            this.mymap.LevelsKeepInMemmory = 5;
            this.mymap.Location = new System.Drawing.Point(6, 6);
            this.mymap.MarkersEnabled = true;
            this.mymap.MaxZoom = 2;
            this.mymap.MinZoom = 2;
            this.mymap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mymap.Name = "mymap";
            this.mymap.NegativeMode = false;
            this.mymap.PolygonsEnabled = true;
            this.mymap.RetryLoadTile = 0;
            this.mymap.RoutesEnabled = true;
            this.mymap.ShowTileGridLines = false;
            this.mymap.Size = new System.Drawing.Size(699, 474);
            this.mymap.TabIndex = 3;
            this.mymap.Zoom = 0D;
            this.mymap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.mymap_OnMarkerClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 34);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(731, 512);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mymap);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(723, 486);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Χάρτης";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(723, 486);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Στοιχεία";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 573);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "Δρομολόγια Φορτηγών MinCost";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader reverseGeo;
        private System.Windows.Forms.ColumnHeader latt;
        private System.Windows.Forms.ColumnHeader lng;
        private System.Windows.Forms.ColumnHeader distance;
        private System.Windows.Forms.ColumnHeader cummdistance;
        private System.Windows.Forms.ColumnHeader volume;
        private System.Windows.Forms.ColumnHeader cummVolume;
        private System.Windows.Forms.ColumnHeader cost;
        private System.Windows.Forms.ColumnHeader cummCost;
        private System.Windows.Forms.ComboBox comboBox1;
        private GMap.NET.WindowsForms.GMapControl mymap;
        private System.Windows.Forms.ColumnHeader percvolume;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

