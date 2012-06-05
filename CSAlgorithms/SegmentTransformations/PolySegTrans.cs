using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSModel.Interfaces;
using CSModel.Model;

namespace CSAlgorithms.SegTrans
{
   public class PolynomialSegmentTransformation : ICurveSegmentTransformation
   {
      private readonly Func<IEnumerable<CurveSegment>, bool> _curveSegmentPredicate;
      private readonly int _order;

      public PolynomialSegmentTransformation(Func<IEnumerable<CurveSegment>, bool> curveSegmentPredicate, int order)
      {
         _curveSegmentPredicate = curveSegmentPredicate;
         _order = order;
      }

      public CurveSegment Transform(CurveSegment curveSegment)
      {
         var dataPointsAsList = curveSegment.DataPoints.ToList();

         if (dataPointsAsList.Count < 3) throw new ArgumentException("Curve segment is too small");

         var firstPoint = dataPointsAsList.First();
         var lastPoint = dataPointsAsList.Last();

         throw new NotImplementedException();
      }

   }
}
