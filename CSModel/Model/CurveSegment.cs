using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSModel.Model
{
	[Serializable]
	public class CurveSegment
	{
		private readonly List<DataPoint> _dataPoints;

		public CurveSegment()
		{
			_dataPoints = new List<DataPoint>();
		}

		public CurveSegment(IList<DataPoint> dataPoints)
		{
			_dataPoints = new List<DataPoint>(dataPoints);
		}

		public IList<DataPoint> DataPoints
		{
			get { return _dataPoints.AsReadOnly(); }
		}
	}
}