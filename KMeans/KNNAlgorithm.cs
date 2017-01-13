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

        public KNNAlgorithm() { }
        public KNNAlgorithm(List<KMeans.Centroid> pp)
        {
            _features = pp;
            CheckFeaturesDimensions();
        }

        private void CheckFeaturesDimensions()
        {
            int d = 0;
            foreach(Centroid c in _features)
            {
                if (d != 0)
                    if (d != c.Coordinates.Count)
                        throw new NotSameDimensionException();
                d = c.Coordinates.Count;
            }
        }

        public Centroid GetPointFeature(KMeans.Point p)
        {
            if(p.Coordinates.Count != _features?[0].Coordinates.Count)
                throw new NotSameDimensionException();
            Centroid res = null;
            double minDist = Double.MaxValue;
            foreach (Centroid c in _features)
            {
                double vectorSum = 0;
                for (int i = 0; i < p.Coordinates.Count; i++)
                    vectorSum += Math.Pow(p.Coordinates[i] - c.Coordinates[i], 2);
                if (vectorSum < minDist)
                {
                    minDist = vectorSum;
                    res = c;
                }
            }
            return res;
        }
    }
}
