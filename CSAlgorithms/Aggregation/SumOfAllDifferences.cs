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
			var curveACount = a.Segments.Count();
			var curveBCount = b.Segments.Count();
			if (curveACount != b.Segments.Count()) throw new ArgumentException("Curve A must have as many curve segments as Curve B");

			return Enumerable.Range(0, curveACount).AsParallel().Select(i => { return Math.Abs(SAD(a.Segments.ElementAt(i), b.Segments.ElementAt(i))); }).Sum();
		}

		public static double SAD(CurveSegment a, CurveSegment b)
		{
			var aDataPoints = a.DataPoints;
			var bDataPoints = b.DataPoints;

			var aSegCount = aDataPoints.Count();
			var bSegCount = bDataPoints.Count();

			if (aSegCount != bSegCount) throw new ArgumentException("Curve Segment A must have as many datapoints as Curve Segment B");

			return Enumerable.Range(0, aSegCount).AsParallel().Select(i => { return Math.Abs(aDataPoints.ElementAt(i).YCoordinate - bDataPoints.ElementAt(i).YCoordinate); }).Sum();
		}
	}
}
