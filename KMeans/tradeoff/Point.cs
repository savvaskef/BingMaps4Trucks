using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace classes
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
        public string ID;
    }

    public class centroid
    {
        public double x;
        public double y;
        public string ID;
    }

    public class distanceEntry {
    public string Point1;
    public string Point2;
    public double distance;
    }

}
