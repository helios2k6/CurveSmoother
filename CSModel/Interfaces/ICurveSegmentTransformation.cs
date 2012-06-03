using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSModel.Model;

namespace CSModel.Interfaces
{
	public interface ICurveSegmentTransformation
	{
		CurveSegment Transform(CurveSegment curveSegment);
	}
}
