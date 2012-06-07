using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSModel.Interfaces;
using CSModel.Model;

namespace CSAlgorithms.SegTrans
{
	public class PolynomialSegmentTransformation : ICurveSegmentTransformation
	{
		private static List<List<T>> CalculateAllCombinations<T>(IList<T> indexes, uint choose, List<List<T>> accum)
		{
			//Sanity check
			if (indexes.Count < choose) throw new ArgumentException("Number of items is less than the number chosen");

			if (choose > 0)
			{
				for (var i = 0; i < indexes.Count; i++)
				{
					var currentlyChosenElement = indexes[i];

					var subList = new List<T>(indexes);
					subList.RemoveAt(i);

					var recursiveResult = CalculateAllCombinations(subList, choose - 1, accum);

					foreach (var rr in recursiveResult)
					{
						rr.Add(currentlyChosenElement);
					}

					accum.AddRange(recursiveResult);
				}
			}
			return accum;
		}

		private readonly Func<IEnumerable<CurveSegment>, bool> _curveSegmentPredicate;
		private readonly int _order;

		public PolynomialSegmentTransformation(Func<IEnumerable<CurveSegment>, bool> curveSegmentPredicate, int order)
		{
			if (order < 2) throw new ArgumentException("Order of polynomial transformation must be greater than 1");

			_curveSegmentPredicate = curveSegmentPredicate;
			_order = order;
		}

		public CurveSegment Transform(CurveSegment curveSegment)
		{
			var dataPointsAsList = curveSegment.DataPoints.ToList();

			if (dataPointsAsList.Count <= _order) throw new ArgumentException("Curve segment is too small");

			var firstPoint = dataPointsAsList.First();
			var lastPoint = dataPointsAsList.Last();

			throw new NotImplementedException();
		}

		private IList<double> CalculateCoefficients(IList<DataPoint> dataPoints)
		{
			throw new NotImplementedException();
		}

		private CurveSegment InterpolateDataPoints(IList<double> coefficients, IList<double> xCoordinates)
		{
			throw new NotImplementedException();
		}
	}
}
