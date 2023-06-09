using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using classes;
using System.Data.SqlServerCe;
using Exhaust;
using distanceMatrix;
using MapGUI;


namespace marginalfunc
{
    class TrackRoutes
    {
        static bool tradeEnabled = true;
        static void Main(string[] args)
        {
            double totalcapacity = 0; double totalvolume = 0;
            double remainingVolume = 0;

            //connect to database from where classes are populated
            SqlCeConnection con = new SqlCeConnection();
            con.ConnectionString = @"Data Source=C:\Users\savvas.MIK3\Documents\Visual Studio 2010\Projects\CLUSTERmanyrequests\CLUSTERmanypoints\Routing.sdf ";
            con.Open();


            SqlCeCommand com = new SqlCeCommand("Select ID,x,y,volume  from mapPoints", con);
            SqlCeDataReader r = com.ExecuteReader();

            List<cxy> cxyPoints = new List<cxy>();
            List<double> Volume = new List<double>();

            while (r.Read())
            {
                cxy cxypoint = new cxy();
                cxypoint.x = Convert.ToDouble(r["x"]);
                cxypoint.y = Convert.ToDouble(r["y"]);
                cxypoint.cluster = 1;
                cxypoint.ID = r["ID"].ToString();
                cxyPoints.Add(cxypoint);
                // or: rdr["EmployeeKey"];
                Volume.Add(Convert.ToDouble(r["volume"]));
                totalvolume += Convert.ToDouble(r["volume"]);
            }
            r.Close();



            com = new SqlCeCommand("Select ID,x,y,truckCost,truckCapacity from mapHomes", con);
            r = com.ExecuteReader();

            List<cxy> cxyHomes = new List<cxy>();
            List<double> homesUnitCost = new List<double>();
            List<double> homesUnitCapacity = new List<double>();


            while (r.Read())
            {
                cxy cxypoint = new cxy();
                cxypoint.y = Convert.ToDouble(r["y"]);
                cxypoint.x = Convert.ToDouble(r["x"]);
                cxypoint.ID = r["ID"].ToString();
                cxyHomes.Add(cxypoint);
                homesUnitCapacity.Add(Convert.ToDouble(r["truckCapacity"]));
                homesUnitCost.Add(Convert.ToDouble(r["truckCost"]));
                totalcapacity += Convert.ToDouble(r["truckCapacity"]);

            }
            r.Close();

            com = new SqlCeCommand("Select ID,x,y from mapFins", con);
            r = com.ExecuteReader();

            List<cxy> cxyFins = new List<cxy>();
            while (r.Read())
            {
                cxy cxypoint = new cxy();
                cxypoint.y = Convert.ToDouble(r["y"]);
                cxypoint.x = Convert.ToDouble(r["x"]);
                cxypoint.ID = r["ID"].ToString();
                cxyFins.Add(cxypoint);

            }
            r.Close();
            con.Close();

            List<string> matrixPoint = new List<string>();

            List<string> pointsStrings = new List<string>();
            List<string> pointsStringsclone = new List<string>();

            foreach (cxy p in cxyHomes)
            {
                matrixPoint.Add(p.ID + "," + p.x.ToString().Replace(",", ".") + "," + p.y.ToString().Replace(",", "."));
            }
            foreach (cxy p in cxyPoints)
            {
                matrixPoint.Add(p.ID + "," + p.x.ToString().Replace(",", ".") + "," + p.y.ToString().Replace(",", "."));
                pointsStrings.Add(p.ID);
                pointsStringsclone.Add(p.ID);
            }

            foreach (cxy p in cxyFins)
            {
                matrixPoint.Add(p.ID + "," + p.x.ToString().Replace(",", ".") + "," + p.y.ToString().Replace(",", "."));
            }

            List<distanceEntry> subDists = distanceMatrix.Matrix.IMPORTnxn(matrixPoint);
           
            
            
            //   *** find truck combination that produce the less cost
            List<int> remaining = new List<int>();
            List<int> constructed = new List<int>();
            List<List<int>> output = new List<List<int>>();
            List<List<int>> output2 = new List<List<int>>();
            for (int i = 0; i < cxyHomes.Count; i++)
                remaining.Add(i); int kmax = cxyHomes.Count - 1;

            Exhaust.ExhaustiveSearch.nthLengthCombo(remaining, constructed, 1, cxyHomes.Count,ref  output);
            Exhaust.ExhaustiveSearch.appearOnce(output, ref output2);
            
            List<int> chosenHomes=OptimumCostTrucks(output2,homesUnitCapacity,totalvolume,homesUnitCost);
            
            
           //loop for the combination of homes(tracks with various costs) 
           double[,] marginDists ;
           List<List<string>> routes = new List<List<string>>();
           for (int hidx = 0; hidx < chosenHomes.Count; hidx++)
           
           {
               List<string> route = new List<string>();
                cxy home = cxyHomes[chosenHomes[hidx]];
                int idx=0; string pointtext="";
                double truckLoad = 0; double truckCapacity = homesUnitCapacity[hidx];
               
                double minval = Math.Pow(10, 10); double funcval;
                string prevtext = "Home" + (hidx + 1);
                //  *** from point to point with criterion min of f(x) :here =(distance)
                while (truckLoad < truckCapacity)
                 {
                    minval = Math.Pow(10, 10);
                    List<string> PrevString = new List<string>();
                    PrevString.Add(prevtext);
                    marginDists = distanceMatrix.Matrix.submatrix(subDists, PrevString, pointsStrings);

                    //  *** from point to point with criterion min of f(x) :here =(distance)   
                    for (int pidx = 0; pidx < marginDists.Length;pidx++ )
                    {
                        funcval = marginDists[0, pidx];
                        if (funcval<minval){
                            minval = funcval;
                            prevtext = pointsStrings[pidx];
                            idx = Convert.ToInt16(prevtext.Substring(5)) - 1;
                        }

                    }
                    truckLoad += Volume[idx];
                    if (truckLoad < truckCapacity){
                    pointsStrings.Remove(prevtext);
                    route.Add(prevtext);
                    }
                }

                routes.Add (route); 
           }
           //Find centres for tradeoff & remaining 
           List<cxy> centres = new List<cxy>();
            centres=calculatedCenters(routes,cxyPoints);
              
            //allocate remaining
            cxy tempcxy=new cxy();
            int leftoversGroup = 0;
            while (pointsStrings.Count>0) {
               int hidx=0;
               string pointString=pointsStrings[0];
               double mindist=Math.Pow(10,10);
               tempcxy = GetPoint(pointString,cxyPoints);
               foreach (cxy centre in centres) {

                   double dist = distanceMatrix.Matrix.dist(tempcxy.x, tempcxy.y, centre.x, centre.y);
                   if (dist<mindist ){
                       mindist=dist;
                         leftoversGroup=hidx; 

                   }
               hidx=hidx+1;
               }
               routes[leftoversGroup].Add(pointString);
               pointsStrings.Remove(pointString);

           }

            //tradeoff points that are closer to  routecenters other than allocated
            if (tradeEnabled){
                 tempcxy = new cxy();
                while (pointsStringsclone.Count > 0)
                {
                    int hidx = 0;
                    string pointString = pointsStringsclone[0];
                    double mindist = Math.Pow(10, 10);
                    tempcxy = GetPoint(pointString, cxyPoints);
                    foreach (cxy centre in centres)
                    {

                        double dist = distanceMatrix.Matrix.dist(tempcxy.x, tempcxy.y, centre.x, centre.y);
                        if (dist < mindist)
                        {
                            mindist = dist;
                              leftoversGroup = hidx;

                        }
                        hidx = hidx + 1;
                    }
                    foreach (List<string> pstrList in routes)
                    {
                        int startcount = pstrList.Count;
                        for (int idx=0;idx<startcount;idx++)
                        {
                           string pstr = pstrList[idx];
                           if (pstr == pointString) pstrList.Remove(pstr);
                           if (pstr == pointString) idx=idx-1;
                           if (pstr == pointString) startcount=startcount-1;
                    
                        }
                    }
                    routes[leftoversGroup].Add(pointString);
                    pointsStringsclone.Remove(pointString);

                }
            }
            int ridx=0;
            foreach (List<string> route in routes)
            {
                JavaConqa.JavaBuilder(route, cxyHomes[chosenHomes[ridx]], cxyPoints, cxyFins[chosenHomes[ridx]]);
                ridx++;
                }
        }//endsmain

        public static List<cxy> calculatedCenters(List<List<string>>routes,List<cxy>cxyPoints) {
         cxy tempcxy=new cxy();
         List<cxy> centres = new List<cxy>();
        
        for (int hidx = 0; hidx < routes.Count; hidx++)
               {   double Psumx=0;double Psumy=0;int Pcounter=0;
                   List<string> truckroute = routes[hidx];
                   foreach (string Pstring in truckroute) {
                       tempcxy = GetPoint(Pstring, cxyPoints);
                       Psumx += tempcxy.x; Psumy += tempcxy.y; Pcounter += 1;
                   }
                   tempcxy = new cxy();
                     tempcxy.x = Psumx / Pcounter;
                     tempcxy.y = Psumy / Pcounter;
                   centres.Add(tempcxy);

               }
        return centres;
}
        public static cxy GetPoint(string withID,List<cxy> FromPoints) {
            cxy dummy= new cxy();

            foreach (cxy p in FromPoints) {
                if (p.ID.ToUpper() == withID.ToUpper()) {
                    dummy = p;
                    break;
                }
            }

            return dummy;
        }
        public static List<int> OptimumCostTrucks(List<List<int>> combos, List<double> homesUnitCapacity, double Volume, List<double> homesUnitCost)
        {   
            List<int> dummy = new List<int>();
                 double minCost = Math.Pow(10, 10);
           foreach (List<int> c in combos) {
                double cumcapacity = 0; double cumcost = 0;
                for (int i = 0; i < c.Count; i++) {
                   int  idx = c[i];
                    cumcapacity += homesUnitCapacity[idx];
                    cumcost += homesUnitCost[idx];

                }
                if (cumcapacity>Volume){
                    if (minCost>cumcost){
                        minCost=cumcost;
                        dummy=c;
                    }
                } 
           }
            
            return dummy;
        }
 
    }
}
