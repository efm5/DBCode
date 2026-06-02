namespace DBCode {
   internal static class ShortcutManager {
      private static readonly char[] mSSeparator = ['\t'];

      internal static string DefaultFilePath {
         get { return Path.Combine(mDataFolder, "shortcuts.tsv"); }
      }

      internal static List<ShortcutEntry> Load(string pPath) {
         List<ShortcutEntry> entries = [];
         if (!File.Exists(pPath))
            return entries;
         string[] lines = File.ReadAllLines(pPath, Encoding.UTF8);
         for (int index = 1; index < lines.Length; index++) {
            string line = lines[index];
            if (string.IsNullOrWhiteSpace(line))
               continue;
            string[] parts = line.Split(mSSeparator, 12);
            if (parts.Length < 11)
               continue;
            if (!int.TryParse(parts[0], out int sort))
               continue;
            entries.Add(new ShortcutEntry {
               Sort = sort,
               Id = parts[1],
               Top = parts[2],
               Child = parts[3],
               Grandchild = parts[4],
               Ctrl = parts[5] == "1",
               Shift = parts[6] == "1",
               Alt = parts[7] == "1",
               Key = parts[8],
               DisplayString = parts[9],
               ShortcutLocked = parts[10] == "1",
               Notes = parts.Length > 11 ? parts[11] : ""
            });
         }
         CheckConflicts(entries);
         return entries;
      }

      internal static void Save(string pPath, List<ShortcutEntry> pEntries) {
         StringBuilder builder = new StringBuilder();
         builder.AppendLine("Sort\tId\tTop\tChild\tGrandchild\tCtrl\tShift\tAlt\tKey\tDisplayString\tShortcutLocked\tNotes");
         foreach (ShortcutEntry entry in pEntries) {
            builder.Append(entry.Sort).Append('\t')
               .Append(entry.Id).Append('\t')
               .Append(entry.Top).Append('\t')
               .Append(entry.Child).Append('\t')
               .Append(entry.Grandchild).Append('\t')
               .Append(entry.Ctrl ? '1' : '0').Append('\t')
               .Append(entry.Shift ? '1' : '0').Append('\t')
               .Append(entry.Alt ? '1' : '0').Append('\t')
               .Append(entry.Key).Append('\t')
               .Append(entry.DisplayString).Append('\t')
               .Append(entry.ShortcutLocked ? '1' : '0').Append('\t')
               .AppendLine(entry.Notes);
         }
         File.WriteAllText(pPath, builder.ToString(), Encoding.UTF8);
      }

      internal static Dictionary<string, ToolStripMenuItem> BuildMenuDictionary(params ToolStrip[] pStrips) {
         Dictionary<string, ToolStripMenuItem> dictionary = new Dictionary<string, ToolStripMenuItem>(StringComparer.Ordinal);
         foreach (ToolStrip strip in pStrips)
            CollectItems(strip.Items, dictionary);
         return dictionary;
      }

      private static void CollectItems(ToolStripItemCollection pItems, Dictionary<string, ToolStripMenuItem> pDictionary) {
         foreach (ToolStripItem item in pItems) {
            if (item is not ToolStripMenuItem menuItem)
               continue;
            if (!string.IsNullOrEmpty(menuItem.Name))
               pDictionary[menuItem.Name] = menuItem;
            if (menuItem.HasDropDownItems)
               CollectItems(menuItem.DropDownItems, pDictionary);
         }
      }

      internal static void Apply(List<ShortcutEntry> pEntries, Dictionary<string, ToolStripMenuItem> pMenuDictionary) {
         foreach (ShortcutEntry entry in pEntries) {
            if (!pMenuDictionary.TryGetValue(entry.Id, out ToolStripMenuItem? menuItem))
               continue;
            if (menuItem.HasDropDownItems) {
               menuItem.ShortcutKeys = Keys.None;
               menuItem.ShortcutKeyDisplayString = null;
            } else {
               menuItem.ShortcutKeys = entry.ShortcutKeys;
               menuItem.ShortcutKeyDisplayString = string.IsNullOrEmpty(entry.DisplayString)
                  ? null
                  : entry.DisplayString;
            }
            menuItem.Text = menuItem.OwnerItem == null && !string.IsNullOrEmpty(entry.DisplayString)
               ? entry.ItemText + entry.DisplayString
               : entry.ItemText;
         }
      }

      internal static void EnsureShortcutsFile() {
         string destination = DefaultFilePath;
         if (File.Exists(destination))
            return;
         Directory.CreateDirectory(mDataFolder);
         string source = Path.Combine(mAppFolder, "shortcuts.tsv");
         if (!File.Exists(source))
            return;
         File.Copy(source, destination);
         File.SetAttributes(destination, File.GetAttributes(destination) & ~FileAttributes.ReadOnly);
      }

      internal static List<ShortcutEntry> LoadWithFallback() {
         EnsureShortcutsFile();
         List<ShortcutEntry> entries = Load(DefaultFilePath);
         if (entries.Count > 0)
            return entries;
         return Load(Path.Combine(mAppFolder, "shortcuts.tsv"));
      }

      internal static void SyncContextMenuShortcutDisplays() {
         ThrowIfNull(mContextUndoTSMI, nameof(mContextUndoTSMI));
         ThrowIfNull(mEditUndoTSMI, nameof(mEditUndoTSMI));
         ThrowIfNull(mContextRedoTSMI, nameof(mContextRedoTSMI));
         ThrowIfNull(mEditRedoTSMI, nameof(mEditRedoTSMI));
         ThrowIfNull(mContextCutTSMI, nameof(mContextCutTSMI));
         ThrowIfNull(mEditCutTSMI, nameof(mEditCutTSMI));
         ThrowIfNull(mContextDeleteTSMI, nameof(mContextDeleteTSMI));
         ThrowIfNull(mEditDeleteTSMI, nameof(mEditDeleteTSMI));
         ThrowIfNull(mContextCopyTSMI, nameof(mContextCopyTSMI));
         ThrowIfNull(mEditCopyTSMI, nameof(mEditCopyTSMI));
         ThrowIfNull(mContextCopyAllTSMI, nameof(mContextCopyAllTSMI));
         ThrowIfNull(mEditCopyAllTSMI, nameof(mEditCopyAllTSMI));
         ThrowIfNull(mContextPasteTSMI, nameof(mContextPasteTSMI));
         ThrowIfNull(mEditPasteTSMI, nameof(mEditPasteTSMI));
         ThrowIfNull(mContextSelectAllTSMI, nameof(mContextSelectAllTSMI));
         ThrowIfNull(mEditSelectAllTSMI, nameof(mEditSelectAllTSMI));
         ThrowIfNull(mContextSelectNoneTSMI, nameof(mContextSelectNoneTSMI));
         ThrowIfNull(mEditSelectNoneTSMI, nameof(mEditSelectNoneTSMI));
         ThrowIfNull(mContextFindTSMI, nameof(mContextFindTSMI));
         ThrowIfNull(mEditFindTSMI, nameof(mEditFindTSMI));
         ThrowIfNull(mContextFindNextTSMI, nameof(mContextFindNextTSMI));
         ThrowIfNull(mEditFindNextTSMI, nameof(mEditFindNextTSMI));
         ThrowIfNull(mContextFindPreviousTSMI, nameof(mContextFindPreviousTSMI));
         ThrowIfNull(mEditFindPreviousTSMI, nameof(mEditFindPreviousTSMI));
         ThrowIfNull(mContextReplaceTSMI, nameof(mContextReplaceTSMI));
         ThrowIfNull(mEditReplaceTSMI, nameof(mEditReplaceTSMI));
         ThrowIfNull(mContextGoToTSMI, nameof(mContextGoToTSMI));
         ThrowIfNull(mEditGoToTSMI, nameof(mEditGoToTSMI));
         mContextUndoTSMI.Text = mEditUndoTSMI.Text;
         mContextUndoTSMI.ShortcutKeyDisplayString = mEditUndoTSMI.ShortcutKeyDisplayString;
         mContextRedoTSMI.Text = mEditRedoTSMI.Text;
         mContextRedoTSMI.ShortcutKeyDisplayString = mEditRedoTSMI.ShortcutKeyDisplayString;
         mContextCutTSMI.Text = mEditCutTSMI.Text;
         mContextCutTSMI.ShortcutKeyDisplayString = mEditCutTSMI.ShortcutKeyDisplayString;
         mContextDeleteTSMI.Text = mEditDeleteTSMI.Text;
         mContextDeleteTSMI.ShortcutKeyDisplayString = mEditDeleteTSMI.ShortcutKeyDisplayString;
         mContextCopyTSMI.Text = mEditCopyTSMI.Text;
         mContextCopyTSMI.ShortcutKeyDisplayString = mEditCopyTSMI.ShortcutKeyDisplayString;
         mContextCopyAllTSMI.Text = mEditCopyAllTSMI.Text;
         mContextCopyAllTSMI.ShortcutKeyDisplayString = mEditCopyAllTSMI.ShortcutKeyDisplayString;
         mContextPasteTSMI.Text = mEditPasteTSMI.Text;
         mContextPasteTSMI.ShortcutKeyDisplayString = mEditPasteTSMI.ShortcutKeyDisplayString;
         mContextSelectAllTSMI.Text = mEditSelectAllTSMI.Text;
         mContextSelectAllTSMI.ShortcutKeyDisplayString = mEditSelectAllTSMI.ShortcutKeyDisplayString;
         mContextSelectNoneTSMI.Text = mEditSelectNoneTSMI.Text;
         mContextSelectNoneTSMI.ShortcutKeyDisplayString = mEditSelectNoneTSMI.ShortcutKeyDisplayString;
         mContextFindTSMI.Text = mEditFindTSMI.Text;
         mContextFindTSMI.ShortcutKeyDisplayString = mEditFindTSMI.ShortcutKeyDisplayString;
         mContextFindNextTSMI.Text = mEditFindNextTSMI.Text;
         mContextFindNextTSMI.ShortcutKeyDisplayString = mEditFindNextTSMI.ShortcutKeyDisplayString;
         mContextFindPreviousTSMI.Text = mEditFindPreviousTSMI.Text;
         mContextFindPreviousTSMI.ShortcutKeyDisplayString = mEditFindPreviousTSMI.ShortcutKeyDisplayString;
         mContextReplaceTSMI.Text = mEditReplaceTSMI.Text;
         mContextReplaceTSMI.ShortcutKeyDisplayString = mEditReplaceTSMI.ShortcutKeyDisplayString;
         mContextGoToTSMI.Text = mEditGoToTSMI.Text;
         mContextGoToTSMI.ShortcutKeyDisplayString = mEditGoToTSMI.ShortcutKeyDisplayString;
      }

      private static readonly (string ViewId, string ContextId)[] sScrollingSyncPairs = [
         ("mViewScrollingEdgeTopTSMI",     "mContextScrollingEdgeTopTSMI"),
         ("mViewScrollingEdgeBottomTSMI",  "mContextScrollingEdgeBottomTSMI"),
         ("mViewScrollingEdgeLeftTSMI",    "mContextScrollingEdgeLeftTSMI"),
         ("mViewScrollingEdgeRightTSMI",   "mContextScrollingEdgeRightTSMI"),
         ("mViewScrollingScrollUpTSMI",    "mContextScrollingScrollUpTSMI"),
         ("mViewScrollingScrollDownTSMI",  "mContextScrollingScrollDownTSMI"),
         ("mViewScrollingScrollLeftTSMI",  "mContextScrollingScrollLeftTSMI"),
         ("mViewScrollingScrollRightTSMI", "mContextScrollingScrollRightTSMI"),
         ("mViewScrollingPageUpTSMI",      "mContextScrollingPageUpTSMI"),
         ("mViewScrollingPageDownTSMI",    "mContextScrollingPageDownTSMI"),
         ("mViewScrollingPageLeftTSMI",    "mContextScrollingPageLeftTSMI"),
         ("mViewScrollingPageRightTSMI",   "mContextScrollingPageRightTSMI"),
      ];

      internal static void SyncLockedScrollingEntries(List<ShortcutEntry> pEntries) {
         Dictionary<string, ShortcutEntry> byId = pEntries.ToDictionary(e => e.Id, e => e);
         foreach ((string viewId, string contextId) in sScrollingSyncPairs) {
            if (!byId.TryGetValue(viewId, out ShortcutEntry? viewEntry))
               continue;
            if (!byId.TryGetValue(contextId, out ShortcutEntry? contextEntry))
               continue;
            contextEntry.Ctrl = viewEntry.Ctrl;
            contextEntry.Shift = viewEntry.Shift;
            contextEntry.Alt = viewEntry.Alt;
            contextEntry.Key = viewEntry.Key;
            contextEntry.DisplayString = viewEntry.DisplayString;
         }
      }

      internal static void SyncScrollingContextMenuKeys() {
         ThrowIfNull(mViewScrollingEdgeTopTSMI, nameof(mViewScrollingEdgeTopTSMI));
         ThrowIfNull(mViewScrollingEdgeBottomTSMI, nameof(mViewScrollingEdgeBottomTSMI));
         ThrowIfNull(mViewScrollingEdgeLeftTSMI, nameof(mViewScrollingEdgeLeftTSMI));
         ThrowIfNull(mViewScrollingEdgeRightTSMI, nameof(mViewScrollingEdgeRightTSMI));
         ThrowIfNull(mViewScrollingScrollUpTSMI, nameof(mViewScrollingScrollUpTSMI));
         ThrowIfNull(mViewScrollingScrollDownTSMI, nameof(mViewScrollingScrollDownTSMI));
         ThrowIfNull(mViewScrollingScrollLeftTSMI, nameof(mViewScrollingScrollLeftTSMI));
         ThrowIfNull(mViewScrollingScrollRightTSMI, nameof(mViewScrollingScrollRightTSMI));
         ThrowIfNull(mViewScrollingPageUpTSMI, nameof(mViewScrollingPageUpTSMI));
         ThrowIfNull(mViewScrollingPageDownTSMI, nameof(mViewScrollingPageDownTSMI));
         ThrowIfNull(mViewScrollingPageLeftTSMI, nameof(mViewScrollingPageLeftTSMI));
         ThrowIfNull(mViewScrollingPageRightTSMI, nameof(mViewScrollingPageRightTSMI));
         ThrowIfNull(mContextScrollingEdgeTopTSMI, nameof(mContextScrollingEdgeTopTSMI));
         ThrowIfNull(mContextScrollingEdgeBottomTSMI, nameof(mContextScrollingEdgeBottomTSMI));
         ThrowIfNull(mContextScrollingEdgeLeftTSMI, nameof(mContextScrollingEdgeLeftTSMI));
         ThrowIfNull(mContextScrollingEdgeRightTSMI, nameof(mContextScrollingEdgeRightTSMI));
         ThrowIfNull(mContextScrollingScrollUpTSMI, nameof(mContextScrollingScrollUpTSMI));
         ThrowIfNull(mContextScrollingScrollDownTSMI, nameof(mContextScrollingScrollDownTSMI));
         ThrowIfNull(mContextScrollingScrollLeftTSMI, nameof(mContextScrollingScrollLeftTSMI));
         ThrowIfNull(mContextScrollingScrollRightTSMI, nameof(mContextScrollingScrollRightTSMI));
         ThrowIfNull(mContextScrollingPageUpTSMI, nameof(mContextScrollingPageUpTSMI));
         ThrowIfNull(mContextScrollingPageDownTSMI, nameof(mContextScrollingPageDownTSMI));
         ThrowIfNull(mContextScrollingPageLeftTSMI, nameof(mContextScrollingPageLeftTSMI));
         ThrowIfNull(mContextScrollingPageRightTSMI, nameof(mContextScrollingPageRightTSMI));
         SyncScrollingOne(mViewScrollingEdgeTopTSMI,     mContextScrollingEdgeTopTSMI,     ref mScrollingEdgeTopKey);
         SyncScrollingOne(mViewScrollingEdgeBottomTSMI,  mContextScrollingEdgeBottomTSMI,  ref mScrollingEdgeBottomKey);
         SyncScrollingOne(mViewScrollingEdgeLeftTSMI,    mContextScrollingEdgeLeftTSMI,    ref mScrollingEdgeLeftKey);
         SyncScrollingOne(mViewScrollingEdgeRightTSMI,   mContextScrollingEdgeRightTSMI,   ref mScrollingEdgeRightKey);
         SyncScrollingOne(mViewScrollingScrollUpTSMI,    mContextScrollingScrollUpTSMI,    ref mScrollingScrollUpKey);
         SyncScrollingOne(mViewScrollingScrollDownTSMI,  mContextScrollingScrollDownTSMI,  ref mScrollingScrollDownKey);
         SyncScrollingOne(mViewScrollingScrollLeftTSMI,  mContextScrollingScrollLeftTSMI,  ref mScrollingScrollLeftKey);
         SyncScrollingOne(mViewScrollingScrollRightTSMI, mContextScrollingScrollRightTSMI, ref mScrollingScrollRightKey);
         SyncScrollingOne(mViewScrollingPageUpTSMI,      mContextScrollingPageUpTSMI,      ref mScrollingPageUpKey);
         SyncScrollingOne(mViewScrollingPageDownTSMI,    mContextScrollingPageDownTSMI,    ref mScrollingPageDownKey);
         SyncScrollingOne(mViewScrollingPageLeftTSMI,    mContextScrollingPageLeftTSMI,    ref mScrollingPageLeftKey);
         SyncScrollingOne(mViewScrollingPageRightTSMI,   mContextScrollingPageRightTSMI,   ref mScrollingPageRightKey);
      }

      private static void SyncScrollingOne(ToolStripMenuItem pView, ToolStripMenuItem pContext, ref Keys pField) {
         pField = pView.ShortcutKeys;
         pContext.ShortcutKeys = pView.ShortcutKeys;
         pContext.ShortcutKeyDisplayString = pView.ShortcutKeyDisplayString;
      }

      internal static void TryLoadAndApply(Dictionary<string, ToolStripMenuItem> pMenuDictionary) {
         List<ShortcutEntry> entries = LoadWithFallback();
         if (entries.Count > 0)
            Apply(entries, pMenuDictionary);
         SyncScrollingContextMenuKeys();
      }

      internal static void CheckConflicts(List<ShortcutEntry> pEntries) {
         Dictionary<(bool Ctrl, bool Shift, bool Alt, string Key), int> keyCounts =
            [];
         foreach (ShortcutEntry entry in pEntries)
            entry.HasConflict = false;
         foreach (ShortcutEntry entry in pEntries) {
            if (string.IsNullOrEmpty(entry.Key))
               continue;
            if (entry.ShortcutLocked)
               continue;
            (bool Ctrl, bool Shift, bool Alt, string Key) shortcutKey = (entry.Ctrl, entry.Shift, entry.Alt, entry.Key);
            if (keyCounts.TryGetValue(shortcutKey, out int existingCount))
               keyCounts[shortcutKey] = existingCount + 1;
            else
               keyCounts[shortcutKey] = 1;
         }
         foreach (ShortcutEntry entry in pEntries) {
            if (string.IsNullOrEmpty(entry.Key))
               continue;
            if (entry.ShortcutLocked)
               continue;
            (bool Ctrl, bool Shift, bool Alt, string Key) shortcutKey = (entry.Ctrl, entry.Shift, entry.Alt, entry.Key);
            if (keyCounts.TryGetValue(shortcutKey, out int count) && count > 1)
               entry.HasConflict = true;
         }
      }
   }
}
