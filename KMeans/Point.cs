using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class Point
    {
        public string Name { get; set; }
        public ObservableCollection<double> Coordinates { get; set; }
        public Centroid MyCentroid { get; set; }
		public Point(string n) : this(new ObservableCollection<double>()) { Name = n; }
        public Point(ObservableCollection<double> c) { Coordinates = c; MyCentroid = null; }
        public Point(string n, ObservableCollection<double> d) : this(d) { Name = n; }
    }
}
