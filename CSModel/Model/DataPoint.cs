using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSModel.Constants;

namespace CSModel.Model
{
	[Serializable]
	public class DataPoint : IEquatable<DataPoint>, IComparable<DataPoint>
	{
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

		public int CompareTo(DataPoint other)
		{
			var result = XCoordinate - other.XCoordinate;

			if (Math.Abs(result) < Limits.MaxPrecision) return 0;
			if (result < Limits.MaxPrecision) return -1;
			return 1;
		}
	}
}
