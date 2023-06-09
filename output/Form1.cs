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
using output.ServiceReference1;
using System.Configuration;
namespace output
{
    public partial class Form1 : Form
    {
        List<List<object[]>> sets = new List<List<object[]>>();
        GMapRoute route;
        List<PointLatLng> points = new List<PointLatLng>();
        List<List<PointLatLng>> pointsColl = new List<List<PointLatLng>>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            GMapOverlay routes = new GMapOverlay(mymap, "routes");
            GMapOverlay objects = new GMapOverlay(mymap, "objects");


            this.mymap.MapProvider = GMapProviders.BingMapOld;
            this.mymap.Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            this.mymap.MinZoom = 3;
            this.mymap.MaxZoom = 17;
            this.mymap.Zoom = 8;
            this.mymap.Manager.Mode = AccessMode.ServerAndCache;
            mymap.Overlays.Add(routes);
            mymap.Overlays.Add(objects);

            loadfromDB(@"Data Source="+Application.StartupPath+@"\Routing.sdf");

            for (int i = 0; i < sets.Count; i++)
                this.comboBox1.Items.Add("δρομολόγιο " + sets[i][0][0]);


            this.comboBox1.SelectedIndex = 0;
        }
        private void loadfromDB(string ConnString)
        {       

            SqlCeConnection con = new SqlCeConnection();



            string vbdecimaldelimeter = ","; string Localdecimaldelimeter = ".";
            con.ConnectionString = @"Data Source="+Application.StartupPath+@"\Routing.sdf";
     //       SqlCeEngine engine = new SqlCeEngine(con.ConnectionString);
       //     engine.Upgrade(con.ConnectionString);
            con.Open();
            System.Data.SqlServerCe.SqlCeCommand
            com = new SqlCeCommand("Select routeID,pointID,lattitude,longtitude,distance,cummdistance,volume,cummvolume,percvolume,cost,cummcost from results", con);
            SqlCeDataReader r = com.ExecuteReader();
            bool firstentry = true;
           // List<List<object[]>> sets= new List<List<object[]>>();
            List<object[]> collection = new List<object[]>();

            string oldzero = "0";
            while (r.Read())
            {   
               
                Object[] values = new Object[r.FieldCount];
               
                if   ((!firstentry)&&(oldzero!=r[0].ToString())){
                    this.sets.Add(collection);
                    collection = new List<object[]>();
                }
                    int fieldCount = r.GetValues(values);
                collection.Add(values);
              
                oldzero = r[0].ToString();
                firstentry = false;
            }

               this.sets.Add(collection);
            r.Close();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vbdecimaldelimeter = ","; string Localdecimaldelimeter = ".";
            GMapOverlay objects = this.mymap.Overlays[1];
            GMapOverlay routes = this.mymap.Overlays[0];
             
            int i = this.comboBox1.SelectedIndex;
            
            this.listView1.Items.Clear();
            List<object[]>seti=sets[i];
            for (int j = 0; j < seti.Count; j++)
            {
                ListViewItem newitem = this.listView1.Items.Add(seti[j][0].ToString());
                newitem.SubItems.Add((seti[j][1].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add((seti[j][2].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add((seti[j][3].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add((seti[j][4].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add((seti[j][5].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add((seti[j][6].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add((seti[j][7].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                
                newitem.SubItems.Add((seti[j][8].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                
                newitem.SubItems.Add((seti[j][9].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
                newitem.SubItems.Add((seti[j][10].ToString().Replace(Localdecimaldelimeter, vbdecimaldelimeter)).ToString());
            
            }
            this.listView1.Columns[0].Width = 25;
            this.listView1.Columns[1].Width = 55;
            this.listView1.Columns[2].Width = 63;
            this.listView1.Columns[3].Width = 63;
            this.listView1.Columns[4].Width = 70;
            this.listView1.Columns[5].Width = 80;
            this.listView1.Columns[6].Width = 45;
            this.listView1.Columns[7].Width = 60;

            this.listView1.Columns[8].Width = 70;
            this.listView1.Columns[9].Width = 70;
            this.listView1.Columns[10].Width = 70;

            MapFromEntries(objects, routes, listView1);
        }
    
        
        
        
        
        public void MapFromEntries(GMapOverlay objects, GMapOverlay routes, ListView entries)
        {
            string vbdecimaldelimeter = ","; string Localdecimaldelimeter = ".";
            int idx = routes.Routes.Count + 1;
            objects.Markers.Clear();
            routes.Routes.Clear();
            double cummlat = 0; double cummlng = 0;
            double cummCntr = 0;

            
                double lt1 = Convert.ToDouble(entries.Items[0].SubItems[2].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));
                double lg1 = Convert.ToDouble(entries.Items[0].SubItems[3].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));
                double lt2 = Convert.ToDouble(entries.Items[entries.Items.Count-1].SubItems[2].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));
                double lg2 = Convert.ToDouble(entries.Items[entries.Items.Count - 1].SubItems[3].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));

                // objects.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng(lt1, lg1)));
                GMapMarkerImage ArrayMarkers = new GMapMarkerImage(new PointLatLng(lt1, lg1), output.Properties.Resources.start);
                ArrayMarkers.ToolTipText =  entries.Items[0].SubItems[1].Text;
                objects.Markers.Add(ArrayMarkers);
                
                GMapMarkerImage ArrayMarkers2 = new GMapMarkerImage(new PointLatLng(lt2, lg2), output.Properties.Resources.fin);
                ArrayMarkers2.ToolTipText =   entries.Items[entries.Items.Count-1].SubItems[1].Text;
                objects.Markers.Add(ArrayMarkers2);


                for (int i = 1; i < entries.Items.Count - 1;i++ )
                {
                    lt1 = Convert.ToDouble(entries.Items[i].SubItems[2].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));
                    lg1 = Convert.ToDouble(entries.Items[i].SubItems[3].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));

                    // objects.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng(lt1, lg1)));
                    ArrayMarkers = new GMapMarkerImage(new PointLatLng(lt1, lg1), output.Properties.Resources.cow);
                    ArrayMarkers.ToolTipText = entries.Items[i].SubItems[1].Text;
                    objects.Markers.Add(ArrayMarkers);

                }
                points = new List<PointLatLng>();
                for (int i = 0; i < entries.Items.Count;i++ )
                {
                    lt1 = Convert.ToDouble(entries.Items[i].SubItems[2].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));
                    lg1 = Convert.ToDouble(entries.Items[i].SubItems[3].Text.Replace(Localdecimaldelimeter, vbdecimaldelimeter));

                    
                    points.Add(new PointLatLng(lt1, lg1));


                    cummlat += lt1; cummlng += lg1;

                    cummCntr += 1;
                }
        
            //
             SqlCeConnection con=new SqlCeConnection(@"Data Source="+Application.StartupPath+@"\Routing.sdf");
            SqlCeCommand com = new SqlCeCommand("SELECT VALUE FROM  settings where setting='optimization'", con);
            con.Open();    
            SqlCeDataReader  rd=com.ExecuteReader();
            rd.Read();
            bool mindist = true; 
            if (rd[0].ToString() == "distance") mindist = true; else mindist = false;
                con.Close();
                IRouteService routeService = new RouteServiceClient();
                RouteRequest routeRequest = new RouteRequest();
                routeRequest.Credentials = new Credentials();
                routeRequest.Credentials.ApplicationId = "AjGVsXovYaKDEVcc_N83jVh-cT9d9M4wL-AGIR9mBl06DYDRGnC-miCxBtnDMxuH";

                routeRequest.Options = new RouteOptions();
                routeRequest.Options.RoutePathType = RoutePathType.Points;
              //  routeRequest.Options.RoutePathTypeSpecified = true;
                if (mindist) routeRequest.Options.Optimization = RouteOptimization.MinimizeDistance;
                else routeRequest.Options.Optimization = RouteOptimization.MinimizeTime;
                routeRequest.Options.Mode = TravelMode.Driving;

                Waypoint[] waypoints = new Waypoint[points.Count];
                for (int i = 0; i < points.Count; i++)
                {
                    waypoints[i] = PointToWaypoint( points[i]);
                }
            routeRequest.Waypoints = waypoints;


            //IRouteService r = new RouteServiceClient();
            //RouteResponse rsp= r.CalculateRoute(routeRequest);
            ////http://dev.virtualearth.net/REST/V1/Routes/Driving?wp.0=47.530072,-122.032904&wp.1=47.674912,-122.124045&avoid=minimizeTolls&key=AjGVsXovYaKDEVcc_N83jVh-cT9d9M4wL-AGIR9mBl06DYDRGnC-miCxBtnDMxuH

            //points = new List<PointLatLng>();
            //foreach (Location pnt in rsp.Result.RoutePath.Points){
            //        points.Add(new PointLatLng(pnt.Latitude, pnt.Longitude));
            //    }
                route = new GMapRoute(points, "route");
                route.Stroke.Color = getcolor(0);
                routes.Routes.Add(route);
                
            this.mymap.Position = new PointLatLng(cummlat / cummCntr, cummlng / cummCntr);
        }

        private Waypoint PointToWaypoint(PointLatLng pnt)
        {
            Waypoint waypoint = new Waypoint();
            //waypoint.Description = "LON " + Longitude.ToString() + "LAT " + Latitude.ToString();
            waypoint.Location = new Location();
            waypoint.Location.Latitude = pnt.Lat  ;
            waypoint.Location.Longitude = pnt.Lng;
             return waypoint;
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mymap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
         
            var outsucc = GeoCoderStatusCode.G_GEO_SUCCESS;
            var pp2 = GMapProviders.GoogleMap.GetPlacemark(item.Position, out outsucc);

            MessageBox.Show("Lattitude:" + item.Position.Lat + "," + "Longtitude:" + item.Position.Lng + (char)13 +
      "" + pp2.Address + (char)13 + pp2.AdministrativeAreaName + ", " + pp2.CountryName);         
    
       

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

    }
}
