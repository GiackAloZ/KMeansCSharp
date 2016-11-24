using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class Centroid
    {
        public List<double> Coordinates { get; set; }

        public List<Point> MyPoints { get; set; }

        public Centroid(List<double> c) { Coordinates = c; MyPoints = new List<Point>(); }
    }
}
