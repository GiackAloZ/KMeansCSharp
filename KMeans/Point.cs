using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class Point
    {
        public string Name { get; set; }
        public List<double> Coordinates { get; set; }
        public Centroid MyCentroid { get; set; }
		public Point() { Coordinates = new List<double>(); MyCentroid = null; }
		public Point(string n) { Name = n; Coordinates = new List<double>(); MyCentroid = null; }
        public Point(List<double> c) { Coordinates = c; MyCentroid = null; }
    }
}
