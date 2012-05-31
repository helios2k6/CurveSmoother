using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSModel.Model;

namespace CurveSmoothing
{
	public static class Driver
	{
		private const double MAX_DOUBLE_VALUE = 500.0;
		private const double PRECISION = 0.001;
		private static int NUM_DATAPOINTS = 5000;

		private static readonly Random Random = new Random(5050);

		private static Curve GenerateRandomCurve(int numberOfDataPoints)
		{
			var listOfPoints = new List<DataPoint>();

			for (int i = 0; i < numberOfDataPoints; i++)
			{
				listOfPoints.Add(new DataPoint(Random.NextDouble() * MAX_DOUBLE_VALUE, Random.NextDouble() * MAX_DOUBLE_VALUE));
			}

			return new Curve(new List<CurveSegment> { new CurveSegment(listOfPoints) });
		}

		private static long TimeSAD(Curve a, Curve b, Func<Curve, Curve, double> sadFunc)
		{
			var stopWatch = new Stopwatch();

			stopWatch.Start();
			sadFunc(a, b);
			stopWatch.Stop();

			return stopWatch.ElapsedMilliseconds;
		}

		private static void TimeCSharpSAD()
		{
			Console.WriteLine("Testing SAD");

			Console.WriteLine("Preloading Data");
			var a = GenerateRandomCurve(NUM_DATAPOINTS);
			var b = GenerateRandomCurve(NUM_DATAPOINTS);

			Console.WriteLine("Testing C# Speed");

			var cSharpTime = TimeSAD(a, b, CSAlgorithms.Aggregation.SumOfAllDifferences.SAD);

			Console.WriteLine("C# completed SAD with {0} elements in {1} milliseconds", NUM_DATAPOINTS, cSharpTime);

			Console.WriteLine("End of Tests");
		}

		public static void Main(string[] args)
		{
			if (args.Count() > 0)
				NUM_DATAPOINTS = int.Parse(args[0]);

			TimeCSharpSAD();
		}
	}
}
