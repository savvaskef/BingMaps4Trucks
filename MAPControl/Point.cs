using System;
using System.Drawing;
 
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using GMap.NET.WindowsForms;
using GMap.NET;
 

namespace classes
{
    
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
    public class GMapMarkerImage : GMapMarker
    {
        public System.Drawing.Image image;
        float bearing;
        public float Bearing
        {
            set { bearing = value; }
            get { return bearing; }
        }

        public GMapMarkerImage(PointLatLng p, System.Drawing.Image imge)
            : base(p)
        {
            image = imge;
            Size = new System.Drawing.Size(image.Width, image.Height);
            Offset = new Point(-image.Width / 2, -image.Height / 2);
        }

        public override void OnRender(Graphics g)
        {
 //         g.RotateTransform(this.Bearing - Overlay.Control.Bearing);
            g.DrawImage(image, LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);
            g.ResetTransform();
        }
    }

}
