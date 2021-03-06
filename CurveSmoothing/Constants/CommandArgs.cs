﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurveSmoothing.Constants
{
	public static class CommandArgs
	{
		#region publicly visible variables
		//Input and output
		public static readonly string InputFileArg = "--input";
		public static readonly string OutputFileArg = "--output";

		//Output format
		public static readonly string OutputFormatArg = "--output-format";

		public static readonly string MinimumSafeDistanceArg = "--minimum-safe-distance";

		//Help section
		public static readonly string HelpArg = "--help";
		#endregion

		#region Help Text
		private static readonly string InputFileArgHelp = "File you want to read in";
		private static readonly string OutputFileArgHelp = "File you want to output to";
		private static readonly string OutputFormatArgHelp = "Format of the output file: xml, json, flat";
		private static readonly string MinimumSafeDistanceArgHelp = "The minimum distance you want the spline to be from the original curve";
		private static readonly string HelpArgHelp = "Print this message";
		#endregion

		#region public properties
		public static IList<string> ValidArguments
		{
			get { return new List<string> { InputFileArg, OutputFileArg, OutputFormatArg, MinimumSafeDistanceArg, HelpArg }; }
		}

		public static IDictionary<string, string> HelpMap
		{
			get
			{
				return new Dictionary<string, string> {
				{InputFileArg, InputFileArgHelp},
				{OutputFileArg, OutputFileArgHelp},
				{OutputFormatArg, OutputFormatArgHelp},
				{MinimumSafeDistanceArg, MinimumSafeDistanceArgHelp},
				{HelpArg, HelpArgHelp}
				};
			}
		}
		#endregion

	}
}
