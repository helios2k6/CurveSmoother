using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSModel.Interfaces;
using CSModel.Model;
using CurveSmoothing.Proxies;
using PureMVC.Patterns;

namespace CurveSmoothing.CustomAlgo
{
	public class LinearYTranslation : ICurveTransformation
	{
		private readonly double _translationAmount;

		public LinearYTranslation(double translationAmount)
		{
			_translationAmount = translationAmount;
		}

		public Curve Transform(Curve curve)
		{
			var inputProxy = (InputArgProxy)Facade.Instance.RetrieveProxy(ProxyNames.InputArgProxyName);

			var allSegments = (from n in curve.Segments
							   let nd = (from m in n.DataPoints select new DataPoint(m.XCoordinate, m.YCoordinate))
							   select new CurveSegment(nd));

			return new Curve(allSegments);
		}
	}
}
