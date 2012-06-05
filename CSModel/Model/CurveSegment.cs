using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSModel.Model
{
	[Serializable]
	public class CurveSegment
	{
		private readonly List<DataPoint> _dataPoints;

		private readonly DataPoint _maxima;

		private readonly DataPoint _minima;

		private readonly ManualResetEvent _sortedDataPoints = new ManualResetEvent(false);

		private readonly ManualResetEvent _foundMaxAndMin = new ManualResetEvent(false);

		public CurveSegment()
		{
			_dataPoints = new List<DataPoint>();
		}

		public CurveSegment(IEnumerable<DataPoint> dataPoints)
		{
			_dataPoints = new List<DataPoint>(dataPoints);

			var copyOfDataPoints = new List<DataPoint>(dataPoints);

			Task.Factory.StartNew(() =>
			{
				_dataPoints.Sort();
				_sortedDataPoints.Set();
			});

			Task.Factory.StartNew(() =>
			{
				DataPoint currentMax;
				DataPoint currentMin;

				currentMax = currentMin = copyOfDataPoints.First();

				foreach (var i in _dataPoints)
				{
					//Test current max
					if (currentMax.YCoordinate - i.YCoordinate < 0)
					{
						currentMax = i;
					}
					else if (i.YCoordinate - currentMin.YCoordinate < 0) //Test current min
					{
						currentMin = i;
					}
				}

				_foundMaxAndMin.Set();
			});

		}

		public IEnumerable<DataPoint> DataPoints
		{
			get
			{
				_sortedDataPoints.WaitOne();
				return _dataPoints.AsReadOnly();
			}
		}

		public DataPoint Maxima
		{
			get
			{
				_foundMaxAndMin.WaitOne();
				return _maxima;
			}
		}

		public DataPoint Minima
		{
			get
			{
				_foundMaxAndMin.WaitOne();
				return _minima;
			}
		}
	}
}