using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSModel.Interfaces;
using CSModel.Model;

namespace CSAlgorithms.SegmentTransformations
{
	public class InverseSegmentTransformation : ICurveSegmentTransformation
	{
		private Func<IEnumerable<CurveSegment>, CurveSegment> _evaluationPredicate;

		public InverseSegmentTransformation(Func<IEnumerable<CurveSegment>, CurveSegment> evaluationPredicate)
		{
			_evaluationPredicate = evaluationPredicate;
		}

		public CurveSegment Transform(CurveSegment curveSegment)
		{
			var dataPointsAsList = curveSegment.DataPoints.ToList();

			var numDataPoints = dataPointsAsList.Count;
			if (numDataPoints < 3) throw new ArgumentException("Cannot have less than 3 data points");

			var firstDataPoint = curveSegment.DataPoints.First();
			var lastDataPoint = curveSegment.DataPoints.Last();

			dataPointsAsList.RemoveAt(numDataPoints - 1);
			dataPointsAsList.RemoveAt(0);

			var allDataPoints = from n in dataPointsAsList.AsParallel()
								select SolveInverseCurve(new List<DataPoint> { firstDataPoint, n, lastDataPoint });
			return _evaluationPredicate(allDataPoints);
		}

		private CurveSegment SolveInverseCurve(IList<DataPoint> dataPoints)
		{
			throw new NotImplementedException();
		}
	}
}
