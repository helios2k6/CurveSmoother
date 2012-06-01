using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSModel.Model
{
   public class DataPointXCoordinateComparator : IComparer<DataPoint>
   {
      #region IComparer<DataPoint> Members
      public static readonly double Precision = 0.0001;

      public int Compare(DataPoint right, DataPoint left)
      {
         var result = right.XCoordinate - left.XCoordinate;

         if (Math.Abs(result) < Precision) return 0;
         if (result < Precision) return -1;
         return 1;
      }

      #endregion
   }
}
