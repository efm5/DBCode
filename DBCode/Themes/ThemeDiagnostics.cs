namespace DBCode.Themes {
   public static class ThemeDiagnostics {
      public static void RunStrict(Theme pTheme) {
         List<string> issues = RunReport(pTheme);

         if (issues.Count > 0) {
            string message = $"Theme '{pTheme.mName}' failed diagnostics:\n" +
                             String.Join("\n", issues);
            throw new Exception(message);
         }
      }

      public static List<string> RunReport(Theme pTheme) {
         List<string> issues = new List<string>();

         if (pTheme == null) {
            issues.Add("Theme object is null.");
            return issues;
         }

         ValidateName(pTheme, issues);
         ValidateBrightness(pTheme, issues);
         ValidateFonts(pTheme, issues);
         ValidateColors(pTheme, issues);

         return issues;
      }

      private static void ValidateName(Theme pTheme, List<string> pIssues) {
         if (String.IsNullOrWhiteSpace(pTheme.mName))
            pIssues.Add("Theme name is missing or empty.");
      }

      private static void ValidateBrightness(Theme pTheme, List<string> pIssues) {
         if (!Enum.IsDefined(typeof(ThemeBrightness), pTheme.mBrightness))
            pIssues.Add($"Invalid brightness value: '{pTheme.mBrightness}'.");
      }

      private static void ValidateFonts(Theme pTheme, List<string> pIssues) {
         foreach (FontUsage usage in Enum.GetValues(typeof(FontUsage))) {
            Font? font = pTheme.mFonts[(int)usage];

            if (font == null) {
               pIssues.Add($"Font '{usage}' is null.");
               continue;
            }

            if (font.Size <= 0)
               pIssues.Add($"Font '{usage}' has invalid size: {font.Size}.");

            if (String.IsNullOrWhiteSpace(font.Name))
               pIssues.Add($"Font '{usage}' has an empty font family name.");
         }
      }

      private static void ValidateColors(Theme pTheme, List<string> pIssues) {
         foreach (ColorUsage usage in Enum.GetValues(typeof(ColorUsage))) {
            Color color = pTheme.mColors[(int)usage];

            if (color.A == 0)
               pIssues.Add($"Color '{usage}' is fully transparent (A=0).");

            if (color.R == 0 && color.G == 0 && color.B == 0 && !color.IsNamedColor) {
               // Black is valid, but only if explicitly named or hex-coded.
               // If it's unnamed black, it may indicate a parse failure.
               pIssues.Add($"Color '{usage}' is pure black and unnamed — possible parse failure.");
            }
         }
      }
   }
}
