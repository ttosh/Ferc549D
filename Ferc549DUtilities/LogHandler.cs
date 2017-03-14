using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

using Ferc549DAttributes;

namespace Ferc549DUtilities {
  [Author("Timothy Tosh", version = 1.0)]
  public class LogHandler {

    // private methods...
    public static void WriteToLog(string moduleName, string msg) {
      var now = DateTime.Now.ToString(CultureInfo.InvariantCulture);
      var logLine = string.Format("{0}\t{1}", now, msg);
      var logFile = string.Format("{0}{1}.log", ConfigUtil.LogDirectory, moduleName);
      File.AppendAllText(logFile, logLine);
    }

    // public methods...
    public static void LogError(string moduleName, Exception ex) {
      var sb = new StringBuilder();
      sb.AppendLine(ex.Message);
      if (ex.InnerException != null) {
        sb.AppendLine(ex.InnerException.Message);
      }
      if (ex.StackTrace != null) {
        sb.AppendLine(ex.StackTrace);
      }

      WriteToLog(moduleName, sb.ToString());
    }
    public static void Quit(int exitCode) {
      Environment.Exit(exitCode);
    }
    public static void WriteArgsToLog(string moduleName, string[] args) {
      var argString = string.Empty;
      for (var i = 0; i < args.Length; i++) {
        argString += string.Format("arg[{0}] = {1}; ", i, args[i]);
      }
      argString += "\n";

      WriteToLog(moduleName, argString);
    }
    public static void WriteToEventLog(string eventSource, string message) {
      var dt = DateTime.UtcNow;
      message = string.Format("{0}: {1}", dt.ToLocalTime(), message);
      EventLog.WriteEntry(eventSource, message, EventLogEntryType.Information, 1);
    }
  }
}
