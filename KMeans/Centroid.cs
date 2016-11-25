using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class Centroid
    {
		public string Name { get; set; }
        public List<double> Coordinates { get; set; }
        public List<Point> MyPoints { get; set; }
		public Centroid() { Coordinates = new List<double>(); MyPoints = new List<Point>(); }
		public Centroid(string n) { Name = n;  Coordinates = new List<double>(); MyPoints = new List<Point>(); }
        public Centroid(List<double> c) { Coordinates = c; MyPoints = new List<Point>(); }
    }
}
