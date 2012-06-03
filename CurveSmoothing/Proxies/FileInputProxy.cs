using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSModel.Model;
using PureMVC.Patterns;

namespace CurveSmoothing.Proxies
{
	public class FileInputProxy : Proxy
	{
		private IList<DataPoint> _dataPoints;

		private readonly ManualResetEvent _eventTrigger = new ManualResetEvent(false);

		public FileInputProxy(IEnumerable<DataPoint> dataPoints)
			: base(ProxyNames.FileInputProxyName)
		{
			var copyOfList = new List<DataPoint>(dataPoints);
			
			Task.Factory.StartNew(() =>
			{
				_dataPoints = copyOfList.AsParallel().OrderBy(t => t, new DataPointXCoordinateComparator()).ToList();
				_eventTrigger.Set();
			});
		}

		public IList<DataPoint> DataPoints
		{
			get
			{
				_eventTrigger.WaitOne();
				return _dataPoints;
			}
		}
	}
}
