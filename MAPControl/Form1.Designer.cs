using GMap.NET.Internals;
using GMap.NET.Projections;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
namespace MAPControl
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.lvSychn = new System.Windows.Forms.ListView();
            this.idx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startLat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startLng = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.finLat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.finLng = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.capacity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.consumption = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbxGeoFin = new System.Windows.Forms.TextBox();
            this.tbxGeoStart = new System.Windows.Forms.TextBox();
            this.tbxcost = new System.Windows.Forms.TextBox();
            this.tbxconsump = new System.Windows.Forms.TextBox();
            this.tbxcapacity = new System.Windows.Forms.TextBox();
            this.tbxLatFin = new System.Windows.Forms.TextBox();
            this.tbxLngFin = new System.Windows.Forms.TextBox();
            this.tbxLngStart = new System.Windows.Forms.TextBox();
            this.tbxLatStart = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnGeoFin = new System.Windows.Forms.Button();
            this.btnGeoStart = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mymap = new GMap.NET.WindowsForms.GMapControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbxVolume = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.lvSynchPnts = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbxGeoPoint = new System.Windows.Forms.TextBox();
            this.tbxLngPoint = new System.Windows.Forms.TextBox();
            this.tbxLatPoint = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnGeoPoint = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.MyMap2 = new GMap.NET.WindowsForms.GMapControl();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button5 = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.lvSychn);
            this.tabPage1.Controls.Add(this.tbxGeoFin);
            this.tabPage1.Controls.Add(this.tbxGeoStart);
            this.tabPage1.Controls.Add(this.tbxcost);
            this.tabPage1.Controls.Add(this.tbxconsump);
            this.tabPage1.Controls.Add(this.tbxcapacity);
            this.tabPage1.Controls.Add(this.tbxLatFin);
            this.tabPage1.Controls.Add(this.tbxLngFin);
            this.tabPage1.Controls.Add(this.tbxLngStart);
            this.tabPage1.Controls.Add(this.tbxLatStart);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.btnGeoFin);
            this.tabPage1.Controls.Add(this.btnGeoStart);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.mymap);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(642, 508);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Δρομολόγια";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(129, 338);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(16, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "Lt";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(129, 308);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "euros";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(129, 282);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "eur/Klm";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(442, 466);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 35);
            this.button3.TabIndex = 25;
            this.button3.Text = "Αποθήκευση";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // lvSychn
            // 
            this.lvSychn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idx,
            this.startLat,
            this.startLng,
            this.finLat,
            this.finLng,
            this.capacity,
            this.cost,
            this.consumption});
            this.lvSychn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lvSychn.FullRowSelect = true;
            this.lvSychn.GridLines = true;
            this.lvSychn.Location = new System.Drawing.Point(6, 370);
            this.lvSychn.MultiSelect = false;
            this.lvSychn.Name = "lvSychn";
            this.lvSychn.ShowGroups = false;
            this.lvSychn.Size = new System.Drawing.Size(616, 89);
            this.lvSychn.TabIndex = 24;
            this.lvSychn.UseCompatibleStateImageBehavior = false;
            this.lvSychn.View = System.Windows.Forms.View.Details;
            this.lvSychn.SelectedIndexChanged += new System.EventHandler(this.lvSychn_SelectedIndexChanged);
            // 
            // idx
            // 
            this.idx.Text = "Δρομολόγιο#";
            this.idx.Width = 56;
            // 
            // startLat
            // 
            this.startLat.Text = "αρχη γ.πλάτος";
            this.startLat.Width = 66;
            // 
            // startLng
            // 
            this.startLng.Text = "αρχη γ.μήκος";
            // 
            // finLat
            // 
            this.finLat.Text = "τερμα γ.πλατος";
            this.finLat.Width = 72;
            // 
            // finLng
            // 
            this.finLng.Text = "τερμα γ.μηκος";
            this.finLng.Width = 61;
            // 
            // capacity
            // 
            this.capacity.Text = "χωρητικότητα";
            this.capacity.Width = 63;
            // 
            // cost
            // 
            this.cost.Text = "αρχικό κοστος";
            this.cost.Width = 69;
            // 
            // consumption
            // 
            this.consumption.Text = "κοστος κίνησης";
            // 
            // tbxGeoFin
            // 
            this.tbxGeoFin.Location = new System.Drawing.Point(59, 166);
            this.tbxGeoFin.Name = "tbxGeoFin";
            this.tbxGeoFin.Size = new System.Drawing.Size(95, 20);
            this.tbxGeoFin.TabIndex = 4;
            // 
            // tbxGeoStart
            // 
            this.tbxGeoStart.Location = new System.Drawing.Point(58, 40);
            this.tbxGeoStart.Name = "tbxGeoStart";
            this.tbxGeoStart.Size = new System.Drawing.Size(96, 20);
            this.tbxGeoStart.TabIndex = 1;
            this.tbxGeoStart.TextChanged += new System.EventHandler(this.tbxGeoStart_TextChanged);
            // 
            // tbxcost
            // 
            this.tbxcost.Location = new System.Drawing.Point(85, 305);
            this.tbxcost.Name = "tbxcost";
            this.tbxcost.Size = new System.Drawing.Size(42, 20);
            this.tbxcost.TabIndex = 8;
            // 
            // tbxconsump
            // 
            this.tbxconsump.Location = new System.Drawing.Point(85, 279);
            this.tbxconsump.Name = "tbxconsump";
            this.tbxconsump.Size = new System.Drawing.Size(41, 20);
            this.tbxconsump.TabIndex = 7;
            // 
            // tbxcapacity
            // 
            this.tbxcapacity.Location = new System.Drawing.Point(85, 335);
            this.tbxcapacity.Name = "tbxcapacity";
            this.tbxcapacity.Size = new System.Drawing.Size(42, 20);
            this.tbxcapacity.TabIndex = 9;
            // 
            // tbxLatFin
            // 
            this.tbxLatFin.Location = new System.Drawing.Point(84, 217);
            this.tbxLatFin.Name = "tbxLatFin";
            this.tbxLatFin.Size = new System.Drawing.Size(70, 20);
            this.tbxLatFin.TabIndex = 5;
            // 
            // tbxLngFin
            // 
            this.tbxLngFin.Location = new System.Drawing.Point(84, 243);
            this.tbxLngFin.Name = "tbxLngFin";
            this.tbxLngFin.Size = new System.Drawing.Size(70, 20);
            this.tbxLngFin.TabIndex = 6;
            // 
            // tbxLngStart
            // 
            this.tbxLngStart.AcceptsReturn = true;
            this.tbxLngStart.Location = new System.Drawing.Point(84, 113);
            this.tbxLngStart.Name = "tbxLngStart";
            this.tbxLngStart.Size = new System.Drawing.Size(70, 20);
            this.tbxLngStart.TabIndex = 3;
            // 
            // tbxLatStart
            // 
            this.tbxLatStart.Location = new System.Drawing.Point(84, 87);
            this.tbxLatStart.Name = "tbxLatStart";
            this.tbxLatStart.Size = new System.Drawing.Size(70, 20);
            this.tbxLatStart.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(59, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "τέρμα";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(59, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "αφετηρία";
            // 
            // btnGeoFin
            // 
            this.btnGeoFin.Location = new System.Drawing.Point(60, 187);
            this.btnGeoFin.Name = "btnGeoFin";
            this.btnGeoFin.Size = new System.Drawing.Size(94, 24);
            this.btnGeoFin.TabIndex = 11;
            this.btnGeoFin.Text = "geocode";
            this.btnGeoFin.UseVisualStyleBackColor = true;
            this.btnGeoFin.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnGeoStart
            // 
            this.btnGeoStart.Location = new System.Drawing.Point(59, 61);
            this.btnGeoStart.Name = "btnGeoStart";
            this.btnGeoStart.Size = new System.Drawing.Size(95, 24);
            this.btnGeoStart.TabIndex = 10;
            this.btnGeoStart.Text = "geocode";
            this.btnGeoStart.UseVisualStyleBackColor = true;
            this.btnGeoStart.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(224, 466);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(218, 35);
            this.button2.TabIndex = 13;
            this.button2.Text = "διαγραφή επιλ.δρομολογίου";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 35);
            this.button1.TabIndex = 12;
            this.button1.Text = "προσθήκη δρομολογίου";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 308);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Αρχικό Κόστος";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Κόστος Κίνησης";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 338);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Χωρητικότητα";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Lng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Lat";
            // 
            // mymap
            // 
            this.mymap.Bearing = 0F;
            this.mymap.CanDragMap = true;
            this.mymap.GrayScaleMode = false;
            this.mymap.LevelsKeepInMemmory = 5;
            this.mymap.Location = new System.Drawing.Point(170, 38);
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
            this.mymap.Size = new System.Drawing.Size(452, 326);
            this.mymap.TabIndex = 0;
            this.mymap.Zoom = 0D;
            this.mymap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.mymap_OnMarkerClick);
            this.mymap.Load += new System.EventHandler(this.mymap_Load);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Lng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lat";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(650, 534);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbxVolume);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.lvSynchPnts);
            this.tabPage2.Controls.Add(this.tbxGeoPoint);
            this.tabPage2.Controls.Add(this.tbxLngPoint);
            this.tabPage2.Controls.Add(this.tbxLatPoint);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.btnGeoPoint);
            this.tabPage2.Controls.Add(this.button7);
            this.tabPage2.Controls.Add(this.button8);
            this.tabPage2.Controls.Add(this.MyMap2);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(642, 508);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Σημεία συλλογής γάλακτος";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // tbxVolume
            // 
            this.tbxVolume.Location = new System.Drawing.Point(56, 152);
            this.tbxVolume.Name = "tbxVolume";
            this.tbxVolume.Size = new System.Drawing.Size(98, 20);
            this.tbxVolume.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(53, 136);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Ποσότητα σημείου";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(442, 466);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(154, 35);
            this.button4.TabIndex = 25;
            this.button4.Text = "Αποθήκευση";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button9_Click);
            // 
            // lvSynchPnts
            // 
            this.lvSynchPnts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvSynchPnts.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lvSynchPnts.FullRowSelect = true;
            this.lvSynchPnts.GridLines = true;
            this.lvSynchPnts.Location = new System.Drawing.Point(6, 370);
            this.lvSynchPnts.MultiSelect = false;
            this.lvSynchPnts.Name = "lvSynchPnts";
            this.lvSynchPnts.ShowGroups = false;
            this.lvSynchPnts.Size = new System.Drawing.Size(593, 89);
            this.lvSynchPnts.TabIndex = 24;
            this.lvSynchPnts.UseCompatibleStateImageBehavior = false;
            this.lvSynchPnts.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Σημείο";
            this.columnHeader1.Width = 56;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "γ.πλάτος";
            this.columnHeader2.Width = 66;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "γ.μήκος";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Ποσότητα σημείου";
            this.columnHeader4.Width = 63;
            // 
            // tbxGeoPoint
            // 
            this.tbxGeoPoint.Location = new System.Drawing.Point(58, 40);
            this.tbxGeoPoint.Name = "tbxGeoPoint";
            this.tbxGeoPoint.Size = new System.Drawing.Size(96, 20);
            this.tbxGeoPoint.TabIndex = 1;
            // 
            // tbxLngPoint
            // 
            this.tbxLngPoint.AcceptsReturn = true;
            this.tbxLngPoint.Location = new System.Drawing.Point(84, 113);
            this.tbxLngPoint.Name = "tbxLngPoint";
            this.tbxLngPoint.Size = new System.Drawing.Size(70, 20);
            this.tbxLngPoint.TabIndex = 3;
            // 
            // tbxLatPoint
            // 
            this.tbxLatPoint.Location = new System.Drawing.Point(84, 87);
            this.tbxLatPoint.Name = "tbxLatPoint";
            this.tbxLatPoint.Size = new System.Drawing.Size(70, 20);
            this.tbxLatPoint.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(59, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Σημείο συλλογής";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // btnGeoPoint
            // 
            this.btnGeoPoint.Location = new System.Drawing.Point(59, 61);
            this.btnGeoPoint.Name = "btnGeoPoint";
            this.btnGeoPoint.Size = new System.Drawing.Size(95, 24);
            this.btnGeoPoint.TabIndex = 10;
            this.btnGeoPoint.Text = "geocode";
            this.btnGeoPoint.UseVisualStyleBackColor = true;
            this.btnGeoPoint.Click += new System.EventHandler(this.btnGeoPoint_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(224, 466);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(218, 35);
            this.button7.TabIndex = 13;
            this.button7.Text = "διαγραφή επιλ.σημείου";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(6, 466);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(218, 35);
            this.button8.TabIndex = 12;
            this.button8.Text = "προσθήκη σημείου";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // MyMap2
            // 
            this.MyMap2.Bearing = 0F;
            this.MyMap2.CanDragMap = true;
            this.MyMap2.GrayScaleMode = false;
            this.MyMap2.LevelsKeepInMemmory = 5;
            this.MyMap2.Location = new System.Drawing.Point(160, 38);
            this.MyMap2.MarkersEnabled = true;
            this.MyMap2.MaxZoom = 2;
            this.MyMap2.MinZoom = 2;
            this.MyMap2.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.MyMap2.Name = "MyMap2";
            this.MyMap2.NegativeMode = false;
            this.MyMap2.PolygonsEnabled = true;
            this.MyMap2.RetryLoadTile = 0;
            this.MyMap2.RoutesEnabled = true;
            this.MyMap2.ShowTileGridLines = false;
            this.MyMap2.Size = new System.Drawing.Size(439, 326);
            this.MyMap2.TabIndex = 0;
            this.MyMap2.Zoom = 0D;
            this.MyMap2.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.MyMap2_OnMarkerClick);
            this.MyMap2.Load += new System.EventHandler(this.MyMap2_Load);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(56, 116);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(25, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Lng";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(56, 90);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(22, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Lat";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.progressBar1);
            this.tabPage3.Controls.Add(this.button5);
            this.tabPage3.Controls.Add(this.radioButton2);
            this.tabPage3.Controls.Add(this.radioButton1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(642, 508);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Αποστάσεις";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(62, 466);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(374, 35);
            this.progressBar1.TabIndex = 27;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(442, 466);
            this.button5.Name = "button5";
            this.button5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button5.Size = new System.Drawing.Size(154, 35);
            this.button5.TabIndex = 26;
            this.button5.Text = "Αποθήκευση";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button10_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.radioButton2.Location = new System.Drawing.Point(62, 144);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(231, 28);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "ελαχιστοποίηση χρόνου";
            this.radioButton2.UseCompatibleTextRendering = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.radioButton1.Location = new System.Drawing.Point(62, 106);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(272, 28);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "ελαχιστοποίηση απόστασης";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 610);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Καταχώρηση σημείων συλλογής,δρομολόγιων & αποστάσεων";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.ListView lvSychn;
        private System.Windows.Forms.ColumnHeader idx;
        private System.Windows.Forms.ColumnHeader startLat;
        private System.Windows.Forms.ColumnHeader startLng;
        private System.Windows.Forms.ColumnHeader finLat;
        private System.Windows.Forms.ColumnHeader finLng;
        private System.Windows.Forms.ColumnHeader capacity;
        private System.Windows.Forms.ColumnHeader cost;
        private System.Windows.Forms.ColumnHeader consumption;
        private System.Windows.Forms.TextBox tbxGeoFin;
        private System.Windows.Forms.TextBox tbxGeoStart;
        private System.Windows.Forms.TextBox tbxcost;
        private System.Windows.Forms.TextBox tbxconsump;
        private System.Windows.Forms.TextBox tbxcapacity;
        private System.Windows.Forms.TextBox tbxLatFin;
        private System.Windows.Forms.TextBox tbxLngFin;
        private System.Windows.Forms.TextBox tbxLngStart;
        private System.Windows.Forms.TextBox tbxLatStart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnGeoFin;
        private System.Windows.Forms.Button btnGeoStart;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private GMapControl mymap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button4;
        public System.Windows.Forms.ListView lvSynchPnts;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox tbxGeoPoint;
        private System.Windows.Forms.TextBox tbxLngPoint;
        private System.Windows.Forms.TextBox tbxLatPoint;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnGeoPoint;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private GMapControl MyMap2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbxVolume;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;

    }
}

