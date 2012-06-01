using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSGeneral
{
   public class CommandLineParser
   {
      private readonly IDictionary<string, IList<string>> _parsingDictionary = new Dictionary<string, IList<string>>();
      private readonly ICollection<string> _invalidOptions = new HashSet<string>();

      public CommandLineParser(IList<string> commandArgs, IList<string> commandLineOptions)
      {
         //Current command we're currently trying to associate with the arguments
         string currentCommand = default(string);

         foreach (var s in commandArgs)
         {
            //We found a commandline option
            if (commandLineOptions.Contains(s))
            {
               currentCommand = s;
               if (!_parsingDictionary.ContainsKey(s))
               {
                  _parsingDictionary[s] = new List<string>();
               }
            }
            else
            {
               if (string.IsNullOrEmpty(currentCommand))
               {
                  _invalidOptions.Add(s);
                  continue;
               }
               
               _parsingDictionary[currentCommand].Add(s);
            }
         }
      }

      public IList<string> this[string param]
      {
         get
         {
            IList<string> result;
            _parsingDictionary.TryGetValue(param, out result);

            return result;
         }
      }

      public ICollection<string> InvalidOptions
      {
         get { return new HashSet<string>(_invalidOptions); }
      }

      public bool HasInvalidOptions
      {
         get { return _invalidOptions.Any(); }
      }
   }
}
