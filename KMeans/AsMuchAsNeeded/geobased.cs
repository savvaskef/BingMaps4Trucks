using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using classes;
using System.Data.SqlServerCe;
using KMeans;
using TSP;
using Exhaust;
using classes;
using MapGUI;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Configuration;

 
namespace AsMuchAsNeeded
{
   public class geogbased
    {
        public static bool exhaustiveSearch = false;
        public static bool googleDistance = false;
        public static double pricePerLitre =2;
        public static void Main(string[] parames) { 
        }

       public static bool calc()
            { 
            double totalcapacity=0;double totalvolume=0;
            double remainingVolume = 0;
 
            //import data from sdf
            SqlCeConnection con = new SqlCeConnection();
            con.ConnectionString = @"Data Source="+"."+@"\Routing.sdf";;
            con.Open();
            
            
            SqlCeCommand com = new SqlCeCommand("Select ID,x,y,volume  from mapPoints", con);
            SqlCeDataReader     r = com.ExecuteReader();

            List<cxy> cxyPoints = new List<cxy>();
            List<double> Volume = new List<double>();
            
            while (r.Read())
            {     cxy cxypoint =new cxy();
                cxypoint.x=Convert.ToDouble(r["x"]);
                cxypoint.y=Convert.ToDouble(r["y"]);
                cxypoint.cluster=1;
                cxypoint.ID=r["ID"].ToString();
                 cxyPoints.Add (cxypoint);
                // or: rdr["EmployeeKey"];
                Volume.Add(Convert.ToDouble(r["volume"]));
                totalvolume+=Convert.ToDouble(r["volume"]);
                }
            r.Close();



            com = new SqlCeCommand("Select ID,x,y,truckCost,truckCapacity,truckTransportCost from mapHomes", con);
            r = com.ExecuteReader();

             List<cxy> cxyHomes= new List<cxy>();
             List<double> homesUnitCost = new List<double> ();
             List<double> homesUnitCapacity = new List<double> ();
             List<double> truckTransCost = new List<double>();


             while (r.Read())
             {
                 cxy cxypoint = new cxy();
                 cxypoint.y=Convert.ToDouble(r["y"]);
                 cxypoint.x=Convert.ToDouble(r["x"]);
                 cxypoint.ID = r["ID"].ToString();
                 cxyHomes.Add(cxypoint);
                 homesUnitCapacity.Add(Convert.ToDouble(r["truckCapacity"]));
                 homesUnitCost.Add(Convert.ToDouble(r["truckCost"]));
                 truckTransCost.Add(Convert.ToDouble(r["truckTransportCost"]));
                
                 totalcapacity+=Convert.ToDouble(r["truckCapacity"]);

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

             List<string> tempxy = new List<string>();
             foreach (cxy p in cxyHomes)
             {
                 tempxy.Add(p.ID + "," + p.x.ToString().Replace(",", ".") + "," + p.y.ToString().Replace(",", "."));
             }
             foreach (cxy p in cxyPoints)
             {
                 tempxy.Add(p.ID + "," + p.x.ToString().Replace(",", ".") + "," + p.y.ToString().Replace(",", "."));
             }
             foreach (cxy p in cxyFins)
             {
                 tempxy.Add(p.ID + "," + p.x.ToString().Replace(",", ".") + "," + p.y.ToString().Replace(",", "."));
             }

            //distanceMatrix.Matrix.constr = @"Data Source=" + Application.StartupPath + @"\Routing.sdf";
            List<distanceEntry> subDists=distanceMatrix.Matrix.IMPORTnxn(tempxy);

            var points = new List<Point>();
            Point tempXY; double tempx; double tempy;

            for (int i = 0; i < cxyPoints.Count; i++)
            {
                cxy f = cxyPoints[i];

                tempx = Convert.ToDouble(f.x);
                tempy = Convert.ToDouble(f.y);
                tempXY = new Point(tempx, tempy);
                points.Add(tempXY);

            }




            // test combinations of n starts/trucks for k clusters 
            remainingVolume = totalvolume;
            int k = 2;
            List<PlausibleSolution> solMonger = new List<classes.PlausibleSolution>();
            while (k <= cxyHomes.Count)
            {
                int totaldata = points.Count;
                List<classes.Point>[] pCol = new KMeansWorker().Cluster(k, points);

                Tuple<List<cxy>, List<centroid>> pClus = Pcol2Pclust(pCol[0], pCol[1]);
                List<cxy> data = pClus.Item1; List<centroid> centroids = pClus.Item2;
 
                List<int> pathCompleted = new List<int>();
                List<List<int>> combs = new List<List<int>>();
                ExhaustiveSearch.CombinationsRemainingtail(centroids.Count, cxyHomes.Count, ref pathCompleted, ref combs);
                //group data by cluster
                double[,] newdim;
                var tabulardata = new List<double[,]>();
                var dimentions = new List<int>();
                var innerList = new List<int>();
                var listlike = new List<List<int>>();
                cxy collpoint = new cxy(); int j;
                for (int i = 1; i <= k; i++)
                {
                    dimentions.Add(0);
                    innerList = new List<int>();
                    for (j = 0; j < totaldata; j++)
                    {
                        collpoint = data[j];
                        if (collpoint.cluster == i)
                        {
                            dimentions[i - 1]++;
                            innerList.Add(j);
                        }//
                    }
                    listlike.Add(innerList);

                    }
                 //generate Fixed(init-truck) costs and variable(gasoline) Costs+ 
                //find trucks combinations
                foreach (List<int> TrucksSelected in combs) {
                    int addedidx= -1;
                    List<double> capacity = new List<double>();
                    List<double> addedvolume = new List<double>();
                    List<double> addeddistance = new List<double>();
                    List<double> fixedcost = new List<double>();

                    List<List<double>> alltrucksallvol = new List<List<double>>();
                    List<List<string>> traxStringcoordinates = new List<List<string>>();
                    foreach (  int t in TrucksSelected ) {
    List<string> pointstrings = new List<string>(); 
                        cxy home = cxyHomes[t-1];
                        pointstrings.Add("Home" + t);
                        fixedcost.Add(homesUnitCost[t-1]);
                        capacity.Add(homesUnitCapacity[t-1]);
                        addedvolume.Add(0);
                        addeddistance.Add(0);
                        addedidx++;
                        List<double> allvoll = new List<double>();
                        List<int> tdata = listlike[addedidx];
                        foreach (int d in tdata)
                        {
                            pointstrings.Add("point"+(1+d).ToString() );
                            addedvolume[addedvolume.Count-1]+=Volume[d];
                            allvoll.Add(Volume [d]);
                        }

                        cxy Fin = cxyFins[t-1];
                          pointstrings.Add("Fin" + t);
                          traxStringcoordinates.Add(pointstrings);
                    alltrucksallvol.Add (allvoll);
                    }
                
                if (CapacityConstraint(addedvolume,capacity)){
                    PlausibleSolution sol = new PlausibleSolution();
                    sol.trucksChosen = TrucksSelected;
                    sol.addeddistance = addeddistance;
                    sol.PointsVolume=alltrucksallvol;
                    sol.addedvolume = addedvolume;
                    sol.capacity = capacity;
                    sol.fixedcost = fixedcost;
                    sol.Truckroute = traxStringcoordinates;
                    sol.order = new List<List<int>>();// (shortestpath);
                    solMonger.Add(sol);
                // add shortest path as distance 
                    for (int iii = 0; iii < addeddistance.Count; iii++)
                    {
                   double[,] forEachRoute = distanceMatrix.Matrix.submatrix(subDists, traxStringcoordinates[iii], traxStringcoordinates[iii]);
                   List<int> shortestpath = new List<int>();
                   double shortestDistance;
                        List<List<int>> paths = new List<List<int>>();
                    
                        if (!exhaustiveSearch)
                    {

                    //    paths.Add(InitAnneal(forEachRoute));
                        Tuple<double, List<int>, List<double>> SP = InitAnneal(forEachRoute);
                                shortestDistance = SP.Item1;
                              shortestpath = SP.Item2;
                              sol.PointsDistance.Add(SP.Item3);
                        
                    }
                    else
                    {
                        int cnt2;
                         
                        { 
                            cnt2 = sol.Truckroute[iii].Count;
                    //       paths.Add(InitExhaustive(cnt2, forEachRoute));
                            Tuple<double, List<int>,List<double>> SP = InitExhaustive(cnt2, forEachRoute);
                            shortestDistance = SP.Item1;
                            shortestpath = SP.Item2;
                            sol.PointsDistance.Add(SP.Item3);
                        }
                    }
                       for(int jj=0;jj<sol.PointsDistance[sol.PointsDistance.Count-1].Count;jj++){
                         sol.PointsDistance[sol.PointsDistance.Count-1][jj]=Convert.ToDouble(sol.PointsDistance[sol.PointsDistance.Count-1][jj])/1000;
                         }

                    sol.addeddistance[iii] = shortestDistance;
                    sol.order.Add(shortestpath);
                    
                    
                    }
                }
                }

                k++;//more Clusters / new division
            }//end while


            //Find mincost solution from selected solutions(that pass the capacity constraint)
            PlausibleSolution tempsol=new PlausibleSolution();
            if (solMonger.Count == 0) { 
            return false;// Messagebox.Show("No solutions collected");
            }   
            double cost=0;double mincost=Math.Pow(10,10);
            foreach (PlausibleSolution sol in solMonger) {
                cost = 0;
                foreach (double fc in sol.fixedcost)
                    cost += fc;
                int idx=0;
                foreach (double dst in sol.addeddistance)
                {
                    cost += dst *  truckTransCost[sol.trucksChosen[idx]-1];
                    idx++;
                }
                if (cost<mincost){
                    mincost = cost;
                     tempsol=new PlausibleSolution();
                    tempsol = sol;
                }

            }
            // order collection points
            List<string> route = new List<string>();
            List<List<String>> routes = new List<List<string>>();
           
              
        
           for (int i = 0; i < tempsol.order.Count;i++ )
            {
                List<int> ord = tempsol.order[i];
                route = new List<string>();
                foreach (int j in ord){
                    route.Add(tempsol.Truckroute[i][j]);
                }
                tempsol.Truckroute[i]=route;
            }

            //get fixed and variable(TRANSPORT) costs into play 
            for (int i = 0; i < tempsol.fixedcost.Count; i++)
            {

                List<double> costs = new List<double>();
                costs.Add(tempsol.fixedcost[i]);
                for (int j = 0; j < tempsol.PointsDistance[i].Count; j++)
                {
                    double dst = tempsol.PointsDistance[i][j];
                    costs.Add(dst *  truckTransCost[tempsol.trucksChosen[i] - 1]);

                }
                tempsol.PointsCost.Add(costs);
            }

            // align all  points collections
            for (int i = 0; i < tempsol.fixedcost.Count; i++)
            {
                tempsol.PointsVolume[i].Insert(0, 0);
                tempsol.PointsVolume[i].Add (0);
                tempsol.PointsDistance[i].Insert(0, 0);
                

            }

            //calculate cumulative volume,distance,cost
            for (int i = 0; i < tempsol.fixedcost.Count; i++)
            {
                List<double> CummVol = new List<double>();
                List<double> PercVol = new List<double>();
                List<double> CummDist = new List<double>();
                List<double> CummCost = new List<double>();
                double cvl = 0; double cvd = 0; double cvc = 0; double pvl = 0;
                for (int j = 0; j < tempsol.PointsVolume[i].Count;j++ )
                {

                    cvl += tempsol.PointsVolume[i][j];
                    cvd += tempsol.PointsDistance[i][j];
                    cvc += tempsol.PointsCost[i][j];
                   pvl = cvl / homesUnitCapacity[tempsol.trucksChosen[i]-1];
                    CummVol.Add(cvl);
                    PercVol.Add(pvl);
                    CummDist.Add(cvd);
                    CummCost.Add(cvc);

                }
                tempsol.PercPointsVolume.Add(PercVol);
                tempsol.CummPointsVolume.Add(CummVol);
                tempsol.CummPointsDistance.Add(CummDist);
                tempsol.CummPointsCost.Add(CummCost);

            }
            
            //align x,y,addresseds b4 export 2 db
            List<classes.cxy>xyColl=new List<classes.cxy>();
            for (int i = 0; i < tempsol.Truckroute.Count; i++)
            {
                List<string> rt = tempsol.Truckroute[i];
                List<string> AddressFromcxyCoords = new List<string>();
                List<double> LattitudeFromcxyCoords = new List<double>();
                List<double> LongtitudeFromcxyCoords = new List<double>();

                for (int j = 0; j < rt.Count; j++)
                {
                    string xyPoint = rt[j];

                    if (xyPoint.Substring(0, 4) == "Home") xyColl = cxyHomes;
                    if (xyPoint.Substring(0, 4) == "poin") xyColl = cxyPoints;
                    if (xyPoint.Substring(0, 3) == "Fin") xyColl = cxyFins;
                    var cxyCoords = GetPoint(xyPoint, xyColl);
                    LattitudeFromcxyCoords.Add(cxyCoords.x);
                    LongtitudeFromcxyCoords.Add(cxyCoords.y);
            
                //PointLatLng p1 = new PointLatLng(cxyCoords.x, cxyCoords.y);
                //var outsucc = GeoCoderStatusCode.G_GEO_SUCCESS;
                //var pp2 = GMapProviders.GoogleMap.GetPlacemark(p1, out outsucc);
                //AddressFromcxyCoords.Add(pp2.Address + ", " + pp2.AdministrativeAreaName + ", " + pp2.CountryName);

                }

                tempsol.lattitudes.Add(LattitudeFromcxyCoords);
                tempsol.longtitudes.Add(LongtitudeFromcxyCoords);
                //tempsol.addresses.Add(AddressFromcxyCoords);

            }           
            //export data
             con.Open();
            com = new SqlCeCommand("delete  from results",con);
            com.ExecuteNonQuery(); 
            for (int i = 0; i < tempsol.Truckroute.Count; i++)
            {
                List<string> rt = tempsol.Truckroute[i];
                 for (int j = 0; j < rt.Count; j++)
                {
            com = new SqlCeCommand("INSERT INTO results (routeID,pointID,lattitude,longtitude,distance,cummdistance,volume,cummvolume,percvolume,cost,cummcost) VALUES ('"
                + tempsol.trucksChosen[i]+ "','"
                + tempsol.Truckroute[i][j] + "',"
                + tempsol.lattitudes[i][j].ToString().Replace(",", ".") + ","
                + tempsol.longtitudes[i][j].ToString().Replace(",", ".") + ","
                + tempsol.PointsDistance[i][j].ToString().Replace(",", ".") + ","
                + tempsol.CummPointsDistance[i][j].ToString().Replace(",", ".") + ","
                + tempsol.PointsVolume[i][j].ToString().Replace(",", ".") + ","
                + tempsol.CummPointsVolume[i][j].ToString().Replace(",", ".") + ","
                + tempsol.PercPointsVolume[i][j].ToString().Replace(",", ".") + ","
                + tempsol.PointsCost[i][j].ToString().Replace(",", ".") + ","
                + tempsol.CummPointsCost[i][j].ToString().Replace(",", ".") + ")",
                con);
            com.ExecuteNonQuery();
                }
            }
                    con.Close();


            // embed Chosen solution on form and load it
            //output MyOutputForm = new output();
            //MyOutputForm.ChosenSolution = tempsol;
            //MyOutputForm.ShowDialog();
      //      int ridx = 0;
      //      foreach (List<string> sroute in routes)
      //      {   cxy house=GetPoint(sroute[0],cxyHomes);
      //          cxy terma=GetPoint(sroute[sroute.Count-1],cxyFins);
      //          sroute.RemoveAt(0);sroute.RemoveAt(sroute.Count-1);
      //JavaConqa.JavaBuilder( sroute, house, cxyPoints, terma);
           
      //          ridx++;
      //      }
                    return true;
        }//endsmain

        public static cxy GetPoint(string withID, List<cxy> FromPoints)
        {
            cxy dummy = new cxy();

            foreach (cxy p in FromPoints)
            {
                if (p.ID.ToUpper() == withID.ToUpper())
                {
                    dummy = p;
                    break;
                }
            }

            return dummy;
        }

        public static Tuple <double ,List<int>,List<double>> InitExhaustive(int cnt, double[,] distances)
        {

            List<int> pathCompleted = new List<int>();
            List<List<int>> combs = new List<List<int>>();

            ExhaustiveSearch.Combinations(cnt - 3, ref pathCompleted, ref combs);
            ExhaustiveSearch.GetPath(cnt -1, combs, distances);


            string path = "";

            for (int i = 0; i < Exhaust.ExhaustiveSearch.order.Count - 1; i++)
            {
                path += Exhaust.ExhaustiveSearch.order[i].ToString() + "->";
            }
            path += Exhaust.ExhaustiveSearch.order[Exhaust.ExhaustiveSearch.order.Count - 1];

            Console.WriteLine("Shortest Route: " + path);

            Console.WriteLine("The shortest distance is: " + Exhaust.ExhaustiveSearch.distance.ToString());

     //       return Exhaust.ExhaustiveSearch.order;
          
            return new Tuple<double, List<int>, List<double>>(Exhaust.ExhaustiveSearch.distance, Exhaust.ExhaustiveSearch.order, Exhaust.ExhaustiveSearch.AllDistances);

        }

        public static Tuple<double, List<int>, List<double>> InitAnneal(double[,] distances)
        {

            TravellingSalesmanProblem problem = new TravellingSalesmanProblem();
            problem.FilePath = "cities.txt";
            problem.Anneal(distances);

            string path = "";
            for (int i = 0; i < problem.CitiesOrder.Count - 1; i++)
            {
                path += problem.CitiesOrder[i] + ">";
            }
            path += problem.CitiesOrder[problem.CitiesOrder.Count - 1];

            Console.WriteLine("Shortest Route: " + path);

            Console.WriteLine("The shortest distance is: " + problem.ShortestDistance.ToString());
         //   return problem.CitiesOrder;
            return new Tuple<double, List<int>, List<double>>(problem.ShortestDistance, problem.CitiesOrder,problem.AllDistances);

        }



        static bool CapacityConstraint(List<double> Less, List<double> More) {
            bool dummy = true;
            for (int i = 0; i < Less.Count; i++) {
                if (Less[i] >= More[i])
                {
                    dummy = false;
                    break;
                }
            }
            return dummy;

        }
        static Tuple<List<cxy>, List<centroid>> Pcol2Pclust(List<classes.Point> Points, List<classes.Point> Clusters)
        {
            bool found; bool foundColor;
            Dictionary<string, int> colorselector = new Dictionary<string, int>();
            cxy tempcxy; centroid tempClust;
            List<cxy> mdata = new List<cxy>();
            List<centroid> mcentroids = new List<centroid>();
            foreach (classes.Point p in Clusters)
            {
                found = false;
                foreach (string clr in colorselector.Keys)
                {
                    if (clr == p.Color.Name) found = true;

                }
                if (!found) colorselector.Add(p.Color.Name, colorselector.Count + 1);

            }
            foreach (classes.Point p in Points)
            {
                tempcxy = new cxy();
                tempcxy.cluster = colorselector[p.Color.Name];
                tempcxy.x = p.X;
                tempcxy.y = p.Y;
                mdata.Add(tempcxy);
            }

            foreach (string clr in colorselector.Keys)
            {
                foreach (classes.Point p in Clusters)
                {

                    if (clr == p.Color.Name)
                    {
                        tempClust = new centroid();
                        tempClust.x = p.X;
                        tempClust.y = p.Y;
                        mcentroids.Add(tempClust);
                        break;
                    }
                }
            }
            return new Tuple<List<cxy>, List<centroid>>(mdata, mcentroids);

        }



    }}
