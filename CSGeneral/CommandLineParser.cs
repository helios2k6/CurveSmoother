using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSGeneral
{
	public class CommandLineParser
	{
		private readonly IDictionary<string, string> _parsingDictionary = new Dictionary<string, string>();
		private readonly IDictionary<string, string> _invalidOptions = new Dictionary<string, string>();

		public CommandLineParser(IList<string> commandArgs, IList<string> commandLineOptions, IList<string> deliminators = null)
		{
			foreach (var s in commandArgs)
			{
				if (commandLineOptions.Contains(s))
				{

				}
			}
		}

		public string this[string param]
		{
			get
			{
				string result;

				return _parsingDictionary.TryGetValue(param, out result) ? result : string.Empty;
			}
		}
	}
}
