using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSModel.Model
{
	[Serializable]
	public class Curve
	{
		private readonly List<CurveSegment> _segments;

		public Curve()
		{
			_segments = new List<CurveSegment>();
		}

		public Curve(IList<CurveSegment> segments)
		{
			_segments = new List<CurveSegment>(segments);
		}

		public IList<CurveSegment> Segments
		{
			get { return _segments.AsReadOnly(); }
		}

		public IList<DataPoint> DataPoints
		{
			get
			{
				return (from r in _segments
						from m in r.DataPoints
						select m).ToList();
			}
		}
	}
}
