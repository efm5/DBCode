namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePickerPanel : ScrollablePanel {
         private readonly TwoLineHeaderLabelCluster? mTitleLabel;
         internal readonly BottomPanel? mThemePickerBottomPanel;
         internal ClusterContainer? mClusterContainer;
         private List<BaseCluster>? mButtonBaseClusters;
         private readonly PickMode mPickMode;

         public ThemePickerPanel(PickMode pPickMode) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mPickMode = pPickMode;
            Name = "themePickerPanel";
            TabIndex = mTabIndex++;
            string usage = (pPickMode == PickMode.Edit) ? "Edit" : "Apply";
            mTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "Theme Picker", usage);
            mButtonBaseClusters = [];
            mClusterContainer = new ClusterContainer(this, mButtonBaseClusters, ClusterLayoutMode.FlowLayout) {
               Dock = DockStyle.None,
               AutoSize = false
            };
            mThemePickerBottomPanel = new BottomPanel(mCurrentTheme);
         }

         public void LayoutPanel() {
            ThrowIfNull(mThemePickerBottomPanel, nameof(mThemePickerBottomPanel));
            ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
            ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
            SuspendLayout();
            mTitleLabel.SuspendLayout();
            ApplyTheme();
            mTitleLabel.LayoutCluster();
            mThemePickerBottomPanel.LayoutControls();
            mClusterContainer.Size = new Size(ClientSize.Width - mIndent,
               ClientSize.Height - mTitleLabel.Height - mThemePickerBottomPanel.Height - mEm4);
            mClusterContainer.Location = new Point(mIndent, mTitleLabel.Bottom + mEm);
            mTitleLabel.LayoutCluster();
            mClusterContainer.LayoutClusters();
            mClusterContainer.AutoSize = true;
            mTitleLabel.ResumeLayout(true);
            ResumeLayout(true);
         }

         private void CreateLayout() {
            ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
            ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
            ThrowIfNull(mThemePickerBottomPanel, nameof(mThemePickerBottomPanel));
            ThrowIfNull(mButtonBaseClusters, nameof(mCurrentTheme));
            CreateButtons();
            ThrowIfNull(mThemePickerBottomPanel.mCancelButton, nameof(mThemePickerBottomPanel.mCancelButton));
            ThrowIfNull(mThemePickerBottomPanel.mHelpButton, nameof(mThemePickerBottomPanel.mHelpButton));
            mThemePickerBottomPanel.mCancelButton.Click += CancelButton_Click;
            mThemePickerBottomPanel.mHelpButton.Click += MainForm.Help_Click;
            mClusterContainer.Invalidate(true);
         }

         private void CreateButtons() {
            ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
            foreach (Theme theme in mThemes.OfType<Theme>()) {
               ThrowIfNull(mButtonBaseClusters, nameof(mButtonBaseClusters));
               AddButtonCluster(mButtonBaseClusters, theme);
            }
         }

         private void AddButtonCluster(List<BaseCluster> pClusters, Theme pTheme) {
            ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
            ButtonCluster cluster = new ButtonCluster(pTheme, pTheme.mName);
            cluster.SuspendLayout();
            ThrowIfNull(cluster.mButton, nameof(cluster.mButton));
            cluster.mButton.Tag = pTheme;
            cluster.mButton.Click += PickThemeButton_Click;
            pClusters.Add(cluster);
            mClusterContainer.Controls.Add(cluster);
         }

         public void ApplyTheme() {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            ThrowIfNull(mButtonBaseClusters, nameof(mButtonBaseClusters));
            ThrowIfNull(mThemePickerBottomPanel, nameof(mThemePickerBottomPanel));
            ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.PanelBackground];
            foreach (BaseCluster cluster in mButtonBaseClusters)
               cluster.SetFontAndColor();
            mThemePickerBottomPanel.SetFontAndColor();
            mTitleLabel.SetFontAndColor();
         }

         private void CloseThemePickerPanel() {
            ThrowIfNull(mForm, nameof(mForm));
            mForm.RestoreFromThemePickerPanel();
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
               ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
               ThrowIfNull(mThemePickerBottomPanel, nameof(mThemePickerBottomPanel));
               ThrowIfNull(mButtonBaseClusters, nameof(mButtonBaseClusters));
               ThrowIfNull(mThemePickerBottomPanel.mCancelButton, nameof(mThemePickerBottomPanel.mCancelButton));
               ThrowIfNull(mThemePickerBottomPanel.mHelpButton, nameof(mThemePickerBottomPanel.mHelpButton));
               mThemePickerBottomPanel.mCancelButton.Click -= CancelButton_Click;
               mThemePickerBottomPanel.mHelpButton.Click -= MainForm.Help_Click;
               foreach (BaseCluster cluster in mButtonBaseClusters) {
                  ThrowIfNull(cluster, nameof(cluster));
                  if (cluster is ButtonCluster buttonCluster) {
                     ThrowIfNull(buttonCluster.mButton, nameof(buttonCluster.mButton));
                     buttonCluster.mButton.Click -= PickThemeButton_Click;
                  }
               }
               mTitleLabel.Dispose();
               mClusterContainer.Dispose();
               mThemePickerBottomPanel.Dispose();
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
