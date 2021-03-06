﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KMeans.Exceptions;
using System.Collections.ObjectModel;

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

		private int _counter = 0;

		private ObservableCollection<double> _maxCoordinate;

		private ObservableCollection<double> _minCoordinate;

		public KMeansAlgorithm(int k) { _centroidsNumber = k; }

		public void AddPoint(ObservableCollection<double> coord)
		{
			_points.Add(new Point(coord));
			_pointsNumber++;
		}

        public void AddPoint(Point p)
        {
            _points.Add(p);
            _pointsNumber++;
        }

        public void AddCentroid(ObservableCollection<double> coord)
		{
			_centroids.Add(new Centroid(coord));
		}

        public void AddCentroid(Centroid c)
        {
            _centroids.Add(c);
        }

        public List<Centroid> CalculateResult()
		{
			Init();

			StartAlgorithm();

			return _centroids;
		}

		public void InitializeAlgorithm()
		{
			_counter = 0;
			Init();
		}

		public List<Centroid> NextStep(out bool end)
		{
			if (_counter++ == 0)
			{
				end = false;
				return _centroids;
			}
			ClearAllSavedData();
			foreach (Point p in _points)
				CalculateAndAssignNearestCentroid(p);
			foreach (Centroid c in _centroids)
				CalculateNewCentroidPosition(c);
			end = CheckConvergence();
			_anyChanges = false;
			if (_counter++ > _pointsNumber * 100)
				throw new NotConvergentException("It seems that the algorithm has done too much iterations. Maybe it doesn't converge?");
			return _centroids;
		}

		private void Init()
		{
			CheckPoints();
			CalculateDimensions();
			CalculateMaxMinCoordinates();
			CalculateCentroidNumber();
			CheckCentroidNumber();
            /*
            ObservableCollection<double> gaussCoord = CalculateGaussCoordinates();
			CalculateRandomCentroids(gaussCoord);
            */
            CalculateRandomCentroids();
		}
        private ObservableCollection<double> CalculateGaussCoordinates()
        {
            ObservableCollection<double> res = new ObservableCollection<double>();
            for (int i = 0; i < _dimensions; i++)
                res.Add(0);
            foreach (Point p in _points)
                for (int i = 0; i < _dimensions; i++)
                    res[i] += p.Coordinates[i];
            for (int i = 0; i < _dimensions; i++)
                res[i] /= _points.Count;
            return res;
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
			foreach (Point p in _points)
			{
				if (res != -1)
				{
					if (res != p.Coordinates.Count)
						throw new NotSameDimensionException("A point has not the same dimensions number as the others");
				}
				else
					res = p.Coordinates.Count;
			}
			foreach (Centroid c in _centroids)
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
			_maxCoordinate = new ObservableCollection<double>();
			_minCoordinate = new ObservableCollection<double>();

			for (int i = 0; i < _dimensions; i++)
			{
				_maxCoordinate.Add(double.MinValue);
				_minCoordinate.Add(double.MaxValue);
			}

			foreach (Point p in _points)
			{
				for (int i = 0; i < _dimensions; i++)
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
            for (int i = 0; i < centroidsLeft; i++)
            {
                ObservableCollection<double> coord = new ObservableCollection<double>();
                ObservableCollection<double> uiList;
                double radiusSquared = 0;
                while (true)
                {
                    uiList = new ObservableCollection<double>();
                    for (int j = 0; j < _dimensions; j++)
                    {
                        double tmp = r.NextDouble() * 2 - 1; //Numero random tra [-1; 1]
                        uiList.Add(tmp);
                        radiusSquared += tmp * tmp;
                    }
                    if (radiusSquared < 1)
                        break;
                    radiusSquared = 0;
                }
                for (int j = 0; j < _dimensions; j++)
                {
                    double tmp = uiList[j] * Math.Sqrt((-2) * Math.Log(radiusSquared) / radiusSquared);
                    double pos = (_maxCoordinate[j] - _minCoordinate[j]) / 2 + _minCoordinate[j] + (tmp);
                    if (pos < _minCoordinate[j] || pos > _maxCoordinate[j])
                        throw new Exception("WOW amazing");
                    coord.Add(pos);
                }
                _centroids.Add(new Centroid(coord));
            }
        }
		private void CalculateRandomCentroids(ObservableCollection<double> gaussCoord)
		{
			int centroidsLeft = _centroidsNumber - _centroids.Count;
			Random r = new Random();
			for (int i = 0; i < centroidsLeft; i++)
			{
				ObservableCollection<double> coord = new ObservableCollection<double>();
                if(_dimensions == 2)
                {
                    double rad = CalculateGaussianRadius(gaussCoord);
                    double rand = r.NextDouble();
                    double z0 = rad * Math.Cos(2 * Math.PI * rand);
                    double z1 = rad * Math.Sin(2 * Math.PI * rand);
                    coord.Add(z0);
                    coord.Add(z1);
                }
                else
                {
                    for (int j = 0; j < _dimensions; j++)
                    {
                        double diff = _maxCoordinate[j] - _minCoordinate[j];
                        coord.Add(r.NextDouble() * diff + _minCoordinate[j]);
                    }
                }
				_centroids.Add(new Centroid(coord));
			}
		}
        private double CalculateGaussianRadius(ObservableCollection<double> gaussCoord)
        {
            double maxDist = double.MinValue;

            foreach(Point p in _points)
            {
                double vectorSum = 0;
                for (int i = 0; i < _dimensions; i++)
                    vectorSum += Math.Pow(p.Coordinates[i] - gaussCoord[i], 2);
                if (vectorSum > maxDist)
                    maxDist = vectorSum;
            }

            return maxDist;
        }
        private void StartAlgorithm()
		{
			bool go = true;
			_counter = 0;
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
				if (_counter++ > _pointsNumber * 100)
					throw new NotConvergentException("It seems that the algorithm has done too much iterations. Maybe it doesn't converge?");
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
			Centroid minCentroid = new Centroid(new ObservableCollection<double>());
			foreach (Centroid c in _centroids)
			{
				double vectorSum = 0;
				for (int i = 0; i < _dimensions; i++)
					vectorSum += Math.Pow(p.Coordinates[i] - c.Coordinates[i], 2);
				if (vectorSum < minDist)
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
			Point minPoint = new Point(new ObservableCollection<double>());
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
			for (int i = 0; i < _dimensions; i++)
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
