using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DBCode {
   internal static partial class Program {
      public static bool gBackgroundWorkerFinished = false;
      public static bool gOffset = false;

      [STAThread]
      private static void Main() {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
         Application.ThreadException += OnUiThreadException;
         AppDomain.CurrentDomain.UnhandledException += OnNonUiThreadException;

         try {
            Application.Run(new MainForm());
         }
         catch (Exception pException) {
            ShowFatalErrorAndExit(pException, "A fatal error occurred during startup.");
         }
      }

      private static void OnUiThreadException(object? pSender, ThreadExceptionEventArgs pThreadExceptionEventArgs) {
         ShowFatalErrorAndExit(pThreadExceptionEventArgs.Exception, "An unexpected error occurred.");
      }

      private static void OnNonUiThreadException(object? pSender, UnhandledExceptionEventArgs pUnhandledExceptionEventArgs) {
         ShowFatalErrorAndExit(pUnhandledExceptionEventArgs.ExceptionObject as Exception, "An unexpected error occurred.");
      }

      private static void ShowFatalErrorAndExit(Exception? pException, string pMessage) {
         if (pException == null)
            return;
         //string report = pException.ToDiagnosticString();
         string report = ExceptionExtensions.ToDiagnosticString(pException);
         ClipboardHelper.TrySetClipboardText(report);
         TimedMessage($"{pMessage}\n\nA detailed diagnostic report has been copied to the clipboard.",
            "DBCode – Fatal Error", 0);
         Environment.Exit(-1);
      }

      internal static T ThrowIfNull<T>([NotNull] T? pValue, string pMemberName,
         [CallerFilePath] string pCallerFilePath = "",
         [CallerLineNumber] int pCallerLineNumber = 0,
         [CallerMemberName] string pCallerMemberName = "") where T : class {
         if (pValue == null) {
            StackTrace stackTrace = new StackTrace(0, true);
            string message = $"Fatal: {pMemberName} was unexpectedly null in {pCallerMemberName} at {Path.GetFileName(pCallerFilePath)}:{pCallerLineNumber}";
            InvalidOperationException exception = new InvalidOperationException(message);
            exception.Data["CapturedStackTrace"] = stackTrace.ToString();
            ShowFatalErrorAndExit(exception, $"A critical component was null: {pMemberName}");
            throw exception;
         }
         return pValue;
      }
   }
}
