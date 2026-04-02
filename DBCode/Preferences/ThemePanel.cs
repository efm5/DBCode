namespace DBCode.Preferences {
   internal sealed partial class ThemePanel : Panel {
      private readonly UIContext mContext;
      private TabControl? mTabControl = null;
      private Panel? mThemeBottomPanel = null;
      private Label? mTitleLabel = null;
      public Button? mThemeBottomHelpButton = null;
      public Button? mThemeApplyButton = null;
      public Button? mThemeOkButton = null;
      public Button? mThemeCancelButton = null;

      public ThemePanel(UIContext pContext) {
         mContext = pContext;
         AssignHelpTag();
         InitializeUI();
      }

      private void AssignHelpTag() {
         Tag = new HelpTag(mContext, "ThemePanel");
      }

      private void InitializeUI() {
         CreateTitleLabel();
         CreateTabControl();
         CreateThemeBottomPanel();
         CreateThemeBottomButtons();
      }

      private void CreateTitleLabel() {
         mTitleLabel = new Label {
            Name = "mTitleLabel",
            TabIndex = mTabIndex++,
            Text = "Theme Settings",
            AutoSize = true,
            Top = mEm
         };
         Controls.Add(mTitleLabel);
      }

      private void CreateTabControl() {
         mTabControl = new TabControl {
            Name = "mTabControl",
            TabIndex = mTabIndex++,
            Left = mEm,
            Width = 800,   // temporary fixed size
            Height = 600  // temporary fixed size
         };
         Controls.Add(mTabControl);
      }

      private void CreateThemeBottomPanel() {
         mThemeBottomPanel = new Panel {
            Name = "mThemeBottomPanel",
            TabIndex = mTabIndex++,
         };
         Controls.Add(mThemeBottomPanel);
      }

      private void CreateThemeBottomButtons() {
         if (mThemeBottomPanel == null)
            return;
         mThemeBottomHelpButton = new Button {
            Name = "mThemeBottomHelpButton",
            TabIndex = mTabIndex++,
            Text = "&Help",
            AutoSize = true,
            Anchor = AnchorStyles.Top | AnchorStyles.Left,
            Left = mEm,
            Top = mBottomButtonTop,
            Tag = new HelpTag(UIContext.Theme, "ThemePanel")
         };
         mThemeBottomHelpButton.Click += Help_Click;
         mThemeBottomPanel.Controls.Add(mThemeBottomHelpButton);
         mThemeCancelButton = new Button {
            Name = "mThemeCancelButton",
            TabIndex = mTabIndex++,
            Text = "&Cancel",
            AutoSize = true,
            Top = mBottomButtonTop
         };
         //DEBUG efm5 2026 04 2 Handler
         mThemeBottomPanel.Controls.Add(mThemeCancelButton);
         mThemeOkButton = new Button {
            Name = "mThemeOkButton",
            TabIndex = mTabIndex++,
            Text = "&OK",
            AutoSize = true,
            Top = mBottomButtonTop
         };
         //DEBUG efm5 2026 04 2 Handler
         mThemeBottomPanel.Controls.Add(mThemeOkButton);
         mThemeApplyButton = new Button {
            Name = "mThemeApplyButton",
            TabIndex = mTabIndex++,
            Text = "&Apply",
            AutoSize = true,
            Top = mBottomButtonTop
         };
         //DEBUG efm5 2026 04 2 Handler
         mThemeBottomPanel.Controls.Add(mThemeApplyButton);
      }

      private void LayoutAndSizeTabControl() {
         //DEBUG efm5 2026 04 2 layout and size the tab control
      }

      private void LayoutTheme() {
         if ((mThemeCancelButton == null) || (mThemeOkButton == null) || (mThemeApplyButton == null) ||
            (mThemeBottomPanel == null) || (mTabControl == null) || (mTitleLabel == null))
            return;
         mThemeCancelButton.Anchor = mTopLeftAnchor;
         mThemeOkButton.Anchor = mTopLeftAnchor;
         mThemeApplyButton.Anchor = mTopLeftAnchor;
         mTabControl.Anchor = mTopLeftAnchor;
         mThemeBottomPanel.Dock = DockStyle.None;
         mThemeBottomPanel.Anchor = mTopLeftAnchor;
         PaintPanel(mThemePanel);
         mTitleLabel.Top = mEm;
         mTabControl.Top = mTitleLabel.Bottom + mEmHalf;
         LayoutAndSizeTabControl();
         mThemeBottomPanel.Width = Math.Max(mTabControl.Left + mTabControl.Width + SystemInformation.VerticalScrollBarWidth,
            TotalWidth(ControlCollectionAsList(mThemeBottomPanel.Controls), mEm));
         mThemeCancelButton.Left = mThemeBottomPanel.Width - mThemeCancelButton.Width - mEm2;
         mThemeOkButton.Left = mThemeCancelButton.Left - mThemeOkButton.Width - mEm;
         mThemeApplyButton.Left = mThemeOkButton.Left - mThemeApplyButton.Width - mEm2;
         mThemeCancelButton.Anchor = mTopRightAnchor;
         mThemeOkButton.Anchor = mTopRightAnchor;
         mThemeApplyButton.Anchor = mTopRightAnchor;
         mThemeBottomPanel.Dock = DockStyle.Bottom;
         mTabControl.Anchor = mTopLeftBottomRightAnchor;
      }
   }
}
