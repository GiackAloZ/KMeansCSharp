using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class KMeansAlgorithm
    {
        private List<Point> _points = new List<Point>();

        private List<Centroid> _centroids = new List<Centroid>();

        private int _pointsNumber = 0;

        private int _centroidsNumber = 0;

        private int _dimensions;

        private bool _anyChanges = false;

        private List<double> _maxCoordinate;

        private List<double> _minCoordinate;

        public KMeansAlgorithm(int k) { _centroidsNumber = k; }

        public void AddPoint(List<double> coord)
        {
            _points.Add(new Point(coord));
            _pointsNumber++;
        }

        public void AddCentroid(List<double> coord)
        {
            _centroids.Add(new Centroid(coord));
        }

        public List<Centroid> CalculateResult()
        {
            Init();

            StartAlgorithm();

            return _centroids;
        }

        private void Init()
        {
            CheckPoints();
            CalculateDimensions();
            CalculateMaxMinCoordinates();
            CalculateCentroidNumber();
            CheckCentroidNumber();
            CalculateRandomCentroids();
        }
        private void CheckCentroidNumber()
        {
            if (_centroidsNumber < 1)
                throw new NotEnoughCentroidsException("There are less than 1 centroid");
            if (_pointsNumber < _centroidsNumber)
                throw new NotEnoughPointsException("Centroids are more than points");
        }
        private void CheckPoints()
        {
            if (_points.Count == 0)
                throw new NotEnoughPointsException("There are 0 points");
        }
        private void CalculateDimensions()
        {
            int res = -1;
            foreach(Point p in _points)
            {
                if(res != -1)
				{
                    if (res != p.Coordinates.Count)
                        throw new NotSameDimensionException("A point has not the same dimensions number as the others");
				}
                else
                    res = p.Coordinates.Count;
            }
            foreach(Centroid c in _centroids)
            {
                if (res != -1)
				{
                    if (res != c.Coordinates.Count)
                        throw new NotSameDimensionException("A centroid has not the same dimensions number as the others");
				}
                else
                    res = c.Coordinates.Count;
            }
            _dimensions = res;
        }
        private void CalculateMaxMinCoordinates()
        {
            _maxCoordinate = new List<double>();
            _minCoordinate = new List<double>();

			for(int i = 0; i < _dimensions; i++)
			{
				_maxCoordinate.Add(double.MinValue);
				_minCoordinate.Add(double.MaxValue);
			}

            foreach(Point p in _points)
            {
                for(int i = 0; i < _dimensions; i++)
                {
                    _maxCoordinate[i] = Math.Max(_maxCoordinate[i], p.Coordinates[i]);
                    _minCoordinate[i] = Math.Min(_minCoordinate[i], p.Coordinates[i]);
                }
            }
            foreach (Centroid c in _centroids)
            {
                for (int i = 0; i < _dimensions; i++)
                {
                    _maxCoordinate[i] = Math.Max(_maxCoordinate[i], c.Coordinates[i]);
                    _minCoordinate[i] = Math.Min(_minCoordinate[i], c.Coordinates[i]);
                }
            }
        }
        private void CalculateCentroidNumber()
        {
            if (_centroids.Count > _centroidsNumber)
                _centroidsNumber = _centroids.Count;
        }
        private void CalculateRandomCentroids()
        {
            int centroidsLeft = _centroidsNumber - _centroids.Count;
            Random r = new Random();
            for(int i = 0; i < centroidsLeft; i++)
            {
                List<double> coord = new List<double>(_dimensions);
                for(int j = 0; j < _dimensions; j++)
                {
                    double diff = _maxCoordinate[j] - _minCoordinate[j];
                    coord.Add(r.NextDouble() * diff + _minCoordinate[j]);
                }
                _centroids.Add(new Centroid(coord));
            }
        }
        private void StartAlgorithm()
        {
            bool go = true;
			int cont = 0;
            while (go)
            {
                ClearAllSavedData();
                foreach (Point p in _points)
                    CalculateAndAssignNearestCentroid(p);
                foreach (Centroid c in _centroids)
                    CalculateNewCentroidPosition(c);
                if (CheckConvergence())
                    go = false;
				_anyChanges = false;
				if (cont++ > 100)
					throw new Exception("lezo");
            }
        }
        private void ClearAllSavedData()
        {
            foreach (Point p in _points)
                p.MyCentroid = null;
            foreach (Centroid c in _centroids)
                c.MyPoints = new List<Point>();
        }
        private void CalculateAndAssignNearestCentroid(Point p)
        {
            double minDist = double.MaxValue;
            Centroid minCentroid = new Centroid(new List<double>());
            foreach(Centroid c in _centroids)
            {
                double vectorSum = 0;
                for (int i = 0; i < _dimensions; i++)
                    vectorSum += Math.Pow(p.Coordinates[i] - c.Coordinates[i], 2);
                if(vectorSum < minDist)
                {
                    minDist = vectorSum;
                    minCentroid = c;
                }
            }
            p.MyCentroid = minCentroid;
            minCentroid.MyPoints.Add(p);
        }
        private void AssignNearestPoint(Centroid c)
        {
            double minDist = double.MaxValue;
            Point minPoint = new Point(new List<double>());
            foreach (Point p in _points)
            {
                double vectorSum = 0;
                for (int i = 0; i < _dimensions; i++)
                    vectorSum += Math.Pow(p.Coordinates[i] - c.Coordinates[i], 2);
                if (vectorSum < minDist)
                {
                    minDist = vectorSum;
                    minPoint = p;
                }
            }
			for (int i = 0; i < _dimensions; i++)
				c.Coordinates[i] = minPoint.Coordinates[i];
            c.MyPoints.Add(minPoint);
            minPoint.MyCentroid = c;
        }
        private void CalculateNewCentroidPosition(Centroid c)
        {
            if (c.MyPoints.Count == 0)
            {
                AssignNearestPoint(c);
                _anyChanges = true;
                return;
            }
            for(int i = 0; i < _dimensions; i++)
            {
                double vectorSum = 0;
                foreach (Point p in c.MyPoints)
                {
                    vectorSum += p.Coordinates[i];
                }
                double tmp = c.Coordinates[i];
                c.Coordinates[i] = vectorSum / c.MyPoints.Count;
                if (tmp != c.Coordinates[i])
                    _anyChanges = true;
            }
        }
        private bool CheckConvergence()
        {
            return !_anyChanges;
        }
    }
}
