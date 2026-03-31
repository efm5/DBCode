using DBCode.Syntax;

namespace DBCode {
   internal static class ExceptionExtensions {
      public static string ToDiagnosticString(Exception pException) {
         StringBuilder stringBuilder = new StringBuilder(4096);

         stringBuilder.AppendLine("=== DBCode Fatal Error Report ===");
         stringBuilder.AppendLine($"Timestamp: {DateTime.Now}");
         stringBuilder.AppendLine();
         AppendExceptionInfo(stringBuilder, pException);
         stringBuilder.AppendLine();
         AppendLoadedAssemblies(stringBuilder);
         stringBuilder.AppendLine();
         AppendManifestResources(stringBuilder);
         stringBuilder.AppendLine();
         AppendEditorState(stringBuilder);
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
            pStringBuilder.AppendLine(pExceptionLevel.StackTrace);
            pStringBuilder.AppendLine();
            pExceptionLevel = pExceptionLevel.InnerException;
            level++;
         }
      }

      private static void AppendLoadedAssemblies(StringBuilder pStringBuilder) {
         pStringBuilder.AppendLine("--- Loaded Assemblies ---");
         Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

         foreach (Assembly assembly in assemblies) {
            try {
               pStringBuilder.AppendLine($"{assembly.GetName().Name}  {assembly.GetName().Version}  {assembly.Location}");
            }
            catch {
               pStringBuilder.AppendLine($"{assembly.GetName().Name}  {assembly.GetName().Version}  <no location>");
            }
         }
      }

      private static void AppendManifestResources(StringBuilder pStringBuilder) {
         pStringBuilder.AppendLine("--- Manifest Resources ---");
         Assembly assembly = Assembly.GetExecutingAssembly();
         string[] names = assembly.GetManifestResourceNames();
         foreach (string name in names)
            pStringBuilder.AppendLine(name);
      }

      private static void AppendEditorState(StringBuilder pStringBuilder) {
         pStringBuilder.AppendLine("--- Editor State ---");
         if (Fields.mHighlighterEngine == null) {
            pStringBuilder.AppendLine("HighlighterEngine: null");
            return;
         }
         HighlighterEngine engine = Fields.mHighlighterEngine;
         pStringBuilder.AppendLine($"Language: {engine.Language}");
         pStringBuilder.AppendLine($"TextLength: {engine.Editor.TextLength}");
         pStringBuilder.AppendLine($"SelectionStart: {engine.Editor.SelectionStart}");
         pStringBuilder.AppendLine($"SelectionLength: {engine.Editor.SelectionLength}");
      }
   }
}
