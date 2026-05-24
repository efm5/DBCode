namespace DBCode {
   internal sealed class ShortcutEntry {
      internal int Sort { get; set; }
      internal string Id { get; set; } = string.Empty;
      internal string Top { get; set; } = string.Empty;
      internal string Child { get; set; } = string.Empty;
      internal string Grandchild { get; set; } = string.Empty;
      internal bool Ctrl { get; set; }
      internal bool Shift { get; set; }
      internal bool Alt { get; set; }
      internal string Key { get; set; } = string.Empty;
      internal string DisplayString { get; set; } = string.Empty;
      internal bool ShortcutLocked { get; set; }
      internal string Notes { get; set; } = string.Empty;

      // Runtime only — not persisted
      internal bool HasConflict { get; set; }

      internal Keys ShortcutKeys {
         get {
            if (string.IsNullOrEmpty(Key)) return Keys.None;
            if (!Enum.TryParse<Keys>(Key, out Keys k)) return Keys.None;
            return (Ctrl ? Keys.Control : Keys.None) |
                   (Shift ? Keys.Shift : Keys.None) |
                   (Alt ? Keys.Alt : Keys.None) | k;
         }
      }

      internal string ItemText {
         get {
            if (!string.IsNullOrEmpty(Grandchild)) return Grandchild;
            if (!string.IsNullOrEmpty(Child)) return Child;
            return Top;
         }
      }
   }
}
