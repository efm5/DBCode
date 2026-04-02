namespace DBCode.Preferences {
   internal sealed partial class ThemePanel : Panel {
      private void Help_Click(object? pSender, EventArgs pEventArgs) {
         if (pSender is Control control && control.Tag is HelpTag tag) {
            if (FindForm() is MainForm mainForm)
               mainForm.GetHelp(tag.Context, tag.Anchor);
         }
      }
   }
}
