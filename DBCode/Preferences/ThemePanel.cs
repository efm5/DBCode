namespace DBCode.Preferences {
   internal sealed partial class ThemePanel : Panel {
      private readonly int mEm;
      private readonly UIContext mContext;
      private TabControl? mTabControl = null;
      private Panel? mThemeBottomPanel = null;
      private Label? mTitleLabel = null;
      public Button? mThemeBottomHelpButton = null;
      public Button? mThemeApplyButton = null;
      public Button? mThemeOkButton = null;
      public Button? mThemeCancelButton = null;

#pragma warning disable IDE0290
      public ThemePanel(int pEm) {
         mEm = pEm;
         mContext = UIContext.Theme;

         AssignHelpTag();
         InitializeUI();
      }
#pragma warning restore IDE0290

      private void AssignHelpTag() {
         Tag = new HelpTag(UIContext.Theme, "ThemePanel");
      }

      private void InitializeUI() {
         CreateTitleLabel();
         CreateTabControl();
         CreateThemeBottomPanel();
         CreateThemeBottomButtons();
      }

      private void CreateTitleLabel() {
         mTitleLabel = new Label();
         mTitleLabel.Text = "Theme Settings";
         mTitleLabel.AutoSize = true;
         mTitleLabel.Left = mEm;
         mTitleLabel.Top = mEm;
         Controls.Add(mTitleLabel);
      }

      private void CreateTabControl() {
         mTabControl = new TabControl();
         mTabControl.Left = mEm;
         mTabControl.Top = (mTitleLabel?.Bottom ?? (2 * mEm)) + mEm;
         mTabControl.Width = 800;   // temporary fixed size
         mTabControl.Height = 600;  // temporary fixed size
         Controls.Add(mTabControl);
      }

      private void CreateThemeBottomPanel() {
         mThemeBottomPanel = new Panel();
         mThemeBottomPanel.Dock = DockStyle.Bottom;
         mThemeBottomPanel.Height = 3 * mEm;
         Controls.Add(mThemeBottomPanel);
      }

      private void CreateThemeBottomButtons() {
         if (mThemeBottomPanel == null)
            return;
         mThemeBottomHelpButton = new Button();
         mThemeBottomHelpButton.Text = "&Help";
         mThemeBottomHelpButton.AutoSize = true;
         mThemeBottomHelpButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
         mThemeBottomHelpButton.Left = mEm;
         mThemeBottomHelpButton.Top = 3;
         mThemeBottomHelpButton.Tag = new HelpTag(UIContext.Theme, "ThemePanel");
         mThemeBottomHelpButton.Click += Help_Click;
         mThemeBottomPanel.Controls.Add(mThemeBottomHelpButton);

         mThemeCancelButton = new Button();
         mThemeCancelButton.Text = "&Cancel";
         mThemeCancelButton.AutoSize = true;
         mThemeCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
         mThemeCancelButton.Left = mThemeBottomPanel.Width - (mThemeCancelButton.Width + (2 * mEm));
         mThemeCancelButton.Top = 3;
         mThemeBottomPanel.Controls.Add(mThemeCancelButton);

         mThemeOkButton = new Button();
         mThemeOkButton.Text = "&OK";
         mThemeOkButton.AutoSize = true;
         mThemeOkButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
         mThemeOkButton.Left = mThemeCancelButton.Left - (mThemeOkButton.Width + mEm);
         mThemeOkButton.Top = 3;
         mThemeBottomPanel.Controls.Add(mThemeOkButton);

         mThemeApplyButton = new Button();
         mThemeApplyButton.Text = "&Apply";
         mThemeApplyButton.AutoSize = true;
         mThemeApplyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
         mThemeApplyButton.Left = mThemeOkButton.Left - (mThemeApplyButton.Width + mEm);
         mThemeApplyButton.Top = 3;
         mThemeBottomPanel.Controls.Add(mThemeApplyButton);
      }

      private void Help_Click(object? pSender, EventArgs pEventArgs) {
         if (pSender is Control control && control.Tag is HelpTag tag) {
            if (FindForm() is MainForm mainForm)
               mainForm.GetHelp(tag.Context, tag.Anchor);
         }
      }

      private void LayoutTheme() {
         // heavy lifting will go here later
      }
   }
}
