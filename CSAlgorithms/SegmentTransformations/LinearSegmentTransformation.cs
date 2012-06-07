using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSModel.Interfaces;
using CSModel.Model;

namespace CSAlgorithms.SegmentTransformations
{
	public class LinearSegmentTransformation : ICurveSegmentTransformation
	{
		private readonly double _xTransformation;
		private readonly double _yTransformation;

		public LinearSegmentTransformation(double xTransformation, double yTransformation)
		{
			_xTransformation = xTransformation;
			_yTransformation = yTransformation;
		}

		#region ICurveSegmentTransformation Members

		public CurveSegment Transform(CurveSegment curveSegment)
		{
			var translatedSegments = from s in curveSegment.DataPoints.AsParallel()
									 select new DataPoint(s.XCoordinate + _xTransformation, s.YCoordinate + _yTransformation);

			return new CurveSegment(translatedSegments);
		}

		#endregion
	}
}
