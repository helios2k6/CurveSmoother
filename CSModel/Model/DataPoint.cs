using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSModel.Model
{
	[Serializable]
	public class DataPoint : IEquatable<DataPoint>
	{
		#region Constants
		public static readonly double Precision = 0.0001;
		#endregion

		#region Static Comparators
		public static int XCoordinateComparator(DataPoint right, DataPoint left)
		{
			var result = right.XCoordinate - left.XCoordinate;

			if (Math.Abs(result) < Precision) return 0;
			if (result < Precision) return -1;
			return 1;
		}

		public static int YCoordinateComparator(DataPoint right, DataPoint left)
		{
			var result = right.YCoordinate - left.YCoordinate;

			if (Math.Abs(result) < Precision) return 0;
			if (result < Precision) return -1;
			return 1;
		}
		#endregion

		public DataPoint(double xCoordinate, double yCoordinate)
		{
			XCoordinate = xCoordinate;
			YCoordinate = yCoordinate;
		}

		public double XCoordinate { get; private set; }
		public double YCoordinate { get; private set; }

		private volatile bool _hashed = false;
		private volatile int _hashCode;

		#region IEquatable<DataPoint> Members

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == typeof(DataPoint) && Equals((DataPoint)obj);
		}

		public bool Equals(DataPoint other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return XCoordinate == other.XCoordinate && YCoordinate == other.YCoordinate;
		}

		#endregion

		#region Default Overrides
		public override int GetHashCode()
		{
			if (!_hashed)
			{
				_hashCode = (int)Math.Ceiling(Math.Pow(XCoordinate, 2) * Math.Pow(YCoordinate, 3));
				_hashed = true;
			}

			return _hashCode;
		}
		#endregion
	}
}
