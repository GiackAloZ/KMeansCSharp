using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class Centroid
    {
		public string Name { get; set; }
        public ObservableCollection<double> Coordinates { get; set; }
        public List<Point> MyPoints { get; set; }
		public Centroid() : this(new ObservableCollection<double>()) {}
		public Centroid(string n) : this() { Name = n; }
        public Centroid(ObservableCollection<double> c) { Coordinates = c; MyPoints = new List<Point>(); }
    }
}
