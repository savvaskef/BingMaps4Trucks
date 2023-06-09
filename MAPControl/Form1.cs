using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Data.SqlServerCe;
using System.Net;
 using System.IO;
using System.Xml;
using System.Text;
using classes;
using System.Threading;
using MAPControl.ServiceReference1;
using System.Configuration;

namespace MAPControl
{
    public partial class Form1 : Form
    {
        GMapRoute route;
        List<PointLatLng> points = new List<PointLatLng>();
        List<List<PointLatLng>> pointsColl = new List<List<PointLatLng>>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vbdecimaldelimeter = ","; string Localdecimaldelimeter = ".";
            GMapOverlay objects = this.mymap.Overlays[1];
            GMapOverlay routes = this.mymap.Overlays[0];

            try
           {ListViewItem newitem =new ListViewItem();
                if ( this.lvSychn.SelectedIndices.Count==0)
                {
                     newitem = this.lvSychn.Items.Add("0");//r["ID"].ToString());
          
                }
                else
                {
                   newitem= this.lvSychn.Items.Insert(this.lvSychn.SelectedIndices[0] , "0");
                }
                newitem.SubItems.Add(Convert.ToDouble(this.tbxLatStart.Text.ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add(Convert.ToDouble(this.tbxLngStart.Text.ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add(Convert.ToDouble(this.tbxLatFin.Text.ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add(Convert.ToDouble(this.tbxLngFin.Text.ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add(Convert.ToDouble(this.tbxcapacity.Text.ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add(Convert.ToDouble(this.tbxcost.Text.ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add(Convert.ToDouble(this.tbxconsump.Text.ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
             }
            catch (SystemException sex)
            {
                MessageBox.Show("Πρέπει να καταχωρηθουν ολα τα πεδία με αριθμούς");
                Console.WriteLine(sex.Message);
                lvSychn.Items.RemoveAt(this.lvSychn.SelectedIndices[0] - 1);
                return;
            }
            fixID(lvSychn.Items);
            MapFromEntries(objects, routes, lvSychn);
       
        }




        public void MapFromEntries(GMapOverlay objects, GMapOverlay routes, ListView entries)
        {
            string vbdecimaldelimeter = ","; string Localdecimaldelimeter = ".";
            int idx = routes.Routes.Count + 1;
            objects.Markers.Clear();
            routes.Routes.Clear();
            //objects.Routes.Clear();
            //routes.Markers.Clear();
           
            double cummlat = 0; double cummlng = 0;
            double cummCntr = 0; 
            for (int i = 0; i < entries.Items.Count; i++)
            {

                double lt1 = Convert.ToDouble(entries.Items[i].SubItems[1].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));
                double lg1 = Convert.ToDouble(entries.Items[i].SubItems[2].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));
                double lt2 = Convert.ToDouble(entries.Items[i].SubItems[3].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));
                double lg2 = Convert.ToDouble(entries.Items[i].SubItems[4].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));

               // objects.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng(lt1, lg1)));
                 GMapMarkerImage ArrayMarkers = new GMapMarkerImage(new PointLatLng(lt1, lg1), MAPControl.Properties.Resources.start);
                ArrayMarkers.ToolTipText = "Home #" + entries.Items[i].SubItems[0].Text;
                objects.Markers.Add(ArrayMarkers);


//                objects.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng(lt2, lg2)));
                GMapMarkerImage ArrayMarkers2 = new GMapMarkerImage(new PointLatLng(lt2, lg2), MAPControl.Properties.Resources.fin);
                ArrayMarkers2.ToolTipText = "Fin #" + entries.Items[i].SubItems[0].Text;
                objects.Markers.Add(ArrayMarkers2);

                points = new List<PointLatLng>();
                points.Add(new PointLatLng(lt1, lg1));
                points.Add(new PointLatLng(lt2, lg2));

                route = new GMapRoute(points, "route");
                route.Stroke.Color = getcolor(i);
                routes.Routes.Add(route);
                cummlat += lt1; cummlat += lt2;
                cummlng += lg1; cummlng += lg2;
                cummCntr += 2;
            }
            this.mymap.Position = new PointLatLng(cummlat/ cummCntr, cummlng/cummCntr);
            
        }


        public void MapFromEntriesPnts(GMapOverlay objects, ListView entries)
        {
            string vbdecimaldelimeter = ","; string Localdecimaldelimeter = ".";
            objects.Markers.Clear();
            double cummlat = 0; double cummlng = 0;
            double cummCntr = 0; 
        
            for (int i = 0; i < entries.Items.Count; i++)
            {

                double lt1 = Convert.ToDouble(entries.Items[i].SubItems[1].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));
                double lg1 = Convert.ToDouble(entries.Items[i].SubItems[2].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));

          //      objects.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng(lt1, lg1)));

                points = new List<PointLatLng>();
                points.Add(new PointLatLng(lt1, lg1));
                 GMapMarkerImage ArrayMarkers= new GMapMarkerImage(new PointLatLng(lt1, lg1),MAPControl.Properties.Resources.cow1);
                 ArrayMarkers.ToolTipText = "#" + entries.Items[i].SubItems[0].Text;
                objects.Markers.Add(ArrayMarkers);

                cummlat += lt1; 
                cummlng += lg1; 
                cummCntr += 1;


            }
            this.MyMap2.Position = new PointLatLng(cummlat / cummCntr, cummlng / cummCntr);
         
        }



        private System.Drawing.Color getcolor(int idx)
        {

            switch (idx)
            {

                case 0: return System.Drawing.Color.Brown;
                    break;
                case 1: return System.Drawing.Color.Black;
                    break;
                case 2: return System.Drawing.Color.LightBlue;
                    break;
                case 3: return System.Drawing.Color.Tomato;
                    break;
                case 4: return System.Drawing.Color.CadetBlue;
                    break;
                case 5: return System.Drawing.Color.BurlyWood;
                    break;
                case 6: return System.Drawing.Color.Coral;
                    break;
                case 7: return System.Drawing.Color.DarkGreen;
                    break;
                case 8: return System.Drawing.Color.DarkGoldenrod;
                    break;
                case 9: return System.Drawing.Color.Gray;
                    break;
                case 10: return System.Drawing.Color.Orange;
                    break;
                default: return System.Drawing.Color.Purple;
                    break;


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //added properties

            //   this.mymap.SetCurrentPositionByKeywords("USA");
            this.mymap.MapProvider = GMapProviders.BingMapOld;
            this.mymap.Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            this.mymap.MinZoom = 3;
            this.mymap.MaxZoom = 17;
            this.mymap.Zoom = 8;
            this.mymap.Manager.Mode = AccessMode.ServerAndCache;

            this.MyMap2.MapProvider = GMapProviders.BingMapOld;
            this.MyMap2.Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            this.MyMap2.MinZoom = 3;
            this.MyMap2.MaxZoom = 17;
            this.MyMap2.Zoom =8;
            this.MyMap2.Manager.Mode = AccessMode.ServerAndCache;


            GMapOverlay objects = new GMapOverlay(mymap, "objects");
            GMapOverlay routes = new GMapOverlay(mymap, "routes");
            GMapOverlay polygons = new GMapOverlay(mymap, "polygons");

            GMapOverlay PntObjects = new GMapOverlay(MyMap2, "objects");
            mymap.Overlays.Add(routes);
            mymap.Overlays.Add(objects);
           
           // mymap.Overlays.Add(polygons);

            MyMap2.Overlays.Add(PntObjects);

            loadfromDB(@"Data Source="+Application.StartupPath+@"\Routing.sdf");
            MapFromEntries(objects, routes, lvSychn);

            loadfromDBPnts(@"Data Source="+Application.StartupPath+@"\Routing.sdf");
            MapFromEntriesPnts(PntObjects, lvSynchPnts);

           
            //routes.Routes.CollectionChanged += new GMap.NET.ObjectModel.NotifyCollectionChangedEventHandler(Routes_CollectionChanged);
            //objects.Markers.CollectionChanged += new GMap.NET.ObjectModel.NotifyCollectionChangedEventHandler(Markers_CollectionChanged);
             // set current marker
            //var  currentMarker = new GMarkerGoogle(mymap.Position, GMarkerGoogleType.arrow);
            //currentMarker.IsHitTestVisible = false;
            //objects.Markers.Add(currentMarker);
            //double Latt=54.6961334816182;double Longt=25.2985095977783;
            //points.Add(new PointLatLng(  Latt,Longt  ));

            //     objects.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng(Latt,Longt)));



        }

        private void loadfromDBPnts(string ConnString)
        {

            SqlCeConnection con = new SqlCeConnection();

            string vbdecimaldelimeter = ","; string Localdecimaldelimeter = ".";
            con.ConnectionString = ConnString;//@"Data Source="+Application.StartupPath+@"\Routing.sdf";;
            con.Open();
            SqlCeCommand com = new SqlCeCommand("Select x,y,volume from mapPoints", con);
            SqlCeDataReader r = com.ExecuteReader();


            while (r.Read())
            {
                try
                {
                    ListViewItem newitem = this.lvSynchPnts.Items.Add("0");//r["ID"].ToString());
                    newitem.SubItems.Add(Convert.ToDouble(r["x"].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                    newitem.SubItems.Add(Convert.ToDouble(r["y"].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                    newitem.SubItems.Add(Convert.ToDouble(r["volume"].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                }
                catch (SystemException sex)
                {
                    MessageBox.Show("Λαθος κατα τη φόρτωση σημείων");
                }
            }

            r.Close();
            fixID(lvSynchPnts.Items);


        }


        private void loadfromDB(string ConnString)
        {

            SqlCeConnection con = new SqlCeConnection();

            string vbdecimaldelimeter = ","; string Localdecimaldelimeter = ".";
            con.ConnectionString = ConnString;//@"Data Source="+Application.StartupPath+@"\Routing.sdf";;
            con.Open();
            SqlCeCommand com = new SqlCeCommand("Select ID,x,y,truckCost,truckCapacity,truckTransportCost from mapHomes", con);
            SqlCeDataReader r = com.ExecuteReader();

            com = new SqlCeCommand("Select ID,x,y from mapFins", con);
            SqlCeDataReader f = com.ExecuteReader();

            while (r.Read())
            {
                f.Read();
                try
                {
                    ListViewItem newitem = this.lvSychn.Items.Add(r["ID"].ToString());
                    newitem.SubItems.Add(Convert.ToDouble(r["x"].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                    newitem.SubItems.Add(Convert.ToDouble(r["y"].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                    newitem.SubItems.Add(Convert.ToDouble(f["x"].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                    newitem.SubItems.Add(Convert.ToDouble(f["y"].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                    newitem.SubItems.Add(Convert.ToDouble(r["truckcapacity"].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                    newitem.SubItems.Add(Convert.ToDouble(r["truckCost"].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                    newitem.SubItems.Add(Convert.ToDouble(r["truckTransportCost"].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                    //newitem.SubItems.Add("0");
                }
                catch (SystemException sex)
                {
                    MessageBox.Show("Λαθος κατα τη φόρτωση δρομολόγιου");
                }
            }

            r.Close();
            f.Close();
            fixID(lvSychn.Items);


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            GeoCoderStatusCode status;
            var pp1 = GMapProviders.GoogleMap.GetPoint(tbxGeoStart.Text, out status);
            tbxLatStart.Text = pp1.GetValueOrDefault().Lat.ToString();
            tbxLngStart.Text = pp1.GetValueOrDefault().Lng.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            GeoCoderStatusCode status;
            var pp1 = GMapProviders.GoogleMap.GetPoint(tbxGeoFin.Text, out status);
            tbxLatFin.Text = pp1.GetValueOrDefault().Lat.ToString();
            tbxLngFin.Text = pp1.GetValueOrDefault().Lng.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lvSychn.SelectedItems.Count <= 0) return;
            var itmiDX = lvSychn.SelectedIndices[0];

            tbxLatStart.Text = lvSychn.Items[itmiDX].SubItems[1].Text;
            tbxLngStart.Text = lvSychn.Items[itmiDX].SubItems[2].Text;
            tbxLatFin.Text = lvSychn.Items[itmiDX].SubItems[3].Text;
            tbxLngFin.Text = lvSychn.Items[itmiDX].SubItems[4].Text;
            tbxcapacity.Text = lvSychn.Items[itmiDX].SubItems[5].Text;
            tbxcost.Text = lvSychn.Items[itmiDX].SubItems[6].Text;
            tbxconsump.Text = lvSychn.Items[itmiDX].SubItems[7].Text;


            lvSychn.SelectedItems[0].Remove();
             GMapOverlay objects = this.mymap.Overlays[1];
            GMapOverlay routes = this.mymap.Overlays[0];
            fixID(lvSychn.Items);
            MapFromEntries(objects, routes, lvSychn);
            this.mymap.ReloadMap(); this.mymap.Refresh();
            if (itmiDX==lvSychn.Items.Count){
                foreach(ListViewItem item in lvSychn.Items) item.Selected = false;
            }
            else{
                lvSychn.Items[itmiDX].Selected = true;
            }
            lvSychn.Focus();
            }


        public static void fixID(ListView.ListViewItemCollection lv)
        {

            for (int ii = 0; ii < lv.Count; ii++)
            {

                lv[ii].SubItems[0].Text = (ii + 1).ToString();
            }

        }
        private void lvSychn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (lvSychn.Items.Count < 2) { MessageBox.Show("Πρέπει να καταχωρήσετε τουλάχιστον 2 δρομολόγια/φορτηγα"); return; }
          
            SqlCeConnection con = new SqlCeConnection();
            con.ConnectionString = @"Data Source="+Application.StartupPath+@"\Routing.sdf";;
            con.Open();

            SqlCeCommand com = new SqlCeCommand("DELETE FROM  mapHomes", con);
            com.ExecuteNonQuery();
            com = new SqlCeCommand("DELETE FROM  mapFins", con);
            com.ExecuteNonQuery();


            for (int i = 0; i <= this.lvSychn.Items.Count - 1; i++)
            {
                var itm = lvSychn.Items[i];
                com = new SqlCeCommand("INSERT INTO mapHomes (ID,x,y,truckCapacity,truckCost,truckTransportCost) VALUES ('"
                    + "Home" + itm.SubItems[0].Text + "',"
                    + itm.SubItems[1].Text.Replace(",", ".") + ","
                    + itm.SubItems[2].Text.Replace(",", ".") + ","
                    + itm.SubItems[5].Text.Replace(",", ".") + ","
                    + itm.SubItems[6].Text.Replace(",", ".") + ","
                    + itm.SubItems[7].Text.Replace(",", ".") + ")",
                    con);
                com.ExecuteNonQuery();
                com = new SqlCeCommand("INSERT INTO mapFins (ID,x,y) VALUES ('"
                + "Fin" + itm.SubItems[0].Text + "',"
                + itm.SubItems[3].Text.Replace(",", ".") + ","
                + itm.SubItems[4].Text.Replace(",", ".") + ")",
                con);
                com.ExecuteNonQuery();

            }


        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void tbxGeoStart_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGeoPoint_Click(object sender, EventArgs e)
        {
            GeoCoderStatusCode status;
            var pp1 = GMapProviders.GoogleMap.GetPoint(tbxGeoPoint.Text, out status);
            tbxLatPoint.Text = pp1.GetValueOrDefault().Lat.ToString();
            tbxLngPoint.Text = pp1.GetValueOrDefault().Lng.ToString();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string vbdecimaldelimeter = ","; string Localdecimaldelimeter = ".";
            GMapOverlay objects = this.MyMap2.Overlays[0];

            try
            {
                ListViewItem newitem = new ListViewItem();
                if ( this.lvSynchPnts.SelectedIndices.Count==0)
                {
                    newitem = this.lvSynchPnts.Items.Add("0");//r["ID"].ToString());

                }
                else
                {
                    newitem = this.lvSynchPnts.Items.Insert(this.lvSynchPnts.SelectedIndices[0], "0");
                }
                newitem.SubItems.Add(Convert.ToDouble(this.tbxLatPoint.Text.ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add(Convert.ToDouble(this.tbxLngPoint.Text.ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add(Convert.ToDouble(this.tbxVolume.Text.ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
            }
            catch (SystemException sex)
            {
                MessageBox.Show("Πρέπει να καταχωρηθουν ολα τα πεδία με αριθμούς");
                Console.WriteLine(sex.Message);
                lvSynchPnts.Items.RemoveAt(lvSynchPnts.Items.Count - 1);
                return;
            }
            fixID(lvSynchPnts.Items);
            MapFromEntriesPnts(objects, lvSynchPnts);
     
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (lvSynchPnts.SelectedItems.Count <= 0) return;
            var itmiDX = lvSynchPnts.SelectedIndices[0];

            tbxLatPoint.Text = lvSynchPnts.Items[itmiDX].SubItems[1].Text;
            tbxLngPoint.Text = lvSynchPnts.Items[itmiDX].SubItems[2].Text;
            tbxVolume.Text = lvSynchPnts.Items[itmiDX].SubItems[3].Text;


            lvSynchPnts.SelectedItems[0].Remove();
            GMapOverlay objects = this.MyMap2.Overlays[0];
            fixID(lvSynchPnts.Items);
            MapFromEntriesPnts(objects, lvSynchPnts);
            if (itmiDX == lvSynchPnts.Items.Count)
            {
                foreach (ListViewItem item in lvSynchPnts.Items) item.Selected = false;
            }
            else
            {
                lvSynchPnts.Items[itmiDX].Selected = true;
            }
            lvSynchPnts.Focus();
  


        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (lvSynchPnts.Items.Count < 5) { MessageBox.Show("Πρέπει να καταχωρήσετε τουλάχιστον 5 σημεία συλλογής"); return; }
       
            SqlCeConnection con = new SqlCeConnection();
            con.ConnectionString = @"Data Source="+Application.StartupPath+@"\Routing.sdf";;
            con.Open();

            SqlCeCommand com = new SqlCeCommand("DELETE FROM  mapPoints", con);
            com.ExecuteNonQuery();

            for (int i = 0; i <= this.lvSynchPnts.Items.Count - 1; i++)
            {
                var itm = lvSynchPnts.Items[i];
                com = new SqlCeCommand("INSERT INTO mapPoints (ID,x,y,volume) VALUES ('"
                    + "point" + itm.SubItems[0].Text + "',"
                    + itm.SubItems[1].Text.Replace(",", ".") + ","
                    + itm.SubItems[2].Text.Replace(",", ".") + ","

                    + itm.SubItems[3].Text.Replace(",", ".") + ")",
                    con);
                com.ExecuteNonQuery();

            }



        }

        private void button10_Click(object sender, EventArgs e)
        {
            if ((lvSychn.Items.Count < 2) || (lvSynchPnts.Items.Count < 5)) { MessageBox.Show("Πρέπει στην φόρμα καταχώρησης να φαίνονται τουλάχιστον 2 δρομολόγια/φορτηγα και 5 σημεία συλλογής"); return; }
          
            SqlCeConnection con = new SqlCeConnection();
            con.ConnectionString = @"Data Source="+Application.StartupPath+@"\Routing.sdf";;
            con.Open();

            SqlCeCommand com =new SqlCeCommand("DELETE FROM  nxn", con);
            com.ExecuteNonQuery();

            com = new SqlCeCommand("DELETE FROM  settings where setting='optimization'", con);
            com.ExecuteNonQuery();
            string opt="";
            if (radioButton1.Checked) opt = "distance"; else opt = "time";
              com = new SqlCeCommand("INSERT INTO settings (setting,value) values('optimization','"+opt+"')", con);
            com.ExecuteNonQuery();

            List<string> tempxy = new List<string>();

            for (int i = 0; i <= this.lvSychn.Items.Count - 1; i++)
            {
                var itm =this.lvSychn.Items[i];
               tempxy.Add("Home"+itm.SubItems[0].Text +"," + itm.SubItems[1].Text.ToString().Replace(",", ".") + "," + itm.SubItems[2].Text.ToString().Replace(",", "."));
            }
            
            for (int i = 0; i <= this.lvSynchPnts.Items.Count - 1; i++)
            {
                var itm = this.lvSynchPnts.Items[i];
                tempxy.Add("point" + itm.SubItems[0].Text + "," + itm.SubItems[1].Text.ToString().Replace(",", ".") + "," + itm.SubItems[2].Text.ToString().Replace(",", "."));
            }
            for (int i = 0; i <= this.lvSychn.Items.Count - 1; i++)
            {
                var itm = this.lvSychn.Items[i];
                tempxy.Add("Fin" + itm.SubItems[0].Text + "," + itm.SubItems[3].Text.ToString().Replace(",", ".") + "," + itm.SubItems[4].Text.ToString().Replace(",", "."));
            }

            this.progressBar1.Maximum =  Convert.ToInt16( Math.Pow(tempxy.Count, 2));

            List<distanceEntry> distances = new List<distanceEntry>();
            distances = EXPORTnxn(tempxy, this.radioButton1.Checked, this.progressBar1);
            foreach (distanceEntry d in distances)
            {
                com = new SqlCeCommand("INSERT INTO nxn (point1,point2,distance) VALUES ('" + d.Point1 + "','" + d.Point2 + "'," + d.distance.ToString().Replace(",", ".") + ")", con);
                com.ExecuteNonQuery();
            }
          
            this.progressBar1.Value = 0;
            con.Dispose();
            con.Close(); 
        }

        public static double googledist(double x1, double y1, double x2, double y2, string prop)
        {




            string origins = x1.ToString().Replace(",", ".") + "," + y1.ToString().Replace(",", ".");

            string destinations = x2.ToString().Replace(",", ".") + "," + y2.ToString().Replace(",", ".");

            double distance = 0;


            Uri myUri = new Uri("http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origins + "&destinations=" + destinations + "&mode=driving&language=en-en&sensor=false");
            //    myUri = new Uri("http://maps.googleapis.com/maps/api/distancematrix/json?origins="+points+"&destinations="+points+"&mode=driving&language=en-en&sensor=false");
            WebRequest myWebRequest = WebRequest.Create(myUri);
            // Assign the response object of 'WebRequest' to a 'WebResponse' variable.
            WebResponse myWebResponse = myWebRequest.GetResponse();
            StreamReader reader = new StreamReader(myWebResponse.GetResponseStream());
            string obj = reader.ReadToEnd();
            myWebResponse.Close();

            Console.WriteLine(obj);
            using (XmlReader XMLreader = XmlReader.Create(new StringReader(obj)))
            {

                for (int i = 0; i < origins.Split(new Char[] { '|' }).Length; i++)
                {
                    XMLreader.ReadToFollowing("row");
                    for (int j = 0; j < destinations.Split(new Char[] { '|' }).Length; j++)
                    {
                        XMLreader.ReadToFollowing("element");
                        XMLreader.ReadToFollowing(prop);
                        string a = XMLreader.ReadInnerXml();
                        distance = Convert.ToDouble(a.Substring(12, -12 + a.LastIndexOf("</value>")));
                    }
                }

            }//endValuesEntry





            return distance;

        }//end getmatrix


        public static double dist(double X1, double Y1, double X2, double Y2)
        {
            //' calculate Euclidean distance
            double DX = X2 - X1;
            double DY = Y2 - Y1;

            return Math.Pow(Math.Pow(DX, 2) + Math.Pow(DY, 2), (0.5)) * 111101.414179072;

        }

        public static List<distanceEntry> EXPORTnxn(List<string> points2lookup, bool mindist,ProgressBar PBAR)
        {
            int lag = 1000000;
            List<distanceEntry> distances = new List<distanceEntry>();
            
              IRouteService routeService = new RouteServiceClient();
                RouteRequest routeRequest = new RouteRequest();
                routeRequest.Credentials = new Credentials();
                routeRequest.Credentials.ApplicationId = "AjGVsXovYaKDEVcc_N83jVh-cT9d9M4wL-AGIR9mBl06DYDRGnC-miCxBtnDMxuH";
                //http://dev.virtualearth.net/REST/V1/Routes/Driving?wp.0=47.530072,-122.032904&wp.1=47.674912,-122.124045&avoid=minimizeTolls&key=AjGVsXovYaKDEVcc_N83jVh-cT9d9M4wL-AGIR9mBl06DYDRGnC-miCxBtnDMxuH
            routeRequest.Options = new RouteOptions();
                routeRequest.Options.RoutePathType = RoutePathType.None;
               routeRequest.Options.Mode = TravelMode.Driving;

            
            List<string> str1list; List<string> str2list;
            foreach (string str1 in points2lookup)
            {
                foreach (string str2 in points2lookup)
                {
                    distanceEntry tempdist = new distanceEntry();
                    str1list = str1.Split(new Char[] { ',' }).ToList();
                    str2list = str2.Split(new Char[] { ',' }).ToList();

                    tempdist.Point1 = str1list[0];
                    tempdist.Point2 = str2list[0];
                    
                    
                    Waypoint[] waypoints = new Waypoint[2];
                    waypoints[0] = new Waypoint();
                    waypoints[0].Location = new Location();
                    waypoints[0].Location.Latitude = Convert.ToDouble(str1list[1].Replace(".", ","));
                    waypoints[0].Location.Longitude = Convert.ToDouble(str1list[2].Replace(".", ","));
                    waypoints[1] = new Waypoint(); 
                    waypoints[1].Location = new Location();
                    waypoints[1].Location.Latitude = Convert.ToDouble(str2list[1].Replace(".", ","));
                    waypoints[1].Location.Longitude = Convert.ToDouble(str2list[2].Replace(".", ","));               
                    routeRequest.Waypoints = waypoints;
                  
                    if (mindist) routeRequest.Options.Optimization = RouteOptimization.MinimizeDistance;
                    else routeRequest.Options.Optimization = RouteOptimization.MinimizeTime;
          

                    IRouteService r = new RouteServiceClient();
                    RouteResponse rsp= r.CalculateRoute(routeRequest);
                    tempdist.distance=rsp.Result.Summary.Distance*1000;
                    distances.Add(tempdist);
                    PBAR.Value = distances.Count;
                    
                  
                }
            }


            return distances;
            
        }

        private void MyMap2_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
              var outsucc=GeoCoderStatusCode.G_GEO_SUCCESS ;
            var pp2 = GMapProviders.GoogleMap.GetPlacemark(item.Position, out outsucc);
  
            MessageBox.Show("Lattitude:" + item.Position.Lat + "," + "Longtitude:" + item.Position.Lng + (char)13 +
       "" + pp2.Address + (char)13 + pp2.AdministrativeAreaName + ", " + pp2.CountryName);
        }

        private void MyMap2_Load(object sender, EventArgs e)
        {
        
        }

        private void mymap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            var outsucc = GeoCoderStatusCode.G_GEO_SUCCESS;
            var pp2 = GMapProviders.GoogleMap.GetPlacemark(item.Position, out outsucc);

            MessageBox.Show("Lattitude:" + item.Position.Lat + "," + "Longtitude:" + item.Position.Lng + (char)13 +
      "" + pp2.Address + (char)13 + pp2.AdministrativeAreaName + ", " + pp2.CountryName);         
    
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mymap_Load(object sender, EventArgs e)
        {

        }
    }
}