using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class Point
    {
        public List<double> Coordinates;

        public Centroid MyCentroid { get; set; }

        public Point(List<double> c) { Coordinates = c; MyCentroid = null; }
    }
}
