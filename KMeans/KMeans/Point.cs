using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace KMeans
{
    public class Point
    {        
        public System.Drawing.Color Color
        {
            get; set;
        }

        public double X
        {
            get; private set;
        }

        public double Y
        {
            get; private set;
        }

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }        
    }
    public class cxy
    {
        public int cluster;
        public double x;
        public double y;
    }

    public class centroid
    {
        public double x;
        public double y;
    }
    public class PlausibleSolution {


        public List<double> capacity ;
        public List<double> addedvolume ;
        public List<double> addeddistance ;
        public List<double> fixedcost ;
        public List<string> TruckCoordinates;

    }           
}
