namespace DBCode {
   internal class TargetWindow {
      internal string mApplicationName = string.Empty;
      internal string mButtonText = string.Empty;
      internal bool mIsAllowed = false;
      internal string mWindowTitle = string.Empty;
      internal nint mWindowHandle = nint.Zero;

      internal TargetWindow(nint pWindowHandle, string pWindowTitle, string pApplicationName, bool pIsAllowed) {
         mWindowHandle = pWindowHandle;
         mWindowTitle = pWindowTitle;
         mIsAllowed = pIsAllowed;
         mApplicationName = string.IsNullOrEmpty(pApplicationName) ? string.Empty :
            char.ToUpper(pApplicationName[0]) + pApplicationName[1..];
         CreateButtonText();
      }

      internal string ProposeDragonFriendlyName() { return mButtonText; }

      internal string ProposedTooltip() {
         string cleanTitle = mWindowTitle.Trim('*', ' ');
         if (string.IsNullOrWhiteSpace(cleanTitle))
            return mApplicationName;
         return $"{mApplicationName} – {cleanTitle}";
      }

      private void CreateButtonText() {
         string cleanTitle = mWindowTitle.Trim('*', ' ');
         string[] separators = [" — ", " – ", " - "];
         string filePart = string.Empty;
         string foundSeparator = string.Empty;
         foreach (string separator in separators) {
            int separatorIndex = cleanTitle.IndexOf(separator, StringComparison.Ordinal);
            if (separatorIndex >= 0) {
               filePart = cleanTitle[..separatorIndex].Trim('*', ' ');
               foundSeparator = separator;
               break;
            }
         }
         if (string.IsNullOrWhiteSpace(filePart)) {
            mButtonText = mApplicationName;
            return;
         }
         if (string.Equals(filePart.Trim('[', ']'), mApplicationName, StringComparison.OrdinalIgnoreCase)) {
            int lastIndex = cleanTitle.LastIndexOf(foundSeparator, StringComparison.Ordinal);
            filePart = lastIndex >= 0 ? cleanTitle[(lastIndex + foundSeparator.Length)..].Trim() : string.Empty;
         }
         if (string.IsNullOrWhiteSpace(filePart)) {
            mButtonText = mApplicationName;
            return;
         }
         filePart = filePart.Trim('[', ']', ' ');
         string extension = Path.GetExtension(filePart);
         if (!string.IsNullOrEmpty(extension) && extension.Length > 1)
            filePart = Path.GetFileNameWithoutExtension(filePart);
         if (!filePart.Contains(' '))
            filePart = MassagePascalName(filePart);
         filePart = filePart.Trim();
         if (string.IsNullOrWhiteSpace(filePart) ||
             string.Equals(filePart, mApplicationName, StringComparison.OrdinalIgnoreCase))
            mButtonText = mApplicationName;
         else
            mButtonText = $"{mApplicationName} – {filePart}";
      }
   }
}
