namespace DBCode {
   internal static partial class Program {
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

      private static void OnUiThreadException(object pSender, ThreadExceptionEventArgs pE) {
         ShowFatalErrorAndExit(pE.Exception, "An unexpected error occurred.");
      }

      private static void OnNonUiThreadException(object pSender, UnhandledExceptionEventArgs pE) {
         ShowFatalErrorAndExit(pE.ExceptionObject as Exception, "An unexpected error occurred.");
      }

      private static void ShowFatalErrorAndExit(Exception pException, string pMessage) {
         MessageBox.Show(
            $"{pMessage}\n\n{pException}",
            "DBCode – Fatal Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
         Environment.Exit(-1);
      }
   }
}
