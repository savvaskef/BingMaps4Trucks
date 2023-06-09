using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Drawing;
using System.Diagnostics;
 
using System.Threading.Tasks;
using classes;
namespace KMeans
{
    public class KMeansWorker
    {
        public static bool googleDistance = false;



        public List<classes.Point>[] Cluster(int clusters, List<classes.Point> points)
        {
            var random = new MersenneTwister();
            var randomCenters = PickRandomCenters(random, clusters, points);
            bool found; bool foundcolor; centroid tempcentroid;
            ProcessGroups(points, ref randomCenters);
            List<classes.Point>[] myIntArray = new List<classes.Point>[2] { points, randomCenters };
            return myIntArray;

            //*************************************************************************************************************

        }

        private void ProcessGroups(List<classes.Point> points, ref List<classes.Point> randomCenters)
        {
            Dictionary<classes.Point, List<classes.Point>> centerAssignments = GetCenterAssignments(points, randomCenters);
            ColorClusters(centerAssignments);

            List<classes.Point> oldCenters = null;
            while (true)
            {
                //calculate average center
                List<classes.Point> newCenters = GetNewCenters(centerAssignments);

                if (CentersEqual(newCenters, oldCenters))
                {
                    break;
                }

                centerAssignments = GetCenterAssignments(points, newCenters);

                ColorClusters(centerAssignments);
                oldCenters = newCenters;
            }
            randomCenters = oldCenters;
        }


        private void ParallelProcessGroups(List<classes.Point> points, List<classes.Point> randomCenters)
        {
            Dictionary<classes.Point, List<classes.Point>> centerAssignments = ParallelGetCenterAssignments(points, randomCenters);
            ColorClusters(centerAssignments);

            List<classes.Point> oldCenters = null;
            while (true)
            {
                //calculate average center
                List<classes.Point> newCenters = ParallelGetNewCenters(centerAssignments);

                if (CentersEqual(newCenters, oldCenters))
                {
                    break;
                }

                centerAssignments = ParallelGetCenterAssignments(points, newCenters);

                ColorClusters(centerAssignments);
                oldCenters = newCenters;
            }
        }

        private static List<classes.Point> PickRandomCenters(MersenneTwister random, int clusters, List<classes.Point> points)
        {
            //pick random points
            var randomCenters = new List<classes.Point>();
            int pickedPointCount = 0;
            while (pickedPointCount < clusters)
            {
                var point = points[random.Next(0, points.Count - 1)];
                if (!randomCenters.Contains(point))
                {
                    randomCenters.Add(point);
                    pickedPointCount++;
                }
            }
            return randomCenters;
        }

        private bool CentersEqual(List<classes.Point> newCenters, List<classes.Point> oldCenters)
        {
            if (newCenters == null || oldCenters == null)
            {
                return false;
            }

            foreach (classes.Point newCenter in newCenters)
            {
                bool found = false;
                foreach (classes.Point oldCenter in oldCenters)
                {
                    if (newCenter.X == oldCenter.X && newCenter.Y == oldCenter.Y)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return false;
                }
            }
            return true;
        }

        //private void DisplayTiming(string message, TimeSpan timespan)
        //{
        //    Dispatcher.CurrentDispatcher.Invoke(new Action(() => MessageBox.Show(message + timespan.TotalSeconds.ToString())));
        //}

        private List<classes.Point> GetNewCenters(Dictionary<classes.Point, List<classes.Point>> centerAssignments)
        {
            double totalX = 0;
            double totalY = 0;

            var newCenters = new List<classes.Point>();

            foreach (classes.Point center in centerAssignments.Keys)
            {
                totalX = 0;
                totalY = 0;

                foreach (classes.Point point in centerAssignments[center])
                {
                    totalX += point.X;
                    totalY += point.Y;
                }

                double averageX = totalX / centerAssignments[center].Count;
                double averageY = totalY / centerAssignments[center].Count;

                var newCenter = new classes.Point((double)averageX, (double)averageY);
                newCenters.Add(newCenter);
                newCenter.Color = Color.Black;

                //  pointPlane.DrawPoint(newCenter);
            }
            return newCenters;
        }

        private List<classes.Point> ParallelGetNewCenters(Dictionary<classes.Point, List<classes.Point>> centerAssignments)
        {
            var newCenters = new classes.Point[centerAssignments.Keys.Count];

            Parallel.ForEach(centerAssignments.Keys, (center, state, i) =>
            {
                double totalX = 0;
                double totalY = 0;

                foreach (classes.Point point in centerAssignments[center])
                {
                    totalX += point.X;
                    totalY += point.Y;
                }

                double averageX = totalX / centerAssignments[center].Count;
                double averageY = totalY / centerAssignments[center].Count;

                var newCenter = new classes.Point((int)averageX, (int)averageY);
                newCenters[i] = newCenter;
                newCenter.Color = Color.Black;
            });



            return newCenters.ToList();
        }

        private Dictionary<classes.Point, List<classes.Point>> GetCenterAssignments(List<classes.Point> points, List<classes.Point> centers)
        {
            var centerAssignments = new Dictionary<classes.Point, List<classes.Point>>();

            //make them red
            foreach (classes.Point point in centers)
            {
                point.Color = Color.Red;
                centerAssignments.Add(point, new List<classes.Point>());
            }

            //pointPlane.DrawPoints(centers);

            foreach (classes.Point point in points)
            {
                double x = point.X;
                double y = point.Y;
                double distance = 0;
                classes.Point closestCenter = null;
                double closestCenterDistance = double.MaxValue;

                foreach (classes.Point centerPoint in centers)
                {
                    double centerX = centerPoint.X;
                    double centerY = centerPoint.Y;
                    if (googleDistance)
                        distance = distanceMatrix.Matrix.googledist(x, y, centerX, centerY, "distance");
                    else                 //Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
                        distance = dist(x, y, centerX, centerY);

                    if (distance < closestCenterDistance)
                    {
                        closestCenterDistance = distance;
                        closestCenter = centerPoint;
                    }
                }

                lock (centerAssignments)
                {
                    centerAssignments[closestCenter].Add(point);
                }
            }

            return centerAssignments;
        }

        private Dictionary<classes.Point, List<classes.Point>> ParallelGetCenterAssignments(List<classes.Point> points, List<classes.Point> centers)
        {
            var centerAssignments = new Dictionary<classes.Point, List<classes.Point>>();

            //make them red
            foreach (classes.Point point in centers)
            {
                point.Color = Color.Red;
                centerAssignments.Add(point, new List<classes.Point>());
            }


            //Parallel.ForEach(points, point =>
            points.AsParallel().ForAll(point =>
            {
                double x = point.X;
                double y = point.Y;

                classes.Point closestCenter = null;
                double closestCenterDistance = double.MaxValue;

                foreach (classes.Point pickedPoint in centers)
                {
                    double centerX = pickedPoint.X;
                    double centerY = pickedPoint.Y;
                    double distance;
                    if (googleDistance)
                        distance = distanceMatrix.Matrix.googledist(x, y, centerX, centerY, "distance");
                    else                 //Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
                        distance = dist(x, y, centerX, centerY);
                    //Math.Sqrt(Math.Pow(x - centerX, 2)  + Math.Pow(y - centerY, 2));

                    if (distance < closestCenterDistance)
                    {
                        closestCenterDistance = distance;
                        closestCenter = pickedPoint;
                    }
                }

                lock (centerAssignments)
                {
                    centerAssignments[closestCenter].Add(point);
                }
            });

            return centerAssignments;
        }
        public static double dist(double X1, double Y1, double X2, double Y2)
        {
            //' calculate Euclidean distance
            double DX = X2 - X1;
            double DY = Y2 - Y1;

            return Math.Pow(Math.Pow(DX, 2) + Math.Pow(DY, 2), (0.5)) * 111101.414179072;

        }
        private void ColorClusters(Dictionary<classes.Point, List<classes.Point>> centerAssignments)
        {
            var colorStack = new Stack<Color>();
            colorStack.Push(Color.Red);
            colorStack.Push(Color.Blue);
            colorStack.Push(Color.Orange);
            colorStack.Push(Color.Purple);
            colorStack.Push(Color.Green);
            colorStack.Push(Color.Magenta);
            colorStack.Push(Color.Fuchsia);
            colorStack.Push(Color.Gold);
            colorStack.Push(Color.Lavender);
            colorStack.Push(Color.Maroon);
            colorStack.Push(Color.Orchid);
            colorStack.Push(Color.Pink);
            colorStack.Push(Color.YellowGreen);
            colorStack.Push(Color.PaleGreen);
            colorStack.Push(Color.Beige);

            //group            
            foreach (classes.Point center in centerAssignments.Keys)
            {
                Color color = colorStack.Pop();
                center.Color = color;
                foreach (classes.Point point in centerAssignments[center])
                {
                    point.Color = color;
                }

                //pointPlane.DrawPoint(center);
                //pointPlane.DrawPoints(centerAssignments[center]);
            }
        }
    }
}
