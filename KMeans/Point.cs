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
		public Point() : this (new ObservableCollection<double>()) { }
		public Point(string n) { Name = n; Coordinates = new ObservableCollection<double>(); MyCentroid = null; }
        public Point(ObservableCollection<double> c) { Coordinates = c; MyCentroid = null; }
    }
}
