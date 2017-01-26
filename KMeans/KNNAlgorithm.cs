using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KMeans.Exceptions;

namespace KMeans
{
    public class KNNAlgorithm
    {
        public List<Centroid> _features;

		public int K { get; set; }

        public KNNAlgorithm() { }
        public KNNAlgorithm(int k, List<KMeans.Centroid> pp)
        {
			K = k;
            _features = pp;
            CheckFeaturesDimensions();
        }

        private int CheckFeaturesDimensions()
        {
            int d = 0;
            foreach(Centroid c in _features)
            {
                if (d != 0)
                    if (d != c.Coordinates.Count)
                        throw new NotSameDimensionException();
                d = c.Coordinates.Count;
            }
			return d;
        }

        public Centroid GetPointFeature(Point evalP)
        {
			if (evalP.Coordinates.Count != CheckFeaturesDimensions())
				throw new NotSameDimensionException("The points have not the same dimensions as the evaluated point");
			List<Tuple<double, Point>> distances = new List<Tuple<double, Point>>();
			foreach(Centroid c in _features)
			{
				foreach(Point p in c.MyPoints)
				{
					distances.Add(new Tuple<double, Point>(p.CalculateDistSquared(evalP), p));
				}
			}
            try
            {
			    distances.Sort();
            }
            catch(Exception ex) { return null; }
			int[] numbers = new int[_features.Count];
			for (int i = 0; i < _features.Count; i++)
				numbers[i] = 0;
			int kk = K;
			for(int i = 0; i < kk; i++)
			{
				if(distances[i].Item2.MyCentroid == null)
				{
					kk++;
					continue;
				}
				numbers[_features.FindIndex((x) => (x == distances[i].Item2.MyCentroid))]++;
			}
			int max = -1;
			Centroid res = null;
			for(int i = 0; i < _features.Count; i++)
			{
				if(max < numbers[i])
				{
					max = numbers[i];
					res = _features[i];
				}
			}
			return res;
        }
    }
}
