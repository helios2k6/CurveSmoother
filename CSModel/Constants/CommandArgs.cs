using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSModel.Constants
{
   public static class CommandArgs
   {
      #region publicly visible variables
      //Input and output
      public static readonly string InputFileArg = "--input";
      public static readonly string OutputFileArg = "--output";

      //Output format
      public static readonly string OutputFormatArg = "--output-format";

      //Help section
      public static readonly string HelpArg = "--help";
      #endregion

      #region Help Text
      private static readonly string InputFileArgHelp = "File you want to read in";
      private static readonly string OutputFileArgHelp = "File you want to output to";
      private static readonly string OutputFormatArgHelp = "Format of the output file: xml, json, flat";
      private static readonly string HelpArgHelp = "Print this message";
      #endregion

      #region public properties
      public static IEnumerable<string> ValidArguments
      {
         get { return new List<string> { InputFileArg, OutputFileArg, OutputFormatArg, HelpArg }; }
      }

      public static IEnumerable<KeyValuePair<string, string>> HelpMap
      {
         get
         {
            return new Dictionary<string, string> {{InputFileArg, InputFileArgHelp},
                                                   {OutputFileArg, OutputFileArgHelp},
                                                   {OutputFormatArg, OutputFormatArgHelp},
                                                   {HelpArg, HelpArgHelp}};
         }
      }
      #endregion

   }
}
