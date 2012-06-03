using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSModel.Constants;
using CSModel.Interfaces;
using CSModel.Model;

namespace CSAlgorithms.CurveSeg
{
	public class MinMaxSegGenerator : ICurveSegmentGenerator
	{
		public IList<CurveSegment> GenerateCurveSegments(IList<DataPoint> dataPoints)
		{
			var numDataPoints = dataPoints.Count();

			if (numDataPoints < 3) throw new ArgumentException("Not enough data points");

			var lastKnownSlope = 0.0;
			var lastKnownSlopeMagnitude = 0.0;

			var listOfCurveSegments = new List<CurveSegment>();

			var listOfCurrentPoints = new List<DataPoint>();
			listOfCurrentPoints.Add(dataPoints.First());

			for (var i = 1; i < dataPoints.Count(); i++)
			{
				var lastDataPoint = dataPoints[i - 1];
				var currentDataPoint = dataPoints[i];

				var discoveredSlope = currentDataPoint.YCoordinate - currentDataPoint.YCoordinate;
				var discoveredSlopeMagnitude = Math.Abs(discoveredSlope);
				/*
				 * Test discovered slope value. There's only three possibilities:
				 * 1. The discovered slope is "zero", which means it's still a part of the current segment
				 * 2. The discovered slope is positive
				 * 3. The discovered slope is negative
				 * 
				 * Compare with the previous slope, which can be:
				 * 1. Positive
				 * 2. Negative
				 * 3. "Zero"
				 */

				/*
				 * If the last known slope is zero or if the previous slope was negative and the current slope is positive
				 * or if the previous slope was positive and the current slope is negative, seal the current segment and
				 * create a new one
				 */
				if ((lastKnownSlopeMagnitude < Limits.MaxPrecision && discoveredSlopeMagnitude > Limits.MaxPrecision) ||
					(lastKnownSlope < 0 && discoveredSlope > 0) ||
					(lastKnownSlope > 0 && discoveredSlope < 0))
				{
					listOfCurveSegments.Add(new CurveSegment(listOfCurrentPoints));

					listOfCurrentPoints = new List<DataPoint>();
				}

				//The slopes are the same polarity
				listOfCurrentPoints.Add(currentDataPoint);
				lastKnownSlope = discoveredSlope;
				lastKnownSlopeMagnitude = discoveredSlopeMagnitude;
			}

			return listOfCurveSegments;
		}
	}
}
