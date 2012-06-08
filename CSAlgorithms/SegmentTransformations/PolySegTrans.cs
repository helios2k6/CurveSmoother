using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSModel.Constants;
using CSModel.Interfaces;
using CSModel.Model;

namespace CSAlgorithms.SegTrans
{
	public class PolynomialSegmentTransformation : ICurveSegmentTransformation
	{
		private static IList<IList<T>> CalculateAllCombinations<T>(IList<T> indexes, int choose, IList<IList<T>> accum)
		{
			//Sanity check
			if (indexes.Count < choose) throw new ArgumentException("Number of items is less than the number chosen");

			if (choose > 0)
			{
				for (var i = 0; i < indexes.Count; i++)
				{
					var currentlyChosenElement = indexes[i];

					var subList = new List<T>(indexes);
					subList.RemoveAt(i);

					var recursiveResult = CalculateAllCombinations(subList, choose - 1, accum);

					foreach (var rr in recursiveResult)
					{
						rr.Add(currentlyChosenElement);
					}

					accum = accum.Union(recursiveResult).ToList();
				}
			}
			return accum;
		}

		private readonly Func<IEnumerable<CurveSegment>, CurveSegment> _curveSegmentPredicate;
		private readonly int _order;

		public PolynomialSegmentTransformation(Func<IEnumerable<CurveSegment>, CurveSegment> curveSegmentPredicate, int order)
		{
			if (order < 2) throw new ArgumentException("Order of polynomial transformation must be greater than 1");

			_curveSegmentPredicate = curveSegmentPredicate;
			_order = order;
		}

		public CurveSegment Transform(CurveSegment curveSegment)
		{
			var dataPointsAsList = curveSegment.DataPoints.ToList();

			if (dataPointsAsList.Count <= _order) throw new ArgumentException("Curve segment is too small");

			var firstPoint = dataPointsAsList.First();
			var lastPoint = dataPointsAsList.Last();

         dataPointsAsList.RemoveAt(dataPointsAsList.Count - 1); //Must remove tail first
         dataPointsAsList.RemoveAt(0); //Then remove the head

         var allDataPointCombinations = CalculateAllCombinations(dataPointsAsList, _order - 1, new List<IList<DataPoint>>());

         var accumulator = new ConcurrentBag<CurveSegment>();

         Parallel.ForEach(allDataPointCombinations, currentCombination =>
         {
            currentCombination.Add(firstPoint);
            currentCombination.Add(lastPoint);

            var coefficients = CalculateCoefficients(currentCombination);

            var segmentResult = InterpolateDataPoints(coefficients, currentCombination);

            accumulator.Add(segmentResult);
         });

         return _curveSegmentPredicate(accumulator);
		}

      private static int SelectPivot(double[,] matrix, int col, int rowStart)
      {
         int selectedRow = rowStart;
         double lastValue = matrix[rowStart, col];

         for (var i = rowStart + 1; i < matrix.GetLength(0); i++)
         {
            double currentValue = matrix[i, col];
            if ((currentValue - lastValue) > Limits.MaxPrecision)
            {
               selectedRow = i;
               lastValue = currentValue;
            }
         }

         return selectedRow;
      }

      private static void SwapRows(double[,] matrix, int rowA, int rowB)
      {
         for (var i = 0; i < matrix.GetLength(1); i++)
         {
            double temp = matrix[rowA, i];
            matrix[rowA, i] = matrix[rowB, i];
            matrix[rowB, i] = temp;
         }
      }

      private static void RowReduce(double[,] matrix)
      {
         var numRows = matrix.GetLength(0);
         var numCols = matrix.GetLength(1);

         for (int row = 0; row < numRows && row < numCols; row++)
         {
            int pivotRow = SelectPivot(matrix, row, row);

            SwapRows(matrix, row, pivotRow);

            for (int belowPivot = row + 1; belowPivot < numRows; belowPivot++)
            {
               for (int col = row + 1; col < numCols; col++)
               {
                  double result = matrix[belowPivot, col] - matrix[row, col] * (matrix[belowPivot, row] / matrix[row, row]);
                  matrix[belowPivot, col] = result;
               }
               matrix[belowPivot, row] = 0;
            }
         }
      }

      private static int DetectPivotCol(double[,] matrix, int row)
      {
         var numCols = matrix.GetLength(1);

         for (int i = 0; i < numCols; i++)
         {
            double result = matrix[row, i];
            if (result != 0)
            {
               return i;
            }
         }
         return -1;
      }

      private static void SubtractRow(double[,] matrix, int rowA, double[] elements)
      {
         var numCols = matrix.GetLength(1);
         for (int i = 0; i < numCols; i++)
         {
            matrix[rowA, i] = matrix[rowA, i] - elements[i];
         }
      }

      private static void NeutralizeRowsAbove(double[,] matrix, int row, int pivotCol)
      {
         var numCols = matrix.GetLength(1);
         double currentElement = matrix[row, pivotCol];
         for (var i = row - 1; i >= 0; i--)
         {
            double elementAbove = matrix[i, pivotCol];

            double multiple = elementAbove / currentElement;

            double[] rowCopy = new double[numCols];
            //Copy row and multiply it by a multiple
            for (int j = 0; j < numCols; j++)
            {
               rowCopy[j] = matrix[row, j] * multiple;
            }

            SubtractRow(matrix, i, rowCopy);
         }
      }

      private static void DivideRowByScalar(double [,] matrix, int row, double scalar)
      {
         var numCols = matrix.GetLength(1);
         for (int i = 0; i < numCols; i++)
         {
            matrix[row, i] = matrix[row, i] / scalar;
         }
      }

      private static void BackSub(double[,] matrix)
      {
         var numRows = matrix.GetLength(0);
         for (var i = 0; i < numRows; i++)
         {
            var pivotCol = DetectPivotCol(matrix, i);
            NeutralizeRowsAbove(matrix, i, pivotCol);
            DivideRowByScalar(matrix, i, matrix[i, pivotCol]);
         }
      }

		private IList<double> CalculateCoefficients(IList<DataPoint> dataPoints)
		{
         //Row by column. Stored in row-major order
         var vandermonteMatrix = new double[_order + 1, _order + 2];
         
         //Cycle through rows
         for(var i = 0; i < _order + 1; i++)
         {
            //Cycle through all columns, except the last one
            for(var j = 0; j < _order + 1; j++)
            {
               vandermonteMatrix[i, j] = Math.Pow(dataPoints[i].XCoordinate, _order - j);
            }
            //Set the y value
            vandermonteMatrix[i, _order + 1] = dataPoints[i].YCoordinate;
         }

         //Solve matrix
         RowReduce(vandermonteMatrix);
         BackSub(vandermonteMatrix);

         //Read the last column
         var coefficients = new List<double>();

         for (var i = 0; i < _order + 1; i++)
         {
            coefficients.Add(vandermonteMatrix[i, _order + 1]);
         }

         return coefficients;
		}

      private CurveSegment InterpolateDataPoints(IList<double> coefficients, IList<DataPoint> coordinates)
		{
         var yResults = coordinates.AsParallel().Select(t =>
         {
            var runningSum = 0.0;
            for(var i = 0; i < coefficients.Count; i++)
            {
               runningSum += coefficients[i] * (Math.Pow(t.XCoordinate, coefficients.Count - i - 1));
            }

            return new DataPoint(t.XCoordinate, runningSum);
         });

         return new CurveSegment(yResults);
		}
	}
}
