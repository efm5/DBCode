namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool PreferencesHasValue(string? pValue) {
         if (string.IsNullOrWhiteSpace(pValue))
            return false;
         return true;
      }

      internal static string PreferencesValueOrDefault(string? pValue, string pDefault) {
         if (string.IsNullOrWhiteSpace(pValue))
            return pDefault;
         return pValue.Trim();
      }

      internal static bool PreferencesBoolOrDefault(bool? pValue, bool pDefault) {
         if (pValue == null)
            return pDefault;
         return pValue.Value;
      }

      internal static int PreferencesIntOrDefault(int? pValue, int pDefault) {
         if (pValue == null)
            return pDefault;
         return pValue.Value;
      }

      internal static float PreferencesFloatOrDefault(float? pValue, float pDefault) {
         if (pValue == null)
            return pDefault;
         return pValue.Value;
      }

      internal static double PreferencesDoubleOrDefault(double? pValue, double pDefault) {
         if (pValue == null)
            return pDefault;
         return pValue.Value;
      }
   }
}
