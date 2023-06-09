using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlServerCe;
 
using System.IO;
using System.Linq;

using classes;
using distanceMatrix;

namespace tradeoff
{
    class Program2
    {
        static List<centroid> homes = new List<centroid>();
        static List<centroid> homes2 = new List<centroid>();
        static List<centroid> fins = new List<centroid>();
        static List<centroid> centroids = new List<centroid>();
        static List<cxy> points = new List<cxy>();
        
        static void Main(string[] args)
        {
            List<string> stringcoordinates = new List<string> 
                {   
                    "1,39.92108,22.548349",
                    "1,39.902568,22.5487269",
                    "1,39.919347,22.5900169",
                    "1,40.997883,22.570899",
                    "1,40.858933,24.7054439",
                    "1,39.931682,22.687889",
                    "1,40.802398,22.050693",
                    "1,40.993764,22.876291",
           
                    "1,40.793103,22.4114959",
                    "1,41.11887,25.405528",
                    "1,40.8488149,25.873185"
            };
        List<double> VolumeInCollPoint = new List<double> 
            {   
            10,
            8,
            10,
            20,
            15,
            10,
            11,
            16,
            10,
            15,
            11
            };

        List<string> homescoordinates = new List<string> 
                { "40.920452,22.640650" ,
                    "40.120193,21.640055",
                    "40.120193,22.640055",
                     "40.620255,22.640250"};
        List<double> homesUnitCost = new List<double> 
                { 100,
                    120,
                    120,
                     90};
        List<double> homesUnitCapacity = new List<double> 
                { 60,
                    50,
                    75,
                     60};
        List<double> truckTransCost = new List<double> 
                { 1,
                    1,
                      1,
                        1};
             List<string> finscoordinates = new List<string> 
            {  "40.120193,21.640055",
                "40.120193,21.640055",
                "40.120193,21.640055",
            "40.120193,21.640055"};


             int dataPoints = stringcoordinates.Count;
             string f;  cxy tempcxy; double tempx; double tempy;
             for (int i = 0; i < dataPoints; i++)
             {
                 f = stringcoordinates[i];
                 tempx = Convert.ToDouble(f.Split(new Char[] { ',' })[1].Replace(".", ","));
                 tempy = Convert.ToDouble(f.Split(new Char[] { ',' })[2].Replace(".", ","));
                 tempcxy = new classes.cxy();
                 tempcxy.x = tempx;
                 tempcxy.y = tempy;
                 tempcxy.cluster = Convert.ToInt16(f.Split(new Char[] { ',' })[0]);
                 tempcxy.ID = "point" + (i+1);
                 points.Add(tempcxy);

             }
                SqlCeConnection con = new SqlCeConnection(); 
                con.ConnectionString =@"Data Source=C:\Users\savvas.MIK3\Documents\Visual Studio 2010\Projects\CLUSTERmanyrequests\CLUSTERmanypoints\Routing.sdf ";
                con.Open();

                SqlCeCommand com = new SqlCeCommand("DELETE FROM  mapPoints", con);
                com.ExecuteNonQuery();
                com = new SqlCeCommand("DELETE FROM  mapHomes", con);
                com.ExecuteNonQuery();
                com = new SqlCeCommand("DELETE FROM  mapFins", con);
                com.ExecuteNonQuery();
                com = new SqlCeCommand("DELETE FROM  nxn", con);
                com.ExecuteNonQuery();
              
            
                for (int i = 0; i < dataPoints; i++) {
                        cxy p=points[i];
                        tempx = p.x;
                        tempy = p.y;
                     //   tempx = 0; tempy = 0;
                        com = new SqlCeCommand("INSERT INTO mapPoints (ID,x,y,volume) VALUES ('"+p.ID+"'," + tempx.ToString().Replace(",", ".") + "," + tempy.ToString().Replace(",", ".") + "," + VolumeInCollPoint[i] + ")", con); 
 
                        com.ExecuteNonQuery(); 
                         }
          
 
             centroid tempcentroid;
             for (int i = 0; i <= homescoordinates.Count - 1; i++)
             {
                 f = homescoordinates[i];
                 tempcentroid = new centroid();
                 tempcentroid.x = Convert.ToDouble(f.Split(new Char[] { ',' })[0].Replace(".", ","));
                 tempcentroid.y = Convert.ToDouble(f.Split(new Char[] { ',' })[1].Replace(".", ","));
                 tempcentroid.ID = "Home" + (i+1);
                 homes.Add(tempcentroid);
                 com = new SqlCeCommand("INSERT INTO mapHomes (ID,x,y,truckCost,truckCapacity,truckTransportCost) VALUES ('" + tempcentroid.ID + "'," + tempcentroid.x.ToString().Replace(",", ".") + "," + tempcentroid.y.ToString().Replace(",", ".") + "," + homesUnitCost[i].ToString().Replace(",", ".") + "," + homesUnitCapacity[i].ToString().Replace(",", ".") + "," + truckTransCost[i].ToString().Replace(",", ".") + ")", con);
                 com.ExecuteNonQuery();
                
                 f = finscoordinates[i];
                 tempcentroid = new centroid();
                 tempcentroid.x = Convert.ToDouble(f.Split(new Char[] { ',' })[0].Replace(".", ","));
                 tempcentroid.y = Convert.ToDouble(f.Split(new Char[] { ',' })[1].Replace(".", ","));
                 tempcentroid.ID = "Fin" + (i + 1);
                 fins.Add(tempcentroid);
                 com = new SqlCeCommand("INSERT INTO mapFins (ID,x,y) VALUES ('" + tempcentroid.ID + "'," + tempcentroid.x.ToString().Replace(",", ".") + "," + tempcentroid.y.ToString().Replace(",", ".") + ")", con);
                 
                 com.ExecuteNonQuery(); 
                       

             }
            
            //add ditances
            
            List<string>tempxy= new List<string>();
            foreach( centroid h in homes ){
                tempxy.Add(h.ID + "," + h.x.ToString().Replace(",", ".") + "," + h.y.ToString().Replace(",", "."));
            }
            foreach( cxy p in points){
                tempxy.Add(p.ID + "," + p.x.ToString().Replace(",", ".") + "," + p.y.ToString().Replace(",", "."));
            }
            foreach( centroid fn in fins){
                tempxy.Add(fn.ID + "," + fn.x.ToString().Replace(",", ".") + "," + fn.y.ToString().Replace(",", "."));
            }


            List<distanceEntry> distances = new List<distanceEntry>();
              distances=distanceMatrix.Matrix.EXPORTnxn(tempxy);
            foreach (distanceEntry d in distances)
            {
                com = new SqlCeCommand("INSERT INTO nxn (point1,point2,distance) VALUES ('"+ d.Point1+"','"+d.Point2+"',"+d.distance.ToString().Replace(",", ".")+")", con);
                com.ExecuteNonQuery(); 
            }   

            con.Dispose();
            con.Close(); 
        
        }//end insertDataIn Base Module
 
        
     
    }
}
    