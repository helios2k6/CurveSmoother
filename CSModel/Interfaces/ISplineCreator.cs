using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSModel.Model;

namespace CSModel.Interfaces
{
	public interface ISplineCreator
	{
		Curve CreateMostOptimalSpline(ICollection<Curve> curves);
	}
}
