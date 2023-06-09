using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using KMeans;
using TSP;
using Exhaust;
using distanceMatrix;
 
namespace program
{   
    public static class starter
    {   static List<centroid> homes = new List<centroid>();
    static List<centroid> homes2 = new List<centroid>();
    static List<centroid> fins = new List<centroid>();
        static List<centroid> centroids = new List<centroid>();

        public static bool exhaustiveSearch = true;
        public static bool googleDistance= false;
       
        
        public static void Main() { 
             var kmeansWorker = new KMeansWorker();
             int clusters = 3;
             var points = new List<KMeans.Point>();
           
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
                    "1,40.8488149,25.873185",


                };

                 List<string> homescoordinates = new List<string> 
                { "40.920452,22.640650" ,
                    "40.120193,21.640055",
                    "40.120193,22.640055",
                     "40.620255,22.640250"};


                 List<string> finscoordinates = new List<string> 
                {  "40.120193,21.640055",
                    "40.120193,21.640055",
                    "40.120193,21.640055",
                    "40.120193,21.640055"};


                 int dataPoints = stringcoordinates.Count;
                 string f; KMeans.Point tempxy; double tempx; double tempy;
                 for (int i = 0; i < dataPoints; i++)
                 {
                     f = stringcoordinates[i];

                     tempx = Convert.ToDouble(f.Split(new Char[] { ',' })[1].Replace(".", ","));
                     tempy = Convert.ToDouble(f.Split(new Char[] { ',' })[2].Replace(".", ","));
                     tempxy = new KMeans.Point(tempx, tempy);
                     points.Add(tempxy);

                 }
                 centroid tempcentroid;
                 for (int i = 0; i <= homescoordinates.Count - 1; i++)
                 {
                     f = homescoordinates[i];
                     tempcentroid = new centroid();
                     tempcentroid.x = Convert.ToDouble(f.Split(new Char[] { ',' })[0].Replace(".", ","));
                     tempcentroid.y = Convert.ToDouble(f.Split(new Char[] { ',' })[1].Replace(".", ","));
                     homes.Add(tempcentroid);
                     f = finscoordinates[i];
                     tempcentroid = new centroid();
                     tempcentroid.x = Convert.ToDouble(f.Split(new Char[] { ',' })[0].Replace(".", ","));
                     tempcentroid.y = Convert.ToDouble(f.Split(new Char[] { ',' })[1].Replace(".", ","));
                     fins.Add(tempcentroid);


                 }


            // find clusters for all points
            int totaldata = points.Count;
             List<KMeans.Point>[] pCol = kmeansWorker.Cluster(clusters, points);

            Tuple<List<cxy>, List<centroid>> pClus = Pcol2Pclust(pCol[0], pCol[1]);
            List<cxy>data = pClus.Item1; List<centroid>centroids = pClus.Item2;
            int k = clusters;
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
            
            // find homes that match the clusters

              List<string> GlobalHomes = new List<string>();
              int kidx = 0;
              string distanzdummy = "";
              homes = findclosesthomes(homes, centroids);
              foreach (centroid c in homes) { 
              distanzdummy = (kidx + 1) + "," + c.x.ToString().Replace(",", ".") + "," + c.y.ToString().Replace(",", ".");
              GlobalHomes.Add(distanzdummy.ToString());
              kidx++;
              }
            
           
//calculate distances

              for (j = 0; j < k; j++)
              {
                  int cnt = dimentions[j];
                  newdim = new double[cnt + 2, cnt + 2];
                  tabulardata.Add(newdim);
                  for (int ii = 0; ii < cnt + 2; ii++)
                  {
                      for (int jj = 0; jj < cnt + 2; jj++)
                      {

                             var startfin = false;
                             double point1x = 0; double point1y = 0;
                             if (ii == 0)
                             {
                                 startfin = true;
                                 point1x = homes[j].x;
                                 point1y = homes[j].y;
                             }
                             if (ii == cnt + 1)
                             {
                                 startfin = true;
                                 point1x = fins[0].x;
                                 point1y = fins[0].y;
                             }
                             if (startfin != true)
                             {
                                 point1x = data[listlike[j][ii - 1]].x;
                                 point1y = data[listlike[j][ii - 1]].y;
                             }

                             startfin = false;
                             double point2x = 0; double point2y = 0;
                             if (jj == 0)
                             {
                                 startfin = true;
                                 point2x = homes[j].x;
                                 point2y = homes[j].y;
                             }
                             if (jj == cnt + 1)
                             {
                                 startfin = true;
                                 point2x = fins[j].x;
                                 point2y = fins[j].y;
                             }
                             if (startfin != true)
                             {
                                 point2x = data[listlike[j][jj - 1]].x;
                                 point2y = data[listlike[j][jj - 1]].y;
                             }


                             if (googleDistance)
                             tabulardata[j][ii, jj] =distanceMatrix.Matrix.googledist(point1x, point1y, point2x, point2y,"distance");
                             else
                             tabulardata[j][ii, jj] = dist(point1x, point1y, point2x, point2y);

                         }
                     }
                 

             }
             //******************************************************************************
            List<List<int>>paths= new List<List<int>>();
            if (!exhaustiveSearch)
             {
                 for (int i = 0; i < centroids.Count; i++)

                   paths.Add(InitAnneal(tabulardata[i]));

             }
             else
             {
                 int cnt2;
                 for (int i = 0; i < centroids.Count; i++)
                 {
                     cnt2 = dimentions[i];
                     paths.Add(InitExhaustive(cnt2, tabulardata[i]));
                 }
             }
            List<string> GlobalCentroids = new List<string>();
            List<string> GlobalPoints = new List<string>();
            string dummy = "";
            for (int i = 0; i < k; i++)
            {

                List<int> order = paths[i];

                foreach (int jj in order)
                {


                    if (jj == 0) { dummy = GlobalHomes[i]; }
                    if (jj == order.Count - 1) { dummy = (i + 1) + "," + finscoordinates[0]; }
                    if ((jj > 0) && (jj < order.Count - 1))
                    {
                        dummy = stringcoordinates[listlike[i][jj-1]];
                        dummy = (+1 + i) + dummy.Substring(dummy.IndexOf(","));  
                    }

                    GlobalPoints.Add(dummy);
                }

                GlobalCentroids.Add((i + 1) + "," + centroids[i].x.ToString()  + "," + centroids[i].y.ToString() );
            }           
            
         }

        static List<centroid> findclosesthomes(List<centroid> starts, List<centroid> centres)
        { 
          List<int> pathCompleted = new List<int>();
          List<List<int>> combs = new List<List<int>>();
          ExhaustiveSearch.CombinationsRemainingtail(centres.Count ,starts.Count, ref pathCompleted, ref combs);

          List<int> combo= new List<int>();
           int distanzidx = 0; int kidx = 0; int selectedidx = 0;
           string distanzdummy = "";
           distanzidx = 0;  
           double mindistanz = Math.Pow(10, 10);
           double distanz=0; double sumdistanz=0; 
            
            foreach (List<int> c in combs) {
              sumdistanz = 0; 
              for(int i=0;i<c.Count;i++){

                  centroid h = starts[c[i]-1];
                  centroid r = centres[i];

                  if (googleDistance)
                      sumdistanz += distanceMatrix.Matrix.googledist(r.x, r.y, h.x, h.y, "distance");
                  else
                      sumdistanz += dist(r.x, r.y, h.x, h.y);
                  }

              if (sumdistanz < mindistanz)
              {
                  combo = c;
                  mindistanz = sumdistanz;
              }
            }
            List<centroid> houses = new List<centroid>();
            for (int i = 0; i < centres.Count; i++) { 
                houses.Add(starts[combo[i]-1]);
            
            }
            return houses;
        }


        static List<centroid> cloneCentroid(List<centroid> source)
        {
            var dummylist = new List<centroid>();
            centroid tempxy;
            foreach (centroid c in source)
            {
                tempxy = new centroid();
                tempxy.x = c.x; tempxy.y = c.y;
                dummylist.Add(tempxy);
            }
            return dummylist;
        }
    

        static Tuple<List<cxy>, List<centroid>> Pcol2Pclust(List<KMeans.Point> Points, List<KMeans.Point> Clusters)
        {
            bool found; bool foundColor;
            Dictionary<string, int> colorselector = new Dictionary<string, int>();
            cxy tempcxy; centroid tempClust;
            List<cxy> mdata = new List<cxy>();
            List<centroid> mcentroids = new List<centroid>();
            foreach (KMeans.Point p in Clusters) {
                found = false;
                foreach (string clr in colorselector.Keys) {
                    if (clr == p.Color.Name) found = true;

                }
                if (!found) colorselector.Add(p.Color.Name, colorselector.Count + 1);
 
            }
            foreach (KMeans.Point p in Points) {
                tempcxy = new cxy();
                tempcxy.cluster = colorselector[p.Color.Name];
                tempcxy.x = p.X;
                tempcxy.y = p.Y;
                mdata.Add(tempcxy);
            }
          
            foreach (string clr in colorselector.Keys){
            foreach (KMeans.Point p in Clusters) {

                if(clr==p.Color.Name){
                tempClust = new centroid();
                tempClust.x = p.X;
                tempClust.y = p.Y;
                mcentroids.Add(tempClust);
                break;
                }
            }
            }
            return new Tuple<List<cxy>, List<centroid>>(mdata,mcentroids);
      
      }


        public static List<int>InitExhaustive(int cnt, double[,] distances)
      {

          List<int> pathCompleted = new List<int>();
          List<List<int>> combs = new List<List<int>>();

          ExhaustiveSearch.Combinations(cnt - 1, ref pathCompleted, ref combs);
          ExhaustiveSearch.GetPath(cnt + 1, combs, distances);


          string path = "";

          for (int i = 0; i < Exhaust.ExhaustiveSearch.order.Count - 1; i++)
          {
              path += Exhaust.ExhaustiveSearch.order[i].ToString() + "->";
          }
          path += Exhaust.ExhaustiveSearch.order[Exhaust.ExhaustiveSearch.order.Count - 1];

          Console.WriteLine("Shortest Route: " + path);

          Console.WriteLine("The shortest distance is: " + Exhaust.ExhaustiveSearch.distance.ToString());

          return Exhaust.ExhaustiveSearch.order;
 

      }

        public static List<int> InitAnneal(double[,] distances)
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
          return problem.CitiesOrder;
 
      }


      static int MyMin(int num1, int num2)
      {

          if (num1 < num2)
          {
              return num1;
          }
          else
          {
              return num2;
          }
      }
      public static double dist(double X1, double Y1, double X2, double Y2)
      {
          //' calculate Euclidean distance
          double DX = X2 - X1;
          double DY = Y2 - Y1;

          return Math.Pow(Math.Pow(DX, 2) + Math.Pow(DY, 2), (0.5))*111101.414179072;

      }
      public static bool RThereIsinCluster(List<cxy> entries, int i)
      {
          bool dummy = false;
          foreach (cxy entry in entries)
          {
              if (entry.cluster == i)
              {
                  dummy = true; break;
              }
          }
          return dummy;
      }
      public static int LastI(List<cxy> entries, int i)
      {
          int dummy = 0;
          for (int j = 0; j < entries.Count; j++)
          {
              cxy entry = entries[j];
              if (entry.cluster == i)
              {
                  dummy = j + 1;
              }
          }
          return dummy;
      }

    


    }
}
