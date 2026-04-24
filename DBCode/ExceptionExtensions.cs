using DBCode.Syntax;

namespace DBCode {
   internal static class ExceptionExtensions {
      public static string ToDiagnosticString(Exception pException) {
         StringBuilder stringBuilder = new StringBuilder(4096);
         stringBuilder.AppendLine("=== DBCode Fatal Error Report ===");
         stringBuilder.AppendLine($"Timestamp: {DateTime.Now}");
         stringBuilder.AppendLine();
         AppendExceptionInfo(stringBuilder, pException);
         return stringBuilder.ToString();
      }

      private static void AppendExceptionInfo(StringBuilder pStringBuilder, Exception pException) {
         Exception? pExceptionLevel = pException;
         int level = 0;
         while (pExceptionLevel != null) {
            pStringBuilder.AppendLine($"--- Exception Level {level} ---");
            pStringBuilder.AppendLine($"Type: {pExceptionLevel.GetType().FullName}");
            pStringBuilder.AppendLine($"Message: {pExceptionLevel.Message}");
            pStringBuilder.AppendLine($"Source: {pExceptionLevel.Source}");
            pStringBuilder.AppendLine($"TargetSite: {pExceptionLevel.TargetSite}");
            pStringBuilder.AppendLine("Stack Trace:");
            if (pExceptionLevel.Data.Contains("CapturedStackTrace")) {
               pStringBuilder.AppendLine(pExceptionLevel.Data["CapturedStackTrace"]?.ToString() ?? "<no stack trace available>");
            }
            else {
               pStringBuilder.AppendLine(pExceptionLevel.StackTrace ?? "<no stack trace available>");
            }
            pStringBuilder.AppendLine();
            pExceptionLevel = pExceptionLevel.InnerException;
            level++;
         }
      }
   }
}
