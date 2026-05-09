namespace DBCode {
   internal static class TargetListManager {
      private static List<string>? mCachedAllowed = null;
      private static List<string>? mCachedDisallowed = null;

      public static void EnsureDataFiles() {
         try {
            if (!Directory.Exists(mDataFolder))
               Directory.CreateDirectory(mDataFolder);
            if (!File.Exists(mAllowedTargetWindows))
               File.WriteAllLines(mAllowedTargetWindows, mAllowed);
            if (!File.Exists(mDisallowedTargetWindows))
               File.WriteAllLines(mDisallowedTargetWindows, mDisallowed);
         }
         catch (Exception pException) {
            throw new InvalidOperationException("Failed to initialize target window data files.", pException);
         }
      }

      public static void PopulateGrid(DataGridView pGrid, string pFilePath) {
         try {
            pGrid.Rows.Clear();
            foreach (string line in File.ReadLines(pFilePath)) {
               string trimmed = line.Trim();
               if (trimmed.Length > 0)
                  pGrid.Rows.Add(trimmed);
            }
         }
         catch (Exception pException) {
            throw new InvalidOperationException($"Failed to load target window list from {pFilePath}.", pException);
         }
      }

      public static void SaveGrid(DataGridView pGrid, string pFilePath) {
         try {
            List<string> items = [];
            foreach (DataGridViewRow row in pGrid.Rows) {
               if (row.IsNewRow)
                  continue;
               string? value = row.Cells[0].Value as string;
               if (!string.IsNullOrWhiteSpace(value))
                  items.Add(value.Trim());
            }
            File.WriteAllLines(pFilePath, items);
            InvalidateCache(); // next window enumeration picks up the edits
         }
         catch (Exception pException) {
            throw new InvalidOperationException($"Failed to save target window list to {pFilePath}.", pException);
         }
      }

      public static bool IsAllowed(string pTitle) {
         foreach (string entry in GetAllowed())
            if (pTitle.Contains(entry, StringComparison.OrdinalIgnoreCase))
               return true;
         return false;
      }

      public static bool IsDisallowed(string pTitle) {
         foreach (string entry in GetDisallowed())
            if (pTitle.Contains(entry, StringComparison.OrdinalIgnoreCase))
               return true;
         return false;
      }

      public static void InvalidateCache() {
         mCachedAllowed = null;
         mCachedDisallowed = null;
      }

      private static List<string> GetAllowed() {
         if (mCachedAllowed == null)
            mCachedAllowed = LoadList(mAllowedTargetWindows);
         return mCachedAllowed;
      }

      private static List<string> GetDisallowed() {
         if (mCachedDisallowed == null)
            mCachedDisallowed = LoadList(mDisallowedTargetWindows);
         return mCachedDisallowed;
      }

      private static List<string> LoadList(string pFilePath) {
         List<string> result = [];
         foreach (string line in File.ReadLines(pFilePath)) {
            string trimmed = line.Trim();
            if (trimmed.Length > 0)
               result.Add(trimmed);
         }
         return result;
      }
   }
}
