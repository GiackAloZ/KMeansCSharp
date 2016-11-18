using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class KMeansAlgorithm
    {
        private List<Point> _points;

        private List<Centroid> _centroids;

        private int _centroidNumber;

        private int _dimensions;

        private List<double> _maxCoordinate;

        private List<double> _minCoordinate;

        public KMeansAlgorithm(int k) { _centroidNumber = k; }

        public void AddPoint(List<double> coord)
        {
            _points.Add(new Point(coord));
        }

        public void AddCentroid(List<double> coord)
        {
            _centroids.Add(new Centroid(coord));
        }

        public List<List<double>> CalculateResult()
        {
            Init();

            throw new NotImplementedException();
        }

        private void Init()
        {
            CheckPoints();

            CalculateDimensions();
            CalculateMaxMinCoordinates();
            CalculateCentroidNumber();
            CalculateRandomCentroids();
        }

        private void CheckPoints()
        {
            if (_points.Count == 0)
                throw new NotEnoughPointsException();
        }
        private void CalculateDimensions()
        {
            int res = -1;
            foreach(Point p in _points)
            {
                if(res != -1)
                    if (res != p.Coordinates.Count)
                        throw new NotSameDimensionException();
                else
                    res = p.Coordinates.Count;
            }
            foreach(Centroid c in _centroids)
            {
                if (res != -1)
                    if (res != c.Coordinates.Count)
                        throw new NotSameDimensionException();
                else
                    res = c.Coordinates.Count;
            }
            _dimensions = res;
        }
        private void CalculateMaxMinCoordinates()
        {
            _maxCoordinate = new List<double>(_dimensions);
            _minCoordinate = new List<double>(_dimensions);
            for(int i = 0; i < _dimensions; i++)
            {
                _maxCoordinate[i] = double.MinValue;
                _minCoordinate[i] = double.MaxValue;
            }

            throw new NotImplementedException();
        }
        private void CalculateCentroidNumber()
        {
            if (_centroids.Count > _centroidNumber)
                _centroidNumber = _centroids.Count;
        }
        private void CalculateRandomCentroids()
        {
            int centroidsLeft = _centroidNumber - _centroids.Count;
            Random r = new Random();
            for(int i = 0; i < centroidsLeft; i++)
            {

            }

            throw new NotImplementedException();
        }
    }
}
