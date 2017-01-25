using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

namespace KMeans
{
	[DataContract(Name = "KMeansCentroid", Namespace = "https://balland.ddns.net", IsReference = true)]
    public class Centroid : IComparable<Centroid>
    {
		[DataMember]
		public string Name { get; set; }
		[DataMember]
        public ObservableCollection<double> Coordinates { get; set; }
		[DataMember]
        public List<Point> MyPoints { get; set; }
		public Centroid(string n) : this(new ObservableCollection<double>()) { Name = n; }
        public Centroid(ObservableCollection<double> c) { Coordinates = c; MyPoints = new List<Point>(); }
        public Centroid(string n, ObservableCollection<double> d) : this(d) { Name = n; }

		public int CompareTo(Centroid other)
		{
			return string.Compare(Name, other.Name);
		}
	}
}
