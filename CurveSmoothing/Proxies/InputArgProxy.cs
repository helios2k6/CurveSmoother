using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSGeneral;
using CSModel.Constants;
using PureMVC.Patterns;

namespace CurveSmoothing.Proxies
{
   public class InputArgProxy : Proxy
   {
      #region private static constants
      private static readonly double DefaultMinimumDistance = 1.0;
      #endregion

      private CommandLineParser _parser;

      public InputArgProxy(IList<string> commandlineArgs)
         : base(ProxyNames.InputArgProxyName)
      {
         _parser = new CommandLineParser(commandlineArgs, CommandArgs.ValidArguments);
      }

      public string InputFile { get { return _parser[CommandArgs.InputFileArg].FirstOrDefault(); } }

      public string Outputfile { get { return _parser[CommandArgs.OutputFileArg].FirstOrDefault(); } }

      public string OutputFormat { get { return _parser[CommandArgs.OutputFormatArg].FirstOrDefault(); } }

      public double MinimumSafeDistance
      {
         get
         {
            double parseResult;
            return double.TryParse(_parser[CommandArgs.MinimumSafeDistanceArg].FirstOrDefault(), out parseResult) ? parseResult : DefaultMinimumDistance;
         }
      }

      public bool PrintHelp { get { return _parser[CommandArgs.HelpArg] != null; } }

      public bool HasInvalidArgs { get { return _parser.HasInvalidOptions; } }
   }
}
