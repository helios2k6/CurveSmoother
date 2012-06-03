using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSModel.Constants;

namespace CSModel.Model
{
	public class DataPointXCoordinateComparator : IComparer<DataPoint>
	{
		#region IComparer<DataPoint> Members

		public int Compare(DataPoint right, DataPoint left)
		{
			var result = right.XCoordinate - left.XCoordinate;

			if (Math.Abs(result) < Limits.MaxPrecision) return 0;
			if (result < Limits.MaxPrecision) return -1;
			return 1;
		}

		#endregion
	}
}
