using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

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

    public class PlausibleSolution
    {

        public List<int> trucksChosen;
        public List<double> capacity;
        public List<double> addedvolume;
        public List<double> addeddistance;
        public List<double> fixedcost;
        public List<List<string>> Truckroute;
        public List<List<int>> order;
        public List<List<double>> PointsVolume;
        public List<List<double>> PointsDistance=new List<List<double>>();
        public List<List<double>> PointsCost=new List<List<double>>();
        public List<List<double>> CummPointsVolume= new List<List<double>>();
        public List<List<double>> PercPointsVolume = new List<List<double>>();

        public List<List<double>> CummPointsDistance = new List<List<double>>();
        public List<List<double>> CummPointsCost = new List<List<double>>();
        public List<List<string>> addresses;
        public List<List<double>> lattitudes= new List<List<double>>();
        public List<List<double>> longtitudes = new List<List<double>>();
        
    }

}
