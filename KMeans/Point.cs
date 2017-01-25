using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMeans.Exceptions;

using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

namespace KMeans
{
	[DataContract(Name = "KMeansPoint", Namespace = "http://balland.ddns.net", IsReference = true) ]
	public class Point : IComparable<Point>
    {
		[DataMember]
        public string Name { get; set; }
		[DataMember]
        public ObservableCollection<double> Coordinates { get; set; }
        public Centroid MyCentroid { get; set; }
		public Point(string n) : this(new ObservableCollection<double>()) { Name = n; }
        public Point(ObservableCollection<double> c) { Coordinates = c; MyCentroid = null; }
        public Point(string n, ObservableCollection<double> d) : this(d) { Name = n; }

		public double CalculateDistSquared(Point p)
		{
			if (p.Coordinates.Count != Coordinates.Count)
				throw new NotSameDimensionException("The two points have not the same dimensions");
			double res = 0;
			for(int i = 0; i < Coordinates.Count; i++)
			{
				res += Math.Pow(Coordinates[i] - p.Coordinates[i], 2);
			}
			return res;
		}

		public int CompareTo(Point other)
		{
			return string.Compare(Name, other.Name);
		}
	}
}
