using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSModel.Model;

namespace CSAlgorithms.Aggregation
{
	public static class SumOfAllDifferences
	{
		public static double SAD(Curve a, Curve b)
		{
			var aDataPoints = a.DataPoints;
			var bDataPoints = b.DataPoints;

			if (aDataPoints.Count != bDataPoints.Count) throw new ArgumentException("Curve A must have as many datapoints as Curve B");

			return Enumerable.Range(0, aDataPoints.Count).AsParallel().Select(
			   i =>
			   {
				   return Math.Abs(aDataPoints[i].YCoordinate - bDataPoints[i].YCoordinate);
			   }).Sum();
		}
	}
}
